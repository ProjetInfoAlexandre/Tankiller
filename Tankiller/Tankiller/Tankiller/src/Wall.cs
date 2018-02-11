using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tankiller.src
{
    public class Wall : Entity
    {
        /// <summary>
        /// Evite de re-créer un random à chaque fois
        /// Mieux pour le seed
        /// </summary>
        private static readonly Random Alea = new Random();

        /// <summary>
        /// Indique si le mur peut être détruit ou non
        /// </summary>
        public bool Breakable { get; set; }

        public Wall(int x, int y, bool breakable, Game myGame) : base(x, y, myGame)
        {
            this.Breakable = breakable;
        }

        /// <summary>
        /// Détruit le mur
        /// Place un item avec certaines probabilités
        /// </summary>
        public void Destroy()
        {
            if (!Breakable) return;

            myGame.GetWalls().Remove(this);

            int alea = Alea.Next() % 10;
            if (alea <= 2) myGame.PlaceItem(X, Y, ItemType.SPEED);//30%
            else if (alea <= 4) myGame.PlaceItem(X, Y, ItemType.POWER);//20%
            else if (alea <= 5) myGame.PlaceItem(X, Y, ItemType.BOMB);//10%
            //et 40% de rien
        }
    }
}