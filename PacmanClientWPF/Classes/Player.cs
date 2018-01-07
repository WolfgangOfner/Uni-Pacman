namespace PacmanClientWPF.Classes
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using System.Windows;
   using System.Windows.Media;
   using PacmanClientWPF.Interfaces;

   public class Player : Character
   {
      #region Properties
      public int Lives { get; private set; }
      #endregion

      #region Constructor
      public Player(IGameboard gameboard, int posx, int posy)
         : base(gameboard, posx, posy, Colors.Yellow)
      {
         Lives = 1;
         PositionZ = 1;
      }
      #endregion

      #region Methods
      protected override Geometry LoadGeometry()
      {
         return new EllipseGeometry(new Rect(0, 0, Gameboard.CellSize, Gameboard.CellSize));
      }

      /// <summary>
      /// "Consumes" a given list of GameElement.
      /// </summary>
      /// <param name="elements">The list of GameElements which the player collided with.</param>
      protected override void Consume(List<GameElement> elements)
      {
         // If the player consumes a ghost, he "dies".
         var ghost = elements.OfType<Ghost>().FirstOrDefault();
         if (ghost != null)
         {
            Remove();
            return;
         }

         // If the player consumes a coin, it will be removed and an event will inform the game about it
         var coin = elements.OfType<Coin>().SingleOrDefault();
         if (coin != null)
         {
            coin.Remove();
            CoinConsumed();
         }
      }

      /// <summary>
      /// "Kills" the player by removing it from the canvas and invokes an event.
      /// </summary>
      public override void Remove()
      {
         Lives--;    //Remove a life
         Death();    //throw an event to tell the game, that the player died
      }
      #endregion

      #region Events
      public event Action Death = delegate { };
      public event Action CoinConsumed = delegate { };
      #endregion
   }
}
