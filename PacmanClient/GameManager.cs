using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacManLib;
namespace PacmanClient
{
    public class GameManager
    {
        public int GameStatus { get; set; }

        public bool Draw { get; set; }

        public int MapChange { get; set; }

        public bool CheckIfPackageChanged(PacMan p, Ghost g)
        {
            bool changed = false;

            if ((p.OldLeft != p.Left || p.OldTop != p.Top) && (g.OldLeft != g.Left || g.OldTop != g.Top))
            {
                changed = true;
                return changed;
            }

            return changed;
        }

        public void CheckifEnd(int gamestatus)
        {
            if (gamestatus == -1)
            {
                GUI.DrawEnd();
                Console.ReadKey(true);
                Environment.Exit(1);
            }
            else if (gamestatus == 1)
            {
                GUI.DrawWin();
                Console.ReadKey(true);
                Environment.Exit(1);
            }
        }
    }
}
