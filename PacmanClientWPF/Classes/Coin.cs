using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using PacmanClientWPF.Interfaces;

namespace PacmanClientWPF.Classes
{
   public class Coin : GameElement
   {
      #region Constructor
      public Coin(IGameboard gameboard, int posx, int posy)
         : base(gameboard, posx, posy, Colors.HotPink)
      {
         PositionZ = 0;
      }
      #endregion

      #region Methods
      protected override Geometry LoadGeometry()
      {
         var radius = Gameboard.CellSize / 3;

         return new EllipseGeometry(new Rect(radius, radius, radius, radius));
      }
      #endregion
   }
}
