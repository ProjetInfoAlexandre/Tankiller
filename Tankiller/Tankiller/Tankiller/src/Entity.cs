using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tankiller.src
{
    public class Entity
    {
        /// <summary>
        /// Position X de l'entité
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Position Y de l'entité
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Partie dans laquelle l'entité est invoquée
        /// </summary>
        protected Game myGame;

        /// <summary>
        /// Crée une entité
        /// </summary>
        /// <param name="x">Position X initiale de l'entité</param>
        /// <param name="y">Position Y initiale de l'entité</param>
        /// <param name="myGame"></param>
        public Entity(int x, int y, Game myGame)
        {
            this.X = x;
            this.Y = y;
            this.myGame = myGame;
        }

    }
}
