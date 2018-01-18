using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tankiller
{
    public class Entity
    {
        private int x { get; set; }
        private int y { get; set; }

        public Entity(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

    }
}
