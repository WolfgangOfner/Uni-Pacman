using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManLib
{
    [Serializable()]
    public class InitialNetPackage
    {
        public List<char[,]> Maps { get; set; }

        public int Index { get; set; }

        public InitialNetPackage(List<char[,]> maps, int index)
        {
            this.Maps = maps;
            this.Index = index;
        }
    }
}
