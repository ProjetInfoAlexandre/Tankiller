using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tankiller.src
{
    public enum Direction
    {
        TOP, BOT, RIGHT, LEFT
    }

    static class DirectionMethods
    {
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
