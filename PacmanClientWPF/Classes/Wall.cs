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

   public class Wall : GameElement
   {
      #region Constructor
      public Wall(IGameboard gameboard, int posx, int posy)
         : base(gameboard, posx, posy, Colors.Gray)
      {
      }
      #endregion

      #region Methods
      protected override Geometry LoadGeometry()
      {
         return new RectangleGeometry(new Rect(0, 0, Gameboard.CellSize, Gameboard.CellSize));
      }
      #endregion
   }
}
