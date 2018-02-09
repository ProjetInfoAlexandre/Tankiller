using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tankiller.src;

namespace Tankiller
{
    public class Item : Entity
    {
        public ItemType Type { get; set; }

        public Item(int x, int y, Game game, ItemType type) : base(x, y, game)
        {
            this.Type = type;
        }
    }
}
