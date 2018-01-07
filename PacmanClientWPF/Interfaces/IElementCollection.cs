namespace PacmanClientWPF.Interfaces
{
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading.Tasks;
   using PacmanClientWPF.Classes;

   public interface IElementCollection
   {
      /// <summary>
      /// In order to get a complete abstraction from the Canvas and similar WPF container elements, 
      /// this interface is used in IGameboard.
      /// </summary>
      //Methods
      void Clear();
      void Add(GameElement gameElement);
      void Remove(GameElement gameElement);
      IEnumerator GetEnumerator();
      IEnumerable<T> OfType<T>();
   }
}
