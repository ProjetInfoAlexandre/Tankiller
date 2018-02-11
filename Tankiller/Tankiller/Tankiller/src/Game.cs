using System.Collections.Generic;
using System.Diagnostics;

namespace Tankiller.src
{
    public class Game
    {
        private List<Tank> tanks = new List<Tank>();
        private List<Bomb> bombs = new List<Bomb>();
        private List<Wall> walls = new List<Wall>();
        private List<Item> items = new List<Item>();

        /// <summary>
        /// Largeur du jeu
        /// </summary>
        public int Width { get; }
        /// <summary>
        /// Hauteur du jeu
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Chronomêtre lancé au début de la partie
        /// </summary>
        public Stopwatch Timer = new Stopwatch();

        private List<Bomb> missiles = new List<Bomb>();
        public Game(int width, int height)
        {
            if (width < 3) width = 3;
            if (height < 3) height = 3;

            this.Width = width;
            this.Height = height;

            Tank t = new Tank(1, height / 2, this, Direction.RIGHT);
            tanks.Add(t);

            t = new Tank(width - 2, height / 2, this, Direction.LEFT);
            tanks.Add(t);

            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    if ((i == 1 || i == width - 2) && j == height / 2) continue;
                    if ((i == 2 || i == width - 3) && j == height / 2) continue;
                    if ((i == 1 || i == width - 2) && (j == width / 2 + 1 || j == width / 2 - 1)) continue;
                    

                    walls.Add(new Wall(i, j, i != 0 && i != width - 1 && j != 0 && j != height - 1, this));
                }
            }

            Timer.Start();
        }

        ~Game()
        {
            Timer.Stop();
            Timer = null;

            walls.Clear();
            bombs.Clear();
            tanks.Clear();
        }

        /// <summary>
        /// Renvoie les tanks (vivant ET morts) de la partie
        /// </summary>
        /// <returns>Liste des tanks</returns>
        public List<Tank> GetTanks()
        {
            return tanks;
        }

        /// <summary>
        /// Renvoie les murs (non détruits) de la partie
        /// </summary>
        /// <returns>Liste des murs</returns>
        public List<Wall> GetWalls()
        {
            return walls;
        }

        /// <summary>
        /// Renvoie les bombes (placées) de la partie
        /// </summary>
        /// <returns>Liste des bombes</returns>
        public List<Bomb> GetBombs()
        {
            return bombs;
        }

        /// <summary>
        /// Renvoie les entités de la partie
        /// </summary>
        /// <returns>Liste des entitées de la partie</returns>
        public List<Item> GetItems()
        {
            return items;
        }

        /// <summary>
        /// Place une bombe aux coordonnées données
        /// </summary>
        /// <param name="x">Position X de la bombe à placer</param>
        /// <param name="y">Position Y de la bombe à placer</param>
        /// <param name="tank">Tank source de la bombe</param>
        /// <param name="power">Puissance de la bombe</param>
        /// <returns>Bombe placée, null si impossible à placer</returns>
        public Bomb PlaceBomb(int x, int y, Tank tank, int power)
        {
            foreach (Bomb b in bombs) if (b.X == x && b.Y == y) return null;
            if (x <= 0 || x >= Width - 1 || y <= 0 || y >= Height - 1) return null;

            Bomb bomb = new Bomb(x, y, this, tank, Timer.ElapsedMilliseconds, 3000, power);

            bombs.Add(bomb);

            return bomb;
        }

        /// <summary>
        /// Place un item aux coordonnées données
        /// </summary>
        /// <param name="x">Position X de l'item à placer</param>
        /// <param name="y">Position Y de l'item à placer</param>
        /// <param name="type">Type de l'item</param>
        /// <returns>Item placé</returns>
        public Item PlaceItem(int x, int y, ItemType type)
        {
            Item item = new Item(x, y, this, type);
            items.Add(item);

            return item;
        }
    }
}
