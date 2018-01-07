using System;
using System.Collections.Generic;
using System.Linq;

namespace PacmanClientWPF.Interfaces
{
   using System.Net.Sockets;
   using System.Text;
   using System.Threading.Tasks;

   using PacmanClientWPF.Classes;

   /// <summary>
   /// In order to get a complete abstraction from the Canvas element on the UI, 
   /// this interface is used all over the game's model.
   /// In addition to that, it's important to wrap methods used to change 
   /// the attached properties of Canvas. (e.g. Canvas.SetTop)
   /// </summary>
   public interface IGameboard
   {
      //Properties
      int CellSize { get; set; }
      //bool IsTcp { get; set; }
      //int TcpClientId { get; set; }
      //NetworkStream NetworkStream { get; set; }

      IElementCollection Children { get; }

      //Methods
      void SetElement(GameElement gameElement);

      List<GameElement> GetChilds(double posx, double posy);
   }
}
