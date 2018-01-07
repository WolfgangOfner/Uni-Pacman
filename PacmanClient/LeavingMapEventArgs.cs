using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanClient
{
    public class LeavingMapEventArgs : EventArgs
    {
        public LeavingMapEventArgs(int direction)
        {
            this.Direction = direction;
        }

        public int Direction { get; set; }
    }
}
