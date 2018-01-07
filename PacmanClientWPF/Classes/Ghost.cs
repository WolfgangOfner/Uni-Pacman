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

   public class Ghost : Character
   {
      #region Fields
      private Move lastMove;
      #endregion

      #region Constructor
      public Ghost(IGameboard gameboard, int posx, int posy)
         : base(gameboard, posx, posy, Colors.Red)
      {
         PositionZ = 2;
      }
      #endregion

      #region Methods
      protected override Geometry LoadGeometry()
      {
         return new RectangleGeometry(new Rect(0, 0, Gameboard.CellSize, Gameboard.CellSize));
      }

      /// <summary>
      /// "Consumes" a given list of GameElement.
      /// </summary>
      /// <param name="elements">The list of GameElements which the ghost collided with.</param>
      protected override void Consume(List<GameElement> elements)
      {
         //If the ghost consumes a player, it will be removed.
         var player = elements.OfType<Player>().SingleOrDefault();
         if (player != null)
         {
            player.Remove();
         }
      }

      /// <summary>
      /// Moves the Ghost into a random direction (the last move is taken into consideration)
      /// To improve the Ghost AI slightly (make Ghosts move "foreward" most of the time) 
      /// following rules are applied:
      /// - do a valid Move every round (trying again after hitting the wall)
      /// - "prefer" the last move by adding it to the list of possible moves in GetRandomMove
      /// </summary>
      /// <param name="seed">A seed to achieve better 'randomness'.</param>
      public void MoveRandom(int seed)
      {
         Move move;

         //try new moves as long as it was successful
         do
         {
            move = GetRandomMove(seed, this.lastMove);
         }
         while (!move.Invoke());

         //Save the successful move as for the next step
         this.lastMove = move;
      }

      /// <summary>
      /// Responsible for creating new random moves.
      /// </summary>
      /// <param name="seed">A seed to achieve better 'randomness'.</param>
      /// <param name="lastMove">The last move is taken into consideration.</param>
      /// <returns>A random Move.</returns>
      private Move GetRandomMove(int seed, Move lastMove)
      {
         //put all possible Move-Methods into a list
         var moves = new List<Move>() { MoveUp, MoveLeft, MoveDown, MoveRight };

         // Add the lastMove to the list as well (this is done multiple times 
         // to make it more likely for the lastMove to be chosen again)
         if (lastMove != null)
            for (int i = 0; i < 4; i++)
               moves.Add(lastMove);

         //create a "random" number
         var rnd = new Random(seed + System.Environment.TickCount);
         var number = rnd.Next(moves.Count);

         return moves[number];
      }
      #endregion
   }
}
