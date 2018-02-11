using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tankiller.src;

namespace Tankiller
{
    public class Tank : Entity
    {
        /// <summary>
        /// Durée minimale de déplacement d'une case à l'autre
        /// </summary>
        public static readonly long MinMovementDuration = 200;

        /// <summary>
        /// Direction actuelle du tank
        /// </summary>
        public Direction Direction { get; set; }
        /// <summary>
        /// Position précédante du tank
        /// </summary>
        public long LastMovement { get; set; }
        /// <summary>
        /// Temps en milisecondes auquel le tank a bougé pour la dernière fois
        /// (Voir Game.Timer)
        /// </summary>
        public long MovementDuration { get; set; } = 500;
        /// <summary>
        /// Indique si le tank est en vie ou non
        /// </summary>
        public bool Alive { get; set; } = true;
        /// <summary>
        /// Position X précédante
        /// </summary>
        public int LastX { get; set; }
        /// <summary>
        /// Position Y précédante
        /// </summary>
        public int LastY { get; set; }

        /// <summary>
        /// Items collectés par le tank
        /// </summary>
        private Dictionary<ItemType, int> Items = new Dictionary<ItemType, int>();

        public Tank(int x, int y, Game myGame, Direction direction) : base(x, y, myGame)
        {
            Direction = direction;
            LastX = x - direction.GetModX();
            LastY = y - direction.GetModY();

            foreach (ItemType itemType in Enum.GetValues(typeof(ItemType))) Items.Add(itemType, 0);
        }

        /// <summary>
        /// Place une bombe aux coordonnées actuelles
        /// </summary>
        public void Bomb()
        {
            if (!Alive) return;

            int placedBombs = 0;
            foreach (Bomb bomb in myGame.GetBombs()) if (bomb.Source == this) ++placedBombs;

            //nombre max de bombes
            if (placedBombs >= Items[ItemType.BOMB] + 1) return;
        
            //on test s'il est entre 2 cases, sur laquelle placée la bombe
            if (LastMovement + MovementDuration / 2 > myGame.Timer.ElapsedMilliseconds)
                myGame.PlaceBomb(LastX, LastY, this, 1 + Items[ItemType.POWER]);
            else myGame.PlaceBomb(X, Y, this, 1 + Items[ItemType.POWER]);
        }

        /// <summary>
        /// Déplace le tank dans la direction donnée
        /// </summary>
        /// <param name="d">Direction dans laquelle déplacer le tank</param>
        public void Move(Direction d)
        {
            if (!Alive) return;

            //AN'a pas fini son dernier déplacement
            if (myGame.Timer.ElapsedMilliseconds < LastMovement + MovementDuration) return;

            //on tourne la tete meme si on ne peut pas s'y deplacer
            Direction = d;

            //En dehors de la carte
            if (X + d.GetModX() < 0 || X + d.GetModX() >= myGame.Width) return;
            if (Y + d.GetModY() < 0 || Y + d.GetModY() >= myGame.Height) return;

            //Colision avec d'autres tanks
            List<Tank> tanks = myGame.GetTanks();
            foreach(Tank t in tanks)
            {
                if (t == this) continue;
                if (t.X == X + d.GetModX() && t.Y == Y + d.GetModY()) return;
            }

            //Colision avec les murs
            List<Wall> walls = myGame.GetWalls();
            foreach (Wall w in walls)
            {
                if (w.X == X + d.GetModX() && w.Y == Y + d.GetModY()) return;
            }

            //Colision avec les bombes
            List<Bomb> bombs = myGame.GetBombs();
            foreach (Bomb bomb in bombs)
            {
                if (bomb.X == X + d.GetModX() && bomb.Y == Y + d.GetModY()) return;
            }

            //Mise à jour de la dernière position
            LastX = X;
            LastY = Y;

            //on aurait pu utilise Direciton.getMod
            switch (d)
            {
                case Direction.BOT: this.Y++; break;
                case Direction.RIGHT: this.X++; break;
                case Direction.TOP: this.Y--; break;
                case Direction.LEFT: this.X--; break;
            }

            LastMovement = myGame.Timer.ElapsedMilliseconds;

            //Collecte des items sur la case
            List<Item> collected = new List<Item>();
            foreach (Item item in myGame.GetItems())
            {
                if (item.X == X && item.Y == Y)
                {
                    AddItem(item.Type);
                    collected.Add(item);
                }
            }

            //on enlève les items colléctés du jeu
            foreach (Item item in collected) myGame.GetItems().Remove(item);
        }

        //Ajout d'un item au tank
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
