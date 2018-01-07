using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PacManLib;

namespace PacmanClient
{
    [Serializable()]
    public class Program
    {
        public static NetworkStream ns;

        public static event EventHandler<PackageRecievedEventArgs> OnPackageReceived;

        public static bool SuspendThread;

        public static int LeavingMap;

        public static int LeavingMap2;

        public static int ClientId;

        public static object locker = new object();

        public static string GetIP()
        {
            string ip = string.Empty;
            bool parse = false;
            Console.ForegroundColor = ConsoleColor.Gray;
            while (!parse)
            {
                Console.Write("Please enter a valid IP Adress: ");
                ip = Console.ReadLine();

                try
                {
                    IPAddress.Parse(ip);
                    parse = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("IP not valid! Try again!!");
                    continue;
                }
            }
            return ip;
        }

        static void Main(string[] args)
        {
            Console.Clear();
            Console.SetWindowSize(84, 68);
            Console.SetBufferSize(84, 68);

            //////////////////////////////////////////////////////////////////////////
            string ip = GetIP();
            TcpClient client = Networking.EstablishConnection(ip, 13000);
            ns = client.GetStream();

            InitialNetPackage netpack = Networking.RecieveInitialPackage(ns);

            try
            {
                ClientId = netpack.Index;
            }
            catch (NullReferenceException)
            {

                GUI.DrawException();
            }


            int counter = 0;
            GameManager game = new GameManager();

            if (ClientId == 0)
            {
                Map map = new Map();
                map.CreateMap(netpack.Maps.ElementAt(netpack.Index));
                map.CreateCoinArray();
                GUI.DrawMap(map.Maparray);


                NetPackage recieved;
                PacMan pacman = new PacMan(map);
                Ghost ghost = new Ghost(map, 41, 32);


                pacman.OnLeavingMap += new EventHandler<LeavingMapEventArgs>(OnPacManLeavingMapCall_Back);

                GUI.InitializeKI(pacman, ghost);

                pacman.MoveThread = new Thread(new ParameterizedThreadStart(pacman.Worker));
                pacman.MoveThread.Start(map);
                //Ghost ghostOtherMap = new Ghost(map, 10, 10);

                while (true)
                {
                    bool run = true;
                    int timecounter = 0;
                    if (counter > 0)
                    {
                        GUI.MovePacMan(map, pacman);
                    }


                    if (LeavingMap == -1 || LeavingMap == 1)
                    {
                        pacman.SwitchPacManToOtherSide(map, LeavingMap);
                    }

                    else
                    {
                        GUI.DrawPacMan(pacman.Direction, pacman);
                    }

                    NetPackage np = CreateNetPackage(ghost, pacman, map);
                    Networking.SendPackage(np, ns);

                    while (run)
                    {
                        if (ns.DataAvailable)
                        {
                            recieved = Networking.RecievePackage(ns);

                            if (recieved == null)
                            {
                                continue;
                            }

                            game.CheckifEnd(recieved.GameStatus);
                            ghost.OldLeft = ghost.Left;
                            ghost.OldTop = ghost.Top;
                            ghost.Left = recieved.Pos_GHOST_left;
                            ghost.Top = recieved.Pos_GHOST_top;

                            if (LeavingMap != 0)
                            {

                                if (recieved.CoinArray != null)
                                {
                                    GUI.ClearFigure(ghost.Left, ghost.Top);
                                    DrawOtherMap(netpack.Maps, map, pacman.MapIndex, recieved.CoinArray, pacman);
                                    Thread.Sleep(500);
                                }
                                else
                                {
                                    continue;
                                }

                                GUI.DrawPacMan(pacman.Direction, pacman);
                                ghost.OldLeft = ghost.Left;
                                ghost.OldTop = ghost.Top;
                            }
                            GUI.DrawGhost(map, ghost);
                            run = false;

                        }
                        else
                        {
                            timecounter += 10;
                            Thread.Sleep(10);

                            if (timecounter == 5000)
                            {
                                GUI.DrawException();
                            }
                        }
                    }
                    counter++;
                }
            }
            else
            {

                Map map = new Map();

                map.CreateMap(netpack.Maps.ElementAt(netpack.Index));
                map.CreateCoinArray();
                GUI.StartUp(map.Maparray.GetLength(0) + 2, map.Maparray.GetLength(1) + 2);
                GUI.DrawMap(map.Maparray);

                Ghost ghost = new Ghost(map, 41, 32);

                PacMan pacman = new PacMan(map);

                pacman.MapIndex = ClientId;

                GUI.InitializeKI(pacman, ghost);

                int pacmancounter = 0;
                NetPackage recieved;
                while (true)
                {
                    bool run = true;

                    NetPackage np = CreateNetPackageWithoutPacMan(ghost, map);
                    Networking.SendPackage(np, ns);

                    int timecounter = 0;
                    while (run)
                    {

                        if (ns.DataAvailable)
                        {
                            recieved = Networking.RecievePackage(ns);

                            if (!recieved.DrawPacMan)
                            {
                                Thread.Sleep(100);
                            }

                            if (recieved == null)
                            {
                                continue;
                            }

                            if (recieved.CoinArray != null)
                            {
                                map.CoinArray = recieved.CoinArray;
                            }

                            game.CheckifEnd(recieved.GameStatus * (-1));
                            ghost.OldLeft = ghost.Left;
                            ghost.OldTop = ghost.Top;
                            ghost.Left = recieved.Pos_GHOST_left;
                            ghost.Top = recieved.Pos_GHOST_top;


                            if (recieved.DrawPacMan)
                            {
                                GUI.ClearFigure(pacman.Left, pacman.Top);
                                DrawOtherPacman(recieved.Pos_PAC_left, recieved.Pos_PAC_top, pacman);
                                pacmancounter++;
                            }
                            else
                            {
                                if (pacmancounter > 0)
                                {
                                    GUI.ClearFigure(pacman.Left, pacman.Top);
                                }
                            }

                            GUI.DrawGhost(map, ghost);

                            run = false;
                        }
                        else
                        {
                            timecounter += 10;
                            Thread.Sleep(10);

                            if (timecounter == 5000)
                            {
                                GUI.DrawException();
                            }
                        }

                        //GUI.DrawException();
                    }
                }
            }
        }

        public static void DrawOtherPacman(int left, int top, PacMan pacman)
        {
            if (LeavingMap2 == -1)
            {
                pacman.OldLeft = 81;
                pacman.OldTop = 32;

            }
            else if (LeavingMap2 == 1)
            {
                pacman.OldLeft = 2;
                pacman.OldTop = 32;
            }

            if (left > pacman.Left)
            {
                pacman.OldLeft = left - 1;
                pacman.OldTop = top;

                pacman.Direction = 4;
            }
            else if (left < pacman.Left)
            {
                pacman.OldLeft = left + 1;
                pacman.OldTop = top;
 
                pacman.Direction = 3;
            }
            else if (top > pacman.Top)
            {
                pacman.OldTop = top - 1;
                pacman.OldLeft = left;

                pacman.Direction = 2;
            }
            else if (top < pacman.Top)
            {
                pacman.OldTop = top + 1;
                pacman.OldLeft = left;

                pacman.Direction = 1;
            }

            pacman.Left = left;
            pacman.Top = top;

            GUI.DrawPacMan(pacman.Direction, pacman);
            LeavingMap2 = 0;
        }

        private static void DrawOtherMap(List<char[,]> MapList, Map map, int index, bool[,] coinarray, PacMan p)
        {
            Console.Clear();

            if (index + LeavingMap < 0)
            {
                index = MapList.Count - 1;
                p.MapIndex = MapList.Count - 1;
            }
            else if (index + LeavingMap == MapList.Count)
            {
                index = 0;
                p.MapIndex = index;
            }
            else
            {
                p.MapIndex = index + LeavingMap;
            }

            map.CreateMap(MapList.ElementAt(p.MapIndex));
            map.CoinArray = coinarray;

            lock (locker)
            {
                for (int i = 0; i < map.Maparray.GetLength(0); i++)
                {
                    Console.SetCursorPosition(0, i);

                    for (int j = 0; j < map.Maparray.GetLength(1); j++)
                    {
                        if (map.Maparray[i, j] is Coin)
                        {
                            if (coinarray[i, j] == true)
                            {
                                Console.BackgroundColor = (map.Maparray[i, j] as GameObject).Color;
                                Console.ForegroundColor = (map.Maparray[i, j] as GameObject).Color;
                                Console.Write(map.Maparray[i, j]);
                                Console.BackgroundColor = ConsoleColor.Black;
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.Black;

                                Console.Write(" ");
                                Console.BackgroundColor = ConsoleColor.Black;
                            }
                        }
                        else
                        {
                            Console.BackgroundColor = (map.Maparray[i, j] as GameObject).Color;
                            Console.ForegroundColor = (map.Maparray[i, j] as GameObject).Color;
                            Console.Write(map.Maparray[i, j]);
                            Console.BackgroundColor = ConsoleColor.Black;
                        }


                    }
                }

               // GUI.DrawMap(map.Maparray);
            }

            LeavingMap = 0;
        }

        private static NetPackage CreateNetPackageWithoutPacMan(Ghost g, Map map)
        {
            NetPackage np = new NetPackage(Program.ClientId, -10, -10, 0, false, g.Left, g.Top, map.CoinArray, 0);
            return np;
        }

        public static NetPackage CreateNetPackage(Ghost g, PacMan p, Map map)
        {
            NetPackage np = new NetPackage(Program.ClientId, p.Left, p.Top, LeavingMap, true, g.Left, g.Top, map.CoinArray, p.CoinsCollected);

            return np;
        }

        public static void OnGhostChangedPositionCB(object sender, GhostPositionEventArgs e)
        {
            SuspendThread = true;
        }

        public static void OnPacManLeavingMapCall_Back(object sender, LeavingMapEventArgs e)
        {
            if (e.Direction == -1)
            {
                LeavingMap = -1;
                LeavingMap2 = -1;
            }
            else if (e.Direction == 1)
            {
                LeavingMap = 1;
                LeavingMap2 = 1;
            }
        }
    }
}
