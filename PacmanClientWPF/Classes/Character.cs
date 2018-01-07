namespace PacmanClientWPF.Classes
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using System.Windows.Media;
   using PacmanClientWPF.Interfaces;

   public delegate bool Move();

   public abstract class Character : GameElement
   {
      #region Fields
      private readonly double defaultX;
      private readonly double defaultY;
      #endregion

      #region Constructor
      public Character(IGameboard canvas, int posx, int posy, Color color)
         : base(canvas, posx, posy, color)
      {
         this.defaultX = posx;
         this.defaultY = posy;
      }
      #endregion

      #region Methods
      /// <summary>
      /// Resets the character to its default position (the position he was created on).
      /// </summary>
      public void Reset()
      {
         PositionX = this.defaultX;
         PositionY = this.defaultY;
      }

      /// <summary>
      /// Moves the character to the cell on the right if it is possible 
      /// and react to the deteced element.
      /// </summary>
      /// <returns>True/False whether or not the move was successful.</returns>
      public bool MoveRight()
      {
         return Move(Gameboard.CellSize, 0);
      }

      /// <summary>
      /// Moves the character to the cell on the left if it is possible 
      /// and react to the deteced element.
      /// </summary>
      /// <returns>True/False whether or not the move was successful.</returns>
      public bool MoveLeft()
      {
         return Move(-Gameboard.CellSize, 0);
      }

      /// <summary>
      /// Moves the character to the cell above if it is possible 
      /// and react to the deteced element.
      /// </summary>
      /// <returns>True/False whether or not the move was successful.</returns>
      public bool MoveUp()
      {
         return Move(0, -Gameboard.CellSize);
      }

      /// <summary>
      /// Moves the character to the cell underneath if it is possible 
      /// and react to the deteced element.
      /// </summary>
      /// <returns>True/False whether or not the move was successful.</returns>
      public bool MoveDown()
      {
         return Move(0, Gameboard.CellSize);
      }

      /// <summary>
      /// Moves the character to the given position (checks for conflicts and consumes).
      /// </summary>
      /// <param name="addx">The value which will be add to the current x-coordinate of the character.</param>
      /// <param name="addy">The value which will be add to the current y-coordinate of the character.</param>
      /// <returns>True/False whether or not the move was successful.</returns>
      private bool Move(double addx, double addy)
      {
         if (this.CheckGroove(addx, addy))
         {
            PositionX = PositionX + addx;
            PositionY = PositionY + addy;
            this.Consume(Gameboard.GetChilds(PositionX, PositionY));
            return true;
         }

         return false;
      }

      private bool CheckGroove(double x, double y)
      {
         double newPositionX = PositionX + x;
         double newPositionY = PositionY + y;
         double check_x;
         double check_y;

         if (x < 0.0 || x > 0.0)
         {
            if (x < 0.0)
            {
               check_x = newPositionX - Gameboard.CellSize;
               check_y = newPositionY - Gameboard.CellSize;
               if (this.AllowedToMove(Gameboard.GetChilds(check_x, check_y)))
               {
                  check_y = newPositionY;
                  if (this.AllowedToMove(Gameboard.GetChilds(check_x, check_y)))
                  {
                     check_y = newPositionY + Gameboard.CellSize;
                     if (this.AllowedToMove(Gameboard.GetChilds(check_x, check_y)))
                     {
                        return true;
                     }
                  }
               }
               return false;
            }
            else
            {
               check_x = newPositionX + Gameboard.CellSize;
               check_y = newPositionY - Gameboard.CellSize;
               if (this.AllowedToMove(Gameboard.GetChilds(check_x, check_y)))
               {
                  check_y = newPositionY;
                  if (this.AllowedToMove(Gameboard.GetChilds(check_x, check_y)))
                  {
                     check_y = newPositionY + Gameboard.CellSize;
                     if (this.AllowedToMove(Gameboard.GetChilds(check_x, check_y)))
                     {
                        return true;
                     }
                  }
               }
               return false;
            }
         }

         if (y < 0.0 || y > 0.0)
         {
            if (y < 0.0)
            {
               check_y = newPositionY - Gameboard.CellSize;
               check_x = newPositionX - Gameboard.CellSize;
               if (this.AllowedToMove(Gameboard.GetChilds(check_x, check_y)))
               {
                  check_x = newPositionX;
                  if (this.AllowedToMove(Gameboard.GetChilds(check_x, check_y)))
                  {
                     check_x = newPositionX + Gameboard.CellSize;
                     if (this.AllowedToMove(Gameboard.GetChilds(check_x, check_y)))
                     {
                        return true;
                     }
                  }
               }
               return false;
            }
            else
            {
               check_y = newPositionY + Gameboard.CellSize;
               check_x = newPositionX - Gameboard.CellSize;
               if (this.AllowedToMove(Gameboard.GetChilds(check_x, check_y)))
               {
                  check_x = newPositionX;
                  if (this.AllowedToMove(Gameboard.GetChilds(check_x, check_y)))
                  {
                     check_x = newPositionX + Gameboard.CellSize;
                     if (this.AllowedToMove(Gameboard.GetChilds(check_x, check_y)))
                     {
                        return true;
                     }
                  }
               }
               return false;
            }
         }

         return false;
      }

      /// <summary>
      /// Determines whether or not the character is allowed to move, based on what was found on that position.
      /// </summary>
      /// <param name="elements">A list of elements on the given position.</param>
      /// <returns>True if the character is allowed to move.</returns>
      private bool AllowedToMove(List<GameElement> elements)
      {
         return !elements.OfType<Wall>().Any();
      }

      protected abstract void Consume(List<GameElement> elements);
      #endregion
   }
}
