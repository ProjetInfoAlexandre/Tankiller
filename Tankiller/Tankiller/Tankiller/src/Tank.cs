using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tankiller.src;

namespace Tankiller
{
    public class Tank : Entity
    {
        public Direction direction { get; set; }

        public Tank(int x , int y, Game myGame) : base(x, y, myGame)
        {
            direction = Direction.TOP;
        }

        public void Shoot()
        {
            new Missile(x, y,direction,  myGame, this);
        }

        public void Move(Direction d)
        {
            // Si c'est pas bien stp me flame pas (or reported). J'suis gentil.
            List<Tank> tanks = myGame.GetTanks();
            List<Wall> walls = myGame.GetWalls();

            Boolean right = true, left = true, top = true, bot = true;

            foreach (Tank t in tanks)
            {
                if (t.x == this.x - 1 && t.y == this.y)
                {
                    left = false;
                    direction = Direction.LEFT;
                }
                else if (t.x == this.x + 1 && t.y == this.y)
                {
                    right = false;
                    direction = Direction.RIGHT;
                }
                else if (t.x == this.x && t.y == this.y - 1)
                {
                    bot = false;
                    direction = Direction.BOT;
                }
                else if (t.x == this.x && t.y == this.y + 1)
                {
                    top = false;
                    direction = Direction.TOP;
                }
            }

            foreach(Wall w in walls)
            {
                if (w.x == this.x - 1 && w.y == this.y)
                {
                    left = false;
                    direction = Direction.LEFT;
                }
                else if (w.x == this.x + 1 && w.y == this.y)
                {
                    right = false;
                    direction = Direction.RIGHT;
                }
                else if (w.x == this.x && w.y == this.y - 1)
                {
                    bot = false;
                    direction = Direction.BOT;
                }
                else if (w.x == this.x && w.y == this.y + 1)
                {
                    top = false;
                    direction = Direction.TOP;
                }
            }

            switch (d)
            {
                case Direction.BOT:
                    if(bot)
                    {
                        this.y++;
                    }
                    break;

                case Direction.RIGHT:
                    if(right)
                    {
                        this.x++;
                    }
                    break;

                case Direction.TOP:
                    if(top)
                    {
                        this.y--;
                    }
                    break;

                case Direction.LEFT:
                    if(left)
                    {
                        this.x--;
                    }
                    break;
            }

            Tankiller.Update();
        }
    }
}
