using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanClient
{
    public class KIComponent
    {
        public  KIComponent(Ghost ghost, PacMan pacman, int oldDirection)
        {
            pacman.OnChangedMyPosition += new EventHandler<PacManPositionEventArgs>(OnPacManPositionChanged_CallBack);
            ghost.OnChangedMyPosition += new EventHandler<GhostPositionEventArgs>(OnGhostPositionChanged_CallBack);
            this.GhostLeft = 41;
            this.GhostTop = 32;
            this.OldDirection = oldDirection;
        }

        public Map Map
        {
            get;
            set;
        }

        public int GhostLeft { get; set; }

        public int GhostTop { get; set; }

        public int PacManTop { get; set; }

        public int PacManLeft { get; set; }

        public int OldDirection { get; set; }

        public int NewDirectionNumber(List<int> ExcludeList)
        {
            var range = Enumerable.Range(1, 4).Where(i => !ExcludeList.Contains(i));

            var rand = new System.Random();
            int index = rand.Next(0, 4 - ExcludeList.Count);
            return range.ElementAt(index);
        }

        public int ClosestDirectionPacman(Map map)
        {
            int direction;
            int var_top;
            int var_left;
            List<int> ExcludeList = new List<int>();

            //ghost_top >= pacman_top -> UP && ghost_left >= pacman_left -> LEFT
            if (this.GhostTop >= this.PacManTop)
            {
                if (this.GhostLeft >= this.PacManLeft)
                {
                    var_top = this.GhostTop - this.PacManTop;
                    var_left = this.GhostLeft - this.PacManLeft;

                    //Prioritize UP and LEFT
                    if (var_left == 0)
                    {
                        if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop - 1))
                        {
                            direction = 1;
                        }
                    }
                    if (var_top >= var_left)
                    {
                        if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop - 1))
                        {
                            direction = 1;
                        }
                        else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft - 1, this.GhostTop))
                        {
                            direction = 3;
                        }
                        else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft + 1, this.GhostTop))
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
                            if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft - 1, this.GhostTop))
                            {
                                direction = 3;
                            }
                        }
                        if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft - 1, this.GhostTop))
                        {
                            direction = 3;
                        }
                        else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop - 1))
                        {
                            direction = 1;
                        }
                        else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop + 1))
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
                    var_top = this.GhostTop - this.PacManTop;
                    var_left = this.PacManLeft - this.GhostLeft;
                    //Prioritize UP and RIGHT
                    if (var_top >= var_left)
                    {
                        if (var_left == 0)
                        {
                            if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop - 1))
                            {
                                direction = 1;
                            }
                        }
                        if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop - 1))
                        {
                            direction = 1;
                        }
                        if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft + 1, this.GhostTop))
                        {
                            direction = 4;
                        }
                        else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft - 1, this.GhostTop))
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
                            if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft + 1, this.GhostTop))
                            {
                                direction = 4;
                            }
                        }
                        if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft + 1, this.GhostTop))
                        {
                            direction = 4;
                        }
                        else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop - 1))
                        {
                            direction = 1;
                        }
                        else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop + 1))
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
                var_top = this.PacManTop - this.GhostTop;
                var_left = this.GhostLeft - this.PacManLeft;

                if (this.GhostLeft > this.PacManLeft)
                {
                    if (var_left == 0)
                    {
                        if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop + 1))
                        {
                            direction = 2;
                        }
                    }
                    //Prioritize DOWN and LEFT
                    if (var_top < var_left)
                    {
                        if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop + 1))
                        {
                            direction = 2;
                        }
                        else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft + 1, this.GhostTop))
                        {
                            direction = 4;
                        }
                        else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft - 1, this.GhostTop))
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
                            if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft - 1, this.GhostTop))
                            {
                                direction = 3;
                            }
                        }
                        if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft - 1, this.GhostTop))
                        {
                            direction = 3;
                        }
                        else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop + 1))
                        {
                            direction = 2;
                        }
                        else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop - 1))
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
                    var_top = this.PacManTop - this.GhostTop;
                    var_left = this.PacManLeft - this.GhostLeft;
                    //Prioritize DOWN and RIGHT
                    if (var_left == 0)
                    {
                        if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop + 1))
                        {
                            direction = 2;
                        }
                    }
                    if (var_top > var_left)
                    {
                        if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop + 1))
                        {
                            direction = 2;
                        }
                        else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft + 1, this.GhostTop))
                        {
                            direction = 4;
                        }
                        else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft - 1, this.GhostTop))
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
                            if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft + 1, this.GhostTop))
                            {
                                direction = 4;
                            }
                        }
                        if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft + 1, this.GhostTop))
                        {
                            direction = 4;
                        }
                        else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop + 1))
                        {
                            direction = 2;
                        }
                        else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop - 1))
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
                        if (this.OldDirection == 2)
                        {
                            if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft - 1, this.GhostTop) && GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft + 1, this.GhostTop) && GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop - 1))
                            {
                                ExcludeList.Add(1);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft - 1, this.GhostTop) && GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop - 1))
                            {
                                ExcludeList.Add(1);
                                ExcludeList.Add(4);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft + 1, this.GhostTop) && GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop - 1))
                            {
                                ExcludeList.Add(1);
                                ExcludeList.Add(3);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft - 1, this.GhostTop) && GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft + 1, this.GhostTop))
                            {
                                ExcludeList.Add(2);
                                ExcludeList.Add(1);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft - 1, this.GhostTop))
                            {
                                direction = 3;
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft + 1, this.GhostTop))
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
                        if (this.OldDirection == 1)
                        {
                            if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft - 1, this.GhostTop) && GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft + 1, this.GhostTop) && GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop + 1))
                            {
                                ExcludeList.Add(2);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft - 1, this.GhostTop) && GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop + 1))
                            {
                                ExcludeList.Add(2);
                                ExcludeList.Add(4);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft + 1, this.GhostTop) && GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop + 1))
                            {
                                ExcludeList.Add(2);
                                ExcludeList.Add(3);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft - 1, this.GhostTop) && GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft + 1, this.GhostTop))
                            {
                                ExcludeList.Add(2);
                                ExcludeList.Add(1);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft - 1, this.GhostTop))
                            {
                                direction = 3;
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft + 1, this.GhostTop))
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
                        if (this.OldDirection == 4)
                        {
                            if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop + 1) && GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop - 1) && GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft + 1, this.GhostTop))
                            {
                                ExcludeList.Add(3);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft + 1, this.GhostTop) && GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop + 1))
                            {
                                ExcludeList.Add(3);
                                ExcludeList.Add(1);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft + 1, this.GhostTop) && GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop - 1))
                            {
                                ExcludeList.Add(2);
                                ExcludeList.Add(3);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop - 1) && GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop + 1))
                            {
                                ExcludeList.Add(3);
                                ExcludeList.Add(4);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop - 1))
                            {
                                direction = 1;
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop + 1))
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
                        if (this.OldDirection == 3)
                        {
                            if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop + 1) && GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop - 1) && GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft + 1, this.GhostTop))
                            {
                                ExcludeList.Add(4);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft - 1, this.GhostTop) && GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop + 1))
                            {
                                ExcludeList.Add(4);
                                ExcludeList.Add(1);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft - 1, this.GhostTop) && GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop - 1))
                            {
                                ExcludeList.Add(2);
                                ExcludeList.Add(4);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop - 1) && GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop + 1))
                            {
                                ExcludeList.Add(3);
                                ExcludeList.Add(4);
                                direction = this.NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop - 1))
                            {
                                direction = 1;
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, this.GhostLeft, this.GhostTop + 1))
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

            this.OldDirection = direction;
            return direction;
        }

        public int GetRandomNumber(Map map)
        {
            List<int> ExcludeList = new List<int>();
            int direction = NewDirectionNumber(ExcludeList);

            switch (direction)
            {
                case 1:
                    {
                        ExcludeList.Clear();
                        if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft, GhostTop - 1))
                        {

                            if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft - 1, GhostTop) && GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft + 1, GhostTop))
                            {
                                ExcludeList.Add(2);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft - 1, GhostTop))
                            {
                                ExcludeList.Add(2);
                                ExcludeList.Add(4);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft + 1, GhostTop))
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
                            if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft - 1, GhostTop) && GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft + 1, GhostTop))
                            {
                                ExcludeList.Add(1);
                                ExcludeList.Add(2);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft - 1, GhostTop))
                            {
                                ExcludeList.Add(1);
                                ExcludeList.Add(2);
                                ExcludeList.Add(4);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft + 1, GhostTop))
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
                        ExcludeList.Clear();
                        if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft, GhostTop + 1))
                        {

                            if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft - 1, GhostTop) && GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft + 1, GhostTop))
                            {
                                ExcludeList.Add(1);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft - 1, GhostTop))
                            {
                                ExcludeList.Add(1);
                                ExcludeList.Add(4);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft + 1, GhostTop))
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
                            if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft - 1, GhostTop) && GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft + 1, GhostTop))
                            {
                                ExcludeList.Add(1);
                                ExcludeList.Add(2);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft - 1, GhostTop))
                            {
                                ExcludeList.Add(1);
                                ExcludeList.Add(2);
                                ExcludeList.Add(4);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft + 1, GhostTop))
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
                        ExcludeList.Clear();


                        if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft - 1, GhostTop))
                        {
                            if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft, GhostTop - 1) && GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft, GhostTop + 1))
                            {
                                ExcludeList.Add(4);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft, GhostTop - 1))
                            {
                                ExcludeList.Add(2);
                                ExcludeList.Add(4);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft, GhostTop + 1))
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
                            if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft, GhostTop - 1) && GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft, GhostTop + 1))
                            {
                                ExcludeList.Add(4);
                                ExcludeList.Add(3);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft, GhostTop - 1))
                            {
                                ExcludeList.Add(4);
                                ExcludeList.Add(2);
                                ExcludeList.Add(3);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft, GhostTop + 1))
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
                        ExcludeList.Clear();

                        if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft + 1, GhostTop))
                        {


                            if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft, GhostTop - 1) && GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft, GhostTop + 1))
                            {
                                ExcludeList.Add(3);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft, GhostTop - 1))
                            {
                                ExcludeList.Add(2);
                                ExcludeList.Add(3);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft, GhostTop + 1))
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
                            if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft, GhostTop - 1) && GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft, GhostTop + 1))
                            {
                                ExcludeList.Add(3);
                                ExcludeList.Add(4);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft, GhostTop - 1))
                            {
                                ExcludeList.Add(3);
                                ExcludeList.Add(2);
                                ExcludeList.Add(4);
                                direction = NewDirectionNumber(ExcludeList);
                            }
                            else if (GUI.CheckIfMoveIsValid(map.Maparray, GhostLeft, GhostTop + 1))
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

        public void OnPacManPositionChanged_CallBack(object sender, PacManPositionEventArgs e)
        {
            this.PacManLeft = e.Left;
            this.PacManTop = e.Top;
        }

        public void OnGhostPositionChanged_CallBack(object sender, GhostPositionEventArgs e)
        {
            this.GhostLeft = e.Left;
            this.GhostTop = e.Top;
        }
    }
}
