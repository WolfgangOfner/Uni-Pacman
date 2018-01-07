namespace PacmanClientWPF.Controls
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using System.Windows;
   using System.Windows.Controls;
   using PacmanClientWPF.Classes;
   using PacmanClientWPF.Interfaces;

   /// <summary>
   /// Important: Overriding the Children collection won't display any added UIElements inside the canvas. 
   /// The methods like Add() have to make sure, that every element is passed to Canvas' property 'InternalChildren'.
   /// </summary>
   public class GridCanvasElements : UIElementCollection, IElementCollection
   {
      #region Fields
      private readonly Canvas canvas;
      #endregion

      #region Constructor
      public GridCanvasElements(UIElement visualParent, FrameworkElement logicalParent)
         : base(visualParent, logicalParent)
      {
         this.canvas = logicalParent as Canvas;
      }
      #endregion

      #region Methods
      /// <summary>
      /// Adds the given element to the collection.
      /// </summary>
      /// <param name="gameElement">The element to add.</param>
      public void Add(GameElement gameElement)
      {
         this.canvas.Children.Add(gameElement);
      }

      /// <summary>
      /// Removes the given element of the collection.
      /// </summary>
      /// <param name="gameElement">The element ot remove.</param>
      public void Remove(GameElement gameElement)
      {
         this.canvas.Children.Remove(gameElement);
      }

      /// <summary>
      /// Removes all elements of the collection.
      /// </summary>
      public new void Clear()
      {
         this.canvas.Children.Clear();
      }

      /// <summary>
      /// Returns a list of child elements with a certain type.
      /// </summary>
      /// <returns>A list of elements of the specified type.</returns>
      public IEnumerable<T> OfType<T>()
      {
         var list = new List<T>();
         foreach (var child in this.canvas.Children)
         {
            if (child.GetType() == typeof(T))
            {
               list.Add((T)child);
            }
         }
         return list;
      }
      #endregion
   }
}
