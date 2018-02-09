using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tankiller.src;

namespace Tankiller
{
    public class Tank : Entity
    {
        public static readonly long MinMovementDuration = 200;

        public Direction Direction { get; set; }
        public long LastMovement { get; set; }
        public long MovementDuration { get; set; } = 500;
        public bool Alive { get; set; } = true;
        public int LastX { get; set; }
        public int LastY { get; set; }

        private Dictionary<ItemType, int> Items = new Dictionary<ItemType, int>();

        public Tank(int x, int y, Game myGame, Direction direction) : base(x, y, myGame)
        {
            Direction = direction;
            LastX = x - direction.GetModX();
            LastY = y - direction.GetModY();

            foreach (ItemType itemType in Enum.GetValues(typeof(ItemType))) Items.Add(itemType, 0);
        }

        public void Bomb()
        {
            if (!Alive) return;

            int placedBombs = 0;
            foreach (Bomb bomb in myGame.GetBombs()) if (bomb.Source == this) ++placedBombs;

            //nombre max de bombes
            if (placedBombs >= Items[ItemType.BOMB] + 1) return;
        
            if (LastMovement + MovementDuration / 2 > myGame.Timer.ElapsedMilliseconds)
            {
                //encore en mouvement
                myGame.PlaceBomb(LastX, LastY, this, 1 + Items[ItemType.POWER]);
            }
            else myGame.PlaceBomb(X, Y, this, 1 + Items[ItemType.POWER]);
        }

        public void Move(Direction d)
        {
            if (!Alive) return;

            if (myGame.Timer.ElapsedMilliseconds < LastMovement + MovementDuration) return;

            //on tourne la tete meme si on ne peut pas s'y deplacer
            Direction = d;

            if (X + d.GetModX() < 0 || X + d.GetModX() >= myGame.Width) return;
            if (Y + d.GetModY() < 0 || Y + d.GetModY() >= myGame.Height) return;

            List<Tank> tanks = myGame.GetTanks();
            foreach(Tank t in tanks)
            {
                if (t == this) continue;
                if (t.X == X + d.GetModX() && t.Y == Y + d.GetModY()) return;
            }

            List<Wall> walls = myGame.GetWalls();
            foreach (Wall w in walls)
            {
                if (w.X == X + d.GetModX() && w.Y == Y + d.GetModY()) return;
            }

            List<Bomb> bombs = myGame.GetBombs();
            foreach (Bomb bomb in bombs)
            {
                if (bomb.X == X + d.GetModX() && bomb.Y == Y + d.GetModY()) return;
            }

            LastX = X;
            LastY = Y;

            switch (d)
            {
                case Direction.BOT: this.Y++; break;
                case Direction.RIGHT: this.X++; break;
                case Direction.TOP: this.Y--; break;
                case Direction.LEFT: this.X--; break;
            }

            LastMovement = myGame.Timer.ElapsedMilliseconds;

            List<Item> collected = new List<Item>();
            foreach (Item item in myGame.GetItems())
            {
                if (item.X == X && item.Y == Y)
                {
                    AddItem(item.Type);
                    collected.Add(item);
                }
            }

            foreach (Item item in collected) myGame.GetItems().Remove(item);
        }

        public void AddItem(ItemType item)
        {
            ++Items[item];

            if (item == ItemType.SPEED)
            {
                MovementDuration = Math.Max(MovementDuration - 20, MinMovementDuration);
            }
        }
    }
}
