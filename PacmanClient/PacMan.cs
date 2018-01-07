using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PacmanClient
{
    [Serializable()]
    public class PacMan : GameObject, IMoveable
    {
        public PacMan(Map map)
        {
            this.Left = 5;
            this.Top = 5;
            this.Name = 'C';
            this.OldLeft = 5;
            this.OldTop = 5;
            this.MapIndex = 0;
            this.Color = ConsoleColor.Yellow;

            //Program.OnPackageReceived += new EventHandler<PackageRecievedEventArgs>(OnPackageReceived_CallBack);
        }

        public event EventHandler<PacManPositionEventArgs> OnChangedMyPosition;

        public event EventHandler<LeavingMapEventArgs> OnLeavingMap;

        public void MoveUp()
        {
            this.OldTop = this.Top;
            this.OldLeft = this.Left;
            this.Top -= 1;
            this.FireOnChangedMyPosition();
        }

        public void MoveDown()
        {
            this.OldLeft = this.Left;
            this.OldTop = this.Top;
            this.Top += 1;
            this.FireOnChangedMyPosition();
        }

        public void MoveLeft()
        {
            this.OldTop = this.Top;
            this.OldLeft = this.Left;
            this.Left -= 1;
            this.FireOnChangedMyPosition();
        }

        public void MoveRight()
        {
            this.OldTop = this.Top;
            this.OldLeft = this.Left;
            this.Left += 1;
            this.FireOnChangedMyPosition();
        }

        public int MapIndex { get; set; }

        public int OldLeft
        {
            get;
            set;
        }

        public int OldTop
        {
            get;
            set;
        }

        public Thread MoveThread
        {
            get;
            set;
        }

        public int Direction { get; set; }

        public int CoinsCollected { get; set; }

        public bool CheckPacManPositionforSideChange(Map map, int direction)
        {
            if (direction == 1)
            {
                if (this.Left == 1 && this.Top == 32)
                {
                    return true;
                }
            }
            else if (direction == 2)
            {
                if (this.Left == 80 && this.Top == 32)
                {
                    return true;
                }
            }
            return false;
        }

        public void SwitchPacManToOtherSide(Map map, int direction)
        {
            if (direction == 1)
            {
                if (this.Left == 80 && this.Top == 32)
                {
                    this.Left = 2;
                    this.Top = 32;
                }
            }
            else if (direction == -1)
            {
                if (this.Left == 1 && this.Top == 32)
                {
                    this.Left = 81;
                    this.Top = 32;
                }
            }

            Console.SetCursorPosition(0, 31);
            Console.Write(' ');
            Console.SetCursorPosition(0, 33);
            Console.Write(' ');
            Console.SetCursorPosition(81, 31);
            Console.Write(' ');
            Console.SetCursorPosition(81, 33);
            Console.Write(' ');
        }

        public void Worker(object data)
        {
            Map map = data as Map;
            this.Direction = 4;

            while (true)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        {
                            if (GUI.CheckIfMoveIsValid(map.Maparray, this.Left, this.Top - 1) || GUI.CheckIfMoveIsValid(map.Maparray, this.Left + 1, this.Top - 1) || GUI.CheckIfMoveIsValid(map.Maparray, this.Left - 1, this.Top - 1))
                            {
                                this.Direction = 1;
                            }

                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            if (GUI.CheckIfMoveIsValid(map.Maparray, this.Left, this.Top + 1) || GUI.CheckIfMoveIsValid(map.Maparray, this.Left + 1, this.Top + 1) || GUI.CheckIfMoveIsValid(map.Maparray, this.Left - 1, this.Top + 1))
                            {
                                this.Direction = 2;
                            }
                            break;
                        }
                    case ConsoleKey.LeftArrow:
                        {
                            if (GUI.CheckIfMoveIsValid(map.Maparray, this.Left - 1, this.Top) || GUI.CheckIfMoveIsValid(map.Maparray, this.Left - 1, this.Top - 1) || GUI.CheckIfMoveIsValid(map.Maparray, this.Left - 1, this.Top + 1))
                            {
                                this.Direction = 3;
                            }
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            if (GUI.CheckIfMoveIsValid(map.Maparray, this.Left + 1, this.Top) || GUI.CheckIfMoveIsValid(map.Maparray, this.Left + 1, this.Top - 1) || GUI.CheckIfMoveIsValid(map.Maparray, this.Left + 1, this.Top + 1))
                            {
                                this.Direction = 4;
                            }
                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            Environment.Exit(1);
                            break;
                        }
                    default:
                        break;
                }
                //Thread.Sleep(100);
            }
        }

        public Thread PacManThread
        {
            get;
            set;
        }

        public void FireOnChangedMyPosition()
        {
            PacManPositionEventArgs peventargs = new PacManPositionEventArgs(this.Left, this.Top);

            if (this.OnChangedMyPosition != null)
            {
                this.OnChangedMyPosition(this, peventargs);
            }
        }

        public void FireOnLeavingMap(int direction)
        {
            LeavingMapEventArgs e = new LeavingMapEventArgs(direction);

            if (this.OnLeavingMap != null)
            {
                this.OnLeavingMap(this, e);
            }
        }

        
    }
}
