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
        public long Delay { get; set; }

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

                if ((wall.X == X && wall.Y - Y <= Power && wall.Y - Y >= -Power) ||
                    (wall.Y == Y && wall.X - X <= Power && wall.X - X >= -Power))
                {
                    exploded.Add(wall);
                }
            }

            foreach (Wall w in exploded) myGame.GetWalls().Remove(w);

            foreach (Tank tank in myGame.GetTanks())
            {
                
                if ((tank.X == X && tank.Y - Y <= Power && tank.Y - Y >= -Power) ||
                    (tank.Y == Y && tank.X - X <= Power && tank.X - X >= -Power))
                {
                    exploded.Add(tank);
                }

                if (tank.LastMovement + tank.MovementDuration > myGame.timer.ElapsedMilliseconds)
                {
                    //pas fini de bouger donc on regarde aussi la provenance
                    if ((tank.LastX == X && tank.LastY - Y <= Power && tank.LastY - Y >= -Power) ||
                        (tank.LastY == Y && tank.LastX - X <= Power && tank.LastX - X >= -Power))
                    {
                        exploded.Add(tank);
                    }
                }
            }

            foreach (Bomb bomb in myGame.GetBombs())
            {
                if ((bomb.X == X && bomb.Y - Y <= Power && bomb.Y - Y >= -Power) ||
                    (bomb.Y == Y && bomb.X - X <= Power && bomb.X - X >= -Power))
                {
                    bomb.Delay = Math.Min(bomb.Delay, myGame.timer.ElapsedMilliseconds - bomb.Placed + 100);
                }
            }

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
