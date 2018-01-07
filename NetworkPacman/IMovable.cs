using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkPacman
{
        public interface IMoveable
        {
            void MoveUp();

            void MoveDown();

            void MoveLeft();

            void MoveRight();

            int OldLeft { get; set; }

            int OldTop { get; set; }
        }
}
