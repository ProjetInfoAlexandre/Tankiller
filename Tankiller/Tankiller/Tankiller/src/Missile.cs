using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tankiller.src
{
    public class Missile : Entity
    {
        private Tank source;

        private Direction direction;
        public Missile(int x, int y, Direction direction, Game myGame, Tank source) : base(x, y, myGame)
        {
            this.source = source;
            this.direction = direction;
        }
    }
}
