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

            Boolean right = true, left = true, top = true, bot = true;

            foreach (Tank t in tanks)
            {
                if (t.X == this.X - 1 && t.Y == this.Y)
                {
                    left = false;
                    Direction = Direction.LEFT;
                }
                else if (t.X == this.X + 1 && t.Y == this.Y)
                {
                    right = false;
                    Direction = Direction.RIGHT;
                }
                else if (t.X == this.X && t.Y == this.Y - 1)
                {
                    bot = false;
                    Direction = Direction.BOT;
                }
                else if (t.X == this.X && t.Y == this.Y + 1)
                {
                    top = false;
                    Direction = Direction.TOP;
                }
            }

            foreach(Wall w in walls)
            {
                if (w.X == this.X - 1 && w.Y == this.Y)
                {
                    left = false;
                    Direction = Direction.LEFT;
                }
                else if (w.X == this.X + 1 && w.Y == this.Y)
                {
                    right = false;
                    Direction = Direction.RIGHT;
                }
                else if (w.X == this.X && w.Y == this.Y - 1)
                {
                    bot = false;
                    Direction = Direction.BOT;
                }
                else if (w.X == this.X && w.Y == this.Y + 1)
                {
                    top = false;
                    Direction = Direction.TOP;
                }
            }

            switch (d)
            {
                case Direction.BOT:
                    if(bot)
                    {
                        this.Y++;
                    }
                    break;

                case Direction.RIGHT:
                    if(right)
                    {
                        this.X++;
                    }
                    break;

                case Direction.TOP:
                    if(top)
                    {
                        this.Y--;
                    }
                    break;

                case Direction.LEFT:
                    if(left)
                    {
                        this.X--;
                    }
                    break;
            }

            //Tankiller.Update();
        }
    }
}
