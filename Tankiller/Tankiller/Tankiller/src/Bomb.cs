using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tankiller.src
{
    public class Bomb : Entity
    {
        public Tank Source { get; }
        public long Placed { get; }
        public long Delay { get; set; }

        private int Power;
        
        public Bomb(int x, int y, Game myGame, Tank source, long placed, long delay, int power) : base(x, y, myGame)
        {
            this.Source = source;
            this.Placed = placed;
            this.Delay = delay;
            this.Power = power;
        }


        public List<Entity> Explode()
        {
            List<Entity> exploded = new List<Entity>();

            //destruction des items, LAISSE AVANT DESTRUCTION DES MURS
            List<Item> destroyedItems = new List<Item>();
            foreach (Item item in myGame.GetItems())
            {
                if ((item.X == X && item.Y - Y <= Power && item.Y - Y >= -Power) ||
                    (item.Y == Y && item.X - X <= Power && item.X - X >= -Power))
                {
                    destroyedItems.Add(item);
                }
            }
            foreach (Item item in destroyedItems) myGame.GetItems().Remove(item);

            //destruction des murs
            foreach (Wall wall in myGame.GetWalls())
            {
                if (!wall.Breakable) continue;

                if ((wall.X == X && wall.Y - Y <= Power && wall.Y - Y >= -Power) ||
                    (wall.Y == Y && wall.X - X <= Power && wall.X - X >= -Power))
                {
                    exploded.Add(wall);
                }
            }
            foreach (Wall w in exploded) w.Destroy();

            //destruction des tanks
            foreach (Tank tank in myGame.GetTanks())
            {
                
                if ((tank.X == X && tank.Y - Y <= Power && tank.Y - Y >= -Power) ||
                    (tank.Y == Y && tank.X - X <= Power && tank.X - X >= -Power))
                {
                    exploded.Add(tank);
                }

                if (tank.LastMovement + tank.MovementDuration > myGame.Timer.ElapsedMilliseconds)
                {
                    //pas fini de bouger donc on regarde aussi la provenance
                    if ((tank.LastX == X && tank.LastY - Y <= Power && tank.LastY - Y >= -Power) ||
                        (tank.LastY == Y && tank.LastX - X <= Power && tank.LastX - X >= -Power))
                    {
                        exploded.Add(tank);
                    }
                }
            }

            //activation de la bombe touchée la plus proche dans chaque direction
            Bomb Top = null, Bot = null, Left = null, Right = null;
            foreach (Bomb bomb in myGame.GetBombs())
            {
                if (bomb.X == X)
                {
                    if (bomb.Y > Y && bomb.Y - Y <= Power)
                    {
                        if (Top == null) Top = bomb;
                        else if (Top.Y > bomb.Y) Top = bomb;
                    }
                    else if (bomb.Y < Y && Y - bomb.Y <= Power)
                    {
                        if (Bot == null) Bot = bomb;
                        else if (Bot.Y < bomb.Y) Bot = bomb;
                    }
                }

                if (bomb.Y == Y)
                {
                    if (bomb.X > X && bomb.X - X <= Power)
                    {
                        if (Right == null) Right = bomb;
                        else if (Right.Y > bomb.Y) Right = bomb;
                    }
                    else if (bomb.X < X && X - bomb.X <= Power)
                    {
                        if (Left == null) Left = bomb;
                        else if (Left.X < bomb.X) Left = bomb;
                    }
                }
            }
            if (Top != null) Top.Delay = Math.Min(Top.Delay, myGame.Timer.ElapsedMilliseconds - Top.Placed + 200);
            if (Bot != null) Bot.Delay = Math.Min(Bot.Delay, myGame.Timer.ElapsedMilliseconds - Bot.Placed + 200);
            if (Left != null) Left.Delay = Math.Min(Left.Delay, myGame.Timer.ElapsedMilliseconds - Left.Placed + 200);
            if (Right != null) Right.Delay = Math.Min(Right.Delay, myGame.Timer.ElapsedMilliseconds - Right.Placed + 200);

            return exploded;
        }

        public List<int[]> getInvolvedPositions()
        {
            List<int[]> involved = new List<int[]>();

            for (int i = X - Power; i <= X + Power; ++i)
            {
                if (i == X) continue;
                if (i < 0 || i >= myGame.Width) continue;
                involved.Add(item: new int[3] { i, Y , (int)Direction.LEFT});
            }

            for (int i = Y - Power; i <= Y + Power; ++i)
            {
                if (i == Y) continue;
                if (i < 0 || i >= myGame.Height) continue;
                involved.Add(new int[3] { X, i , (int)Direction.TOP});
            }

            return involved;
        }
    }
}
