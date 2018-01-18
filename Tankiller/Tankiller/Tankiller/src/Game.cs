using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tankiller.src
{
    public class Game
    {
        private Entity[][] entities;

        private int width { get; }

        private int height { get; }

        private List<Missile> missiles = new List<Missile>();
        public Game(int width, int height)
        {
            this.width = width;
            this.height = height;

            for(int i = 0; i < width; ++i)
            {
                for(int j = 0; j < height; ++j)
                {
                    if (i == width - 1 || j * i == 0 || j == height - 1)
                    {
                        entities[i][j] = new Wall(i, j, false);
                    }
                    else
                    {
                        entities[i][j] = null;
                    }
                }
            }
            entities[1][1] = new Tank(1, 1);
            entities[2][2] = new Wall(2, 2, true);
        }

        public void shootMissile(int x, int y, Tank tank)
        {

        }
    }
}
