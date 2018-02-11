using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tankiller.src
{
    /// <summary>
    /// Représente une diréction en 2D
    /// </summary>
    public enum Direction
    {
        TOP, BOT, RIGHT, LEFT
    }

    static class DirectionMethods
    {
        /// <summary>
        /// Renvoie le composant X de cette direction
        /// </summary>
        /// <param name="direction">Direction en question</param>
        /// <returns>-1 pour gauche, 1 pour droite, 0 pour aucun</returns>
        public static int GetModX(this Direction direction)
        {
            switch (direction)
            {
                case Direction.BOT: return 0;
                case Direction.TOP: return 0;
                case Direction.LEFT: return -1;
                default: return 1;
            }
        }

        /// <summary>
        /// Renvoie le composant Y de cette direction
        /// </summary>
        /// <param name="direction">Direction en question</param>
        /// <returns>-1 pour haut, 1 pour bas, 0 pour aucun</returns>
        public static int GetModY(this Direction direction)
        {
            switch (direction)
            {
                case Direction.BOT: return 1;
                case Direction.TOP: return -1;
                case Direction.LEFT: return 0;
                default: return 0;
            }
        }
    }
}
