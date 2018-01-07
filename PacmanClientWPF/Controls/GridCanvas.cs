namespace PacmanClientWPF.Controls
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Net.Sockets;
   using System.Text;
   using System.Threading.Tasks;
   using System.Windows.Controls;
   using System.Windows;
   using PacmanClientWPF.Classes;
   using PacmanClientWPF.Interfaces;

   public class GridCanvas : Canvas, IGameboard
   {
      #region Fields
      private IElementCollection children;
      public static readonly DependencyProperty CellSizeProperty = DependencyProperty.Register("CellSize", typeof(int), typeof(GridCanvas), new UIPropertyMetadata(0));
      #endregion

      #region Properties
      public new IElementCollection Children
      {
         get { return this.children; }
      }

      public int CellSize
      {
         get { return (int)GetValue(CellSizeProperty); }
         set { SetValue(CellSizeProperty, value); }
      }

      public bool IsTcp { get; set; }
      public int TcpClientId { get; set; }
      public NetworkStream NetworkStream { get; set; }


      #endregion

      #region Constructor
      public GridCanvas()
      {
         this.children = new GridCanvasElements(this, this);
      }
      #endregion

      #region Methods
      /// <summary>
      /// Sets the GameElement visually based on its logical position.
      /// </summary>
      /// <param name="gameElement">The GameElement to set on the UI.</param>
      public void SetElement(GameElement gameElement)
      {
         Canvas.SetLeft(gameElement, gameElement.PositionX);
         Canvas.SetTop(gameElement, gameElement.PositionY);
         Canvas.SetZIndex(gameElement, gameElement.PositionZ);
      }

      /// <summary>
      /// Returns the child elements on a certain position within the canvas.
      /// </summary>
      /// <param name="posx">The position relatively to the left.</param>
      /// <param name="posy">The position relatively to the top.</param>
      /// <returns>The elements determined or null</returns>
      public List<GameElement> GetChilds(double posx, double posy)
      {
         var list = new List<GameElement>();
         foreach (var child in InternalChildren) //See explanation in GridCanvasElements
         {
            var gameElement = child as GameElement;
            if (gameElement != null && gameElement.PositionX == posx && gameElement.PositionY == posy)
            {
               list.Add(gameElement);
            }
         }
         return list;
      }
      #endregion
   }
}
