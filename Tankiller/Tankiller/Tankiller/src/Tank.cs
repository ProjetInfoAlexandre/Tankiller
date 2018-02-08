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
        public long LastMovement { get; set; }
        public long MovementDuration { get; set; } = 500;

        public Tank(int x , int y, Game myGame) : base(x, y, myGame)
        {
            Direction = Direction.TOP;
        }

        public void Bomb()
        {
            if (LastMovement + MovementDuration > myGame.timer.ElapsedMilliseconds)
            {
                //encore en mouvement
                myGame.PlaceBomb(X - Direction.GetModX(), Y - Direction.GetModY(), this);
            }
            else myGame.PlaceBomb(X, Y, this);
        }

        public void Move(Direction d)
        {
            if (myGame.timer.ElapsedMilliseconds < LastMovement + MovementDuration) return;

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

            switch (d)
            {
                case Direction.BOT: this.Y++; break;
                case Direction.RIGHT: this.X++; break;
                case Direction.TOP: this.Y--; break;
                case Direction.LEFT: this.X--; break;
            }

            LastMovement = myGame.timer.ElapsedMilliseconds;

            //Tankiller.Update();
        }
    }
}
