using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tankiller.src
{
    public class Entity
    {
        public int X { get; set; }
        public int Y { get; set; }

        protected Game myGame;

        public Entity(int x, int y, Game myGame)
        {
            this.X = x;
            this.Y = y;
            this.myGame = myGame;
        }

    }
}
