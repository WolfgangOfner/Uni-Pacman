using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PacmanClient
{
    [Serializable()]
    public class Ghost : GameObject, IMoveable
    {

        public Ghost(Map map, int left, int top)
        {
            this.Left = left;
            this.Top = top;
            this.Color = ConsoleColor.Red;
            this.OldLeft = left;
            this.OldTop = top;
            this.Name = 'A';
            this.OldDirection = 1;
            Program.OnPackageReceived += new EventHandler<PackageRecievedEventArgs>(OnPackageReceived_CallBack);
        }

        public event EventHandler<GhostPositionEventArgs> OnChangedMyPosition;

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

        public int OldDirection { get; set; }

        public Thread GhostThread { get; set; }

        public bool CheckGhostPositionforSideChange(Map map, int direction)
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

        public void SwitchGhostToOtherSide(Map map, int direction)
        {
            if (direction == 1)
            {
                if (this.Left == 1 && this.Top == 32)
                {
                    this.Left = 81;
                    this.Top = 32;
                    this.OldLeft = 1;
                    this.OldTop = 32;
                }
            }
            else if (direction == 2)
            {
                if (this.Left == 80 && this.Top == 32)
                {
                    this.Left = 1;
                    this.Top = 32;
                    this.OldLeft = 80;
                    this.OldTop = 32;
                }
            }
        }

        public void MoveGhost(Map map)
        {
            int direction = this.OldDirection;

            switch (direction)
            {
                case 1:
                    {
                        if (GUI.CheckIfMoveIsValid(map.Maparray, this.Left, this.Top - 1))
                        {
                            this.MoveUp();

                            if ((GUI.CheckIfMoveIsValid(map.Maparray, this.Left - 1, this.Top) && GUI.CheckIfMoveIsValid(map.Maparray, this.Left + 1, this.Top)) || GUI.CheckIfMoveIsValid(map.Maparray, this.Left - 1, this.Top) || GUI.CheckIfMoveIsValid(map.Maparray, this.Left + 1, this.Top))
                            {
                                if (Program.ClientId == 0)
                                {
                                    direction = GUI.KI.ClosestDirectionPacman(map);
                                }
                                else
                                {
                                    direction = GUI.KI.GetRandomNumber(map);
                                }

                            }
                            else
                            {
                                direction = 1;
                            }
                        }
                        else
                        {
                            if ((GUI.CheckIfMoveIsValid(map.Maparray, this.Left - 1, this.Top) && GUI.CheckIfMoveIsValid(map.Maparray, this.Left + 1, this.Top)) || GUI.CheckIfMoveIsValid(map.Maparray, this.Left - 1, this.Top) || GUI.CheckIfMoveIsValid(map.Maparray, this.Left + 1, this.Top))
                            {
                                if (Program.ClientId == 0)
                                {
                                    direction = GUI.KI.ClosestDirectionPacman(map);
                                }
                                else
                                {
                                    direction = GUI.KI.GetRandomNumber(map);
                                }
                            }
                            else
                            {
                                direction = 2;
                            }
                        }
                        break;
                    }
                case 2:
                    {

                        if (GUI.CheckIfMoveIsValid(map.Maparray, this.Left, this.Top + 1))
                        {
                            this.MoveDown();

                            if ((GUI.CheckIfMoveIsValid(map.Maparray, this.Left - 1, this.Top) && GUI.CheckIfMoveIsValid(map.Maparray, this.Left + 1, this.Top)) || GUI.CheckIfMoveIsValid(map.Maparray, this.Left - 1, this.Top) || GUI.CheckIfMoveIsValid(map.Maparray, this.Left + 1, this.Top))
                            {
                                if (Program.ClientId == 0)
                                {
                                    direction = GUI.KI.ClosestDirectionPacman(map);
                                }
                                else
                                {
                                    direction = GUI.KI.GetRandomNumber(map);
                                }
                            }
                            else
                            {
                                direction = 2;
                            }
                        }
                        else
                        {
                            if ((GUI.CheckIfMoveIsValid(map.Maparray, this.Left - 1, this.Top) && GUI.CheckIfMoveIsValid(map.Maparray, this.Left + 1, this.Top)) || GUI.CheckIfMoveIsValid(map.Maparray, this.Left - 1, this.Top) || GUI.CheckIfMoveIsValid(map.Maparray, this.Left + 1, this.Top))
                            {
                                if (Program.ClientId == 0)
                                {
                                    direction = GUI.KI.ClosestDirectionPacman(map);
                                }
                                else
                                {
                                    direction = GUI.KI.GetRandomNumber(map);
                                }
                            }
                            else
                            {
                                direction = 1;
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        if (this.CheckGhostPositionforSideChange(map, 1))
                        {
                            this.SwitchGhostToOtherSide(map, 1);
                        }
                        else
                        {
                            if (GUI.CheckIfMoveIsValid(map.Maparray, this.Left - 1, this.Top))
                            {
                                this.MoveLeft();

                                if ((GUI.CheckIfMoveIsValid(map.Maparray, this.Left, this.Top - 1) && GUI.CheckIfMoveIsValid(map.Maparray, this.Left, this.Top + 1)) || GUI.CheckIfMoveIsValid(map.Maparray, this.Left, this.Top - 1) || GUI.CheckIfMoveIsValid(map.Maparray, this.Left, this.Top + 1))
                                {
                                    if (Program.ClientId == 0)
                                    {
                                        direction = GUI.KI.ClosestDirectionPacman(map);
                                    }
                                    else
                                    {
                                        direction = GUI.KI.GetRandomNumber(map);
                                    }
                                }
                                else
                                {
                                    direction = 3;
                                }
                            }
                            else
                            {
                                if ((GUI.CheckIfMoveIsValid(map.Maparray, this.Left, this.Top - 1) && GUI.CheckIfMoveIsValid(map.Maparray, this.Left, this.Top + 1)) || GUI.CheckIfMoveIsValid(map.Maparray, this.Left, this.Top - 1) || GUI.CheckIfMoveIsValid(map.Maparray, this.Left, this.Top + 1))
                                {
                                    if (Program.ClientId == 0)
                                    {
                                        direction = GUI.KI.ClosestDirectionPacman(map);
                                    }
                                    else
                                    {
                                        direction = GUI.KI.GetRandomNumber(map);
                                    }
                                }
                                else
                                {
                                    direction = 4;
                                }
                            }
                        }
                        break;
                    }
                case 4:
                    {
                        if (this.CheckGhostPositionforSideChange(map, 2))
                        {
                            this.SwitchGhostToOtherSide(map, 2);
                        }
                        else
                        {
                            if (GUI.CheckIfMoveIsValid(map.Maparray, this.Left + 1, this.Top))
                            {
                                this.MoveRight();

                                if ((GUI.CheckIfMoveIsValid(map.Maparray, this.Left, this.Top - 1) && GUI.CheckIfMoveIsValid(map.Maparray, this.Left, this.Top + 1)) || GUI.CheckIfMoveIsValid(map.Maparray, this.Left, this.Top - 1) || GUI.CheckIfMoveIsValid(map.Maparray, this.Left, this.Top + 1))
                                {
                                    if (Program.ClientId == 0)
                                    {
                                        direction = GUI.KI.ClosestDirectionPacman(map);
                                    }
                                    else
                                    {
                                        direction = GUI.KI.GetRandomNumber(map);
                                    }
                                }
                                else
                                {
                                    direction = 4;
                                }
                            }
                            else
                            {
                                if ((GUI.CheckIfMoveIsValid(map.Maparray, this.Left, this.Top - 1) && GUI.CheckIfMoveIsValid(map.Maparray, this.Left, this.Top + 1)) || GUI.CheckIfMoveIsValid(map.Maparray, this.Left, this.Top - 1) || GUI.CheckIfMoveIsValid(map.Maparray, this.Left, this.Top + 1))
                                {
                                    if (Program.ClientId == 0)
                                    {
                                        direction = GUI.KI.ClosestDirectionPacman(map);
                                    }
                                    else
                                    {
                                        direction = GUI.KI.GetRandomNumber(map);
                                    }
                                }
                                else
                                {
                                    direction = 3;
                                }
                            }
                        }
                        break;
                    }
                default:
                    break;

            }

            this.OldDirection = direction;
        }

        public void FireOnChangedMyPosition()
        {
            GhostPositionEventArgs e = new GhostPositionEventArgs(this.Left, this.Top);

            if (this.OnChangedMyPosition != null)
            {
                this.OnChangedMyPosition(this, e);
            }
        }

        public void OnPackageReceived_CallBack(object sender, PackageRecievedEventArgs e)
        {
        }

    }
}
