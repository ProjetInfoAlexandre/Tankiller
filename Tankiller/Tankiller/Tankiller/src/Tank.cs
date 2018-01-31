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

            Boolean canMove = true;

            foreach(Tank t in tanks)
            {
                if (t == this) continue;
                if (X == t.X + d.GetModX() && Y == t.Y + d.GetModY())
                    canMove = false;
            }

            foreach (Wall w in walls)
            {
                if (X == w.X + d.GetModX() && Y == w.Y + d.GetModY())
                    canMove = false;
            }

            switch (d)
            {
                case Direction.BOT:
                    if(canMove)
                    {
                        this.Y++;
                    }
                    break;

                case Direction.RIGHT:
                    if(canMove)
                    {
                        this.X++;
                    }
                    break;

                case Direction.TOP:
                    if(canMove)
                    {
                        this.Y--;
                    }
                    break;

                case Direction.LEFT:
                    if(canMove)
                    {
                        this.X--;
                    }
                    break;
            }

            //Tankiller.Update();
        }
    }
}
