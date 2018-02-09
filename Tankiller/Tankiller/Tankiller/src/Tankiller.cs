using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tankiller.src;

namespace Tankiller
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Tankiller : Microsoft.Xna.Framework.Game
    {
        private static readonly int explosion_fade = 150;
        private static readonly int wall_fade = 300;


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private bool pause = false, finished = false;
        private Task finishTask = null;
        private bool Menu = true;

        private Texture2D Texture_tank1 = null;
        private Texture2D Texture_tank2 = null;
        private Texture2D Texture_wall = null;
        private Texture2D Texture_background = null;
        private Texture2D Texture_bomb = null;
        private Texture2D Texture_explosion = null;
        private Texture2D Texture_menu = null;
        private Texture2D Texture_commencer = null;
        private Texture2D Texture_bomb_more = null;
        private Texture2D Texture_bomb_power = null;
        private Texture2D Texture_speed = null;

        private Dictionary<Wall, long> explodedWalls = new Dictionary<Wall, long>();
        private Dictionary<int[], long> explodedTiles = new Dictionary<int[], long>();

        private src.Game game = null;

        public Tankiller()
        {
            game = new src.Game(20, 20);

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 720;
            graphics.PreferredBackBufferHeight = 720;

            Content.RootDirectory = "Content";
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            IsMouseVisible = true;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Texture_tank1 = Content.Load<Texture2D>("tank1");
            Texture_tank2 = Content.Load<Texture2D>("tank2");
            Texture_wall = Content.Load<Texture2D>("mur");
            Texture_background = Content.Load<Texture2D>("background");
            Texture_bomb = Content.Load<Texture2D>("bomb");
            Texture_explosion = Content.Load<Texture2D>("explosion");
            Texture_menu = Content.Load<Texture2D>("menu");
            Texture_commencer = Content.Load<Texture2D>("commencer");
            Texture_bomb_power = Content.Load<Texture2D>("bomb_power");
            Texture_bomb_more = Content.Load<Texture2D>("bomb_more");
            Texture_speed = Content.Load<Texture2D>("vitesse");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Menu) UpdateMenu();
            else UpdateJeu(gameTime);            


            //LAISSER ABSOLUEMENT
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (Menu) DrawMenu(gameTime);
            else DrawGame(gameTime);


            base.Draw(gameTime);
        }

        protected void Pause()
        {
            if (pause = !pause)
            {
                game.Timer.Stop();
                Window.Title = "Tankiller - Pause";
            }
            else
            {
                game.Timer.Start();
                Window.Title = "Tankiller";
            }
        }

        protected void Restart()
        {
            game = null;
            pause = false;
            finished = false;
            finishTask = null;

            explodedTiles.Clear();
            explodedWalls.Clear();

            game = new src.Game(20, 20);
        }

        protected void UpdateMenu()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                Menu = false;
                key_enter = true;
            }
        }

        private long lastPauseKey = 0;
        private bool key_space = false;
        private bool key_esc = false;
        private bool key_enter = false;

        protected void UpdateJeu(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (!finished)
            {
                ///pause
                if (keyboardState.IsKeyDown(Keys.Escape) && !key_esc)
                {
                    key_esc = true;

                    if (gameTime.TotalGameTime.TotalMilliseconds > lastPauseKey + 500)
                    {
                        Pause();
                        lastPauseKey = (long)gameTime.TotalGameTime.TotalMilliseconds;
                    }
                }
                else if (!keyboardState.IsKeyDown(Keys.Escape)) key_esc = false;

                //joueur1
                if (keyboardState.IsKeyDown(Keys.Z))
                {
                    game.GetTanks()[0].Move(Direction.TOP);
                }

                if (keyboardState.IsKeyDown(Keys.S))
                {
                    game.GetTanks()[0].Move(Direction.BOT);
                }

                if (keyboardState.IsKeyDown(Keys.Q))
                {
                    game.GetTanks()[0].Move(Direction.LEFT);
                }

                if (keyboardState.IsKeyDown(Keys.D))
                {
                    game.GetTanks()[0].Move(Direction.RIGHT);
                }

                if (keyboardState.IsKeyDown(Keys.Space) && !key_space)
                {
                    game.GetTanks()[0].Bomb();
                    key_space = true;
                }
                else if (!keyboardState.IsKeyDown(Keys.Space)) key_space = false;

                //joueur2
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    game.GetTanks()[1].Move(Direction.TOP);
                }

                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    game.GetTanks()[1].Move(Direction.BOT);
                }

                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    game.GetTanks()[1].Move(Direction.LEFT);
                }

                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    game.GetTanks()[1].Move(Direction.RIGHT);
                }

                if (keyboardState.IsKeyDown(Keys.Enter) && !key_enter)
                {
                    game.GetTanks()[1].Bomb();
                    key_enter = true;
                }
                else if (!keyboardState.IsKeyDown(Keys.Enter)) key_enter = false;
            }

            List<Wall> walls = explodedWalls.Keys.ToList<Wall>();
            foreach (Wall w in walls)
            {
                if (game.Timer.ElapsedMilliseconds - explodedWalls[w] >= wall_fade) explodedWalls.Remove(w);
            }

            List<int[]> tiles = explodedTiles.Keys.ToList<int[]>();
            foreach (int[] t in tiles)
            {
                if (game.Timer.ElapsedMilliseconds - explodedTiles[t] >= explosion_fade) explodedTiles.Remove(t);
            }

            List<Bomb> toRemove = new List<Bomb>();
            foreach (Bomb bomb in game.GetBombs())
            {
                if (game.Timer.ElapsedMilliseconds - bomb.Placed >= bomb.Delay)
                {
                    foreach (int[] t in bomb.getInvolvedPositions()) explodedTiles.Add(t, game.Timer.ElapsedMilliseconds);

                    List<Entity> exploded = bomb.Explode();

                    foreach (Entity e in exploded)
                    {
                        if (e is Wall) explodedWalls.Add((Wall)e, game.Timer.ElapsedMilliseconds);
                    }

                    if (exploded.Contains(game.GetTanks()[0]))
                    {
                        game.GetTanks()[0].Alive = false;
                        finished = true;
                    }
                    if (exploded.Contains(game.GetTanks()[1]))
                    {
                        game.GetTanks()[1].Alive = false;
                        finished = true;
                    }

                    toRemove.Add(bomb);
                }
            }
            foreach (Bomb b in toRemove) game.GetBombs().Remove(b);

            if (finished && finishTask == null)
            {
                finishTask = Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(500);

                    if (game.GetTanks()[0].Alive)
                        System.Windows.Forms.MessageBox.Show("Victoire du Joueur 1", "Boom !");
                    else if (game.GetTanks()[1].Alive)
                        System.Windows.Forms.MessageBox.Show("Victoire du Joueur 2", "Boom !");
                    else System.Windows.Forms.MessageBox.Show("Egalité", "Boom !");

                    Restart();
                });
            }
        }

        protected void DrawMenu(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            double ratio = (double)Math.Abs(30.0 - ((double)game.Timer.ElapsedMilliseconds % 2000) / 33.0) / 100.0 + 0.7;

            int width = GraphicsDevice.Viewport.Width;
            int height = GraphicsDevice.Viewport.Height;

            spriteBatch.Begin();

            Rectangle position = new Rectangle();

            position.Width = width;
            position.Height = height;
            position.X = position.Y = 0;

            spriteBatch.Draw(Texture_menu, position, Color.White);

            position.Height = (int)(ratio * 0.1 * ((double)height));
            position.Width = (int)(3.7 * ((double)position.Height));
            

            position.X = width / 2 - position.Width / 2;
            position.Y = height / 2 - position.Height / 2;

            spriteBatch.Draw(Texture_commencer, position, Color.White);

            spriteBatch.End();
        }

        protected void DrawGame(GameTime gameTime)
        {
            if (pause) base.Draw(gameTime);

            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            int width = GraphicsDevice.Viewport.Width;
            int height = GraphicsDevice.Viewport.Height;



            spriteBatch.Begin();

            Rectangle position = new Rectangle();

            position.Width = width;
            position.Height = height;
            position.X = position.Y = 0;

            spriteBatch.Draw(Texture_background, position, Color.White);


            position.Width = width / game.Width;
            position.Height = height / game.Height;

            //affichages des murs
            foreach (Wall wall in game.GetWalls())
            {
                position.X = wall.X * position.Width;
                position.Y = wall.Y * position.Height;

                if (wall.Breakable) spriteBatch.Draw(this.Texture_wall, position, Color.White);
                else spriteBatch.Draw(this.Texture_wall, position, Color.Orange);
            }

            //affichage des items
            Rectangle itemPosition = new Rectangle();
            foreach (Item item in game.GetItems())
            {
                Texture2D texture = null;
                switch (item.Type)
                {
                    case ItemType.BOMB: texture = Texture_bomb_more; break;
                    case ItemType.POWER: texture = Texture_bomb_power; break;
                    case ItemType.SPEED: texture = Texture_speed; break;
                }

                itemPosition.Width = (int)(0.8 * position.Width);
                itemPosition.Height = (int)(0.8 * position.Height);

                double ratio = Math.Abs((int)(20 - game.Timer.ElapsedMilliseconds % 2000 / 50)) / 100.0;

                itemPosition.X = position.Width * item.X + (position.Width - itemPosition.Width) / 2;
                itemPosition.Y = position.Height * item.Y + (int)(ratio * position.Height);

                spriteBatch.Draw(texture, itemPosition, Color.White);
            }

            //affichages des murs detruits
            foreach (Wall w in explodedWalls.Keys)
            {
                if (game.Timer.ElapsedMilliseconds - explodedWalls[w] >= wall_fade) continue;

                position.X = w.X * position.Width;
                position.Y = w.Y * position.Height;

                Color color = Color.White;
                color.G = color.B = (byte)((wall_fade - game.Timer.ElapsedMilliseconds + explodedWalls[w]) / 2);

                spriteBatch.Draw(Texture_wall, position, color);
            }

            //affichage des bombes
            Rectangle bombPosition = new Rectangle();
            foreach (Bomb bomb in game.GetBombs())
            {
                double bombRatio = 0.5 + ((double)Math.Abs(25 - ((game.Timer.ElapsedMilliseconds - bomb.Placed) % 2000) / 40)) / 50.0;

                bombPosition.Width = (int)(bombRatio * position.Width);
                bombPosition.Height = (int)(bombRatio * position.Height);

                position.X = bomb.X * position.Width;
                position.Y = bomb.Y * position.Height;

                bombPosition.X = position.Center.X - bombPosition.Width / 2;
                bombPosition.Y = position.Center.Y - bombPosition.Height / 2;

                if (bombPosition.Width % 2 == 0) bombPosition.X++;
                if (bombPosition.Height % 2 == 0) bombPosition.Y++;

                spriteBatch.Draw(this.Texture_bomb, bombPosition, Color.White);
            }

            //affichage des tanks
            int tank_index = 0;
            foreach (Tank tank in game.GetTanks())
            {
                ++tank_index;
                if (!tank.Alive) continue;

                if (tank.LastMovement + tank.MovementDuration <= game.Timer.ElapsedMilliseconds)
                {
                    position.X = tank.X * position.Width;
                    position.Y = tank.Y * position.Height;
                }
                else
                {
                    double ratio = ((double)(game.Timer.ElapsedMilliseconds - tank.LastMovement)) / tank.MovementDuration;
                    position.X = (int)((((double)tank.LastX) + ((double)tank.Direction.GetModX()) * ratio) * (double)position.Width);
                    position.Y = (int)((((double)tank.LastY) + ((double)tank.Direction.GetModY()) * ratio) * (double)position.Height);
                }

                float rotation = 0;
                switch (tank.Direction)
                {
                    case (src.Direction.BOT):
                        rotation = (float)Math.PI;
                        break;

                    case (src.Direction.LEFT):
                        rotation = (float)Math.PI / -2;
                        break;

                    case (src.Direction.RIGHT):
                        rotation = (float)Math.PI / 2;
                        break;
                }

                Vector2 origin = new Vector2(Texture_tank1.Width / 2, Texture_tank1.Height / 2);
                //petit fix (ça commence...)
                position.Location = position.Center;

                spriteBatch.Draw(tank_index == 1 ? Texture_tank1 : Texture_tank2, position, null, Color.White, rotation, origin, SpriteEffects.None, 0);
            }

            //affichage des explosions
            foreach (int[] t in explodedTiles.Keys)
            {
                Rectangle explosionPosition = new Rectangle();
                explosionPosition.X = t[0] * position.Width;
                explosionPosition.Y = t[1] * position.Height;

                explosionPosition.Y += (int)(0.15 * position.Height);
                explosionPosition.Width = position.Width;
                explosionPosition.Height = (int)(0.7 * position.Height);

                Vector2 origin = new Vector2(Texture_explosion.Width / 2, Texture_explosion.Height / 2);
                explosionPosition.Location = explosionPosition.Center;

                if (t[2] == (int)Direction.TOP)
                {
                    spriteBatch.Draw(Texture_explosion, explosionPosition, null, Color.White, (float)Math.PI / 2, origin, SpriteEffects.None, 0);
                }
                else if (t[2] == (int)Direction.LEFT)
                {
                    spriteBatch.Draw(Texture_explosion, explosionPosition, null, Color.White, 0, origin, SpriteEffects.None, 0);
                }
            }

            spriteBatch.End();
        }

    }
}
