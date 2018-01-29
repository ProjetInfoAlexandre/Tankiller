using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tankiller.src;

namespace Tankiller
{
    public class Tank : Entity
    {
        public Direction Direction { get; set; }

        public Tank(int x , int y) : base(x, y)
        {
            Direction = Direction.TOP;
        }

        public void move()
        {

        }
        public void shoot()
        {

        }
    }
}
