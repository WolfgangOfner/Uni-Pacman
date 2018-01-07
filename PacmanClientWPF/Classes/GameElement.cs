namespace PacmanClientWPF.Classes
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using System.Windows.Media;
   using System.Windows.Shapes;
   using PacmanClientWPF.Interfaces;

   public abstract class GameElement : Shape
   {
      #region Fields
      private double positionX;
      private double positionY;
      private int positionZ;
      #endregion

      #region Properties
      protected IGameboard Gameboard { get; set; }

      protected override Geometry DefiningGeometry
      {
         get { return LoadGeometry(); }
      }

      /* In order to separate the setting of "Attached Properties" from the game model while
       * maintaining the setters protected, GameElement has to save the values itself and just passes its own reference to the Gameboard.
       */
      public double PositionX
      {
         get { return this.positionX; }
         protected set { this.positionX = value; Gameboard.SetElement(this); }
      }
      public double PositionY
      {
         get { return this.positionY; }
         protected set { this.positionY = value; Gameboard.SetElement(this); }
      }
      public int PositionZ
      {
         get { return this.positionZ; }
         protected set { this.positionZ = value; Gameboard.SetElement(this); }
      }
      #endregion

      #region Constructor
      /// <summary>
      /// Create a new GameShape.
      /// </summary>
      /// <param name="gameboard">A reference to the gameboard the element gets drawn on.</param>
      /// <param name="posx">The initial starting position relatively to the left.</param>
      /// <param name="posy">The initial starting position relatively to the top.</param>
      /// <param name="color">The basic color of the new shape.</param>
      public GameElement(IGameboard gameboard, int posx, int posy, Color color)
      {
         Gameboard = gameboard;

         Fill = new SolidColorBrush(color);

         Width = Gameboard.CellSize;
         Height = Gameboard.CellSize;

         PositionX = posx;
         PositionY = posy;
      }
      #endregion

      #region Methods
      protected abstract Geometry LoadGeometry();

      /// <summary>
      /// Removes a GameElement from the gameboard
      /// </summary>
      public virtual void Remove()
      {
         Gameboard.Children.Remove(this);
      }
      #endregion
   }
}
