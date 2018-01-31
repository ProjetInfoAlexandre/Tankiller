using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tankiller.src
{
    public class Game
    {
        private List<Tank> tanks = new List<Tank>();

        private List<Wall> walls = new List<Wall>();

        public int Width { get; }

        public int Height { get; }

        private List<Missile> missiles = new List<Missile>();
        public Game(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            tanks.Add(new Tank(1, 1, this));

            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    walls.Add(new Wall(j, i, false, this));
                }
            }
        }

        public List<Tank> GetTanks()
        {
            return tanks;
        }

        public List<Wall> GetWalls()
        {
            return walls;
        }

        public void ShootMissile(int x, int y, Tank tank)
        {

        }
    }
}
