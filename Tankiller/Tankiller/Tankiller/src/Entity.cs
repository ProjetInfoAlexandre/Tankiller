using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tankiller.src
{
    public class Entity
    {
        protected int x { get; set; }
        protected int y { get; set; }

        protected Game myGame;

        public Entity(int x, int y, Game myGame)
        {
            this.x = x;
            this.y = y;
            this.myGame = myGame;
        }

    }
}
