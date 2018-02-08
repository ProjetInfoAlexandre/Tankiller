using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tankiller.src
{
    public class Wall : Entity
    {
        public bool Breakable { get; set; }

        public Wall(int x, int y, bool breakable, Game myGame) : base(x, y, myGame)
        {
            this.Breakable = breakable;
        }

        public void Destroy()
        {
            if (!Breakable) return;

            myGame.GetWalls().Remove(this);

            //TODO
        }
    }
}
