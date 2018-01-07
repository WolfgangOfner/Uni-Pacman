using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PacManLib
{
    [Serializable]
    public class NetPackage
    {
        public int ClientID { get; set; }

        public int Pos_PAC_left { get; set; }

        public int Pos_PAC_top { get; set; }

        public int MapChange { get; set; }

        public bool DrawPacMan { get; set; }

        public int PAC_Coin { get; set; }

        public int Pos_GHOST_left { get; set; }

        public int Pos_GHOST_top { get; set; }

        public bool[,] CoinArray { get; set; }

        /// <summary>
        /// 0 = Game running
        /// 1 = Game won (PacMan won)
        /// -1 = Game lost (Ghosts won) 
        /// </summary>
        public int GameStatus { get; set; }

        public NetPackage(int id, int pac_left, int pac_top, int mapchange, bool draw, int ghost_left, int ghost_top, bool[,] coins, int pac_add_point)
        {
            this.ClientID = id;
            this.Pos_PAC_left = pac_left;
            this.Pos_PAC_top = pac_top;
            this.MapChange = mapchange;
            this.DrawPacMan = draw;
            this.Pos_GHOST_left = ghost_left;
            this.Pos_GHOST_top = ghost_top;
            this.CoinArray = coins;
            this.PAC_Coin = pac_add_point;
            this.GameStatus = 0;
        }
    }
}
