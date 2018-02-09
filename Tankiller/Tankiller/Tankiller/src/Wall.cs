using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tankiller.src
{
    public class Wall : Entity
    {
        private static readonly Random Alea = new Random();

        public bool Breakable { get; set; }

        public Wall(int x, int y, bool breakable, Game myGame) : base(x, y, myGame)
        {
            this.Breakable = breakable;
        }

        public void Destroy()
        {
            if (!Breakable) return;

            myGame.GetWalls().Remove(this);

            int alea = Alea.Next() % 10;
            if (alea <= 2) myGame.PlaceItem(X, Y, ItemType.SPEED);
            else if (alea <= 4) myGame.PlaceItem(X, Y, ItemType.POWER);
            else if (alea <= 5) myGame.PlaceItem(X, Y, ItemType.BOMB);
        }
    }
}
