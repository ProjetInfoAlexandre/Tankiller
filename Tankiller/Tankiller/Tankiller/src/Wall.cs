using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tankiller.src
{
    public class Wall : Entity
    {
        bool breakable { get; set; }

        public Wall(int x, int y, bool breakable, Game myGame) : base(x, y, myGame)
        {
            this.breakable = breakable;
        }
    }
}
