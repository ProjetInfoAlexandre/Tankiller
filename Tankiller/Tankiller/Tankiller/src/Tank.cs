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

        public void move()
        {

        }
        public void shoot()
        {

        }

        public void move(Direction d)
        {
            //Pas fini mais soon
            List<Tank> tanks = myGame.getTanks();
            List<Wall> walls = myGame.getWalls();

            foreach(Tank T in tanks)
            {
                
            }

            foreach(Wall w in walls)
            {

            }

            switch (d)
            {
                case Direction.BOT:
                    if (tanks)
                    {

                    }

                        break;

                case Direction.RIGHT: break;

                case Direction.TOP: break;

                case Direction.LEFT: break;
            }
        }
    }
}
