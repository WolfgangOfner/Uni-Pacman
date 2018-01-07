using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanClient
{
    public class GhostPositionEventArgs : EventArgs
    {
        public GhostPositionEventArgs(int left, int top)
        {
            this.Left = left;
            this.Top = top;
        }

        public int Left { get; set; }

        public int Top { get; set; }
    }
}
