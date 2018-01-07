using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanClient
{
    [Serializable()]
    public class Map
    {
        public Map()
        {
            //this.CreateMap(GUI.ReadMap());
            //this.CreateCoinArray();
        }

        public bool[,] CoinArray{ get; set; }

        public object[,] Maparray { get; set; }

        public List<char[,]> MapList
        {
            get;
            set;
        }

        public void CreateCoinArray()
        {
            this.CoinArray = new bool[this.Maparray.GetLength(0), this.Maparray.GetLength(1)];

            for (int i = 0; i < this.Maparray.GetLength(0); i++)
            {
                for (int j = 0; j < this.Maparray.GetLength(1); j++)
                {
                    if (this.Maparray[i,j] is Coin)
                    {
                        this.CoinArray[i, j] = true;   
                    }
                    else
                    {
                        this.CoinArray[i, j] = false;
                    }
                }
            }
        }

        public void CreateMap(char[,] readmap)
        {
            this.Maparray = new object[readmap.GetLength(0), readmap.GetLength(1)];
            GUI.StartUp(readmap.GetLength(0) + 2, readmap.GetLength(1) + 2); 

            for (int i = 0; i < readmap.GetLength(0); i++)
            {
                for (int j = 0; j < readmap.GetLength(1); j++)
                {
                    switch (readmap[i, j])
                    {
                        case '#':
                            {
                                Wall wall = new Wall();
                                wall.Left = j;
                                wall.Top = i;
                                wall.Name = '#';
                                wall.Color = ConsoleColor.Blue;
                                this.Maparray[i, j] = wall;
                                break;
                            }
                        case 'o':
                            {
                                Coin coin = new Coin();
                                coin.Left = j;
                                coin.Top = i;
                                coin.Name = '°';
                                coin.Color = ConsoleColor.White;
                                this.Maparray[i, j] = coin;


                                break;
                            }
                        case ' ':
                            {
                                Path path = new Path();
                                path.Left = j;
                                path.Top = i;
                                path.Name = ' ';
                                path.Color = ConsoleColor.Black;
                                this.Maparray[i, j] = path;
                                break;
                            }
                        default:
                            {
                                break;
                            }

                    }
                }
            }

             
        }

        public int CollumnIndex
        {
            get;
            set;
        }
    }
}
