namespace PacmanClientWPF.Resources
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.IO;
   using System.Linq;
   using System.Reflection;
   using System.Text;
   using System.Threading.Tasks;
   using PacmanClientWPF.Interfaces;
   using PacmanClientWPF.Classes;

   public class Maze
   {
      /// <summary>
      /// Parses a previously stored maze and creates all GameShapes accordingly.
      /// </summary>
      /// <param name="level">The path to a maze in the pool of resources. 
      /// <example>(e.g. PacmanClientWPF.Resoures.Level01.txt)</example></param>
      /// <param name="canvas">The canvas the shapes being added to.</param>
      public static void Load(string level, IGameboard canvas)
      {
         //Clear the canvas
         canvas.Children.Clear();

         var mapArray = Maze.ParseLevel(level);

         // Iterate through all rows and create the objects on the canvas in the right position
         for (int i = 0; i < mapArray.GetLength(0); ++i)
         {
            for (int j = 0; j < mapArray.GetLength(1); ++j)
            {
               switch (mapArray[i,j])
               {
                  case '#':
                     canvas.Children.Add(new Wall(canvas, j * canvas.CellSize, i * canvas.CellSize));
                     break;
                  case 'C':
                     canvas.Children.Add(new Player(canvas, j * canvas.CellSize, i * canvas.CellSize));
                     break;
                  case 'G':
                     canvas.Children.Add(new Ghost(canvas, j * canvas.CellSize, i * canvas.CellSize));
                     //Insert a coin underneath the ghosts
                     canvas.Children.Add(new Coin(canvas, j * canvas.CellSize, i * canvas.CellSize));
                     break;
                  case 'o':
                     canvas.Children.Add(new Coin(canvas, j * canvas.CellSize, i * canvas.CellSize));
                     break;
               }
            }
         }
      }

      /// <summary>
      /// Search for the level in the embedded resources
      /// </summary>
      /// <param name="name">The name of the Level <example>(e.g. 'Level01' for the Level01.txt in PacMan.Mazes)</example></param>
      /// <returns>Returns a level built in as a resource as a two-dimensional array.</returns>
      private static char[,] ParseLevel(string name)
      {
         string mapString;

         try
         {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
            {
               using (var reader = new StreamReader(stream))
               {
                  mapString = reader.ReadToEnd();
               }
            }
         }
         catch (Exception)
         {
            return new char[,] { };
         }

         string[] lines = mapString.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

         int row = lines.Count();
         int col = lines[0].Length;

         char[,] map = new char[row, col];
         int i = 0;
         foreach (string line in lines)
         {
            for (int j = 0; j < col; ++j)
            {
               map[i, j] = Convert.ToChar(line.Substring(j, 1));
            }
            i++;
         }

         return map;
      }
   }
}
