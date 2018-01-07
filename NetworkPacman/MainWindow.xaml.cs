using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.ComponentModel;
using System.Runtime.Serialization.Formatters.Binary;
using PacManLib;
using System.Windows.Threading;

namespace NetworkPacman
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            tbIp.Text = GetLocalIPAddress();
        }

        public static List<char[,]> allMaps = new List<char[,]>();                                      // contains all loaded Maps
        public static List<ClientRepresentation> connectedClients = new List<ClientRepresentation>();
        public static List<char[,]> randomMap = new List<char[,]>();                                    // list with all maps for clients
        public static GameInfo gameInfo;                                                                // game object with infos, like coins
        public static List<Ghost> allGhosts = new List<Ghost>();
        List<bool[,]> CoinList = new List<bool[,]>();        
        ClientRepresentation clientRepres;                                                              // client object
        DispatcherTimer dispatcherTimer;
        int hour = 0;
        int min = 0;
        int sec = 0;

        private void Btn_StartServer_Click(object sender, RoutedEventArgs e)
        {
            int coinsTotal;
            btnEndServer.IsEnabled = true;
            btnStartServer.IsEnabled = false;
            btnSearch1.IsEnabled = false;
            btnSearch2.IsEnabled = false;
            btnSearch3.IsEnabled = false;
            btnSearch4.IsEnabled = false;
            btnSearch5.IsEnabled = false;
            lbGamestateInfo.Foreground = Brushes.Blue;
            lbGamestateInfo.Content = "Searching for players";

            //CreateRandomMap();
            CreateRandomMapList();
            coinsTotal = CountCoins();
            gameInfo = new GameInfo(coinsTotal, CoinList);
            lbDisplayCoins.Content = "0 / " + coinsTotal;

            BackgroundWorker Listener = new BackgroundWorker();
            Listener.DoWork += Listen;
            Listener.RunWorkerAsync();
        }

        private void CreateRandomMapList()
        {
            Random rand = new Random();

            for (int i = 0; i < Convert.ToInt32(tbPlayerCount.Text); i++)
            {
                int randomIndex = rand.Next(0, allMaps.Count - 1);
                randomMap.Add(allMaps[randomIndex]);
                CoinList.Add(new bool[1, 1]);
            }
        }

        private int CountCoins()
        {
            int maxCoins = 0;

            for (int i = 0; i < randomMap.Count; i++)
            {
                for (int j = 0; j < randomMap[i].GetLength(0); j++)
                {
                    for (int k = 0; k < randomMap[i].GetLength(1); k++)
                    {
                        if (randomMap[i][j, k] == 'o')
                        {
                            maxCoins++;
                        }
                    }
                }
            }

            return maxCoins;
        }

        private void Listen(object sender, DoWorkEventArgs e)
        {
            int playerCount = 0;
            Dispatcher.Invoke(new Action(() => playerCount = Convert.ToInt32(tbPlayerCount.Text)));

            TcpListener server = null;
            try
            {
                Int32 port = 0;
                IPAddress localAddr = IPAddress.Parse("192.168.0.1");

                Dispatcher.Invoke(new Action(() =>
                {
                    port = Convert.ToInt32(tbPort.Text);
                    localAddr = IPAddress.Parse(tbIp.Text);
                }));

                server = new TcpListener(localAddr, port);

                server.Start();


                while (connectedClients.Count < playerCount)
                {
                    TcpClient client = server.AcceptTcpClient();

                    clientRepres = new ClientRepresentation(client, gameInfo, connectedClients.Count);
                    connectedClients.Add(clientRepres);

                    string clientIPAddress = Convert.ToString(IPAddress.Parse(((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString()));
                    Dispatcher.Invoke(new Action(() => lbxPlayerList.Items.Add(clientIPAddress)));
                }

                Dispatcher.Invoke(new Action(() => btnStartGame.IsEnabled = true));
            }

            catch (SocketException)
            {
              
            }

            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }
        }

        private void Search1_Click(object sender, RoutedEventArgs e)
        {
            string path = LoadMap();
            tbMapPath1.Text = path;

            if (path != string.Empty)
            {
                char[,] map1 = FillMap(path);
                allMaps.Add(map1);
            }
        }

        private void Search2_Click(object sender, RoutedEventArgs e)
        {
            string path = LoadMap();
            tbMapPath2.Text = path;

            if (path != string.Empty)
            {
                char[,] map2 = FillMap(path);
                allMaps.Add(map2);
            }
        }

        private void Search3_Click(object sender, RoutedEventArgs e)
        {
            string path = LoadMap();
            tbMapPath3.Text = path;

            if (path != string.Empty)
            {
                char[,] map3 = FillMap(path);
                allMaps.Add(map3);
            }
        }

        private void Search4_Click(object sender, RoutedEventArgs e)
        {
            string path = LoadMap();
            tbMapPath4.Text = path;

            if (path != string.Empty)
            {
                char[,] map4 = FillMap(path);
                allMaps.Add(map4);
            }
        }

        private void Search5_Click(object sender, RoutedEventArgs e)
        {
            string path = LoadMap();
            tbMapPath5.Text = path;

            if (path != string.Empty)
            {
                char[,] map5 = FillMap(path);
                allMaps.Add(map5);
            }
        }

        private string LoadMap()
        {
            string path = string.Empty;

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".txt";
            dlg.Filter = "txt files (*.txt)|*.txt";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                path = dlg.FileName;
            }

            return path;
        }

        private char[,] FillMap(string path)
        {
            int i = 0;
            int j = 0;
            string line = string.Empty;

            StreamReader streamReader = File.OpenText(path);

            while ((line = streamReader.ReadLine()) != null)
            {
                i++;
                j = line.Length;
            }
            streamReader.Close();

            streamReader = File.OpenText(path);

            char[,] map = new char[i, j];

            i = 0;
            line = string.Empty;

            while ((line = streamReader.ReadLine()) != null)
            {
                for (j = 0; j < line.Length; j++)
                {
                    map[i, j] = Convert.ToChar(line.Substring(j, 1));

                }
                i++;
            }

            return map;
        }

        private string GetLocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }

        private void tbPlayercount_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbMapPath1.IsEnabled = true;
            btnSearch1.IsEnabled = true;
        }

        private void tbMapPath1_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnStartServer.IsEnabled = true;
            btnSearch2.IsEnabled = true;
            tbMapPath2.IsEnabled = true;
        }

        private void tbMapPath2_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnSearch3.IsEnabled = true;
            tbMapPath3.IsEnabled = true;
        }

        private void tbMapPath3_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnSearch4.IsEnabled = true;
            tbMapPath4.IsEnabled = true;
        }

        private void tbMapPath4_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnStartServer.IsEnabled = true;
            btnSearch5.IsEnabled = true;
            tbMapPath5.IsEnabled = true;
        }

        private char[,] CreateBigMap()
        {
            int row = 0;
            int col = 0;

            for (int i = 0; i < allMaps.Count; i++)
            {
                col += allMaps[i].GetLength(1);
            }

            row = allMaps[0].GetLength(0);

            char[,] bigMap = new char[row, col];

            return bigMap;
        }

        // old version for array
        //private char[,] CreateRandomMap()
        //{
        //    int col = allMaps[0].GetLength(1) * allMaps.Count * Convert.ToInt32(tbPlayercount.Text);
        //    int row = allMaps[0].GetLength(0);

        //    char[,] randomMap = new char[row, col];

        //    Random rand = new Random();

        //    for (int i = 0; i < Convert.ToInt32(tbPlayercount.Text); i++)
        //    {
        //        int randomIndex = rand.Next(0, allMaps.Count - 1);

        //        for (int j = 0; j < allMaps[randomIndex].GetLength(0); j++)
        //        {
        //            for (int k = 0; k < allMaps[randomIndex].GetLength(1); k++)
        //            {
        //                randomMap[j, k + (allMaps[0].GetLength(1) * (i))] = allMaps[randomIndex][j, k];
        //            }
        //        }
        //    }

        //    return randomMap;
        //}

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            // timer
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            btnStartGame.IsEnabled = false;

            int id = 0;

            foreach (var item in connectedClients)
            {
                InitialNetPackage np = new InitialNetPackage(randomMap, id++);
                Networking.SendInitialPackage(np, item.TcpClient.GetStream());
            }
        }

        private void EndServer_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            string sHour = "0";
            string sMin = "0";
            string sSec = "0";
            sec++;

            if (sec == 60)
            {
                sec = 0;
                min++;
            }

            if (min == 60)
            {
                min = 0;
                hour++;
            }

            if (sec < 10)
            {
                sSec += sec.ToString();
            }
            else
            {
                sSec = sec.ToString();
            }

            if (min < 10)
            {
                sMin += min.ToString();
            }
            else
            {
                sMin = min.ToString();
            }

            if (hour < 10)
            {
                sHour += hour.ToString();
            }
            else
            {
                sHour = hour.ToString();
            }

            lbDurationTime.Content = sHour + ":" + sMin + ":" + sSec;

            UpdateCoins();
        }

        private void UpdateCoins()
        {
            if (gameInfo.GameStatus == 1)
            {
                lbGamestateInfo.Foreground = Brushes.Green;
                lbGamestateInfo.Content = "Pacman won :)";
                dispatcherTimer.Stop();
            }

            else if (gameInfo.GameStatus == -1)
            {
                lbGamestateInfo.Foreground = Brushes.Red;
                lbGamestateInfo.Content = "Pacman lost :(";
                dispatcherTimer.Stop();
            }
            else
            {
                lbGamestateInfo.Foreground = Brushes.Green;
                lbGamestateInfo.Content = "Running";
            }

           lbDisplayCoins.Content = gameInfo.CurrentCoins + " / " + gameInfo.MaxCoins;
        }

    }
}