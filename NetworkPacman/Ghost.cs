using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkPacman
{
    [Serializable()]
    public class Ghost : GameObject, IMoveable
    {
        public Ghost(int left, int top)
        {
            this.Left = left;
            this.Top = top;
            this.Color = ConsoleColor.Red;
            this.OldLeft = left;
            this.OldTop = top;
            this.Name = 'A';
            this.OldDirection = 1;
        }

        public void MoveUp()
        {
            this.OldTop = this.Top;
            this.OldLeft = this.Left;
            this.Top -= 1;
            //this.FireOnChangedMyPosition();
        }

        public void MoveDown()
        {
            this.OldLeft = this.Left;
            this.OldTop = this.Top;
            this.Top += 1;
            //this.FireOnChangedMyPosition();
        }

        public void MoveLeft()
        {
            this.OldTop = this.Top;
            this.OldLeft = this.Left;
            this.Left -= 1;
           // this.FireOnChangedMyPosition();
        }

        public void MoveRight()
        {
            this.OldTop = this.Top;
            this.OldLeft = this.Left;
            this.Left += 1;
          //  this.FireOnChangedMyPosition();
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

        public bool CheckGhostPositionforSideChange(object[,] map, int direction)
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

        public void SwitchGhostToOtherSide(object[,] map, int direction)
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

        public void MoveGhost(object[,] map, int ClientId, int PacManId, KIComponent KI, int PacManLeft, int PacManTop, Ghost ghost)
        {
            int direction = this.OldDirection;

            switch (direction)
            {
                case 1:
                    {
                        if (CheckIfMoveIsValid(map, this.Left, this.Top - 1))
                        {
                            this.MoveUp();

                            if ((CheckIfMoveIsValid(map, this.Left - 1, this.Top) && CheckIfMoveIsValid(map, this.Left + 1, this.Top)) || CheckIfMoveIsValid(map, this.Left - 1, this.Top) || CheckIfMoveIsValid(map, this.Left + 1, this.Top))
                            {
                                if (ClientId == 0 || ClientId == PacManId)
                                {
                                    direction = KI.ClosestDirectionPacman(map, PacManLeft, PacManTop, ghost);
                                }
                                else
                                {
                                    direction = KI.GetRandomNumber(map, ghost);
                                }

                            }
                            else
                            {
                                direction = 1;
                            }
                        }
                        else
                        {
                            if ((CheckIfMoveIsValid(map, this.Left - 1, this.Top) && CheckIfMoveIsValid(map, this.Left + 1, this.Top)) || CheckIfMoveIsValid(map, this.Left - 1, this.Top) || CheckIfMoveIsValid(map, this.Left + 1, this.Top))
                            {
                                if (ClientId == 0 || ClientId == PacManId)
                                {
                                    direction = KI.ClosestDirectionPacman(map, PacManLeft, PacManTop, ghost);
                                }
                                else
                                {
                                    direction = KI.GetRandomNumber(map, ghost);
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

                        if (CheckIfMoveIsValid(map, this.Left, this.Top + 1))
                        {
                            this.MoveDown();

                            if ((CheckIfMoveIsValid(map, this.Left - 1, this.Top) && CheckIfMoveIsValid(map, this.Left + 1, this.Top)) || CheckIfMoveIsValid(map, this.Left - 1, this.Top) || CheckIfMoveIsValid(map, this.Left + 1, this.Top))
                            {
                                if (ClientId == 0 || ClientId == PacManId)
                                {
                                    direction = KI.ClosestDirectionPacman(map, PacManLeft, PacManTop, ghost);
                                }
                                else
                                {
                                    direction = KI.GetRandomNumber(map, ghost);
                                }
                            }
                            else
                            {
                                direction = 2;
                            }
                        }
                        else
                        {
                            if ((CheckIfMoveIsValid(map, this.Left - 1, this.Top) && CheckIfMoveIsValid(map, this.Left + 1, this.Top)) || CheckIfMoveIsValid(map, this.Left - 1, this.Top) || CheckIfMoveIsValid(map, this.Left + 1, this.Top))
                            {
                                if (ClientId == 0 || ClientId == PacManId)
                                {
                                    direction = KI.ClosestDirectionPacman(map, PacManLeft, PacManTop, ghost);
                                }
                                else
                                {
                                    direction = KI.GetRandomNumber(map, ghost);
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
                            if (CheckIfMoveIsValid(map, this.Left - 1, this.Top))
                            {
                                this.MoveLeft();

                                if ((CheckIfMoveIsValid(map, this.Left, this.Top - 1) && CheckIfMoveIsValid(map, this.Left, this.Top + 1)) || CheckIfMoveIsValid(map, this.Left, this.Top - 1) || CheckIfMoveIsValid(map, this.Left, this.Top + 1))
                                {
                                    if (ClientId == 0 || ClientId == PacManId)
                                    {
                                        direction = KI.ClosestDirectionPacman(map, PacManLeft, PacManTop, ghost);
                                    }
                                    else
                                    {
                                        direction = KI.GetRandomNumber(map, ghost);
                                    }
                                }
                                else
                                {
                                    direction = 3;
                                }
                            }
                            else
                            {
                                if ((CheckIfMoveIsValid(map, this.Left, this.Top - 1) && CheckIfMoveIsValid(map, this.Left, this.Top + 1)) || CheckIfMoveIsValid(map, this.Left, this.Top - 1) || CheckIfMoveIsValid(map, this.Left, this.Top + 1))
                                {
                                    if (ClientId == 0 || ClientId == PacManId)
                                    {
                                        direction = KI.ClosestDirectionPacman(map, PacManLeft, PacManTop, ghost);
                                    }
                                    else
                                    {
                                        direction = KI.GetRandomNumber(map, ghost);
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
                            if (CheckIfMoveIsValid(map, this.Left + 1, this.Top))
                            {
                                this.MoveRight();

                                if ((CheckIfMoveIsValid(map, this.Left, this.Top - 1) && CheckIfMoveIsValid(map, this.Left, this.Top + 1)) || CheckIfMoveIsValid(map, this.Left, this.Top - 1) || CheckIfMoveIsValid(map, this.Left, this.Top + 1))
                                {
                                    if (ClientId == 0 || ClientId == PacManId)
                                    {
                                        direction = KI.ClosestDirectionPacman(map, PacManLeft, PacManTop, ghost);
                                    }
                                    else
                                    {
                                        direction = KI.GetRandomNumber(map, ghost);
                                    }
                                }
                                else
                                {
                                    direction = 4;
                                }
                            }
                            else
                            {
                                if ((CheckIfMoveIsValid(map, this.Left, this.Top - 1) && CheckIfMoveIsValid(map, this.Left, this.Top + 1)) || CheckIfMoveIsValid(map, this.Left, this.Top - 1) || CheckIfMoveIsValid(map, this.Left, this.Top + 1))
                                {
                                    if (ClientId == 0 || ClientId == PacManId)
                                    {
                                        direction = KI.ClosestDirectionPacman(map, PacManLeft, PacManTop, ghost);
                                    }
                                    else
                                    {
                                        direction = KI.GetRandomNumber(map, ghost);
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

        //public void FireOnChangedMyPosition()
        //{
        //    GhostPositionEventArgs e = new GhostPositionEventArgs(this.Left, this.Top);

        //    if (this.OnChangedMyPosition != null)
        //    {
        //        this.OnChangedMyPosition(this, e);
        //    }
        //}

        //public void OnPackageReceived_CallBack(object sender, PackageRecievedEventArgs e)
        //{
        //}

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

    }
}
