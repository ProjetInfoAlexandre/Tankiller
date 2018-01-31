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

        public Tank(int x , int y, Game myGame) : base(x, y, myGame)
        {
            Direction = Direction.TOP;
        }

        public void Shoot()
        {
            new Missile(X, Y,Direction,  myGame, this);
        }

        public void Move(Direction d)
        {
            // Si c'est pas bien stp me flame pas (or reported). J'suis gentil.
            List<Tank> tanks = myGame.GetTanks();
            List<Wall> walls = myGame.GetWalls();

            foreach(Tank t in tanks)
            {
                if (t == this) continue;
                if (t.X == X + d.GetModX() && t.Y == Y + d.GetModY())
                    return;
            }

            foreach (Wall w in walls)
            {
                if (w.Y == Y + d.GetModX() && w.Y == Y + d.GetModY())
                    return;
            }

            switch (d)
            {
                case Direction.BOT: this.Y++; break;

                case Direction.RIGHT: this.X++; break;

                case Direction.TOP: this.Y--; break;

                case Direction.LEFT: this.X--; break;
            }

            //Tankiller.Update();
        }
    }
}
