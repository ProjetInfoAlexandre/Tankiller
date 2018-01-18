using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tankiller
{
    public class Wall : Entity
    {
        bool breakable { get; set; }

        public Wall(int x, int y, bool breakable)
        {
            base(x, y);
            this.breakable = breakable;
        }
    }
}
