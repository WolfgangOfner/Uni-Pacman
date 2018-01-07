using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanClient
{
    [Serializable()]
    public class GameObject
    {
        public char Name { get; set; }

        public int Left { get; set; }

        public int Top { get; set; }

        public ConsoleColor Color { get; set; }

        public override string ToString()
        {
            return this.Name.ToString();
        }

        
    }
}
