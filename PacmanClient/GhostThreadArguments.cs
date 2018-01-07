using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanClient
{
    public class GhostThreadArguments
    {
        public GhostThreadArguments(Map map, Ghost ghost)
        {
            this.Map = map;
            this.Ghost = ghost;
        }

        public Map Map
        {
            get;
            set;
        }

        public Ghost Ghost
        {
            get;
            set;
        }
    }
}
