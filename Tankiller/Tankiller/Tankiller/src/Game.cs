using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tankiller.src
{
    public class Game
    {
        private Entity[][] entities;
        public Tank Tank1 { get; }

        public int Width { get; }

        public int Height { get; }

        private List<Missile> missiles = new List<Missile>();
        public Game(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            //init comme en c/c++
            entities = new Entity[width][];
            for (int i = 0; i < width; ++i) entities[i] = new Entity[height];

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

            entities[1][1] = Tank1 = new Tank(1, 1);
            entities[2][2] = new Wall(2, 2, true);
        }

        public Entity getEntity(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height) return null;
            return entities[x][y];
        }

        public void shootMissile(int x, int y, Tank tank)
        {

        }
    }
}
