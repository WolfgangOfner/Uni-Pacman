using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkPacman
{
    public class GameInfo
    {
        public GameInfo(int maxCoins, List<bool[,]> coinList)
        {
            CurrentCoins = 0;
            MaxCoins = maxCoins;
            GameStatus = 0;
            PacManMapIndex = 0;
            CoinList = coinList;
            PacY = 0;
            PacX = 0;
        }

        public int GameStatus { get; set; }

        public int CurrentCoins { get; set; }

        public int MaxCoins { get; set; }

        public int PacManMapIndex { get; set; }

        public List<bool[,]> CoinList { get; set; }

        public int PacX { get; set; }

        public int PacY { get; set; }

        public int GhostX { get; set; }

        public int GhostY { get; set; }

        //public bool NetworkLock { get; set; }

    }
}
