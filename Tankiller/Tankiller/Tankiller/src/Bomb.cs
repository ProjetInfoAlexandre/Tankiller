using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tankiller.src
{
    public class Bomb : Entity
    {
        public Tank Source { get; }
        public long Placed { get; }
        public long Delay { get; }

        private int Power = 3;
        
        public Bomb(int x, int y, Game myGame, Tank source, long placed, long delay) : base(x, y, myGame)
        {
            this.Source = source;
            this.Placed = placed;
            this.Delay = delay;
        }


        public List<Entity> Explode()
        {
            List<Entity> exploded = new List<Entity>();
           
            foreach (Wall wall in myGame.GetWalls())
            {
                if (!wall.Breakable) continue;

                if (wall.X == X)
                {
                    if (wall.Y - Y <= Power && wall.Y - Y >= -Power)
                    {
                        exploded.Add(wall);
                    }
                }
                else if (wall.Y == Y)
                {
                    if (wall.X - X <= Power && wall.X - X >= -Power)
                    {
                        exploded.Add(wall);
                    }
                }
            }

            foreach (Wall w in exploded) myGame.GetWalls().Remove(w);


            return exploded;
        }

        public List<int[]> getInvolvedPositions()
        {
            List<int[]> involved = new List<int[]>();

            for (int i = X - Power; i <= X + Power; ++i)
            {
                if (i == X) continue;
                if (i < 0 || i >= myGame.Width) continue;
                involved.Add(item: new int[3] { i, Y , (int)Direction.LEFT});
            }

            for (int i = Y - Power; i <= Y + Power; ++i)
            {
                if (i == Y) continue;
                if (i < 0 || i >= myGame.Height) continue;
                involved.Add(new int[3] { X, i , (int)Direction.TOP});
            }

            return involved;
        }
    }
}
