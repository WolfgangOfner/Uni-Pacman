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
using PacmanClientWPF.Classes;
using PacmanClientWPF.Util;

namespace PacmanClientWPF
{
   using System.Security.Policy;

   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>

   public partial class MainWindow : Window
   {
      private Game game;

      private bool first = true;

      private bool isTcp = false;

      private bool isStartGame = false;

      public MainWindow()
      {
         InitializeComponent();
      }

      private bool CheckInput()
      {
         bool isInputOK = true;
         if (rdoNoServer.IsChecked == false)
         {
            if (!Util.Util.IsValidIP(txtIP.Text))
            {
               isInputOK = false;
               MessageBox.Show("IP-Adress is wrong", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!Util.Util.IsValidPort(txtPort.Text))
            {
               isInputOK = false;
               MessageBox.Show("IP-Port is wrong", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            try
            {
               Util.PmNet.NetInit(txtIP.Text, txtPort.Text);
               isTcp = true;
            }
            catch (Exception)
            {
               isInputOK = false;
               MessageBox.Show("No Connection", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
         }

         return isInputOK;
      }

      private void StartGame(bool isTcp)
      {
         game = new Game(cnvMain, isTcp);

         if (!isTcp)
         {
            game.GameOver += new Action(Game_GameOver);
         }
         game.Start();
      }

      private void Game_GameOver()
      {
         //invoke some kind of GameOver screen
         ctrlGameOver.Visibility = Visibility.Visible;
      }

      private void Window_KeyDown(object sender, KeyEventArgs e)
      {
         if (game != null)
         {
            game.KeyDown(e.Key);
         }
      }

      private void btnStart_Click(object sender, RoutedEventArgs e)
      {
         if (this.CheckInput())
         {
            this.isStartGame = true;
            btnStart.IsEnabled = false;
            this.StartGame(isTcp);
         }
      }

      private void btnEnd_Click(object sender, RoutedEventArgs e)
      {
         this.Game_GameOver();
      }

      private void RdoNoServer_OnChecked(object sender, RoutedEventArgs e)
      {
         if (!first)
         {
            txtIP.IsEnabled = false;
            txtPort.IsEnabled = false;
         }
         first = false;
      }

      private void RdoServer_OnChecked(object sender, RoutedEventArgs e)
      {
         txtIP.IsEnabled = true;
         txtPort.IsEnabled = true;
      }
   }
}
