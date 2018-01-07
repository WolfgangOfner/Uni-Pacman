using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PacManLib;
using System.Windows.Threading;

namespace NetworkPacman
{
    public class ClientRepresentation
    {
        public ClientRepresentation(TcpClient tcpClient, GameInfo gameInfo, int id)
        {
            this.Id = id;
            this.ListenerThread = new Thread(new ParameterizedThreadStart(ClientWorker));
            this.TcpClient = tcpClient;
            this.GameInfo = gameInfo;
            UpdateRecieved = false;

            this.MyMap = CreateMap(MainWindow.randomMap[this.Id]);
            this.MyGhost = new Ghost(41, 32);
            MainWindow.allGhosts.Add(this.MyGhost);
            this.KI = new KIComponent(this.MyGhost);
            
            ListenerThread.Start(this.TcpClient);
        }

        public TcpClient TcpClient { get; private set; }

        public Thread ListenerThread { get; set; }

        public GameInfo GameInfo { get; set; }

        private Ghost MyGhost { get; set; }

        private KIComponent KI { get; set; }

        private object[,] MyMap { get; set; }

        public int Id { get; set; }

        public bool UpdateRecieved { get; set; }

        private void ClientWorker(object data)
        {
            if (data == null)
            {
                return;
            }

            if (!(data is TcpClient))
            {
                return;
            }

            TcpClient client = (TcpClient)data;
            NetworkStream networkstream = client.GetStream();

            bool run = true;

            while (run)
            {
                try
                {
                    if (networkstream.DataAvailable)
                    {
                        NetPackage np = Networking.RecievePackage(networkstream);
                        EvaluateUpdate(np);
                    }
                }
                catch (ThreadAbortException)
                {
                    Thread.ResetAbort();
                    run = false;
                }

                Thread.Sleep(1);
            }
        }

        private void EvaluateUpdate(NetPackage np)
        {
            EvaluateRecieve(np);

            //while (true)
            //{
                //if (!this.GameInfo.NetworkLock)
                //{
                    EvaluateSend(np);
                    //break;
                //}
            //}
        }

        private void EvaluateRecieve(NetPackage np)
        {
            //bool timedOut = true;

            // check if pacman has died
            if ((np.Pos_PAC_left - 1 == np.Pos_GHOST_left + 1 && np.Pos_PAC_top == np.Pos_GHOST_top) ||
        (np.Pos_PAC_left + 1 == np.Pos_GHOST_left - 1 && np.Pos_PAC_top == np.Pos_GHOST_top) ||
        (np.Pos_PAC_left == np.Pos_GHOST_left && np.Pos_PAC_top - 1 == np.Pos_GHOST_top + 1) ||
        (np.Pos_PAC_left == np.Pos_GHOST_left && np.Pos_PAC_top + 1 == np.Pos_GHOST_top - 1) ||
        (np.Pos_PAC_left == np.Pos_GHOST_left && np.Pos_PAC_top == np.Pos_GHOST_top))
            {
                this.GameInfo.GameStatus = -1;
                this.UpdateRecieved = true;
            }

            // check Gamestatus with coins
            if (this.GameInfo.CurrentCoins == this.GameInfo.MaxCoins)
            {
                this.GameInfo.GameStatus = 1;
                this.UpdateRecieved = true;
            }
            else if (np.GameStatus == -1)
            {
                this.GameInfo.GameStatus = -1;
                this.UpdateRecieved = true;
            }

            if (this.GameInfo.GameStatus == 0)
            {
                // update coins
                this.GameInfo.CurrentCoins += np.PAC_Coin;

                //UpdateCoins(this.GameInfo.CurrentCoins, this.GameInfo.MaxCoins);

                if (this.GameInfo.CoinList[np.ClientID].GetLength(0) == 1)
                {
                    this.GameInfo.CoinList[np.ClientID] = np.CoinArray;                    
                }
                else if (np.ClientID == 0)
                {
                    this.GameInfo.CoinList[this.GameInfo.PacManMapIndex] = np.CoinArray;                    
                }

                // mapchange for pacman
                if (np.ClientID == 0)
                {
                    if (this.GameInfo.PacManMapIndex + np.MapChange == MainWindow.connectedClients.Count)
                    {
                        this.GameInfo.PacManMapIndex = 0;
                        this.GameInfo.PacX = 2;
                        this.GameInfo.PacY = 32;
                    }

                    else if (this.GameInfo.PacManMapIndex + np.MapChange < 0)
                    {
                        this.GameInfo.PacManMapIndex = MainWindow.connectedClients.Count - 1;
                        this.GameInfo.PacX = 81;
                        this.GameInfo.PacY = 32;
                    }

                    else if (np.MapChange == -1)
                    {
                        this.GameInfo.PacManMapIndex += np.MapChange;
                        this.GameInfo.PacX = 81;
                        this.GameInfo.PacY = 32;
                    }

                    else if (np.MapChange == 1)
                    {
                        this.GameInfo.PacManMapIndex += np.MapChange;
                        this.GameInfo.PacX = 2;
                        this.GameInfo.PacY = 32;
                    }

                    else if (np.MapChange == 0 && np.ClientID == 0)
                    {
                        this.GameInfo.PacX = np.Pos_PAC_left;
                        this.GameInfo.PacY = np.Pos_PAC_top;
                    }
                }

                // GHOST

                if (np.ClientID == 0)
                {
                    this.MyGhost = MainWindow.allGhosts[this.GameInfo.PacManMapIndex];

                    if (np.MapChange != 0)
                    {
                        this.KI = new KIComponent(this.MyGhost);                        
                    }
                }

                if ((np.ClientID == 0 && this.GameInfo.PacManMapIndex == 0) || np.ClientID != 0)
                {
                    MyGhost.MoveGhost(this.MyMap, np.ClientID, this.GameInfo.PacManMapIndex, this.KI, this.GameInfo.PacX, this.GameInfo.PacY, this.MyGhost);
                }

                this.UpdateRecieved = true;

                if (np.ClientID == this.GameInfo.PacManMapIndex)
                {
                    while (true)
                    {
                        if (MainWindow.connectedClients[0].UpdateRecieved == true)
                        {
                            break;
                        }
                    }
                }
                    //if (np.ClientID != 0)
                    //{
                    //    bool allPassed = true;

                    //    while (true)
                    //    {
                    //        foreach (var item in MainWindow.connectedClients)
                    //        {
                    //            if (!item.UpdateRecieved)
                    //            {
                    //                allPassed = false;
                    //            }

                    //        }

                    //        if (allPassed)
                    //        {
                    //            this.GameInfo.NetworkLock = false;
                    //            break;
                    //        }
                    //        else
                    //        {
                    //            allPassed = true;
                    //        }
                    //    }
                    //}                
            }
        }

        private void EvaluateSend(NetPackage np)
        {
            bool drawPacMan = false;
            NetPackage sendNp;

            if ((np.ClientID == this.GameInfo.PacManMapIndex || np.ClientID == 0))
            {
                drawPacMan = true;

                sendNp = new NetPackage(np.ClientID, this.GameInfo.PacX, this.GameInfo.PacY, 0, drawPacMan, this.MyGhost.Left, this.MyGhost.Top, this.GameInfo.CoinList[this.GameInfo.PacManMapIndex], 0);
            }
            else
            {
                sendNp = new NetPackage(np.ClientID, this.GameInfo.PacX, this.GameInfo.PacY, 0, drawPacMan, this.MyGhost.Left, this.MyGhost.Top, null, 0);
            }

            sendNp.GameStatus = this.GameInfo.GameStatus;
            Networking.SendPackage(sendNp, this.TcpClient.GetStream());
            this.UpdateRecieved = false;

            if (this.GameInfo.GameStatus != 0)
            {
                this.TcpClient.Close();
                this.ListenerThread.Abort();
            }
        }

        public static object[,] CreateMap(char[,] readmap)
        {
            object[,] Maparray = new object[readmap.GetLength(0), readmap.GetLength(1)];
      //      GUI.StartUp(readmap.GetLength(0) + 2, readmap.GetLength(1) + 2);

            for (int i = 0; i < readmap.GetLength(0); i++)
            {
                for (int j = 0; j < readmap.GetLength(1); j++)
                {
                    if (readmap[i, j] == '#')
                    {
                        Wall wall = new Wall();
                                wall.Left = j;
                                wall.Top = i;
                                wall.Name = '#';
                                wall.Color = ConsoleColor.Blue;
                                Maparray[i, j] = wall;
                    }
                    else
                    {
                        Maparray[i, j] = ' ';
                    }    
                }
            }

            return Maparray;
        }
    }
}
