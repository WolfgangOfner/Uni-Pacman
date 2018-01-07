namespace PacmanClientWPF.Classes
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Net.Sockets;
   using System.Text;
   using System.Threading.Tasks;
   using System.Windows;
   using System.Windows.Input;
   using System.Windows.Threading;
   using PacManLib;
   using PacmanClientWPF.Interfaces;
   using PacmanClientWPF.Resources;

   [Serializable()]
   public class Game
   {
      #region Fields
      private const int FirstGhostPositionX = 38;
      private const int FirstGhostPositionY = 56;

      private const int FirstPlayerPositionX = 5;
      private const int FirstPlayerPositionY = 5;

      private DispatcherTimer gameTimer;

      private IGameboard gameboard;

      private Player player;

      private List<Ghost> ghosts;

      private int currentLevel;
      #endregion

      #region Constructor
      public Game(IGameboard gameboard, bool isTcp)
      {
         this.gameboard = gameboard;

         // Initialize the timer
         this.gameTimer = new DispatcherTimer();
         this.gameTimer.Interval = TimeSpan.FromMilliseconds(150);
         this.gameTimer.Tick += new EventHandler(TimerTick);
      }
      #endregion

      #region Properties
      #endregion

      #region Methods
      /// <summary>
      /// A separate startup method to separate the start from the constructor 
      /// </summary>
      public void Start()
      {
         if (!Util.PmNet.IsTcp)
         {
            NextLevel();
         }
         else
         {
            this.TcpLevel();
         }

         this.gameTimer.Start();

         //Get references to the player and ghosts
         //To prevent a crash in case there isn't a player, FirstOrDefault() is used.
         this.player = this.gameboard.Children.OfType<Player>().FirstOrDefault();
         this.ghosts = this.gameboard.Children.OfType<Ghost>().ToList();

         // Register some events of the player instance
         if (this.player != null)
         {
            this.player.Death += new Action(Player_Death);
            this.player.CoinConsumed += new Action(Player_CoinConsumed);
         }
      }

      /// <summary>
      /// Proceeds to the local maze
      /// </summary>
      public void NextLevel()
      {
         this.currentLevel++;
         Maze.Load("PacmanClientWPF.Resources.Level" + this.currentLevel.ToString("D2") + ".txt", this.gameboard);
      }

      public void TcpLevel()
      {
         //gameboard.IsTcp = this.IsTcp;
         //gameboard.TcpClientId = this.TcpClientId;
         //gameboard.NetworkStream = ns;

         char[,] mapArray = Util.PmNet.Pack.Maps.ElementAt(Util.PmNet.Pack.Index);
         //Clear the gameboard
         gameboard.Children.Clear();

         // Iterate through all rows and create the objects on the gameboard in the right position
         bool[,] coinArray = new bool[mapArray.GetLength(0), mapArray.GetLength(1)];
         
         for (int i = 0; i < mapArray.GetLength(0); i++)
         {
            for (int j = 0; j < mapArray.GetLength(1); j++)
            {
               coinArray[i, j] = false;
               switch (mapArray[i, j])
               {
                  case '#':
                     gameboard.Children.Add(new Wall(gameboard, j * gameboard.CellSize, i * gameboard.CellSize));
                     break;
                  case 'C':
                     gameboard.Children.Add(new Player(gameboard, j * gameboard.CellSize, i * gameboard.CellSize));
                     break;
                  case 'G':
                     gameboard.Children.Add(new Ghost(gameboard, j * gameboard.CellSize, i * gameboard.CellSize));
                     break;
                  case 'o':
                     gameboard.Children.Add(new Coin(gameboard, j * gameboard.CellSize, i * gameboard.CellSize));
                     coinArray[i, j] = true;
                     break;
               }
            }
         }

         Util.PmNet.CoinArray = coinArray;
         gameboard.Children.Add(new Player(gameboard, FirstPlayerPositionX * gameboard.CellSize, FirstPlayerPositionY * gameboard.CellSize));
         gameboard.Children.Add(new Ghost(gameboard, FirstGhostPositionX * gameboard.CellSize, FirstGhostPositionY * gameboard.CellSize));
      }

      /// <summary>
      /// The GameTimers-Tick event basically serves as the "GameLoop"
      /// </summary>
      private void TimerTick(object sender, EventArgs e)
      {
         //Randomly move all ghosts
         this.ghosts.ForEach(g => g.MoveRandom(this.ghosts.IndexOf(g)));
      }

      /// <summary>
      /// Handle Key Down events passed to the game class.
      /// </summary>
      /// <param name="key">The key that is pressed down.</param>
      public void KeyDown(Key key)
      {
         if (!this.gameTimer.IsEnabled)
            return;
         switch (key)
         {
            case Key.Up:
               this.player.MoveUp();
               break;
            case Key.Down:
               this.player.MoveDown();
               break;
            case Key.Left:
               this.player.MoveLeft();
               break;
            case Key.Right:
               this.player.MoveRight();
               break;
            default:
               break;
         }
      }

      /// <summary>
      /// Catches the players death and will invoke the GameOver event
      /// </summary>
      private void Player_Death()
      {
         if (this.player.Lives > 0)
         {
            //Reset the characters
            this.player.Reset();
            this.ghosts.ForEach(g => g.Reset());
         }
         else
         {
            GameOver();
            this.gameTimer.Stop();
         }
      }

      /// <summary>
      /// Fires everytime a coin is consumed. In case there aren't any coins 
      /// left in the gameboard, the next level can be started.
      /// </summary>
      private void Player_CoinConsumed()
      {
         if (!Util.PmNet.IsTcp)
         {
            if (!this.gameboard.Children.OfType<Coin>().Any())
            {
               //NextLevel();
               GameOver();
               this.gameTimer.Stop();
            }
         }
      }
      #endregion

      // Event game over
      public event Action GameOver = delegate { };
   }
}
