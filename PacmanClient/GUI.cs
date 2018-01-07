using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
using PacManLib;


namespace PacmanClient
{
    [Serializable()]
    public static class GUI
    {
        public static object locker2 = new object();

        public static  object locker = new object();

        public static void StartUp(int height, int width)
        {
            Console.Clear();
            Console.Title = "PacMan - Client";
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
            Console.CursorVisible = false;
            Console.Clear();
        }

        public static char[,] ReadMap()
        {
            string line;
            int counter = 0;
            int collumncounter = 0;
            int linecounter = 0;

            // Read the file and display it line by line.
            StreamReader file =
                new StreamReader(@"Z:\Documents\Visual Studio 2013\Projects\PacMan\PacmanClient\bin\Debug\Maps\Map_4.txt");
            // Just to count rows and collumns for dynamic map design.
            while ((line = file.ReadLine()) != null)
            {
                collumncounter = line.Length;

                linecounter++;
            }

            char[,] charMap = new char[linecounter, collumncounter];

            StartUp(linecounter + 2, collumncounter + 2);

            StreamReader file2 =
               new StreamReader(@"Z:\Documents\Visual Studio 2013\Projects\PacMan\PacmanClient\bin\Debug\Maps\Map_4.txt");
            while ((line = file2.ReadLine()) != null)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    charMap[counter, i] = line[i];
                }

                counter++;
            }


            file.Close();
            return charMap;
        }

        public static void DrawMap(object[,] map)
        {
            Console.Clear();

            for (int i = 0; i < map.GetLength(0); i++)
            {
                Console.SetCursorPosition(0, i);

                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.BackgroundColor = (map[i, j] as GameObject).Color;
                    Console.ForegroundColor = (map[i, j] as GameObject).Color;
                    Console.Write(map[i, j]);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }
        }

        public static void DrawPacMan(int direction, PacMan p)
        {
            //Monitor.Enter(locker);

            Console.SetCursorPosition(p.OldLeft - 1, p.OldTop - 1);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(' ');
                }
                Console.SetCursorPosition(p.OldLeft - 1, p.OldTop + i);

            }



            Console.SetCursorPosition(p.Left - 1, p.Top - 1);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.ForegroundColor = p.Color;
                    Console.BackgroundColor = p.Color;
                    Console.Write(p.Name);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.SetCursorPosition(p.Left - 1, p.Top + i);

            }
            Console.ForegroundColor = ConsoleColor.Black;
            switch (direction)
            {
                case 1:
                    {
                        Console.SetCursorPosition(p.Left, p.Top - 1);
                        Console.Write(' ');
                        break;
                    }
                case 2:
                    {
                        Console.SetCursorPosition(p.Left, p.Top + 1);
                        Console.Write(' ');
                        break;
                    }
                case 3:
                    {
                        Console.SetCursorPosition(p.Left - 1, p.Top);
                        Console.Write(' ');
                        break;
                    }
                case 4:
                    {
                        Console.SetCursorPosition(p.Left + 1, p.Top);
                        Console.Write(' ');
                        break;
                    }
                default:
                    break;
            }
            //Monitor.Exit(locker);
        }

        public static bool CheckIfMoveIsValid(object[,] map, int top, int left)
        {
            if (map[left + 1, top] is Wall || map[left - 1, top] is Wall || map[left, top - 1] is Wall || map[left, top + 1] is Wall || map[left + 1, top + 1] is Wall || map[left - 1, top - 1] is Wall || map[left + 1, top - 1] is Wall || map[left - 1, top + 1] is Wall)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool CheckIfCoinonNewPosition(bool[,] coinarray, int top, int left)
        {
            if (coinarray[left, top] == true)
            {
                coinarray[left, top] = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void DrawEnd()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
             Console.SetCursorPosition(0, 31);
             Console.Beep();
            string ende = @"     
                     (       (     (  `         ( /(            )\ )  
                     )\ )    )\    )\))(  (     )\())(   (  (  (()/(  
                    (()/( ((((_)( ((_)()\ )\   ((_)\ )\  )\ )\  /(_)) 
                     /(_))_)\ _ )\(_()((_|(_)    ((_|(_)((_|(_)(_))   
                    (_)) __(_)_\(_)  \/  | __|  / _ \ \ / /| __| _ \  
                      | (_ |/ _ \ | |\/| | _|  | (_) \ V / | _||   /  
                       \___/_/ \_\|_|  |_|___|  \___/ \_/  |___|_|_\ ";

            Console.WriteLine(ende);
            
            Console.ReadKey(true);

        }

        public static void DrawWin()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 31);
            Console.Beep();
            string ende = @"
                    ( /( ( /(           (  (      )\ )  ( /(  
                    )\()))\())     (    )\))(   '(()/(  )\()) 
                    ((_)\((_)\      )\  ((_)()\ )  /(_))((_)\  
                    __ ((_) ((_)  _ ((_) _(())\_)()(_))   _((_) 
                    \ \ / // _ \ | | | | \ \((_)/ /|_ _| | \| | 
                     \ V /| (_) || |_| |  \ \/\/ /  | |  | .` | 
                      |_|  \___/  \___/    \_/\_/  |___| |_|\_|";

            Console.WriteLine(ende);
            Console.ReadKey(true);
        }

        public static void DrawException()
        {
            Console.Clear();
            Console.CursorVisible = false;

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(0, 31);
            Console.Beep();

            string exception = @"          ___                            _   _               _           _     _ 
         / __\___  _ __  _ __   ___  ___| |_(_) ___  _ __   | | ___  ___| |_  / \
        / /  / _ \| '_ \| '_ \ / _ \/ __| __| |/ _ \| '_ \  | |/ _ \/ __| __|/  /
       / /__| (_) | | | | | | |  __/ (__| |_| | (_) | | | | | | (_) \__ \ |_/\_/ 
       \____/\___/|_| |_|_| |_|\___|\___|\__|_|\___/|_| |_| |_|\___/|___/\__\/  ";

            Console.WriteLine(exception);
            Console.ReadKey();
        }


        public static void InitializeKI(PacMan p, Ghost g)
        {
            KI = new KIComponent(g, p, 0);
        }

        public static KIComponent KI
        {
            get;
            set;
        }

        public static void CheckIfDead(Ghost ghost, PacMan p)
        {
            if (p.Left == ghost.Left && p.Top == ghost.Top)
            {
                DrawEnd();
            }

        }

        public static void MovePacMan(Map map, PacMan p)
        {
            switch (p.Direction)
            {
                case 1:
                    {
                        if (GUI.CheckIfMoveIsValid(map.Maparray, p.Left, p.Top - 1))
                        {
                            p.MoveUp();

                            if (GUI.CheckIfCoinonNewPosition(map.CoinArray, p.Left, p.Top - 1))
                            {
                                p.CoinsCollected = 1;
                            }
                            else
                            {
                                p.CoinsCollected = 0;
                            }
                        }
                        //Top-Left
                        else if (GUI.CheckIfMoveIsValid(map.Maparray, p.Left - 1, p.Top - 1))
                        {
                            p.MoveLeft();
                            DrawPacMan(p.Direction, p);
                            p.MoveUp();

                            if (GUI.CheckIfCoinonNewPosition(map.CoinArray, p.Left, p.Top - 1))
                            {
                                p.CoinsCollected = 1;
                            }
                            else
                            {
                                p.CoinsCollected = 0;
                            }
                        }
                        //Top-Right
                        else if (GUI.CheckIfMoveIsValid(map.Maparray, p.Left + 1, p.Top - 1))
                        {
                            p.MoveRight();
                            DrawPacMan(p.Direction, p);
                            p.MoveUp();

                            if (GUI.CheckIfCoinonNewPosition(map.CoinArray, p.Left, p.Top - 1))
                            {
                                p.CoinsCollected = 1;
                            }
                            else
                            {
                                p.CoinsCollected = 0;
                            }
                        }
                        break;
                    }
                case 2:
                    {

                        if (GUI.CheckIfMoveIsValid(map.Maparray, p.Left, p.Top + 1))
                        {
                            p.MoveDown();

                            if (GUI.CheckIfCoinonNewPosition(map.CoinArray, p.Left, p.Top + 1))
                            {
                                p.CoinsCollected = 1;
                            }
                            else
                            {
                                p.CoinsCollected = 0;
                            }
                        }
                        //Down-Left
                        else if (GUI.CheckIfMoveIsValid(map.Maparray, p.Left - 1, p.Top + 1))
                        {
                            p.MoveLeft();
                            DrawPacMan(p.Direction, p);
                            p.MoveDown();

                            if (GUI.CheckIfCoinonNewPosition(map.CoinArray, p.Left, p.Top + 1))
                            {
                                p.CoinsCollected = 1;
                            }
                            else
                            {
                                p.CoinsCollected = 0;
                            }
                        }
                        //Down-Right
                        else if (GUI.CheckIfMoveIsValid(map.Maparray, p.Left + 1, p.Top + 1))
                        {
                            p.MoveRight();
                            DrawPacMan(p.Direction, p);
                            p.MoveDown();

                            if (GUI.CheckIfCoinonNewPosition(map.CoinArray, p.Left, p.Top + 1))
                            {
                                p.CoinsCollected = 1;
                            }
                            else
                            {
                                p.CoinsCollected = 0;
                            }
                        }
                        break;
                    }
                case 3:
                    {

                        if (p.CheckPacManPositionforSideChange(map, 1))
                        {
                            p.FireOnLeavingMap(-1);
                            //p.SwitchPacManToOtherSide(map, 1);
                        }
                        else
                        {
                            if (GUI.CheckIfMoveIsValid(map.Maparray, p.Left - 1, p.Top))
                            {
                                p.MoveLeft();

                                if (GUI.CheckIfCoinonNewPosition(map.CoinArray, p.Left - 1, p.Top))
                                {
                                    p.CoinsCollected = 1;
                                }
                                else
                                {
                                    p.CoinsCollected = 0;
                                }
                            }
                            //Left-Up
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, p.Left - 1, p.Top - 1))
                            {
                                p.MoveUp();
                                DrawPacMan(p.Direction, p);
                                p.MoveLeft();

                                if (GUI.CheckIfCoinonNewPosition(map.CoinArray, p.Left - 1, p.Top))
                                {
                                    p.CoinsCollected = 1;
                                }
                                else
                                {
                                    p.CoinsCollected = 0;
                                }
                            }
                            //Left-Down
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, p.Left - 1, p.Top + 1))
                            {
                                p.MoveDown();
                                DrawPacMan(p.Direction, p);
                                p.MoveLeft();

                                if (GUI.CheckIfCoinonNewPosition(map.CoinArray, p.Left - 1, p.Top))
                                {
                                    p.CoinsCollected = 1;
                                }
                                else
                                {
                                    p.CoinsCollected = 0;
                                }
                            }
                        }
                        break;
                    }
                case 4:
                    {

                        if (p.CheckPacManPositionforSideChange(map, 2))
                        {
                            p.FireOnLeavingMap(1);
                            //p.SwitchPacManToOtherSide(map, 2);
                        }
                        else
                        {
                            if (GUI.CheckIfMoveIsValid(map.Maparray, p.Left + 1, p.Top))
                            {
                                p.MoveRight();

                                if (GUI.CheckIfCoinonNewPosition(map.CoinArray, p.Left + 1, p.Top))
                                {
                                    p.CoinsCollected = 1;
                                }
                                else
                                {
                                    p.CoinsCollected = 0;
                                }
                            }
                            //Right-Up
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, p.Left + 1, p.Top - 1))
                            {
                                p.MoveUp();
                                DrawPacMan(p.Direction, p);
                                p.MoveRight();

                                if (GUI.CheckIfCoinonNewPosition(map.CoinArray, p.Left + 1, p.Top))
                                {
                                    p.CoinsCollected = 1;
                                }
                                else
                                {
                                    p.CoinsCollected = 0;
                                }
                            }
                            //Right-Down
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, p.Left + 1, p.Top + 1))
                            {
                                p.MoveDown();
                                DrawPacMan(p.Direction, p);
                                p.MoveRight();

                                if (GUI.CheckIfCoinonNewPosition(map.CoinArray, p.Left + 1, p.Top))
                                {
                                    p.CoinsCollected = 1;
                                }
                                else
                                {
                                    p.CoinsCollected = 0;
                                }
                            }
                        }

                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            //if (p.MapIndex != 0)
            //{
                Thread.Sleep(100);
            //}

        }
        public static void ClearFigure(int left, int top)
        {

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(left, top + 1);
            Console.Write(' ');
            Console.SetCursorPosition(left - 1, top + 1);
            Console.Write(' ');
            Console.SetCursorPosition(left + 1, top + 1);
            Console.Write(' ');
            Console.SetCursorPosition(left - 1, top - 1);
            Console.Write(' ');
            Console.SetCursorPosition(left + 1, top - 1);
            Console.Write(' ');

            Console.SetCursorPosition(left, top - 1);
            Console.Write(' ');

            Console.SetCursorPosition(left, top);
            Console.Write(' ');


            Console.SetCursorPosition(left - 1, top);
            Console.Write(' ');
            Console.SetCursorPosition(left + 1, top);
            Console.Write(' ');
        }

        public static void DrawGhost(Map map, Ghost ghost)
        {
            lock (locker)
            {
                Console.SetCursorPosition(ghost.OldLeft - 1, ghost.OldTop - 1);
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Black;
                        try
                        {
                            if (map.CoinArray[ghost.OldTop - 1 + i, ghost.OldLeft - 1 + j] == true)
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.BackgroundColor = ConsoleColor.White;
                                Console.Write('o');
                            }
                            else
                            {
                                Console.Write(' ');
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            
                        }
                        
                    }
                    Console.SetCursorPosition(ghost.OldLeft - 1, ghost.OldTop + i);

                }

                //Console.SetCursorPosition(ghost.Left - 1, ghost.Top - 1);
                //for (int i = 0; i < 3; i++)
                //{
                //    for (int j = 0; j < 3; j++)
                //    {
                //        Console.BackgroundColor = ghost.Color;
                //        Console.ForegroundColor = ghost.Color;
                //        Console.Write(ghost.Name);
                //        Console.BackgroundColor = ConsoleColor.Black;
                //    }
                //    Console.SetCursorPosition(ghost.Left - 1, ghost.Top + i);

                //}

                Console.ForegroundColor = ghost.Color;
                Console.SetCursorPosition(ghost.Left, ghost.Top + 1);
                Console.Write('▼');
                Console.SetCursorPosition(ghost.Left - 1, ghost.Top + 1);
                Console.Write('▼');
                Console.SetCursorPosition(ghost.Left + 1, ghost.Top + 1);
                Console.Write('▼');
                Console.SetCursorPosition(ghost.Left - 1, ghost.Top - 1);
                Console.Write('/');
                Console.SetCursorPosition(ghost.Left + 1, ghost.Top - 1);
                Console.Write('\\');

                Console.BackgroundColor = ghost.Color;
                Console.SetCursorPosition(ghost.Left, ghost.Top - 1);
                Console.Write(ghost.Name);

                Console.SetCursorPosition(ghost.Left, ghost.Top);
                Console.Write(ghost.Name);

                Console.BackgroundColor = ConsoleColor.Black;

                Console.SetCursorPosition(ghost.Left - 1, ghost.Top);
                Console.Write('©');
                Console.SetCursorPosition(ghost.Left + 1, ghost.Top);
                Console.Write('©');

                Console.SetCursorPosition(82, 31);
                Console.Write(' ');
                Console.SetCursorPosition(82, 33);
                Console.Write(' ');
                Console.SetCursorPosition(82, 32);
                Console.Write(' ');
             
            }


        }
    }
}
