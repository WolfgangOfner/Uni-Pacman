using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkPacman
{
    public class KIComponent
    {
        public KIComponent(Ghost ghost)
        {
            this.OldDirection = 0;
        }

        public object[,] Map
        {
            get;
            set;
        }

        public int OldDirection { get; set; }

        public int NewDirectionNumber(List<int> ExcludeList)
        {
            var range = Enumerable.Range(1, 4).Where(i => !ExcludeList.Contains(i));

            var rand = new System.Random();
            int index = rand.Next(0, 4 - ExcludeList.Count);
            return range.ElementAt(index);
        }

        public int ClosestDirectionPacman(object[,] map, int PacManLeft, int PacManTop, Ghost ghost)
        {
            int direction;
            int var_top;
            int var_left;
            List<int> ExcludeList = new List<int>();

            //ghost_top >= pacman_top -> UP && ghost_left >= pacman_left -> LEFT
            if (ghost.Top >= PacManTop)
            {
                if (ghost.Left >= PacManLeft)
                {
                    var_top = ghost.Top - PacManTop;
                    var_left = ghost.Left - PacManLeft;

                    //Prioritize UP and LEFT
                    if (var_left == 0)
                    {
                        if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1))
                        {
                            direction = 1;
                        }
                    }
                    if (var_top >= var_left)
                    {
                        if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1))
                        {
                            direction = 1;
                        }
                        else if (Ghost.CheckIfMoveIsValid(map, ghost.Left - 1, ghost.Top))
                        {
                            direction = 3;
                        }
                        else if (Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top))
                        {
                            direction = 4;
                        }
                        else
                        {
                            direction = 2;
                        }
                    }
                    //Prioritize LEFT and UP
                    else
                    {
                        if (var_top == 0)
                        {
                            if (Ghost.CheckIfMoveIsValid(map, ghost.Left - 1, ghost.Top))
                            {
                                direction = 3;
                            }
                        }
                        if (Ghost.CheckIfMoveIsValid(map, ghost.Left - 1, ghost.Top))
                        {
                            direction = 3;
                        }
                        else if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1))
                        {
                            direction = 1;
                        }
                        else if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1))
                        {
                            direction = 2;
                        }
                        else
                        {
                            direction = 4;
                        }
                    }
                }
                else
                {
                    var_top = ghost.Top - PacManTop;
                    var_left = PacManLeft - ghost.Left;
                    //Prioritize UP and RIGHT
                    if (var_top >= var_left)
                    {
                        if (var_left == 0)
                        {
                            if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1))
                            {
                                direction = 1;
                            }
                        }
                        if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1))
                        {
                            direction = 1;
                        }
                        if (Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top))
                        {
                            direction = 4;
                        }
                        else if (Ghost.CheckIfMoveIsValid(map, ghost.Left - 1, ghost.Top))
                        {
                            direction = 3;
                        }
                        else
                        {
                            direction = 2;
                        }
                    }
                    //Prioritize RIGHT and UP
                    else
                    {
                        if (var_top == 0)
                        {
                            if (Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top))
                            {
                                direction = 4;
                            }
                        }
                        if (Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top))
                        {
                            direction = 4;
                        }
                        else if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1))
                        {
                            direction = 1;
                        }
                        else if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1))
                        {
                            direction = 2;
                        }
                        else
                        {
                            direction = 3;
                        }
                    }
                }
            }
            else
            {
                var_top = PacManTop - ghost.Top;
                var_left = ghost.Left - PacManLeft;

                if (ghost.Left > PacManLeft)
                {
                    if (var_left == 0)
                    {
                        if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1))
                        {
                            direction = 2;
                        }
                    }
                    //Prioritize DOWN and LEFT
                    if (var_top < var_left)
                    {
                        if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1))
                        {
                            direction = 2;
                        }
                        else if (Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top))
                        {
                            direction = 4;
                        }
                        else if (Ghost.CheckIfMoveIsValid(map, ghost.Left - 1, ghost.Top))
                        {
                            direction = 3;
                        }
                        else
                        {
                            direction = 1;
                        }

                    }
                    //Prioritize LEFT and DOWN
                    else
                    {
                        if (var_top == 0)
                        {
                            if (Ghost.CheckIfMoveIsValid(map, ghost.Left - 1, ghost.Top))
                            {
                                direction = 3;
                            }
                        }
                        if (Ghost.CheckIfMoveIsValid(map, ghost.Left - 1, ghost.Top))
                        {
                            direction = 3;
                        }
                        else if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1))
                        {
                            direction = 2;
                        }
                        else if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1))
                        {
                            direction = 1;
                        }
                        else
                        {
                            direction = 4;
                        }
                    }
                }
                else
                {
                    var_top = PacManTop - ghost.Top;
                    var_left = PacManLeft - ghost.Left;
                    //Prioritize DOWN and RIGHT
                    if (var_left == 0)
                    {
                        if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1))
                        {
                            direction = 2;
                        }
                    }
                    if (var_top > var_left)
                    {
                        if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1))
                        {
                            direction = 2;
                        }
                        else if (Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top))
                        {
                            direction = 4;
                        }
                        else if (Ghost.CheckIfMoveIsValid(map, ghost.Left - 1, ghost.Top))
                        {
                            direction = 3;
                        }
                        else
                        {
                            direction = 1;
                        }
                    }

                   //Prioritize RIGHT and DOWN
                    else
                    {
                        if (var_top == 0)
                        {
                            if (Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top))
                            {
                                direction = 4;
                            }
                        }
                        if (Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top))
                        {
                            direction = 4;
                        }
                        else if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1))
                        {
                            direction = 2;
                        }
                        else if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1))
                        {
                            direction = 1;
                        }
                        else
                        {
                            direction = 3;
                        }
                    }
                }

            }


            switch (direction)
            {
                case 1:
                    {
                        if (ghost.OldDirection == 2)
                        {
                            if (Ghost.CheckIfMoveIsValid(map, ghost.Left - 1, ghost.Top) && Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top) && Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1))
                            {
                                ExcludeList.Add(1);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left - 1, ghost.Top) && Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1))
                            {
                                ExcludeList.Add(1);
                                ExcludeList.Add(4);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top) && Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1))
                            {
                                ExcludeList.Add(1);
                                ExcludeList.Add(3);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left - 1, ghost.Top) && Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top))
                            {
                                ExcludeList.Add(2);
                                ExcludeList.Add(1);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left - 1, ghost.Top))
                            {
                                direction = 3;
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top))
                            {
                                direction = 4;
                            }
                        }
                        else
                        {
                            direction = 1;
                        }
                        break;
                    }
                case 2:
                    {
                        if (ghost.OldDirection == 1)
                        {
                            if (Ghost.CheckIfMoveIsValid(map, ghost.Left - 1, ghost.Top) && Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top) && Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1))
                            {
                                ExcludeList.Add(2);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left - 1, ghost.Top) && Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1))
                            {
                                ExcludeList.Add(2);
                                ExcludeList.Add(4);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top) && Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1))
                            {
                                ExcludeList.Add(2);
                                ExcludeList.Add(3);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left - 1, ghost.Top) && Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top))
                            {
                                ExcludeList.Add(2);
                                ExcludeList.Add(1);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left - 1, ghost.Top))
                            {
                                direction = 3;
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top))
                            {
                                direction = 4;
                            }
                        }
                        else
                        {
                            direction = 2;
                        }
                        break;
                    }
                case 3:
                    {
                        if (ghost.OldDirection == 4)
                        {
                            if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1) && Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1) && Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top))
                            {
                                ExcludeList.Add(3);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top) && Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1))
                            {
                                ExcludeList.Add(3);
                                ExcludeList.Add(1);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top) && Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1))
                            {
                                ExcludeList.Add(2);
                                ExcludeList.Add(3);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1) && Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1))
                            {
                                ExcludeList.Add(3);
                                ExcludeList.Add(4);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1))
                            {
                                direction = 1;
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1))
                            {
                                direction = 2;
                            }
                        }
                        else
                        {
                            direction = 3;
                        }
                        break;
                    }
                case 4:
                    {
                        if (ghost.OldDirection == 3)
                        {
                            if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1) && Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1) && Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top))
                            {
                                ExcludeList.Add(4);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left - 1, ghost.Top) && Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1))
                            {
                                ExcludeList.Add(4);
                                ExcludeList.Add(1);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left - 1, ghost.Top) && Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1))
                            {
                                ExcludeList.Add(2);
                                ExcludeList.Add(4);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1) && Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1))
                            {
                                ExcludeList.Add(3);
                                ExcludeList.Add(4);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1))
                            {
                                direction = 1;
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1))
                            {
                                direction = 2;
                            }
                        }
                        else
                        {
                            direction = 4;
                        }
                        break;
                    }
            }

            
            return direction;
        }

        public int GetRandomNumber(object[,] map, Ghost ghost)
        {
            List<int> ExcludeList = new List<int>();
            int direction = NewDirectionNumber(ExcludeList);

            switch (direction)
            {
                case 1:
                    {
                        //ExcludeList.Clear();
                        if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1))
                        {

                            if (Ghost.CheckIfMoveIsValid(map, ghost.Left - 1, ghost.Top) && Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top))
                            {
                                ExcludeList.Add(2);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left - 1, ghost.Top))
                            {
                                ExcludeList.Add(2);
                                ExcludeList.Add(4);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top))
                            {
                                ExcludeList.Add(2);
                                ExcludeList.Add(3);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else
                            {
                                direction = 1;
                            }


                        }
                        else
                        {
                            if (Ghost.CheckIfMoveIsValid(map, ghost.Left - 1, ghost.Top) && Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top))
                            {
                                ExcludeList.Add(1);
                                ExcludeList.Add(2);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left - 1, ghost.Top))
                            {
                                ExcludeList.Add(1);
                                ExcludeList.Add(2);
                                ExcludeList.Add(4);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top))
                            {
                                ExcludeList.Add(1);
                                ExcludeList.Add(2);
                                ExcludeList.Add(3);
                                direction = NewDirectionNumber(ExcludeList);
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
                        //ExcludeList.Clear();
                        if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1))
                        {

                            if (Ghost.CheckIfMoveIsValid(map, ghost.Left - 1, ghost.Top) && Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top))
                            {
                                ExcludeList.Add(1);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left - 1, ghost.Top))
                            {
                                ExcludeList.Add(1);
                                ExcludeList.Add(4);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top))
                            {
                                ExcludeList.Add(1);
                                ExcludeList.Add(3);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else
                            {
                                direction = 2;
                            }
                        }
                        else
                        {
                            if (Ghost.CheckIfMoveIsValid(map, ghost.Left - 1, ghost.Top) && Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top))
                            {
                                ExcludeList.Add(1);
                                ExcludeList.Add(2);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left - 1, ghost.Top))
                            {
                                ExcludeList.Add(1);
                                ExcludeList.Add(2);
                                ExcludeList.Add(4);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top))
                            {
                                ExcludeList.Add(1);
                                ExcludeList.Add(2);
                                ExcludeList.Add(3);
                                direction = NewDirectionNumber(ExcludeList);
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
                        //ExcludeList.Clear();


                        if (Ghost.CheckIfMoveIsValid(map, ghost.Left - 1, ghost.Top))
                        {
                            if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1) && Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1))
                            {
                                ExcludeList.Add(4);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1))
                            {
                                ExcludeList.Add(2);
                                ExcludeList.Add(4);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1))
                            {
                                ExcludeList.Add(1);
                                ExcludeList.Add(4);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else
                            {
                                direction = 3;
                            }
                        }
                        else
                        {
                            if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1) && Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1))
                            {
                                ExcludeList.Add(4);
                                ExcludeList.Add(3);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1))
                            {
                                ExcludeList.Add(4);
                                ExcludeList.Add(2);
                                ExcludeList.Add(3);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1))
                            {
                                ExcludeList.Add(4);
                                ExcludeList.Add(1);
                                ExcludeList.Add(3);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else
                            {
                                direction = 4;
                            }
                        }
                    }
                    break;

                case 4:
                    {
                        //ExcludeList.Clear();

                        if (Ghost.CheckIfMoveIsValid(map, ghost.Left + 1, ghost.Top))
                        {


                            if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1) && Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1))
                            {
                                ExcludeList.Add(3);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1))
                            {
                                ExcludeList.Add(2);
                                ExcludeList.Add(3);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1))
                            {
                                ExcludeList.Add(1);
                                ExcludeList.Add(3);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else
                            {
                                direction = 4;
                            }
                        }
                        else
                        {
                            if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1) && Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1))
                            {
                                ExcludeList.Add(3);
                                ExcludeList.Add(4);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top - 1))
                            {
                                ExcludeList.Add(3);
                                ExcludeList.Add(2);
                                ExcludeList.Add(4);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (Ghost.CheckIfMoveIsValid(map, ghost.Left, ghost.Top + 1))
                            {
                                ExcludeList.Add(3);
                                ExcludeList.Add(4);
                                ExcludeList.Add(1);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else
                            {
                                direction = 3;
                            }
                        }
                        break;
                    }

                default:
                    break;

            }
            return direction;

        }

        //public void OnPacManPositionChanged_CallBack(object sender, PacManPositionEventArgs e)
        //{
        //    PacManLeft = e.Left;
        //    PacManTop = e.Top;
        //}

        //public void OnGhostPositionChanged_CallBack(object sender, GhostPositionEventArgs e)
        //{
        //    ghost.Left = e.Left;
        //    ghost.Top = e.Top;
        //}
    }
}
