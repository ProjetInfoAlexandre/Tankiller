using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Tankiller.src;

namespace Tankiller
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Tankiller : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D tank1 = null;
        private Texture2D tank2 = null;
        private Texture2D wall = null;
        private Texture2D background = null;
        private Texture2D bomb = null;
        private Texture2D explosion = null;

        private Dictionary<Wall, long> explodedWalls = new Dictionary<Wall, long>();
        private Dictionary<int[], long> explodedTiles = new Dictionary<int[], long>();

        private src.Game game = null;

        public Tankiller()
        {
            game = new src.Game(20, 20);

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = game.Width * 40;
            graphics.PreferredBackBufferHeight = game.Width * 40;

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
            tank1 = Content.Load<Texture2D>("tank1");
            tank2 = Content.Load<Texture2D>("tank2");
            wall = Content.Load<Texture2D>("mur");
            background = Content.Load<Texture2D>("background");
            bomb = Content.Load<Texture2D>("bomb");
            explosion = Content.Load<Texture2D>("explosion");
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
            MouseState mouseState = Mouse.GetState();
            KeyboardState keyboardState = Keyboard.GetState();

            //Quitter
            if (keyboardState.IsKeyDown(Keys.Escape)) this.Exit();

            // TODO: Add your update logic here
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                game.GetTanks()[0].Move(Direction.TOP);
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                game.GetTanks()[0].Move(Direction.BOT);
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                game.GetTanks()[0].Move(Direction.LEFT);
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                game.GetTanks()[0].Move(Direction.RIGHT);
            }

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                game.GetTanks()[0].Bomb();
            }

            List<Wall> walls = explodedWalls.Keys.ToList<Wall>();
            foreach (Wall w in walls)
            {
                if (game.timer.ElapsedMilliseconds - explodedWalls[w] >= 300) explodedWalls.Remove(w);
            }

            List<int[]> tiles = explodedTiles.Keys.ToList<int[]>();
            foreach (int[] t in tiles)
            {
                if (game.timer.ElapsedMilliseconds - explodedTiles[t] >= 200) explodedTiles.Remove(t);
            }

            List<Bomb> toRemove = new List<Bomb>();
            foreach (Bomb bomb in game.GetBombs())
            {
                if (game.timer.ElapsedMilliseconds - bomb.Placed >= bomb.Delay)
                {
                    foreach (int[] t in bomb.getInvolvedPositions()) explodedTiles.Add(t, game.timer.ElapsedMilliseconds);

                    List<Entity> exploded = bomb.Explode();

                    foreach (Wall w in exploded) explodedWalls.Add(w, game.timer.ElapsedMilliseconds);

                    toRemove.Add(bomb);
                }
            }
            foreach (Bomb b in toRemove) game.GetBombs().Remove(b);


            //LAISSER ABSOLUEMENT
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            
            // TODO: Add your drawing code here
            int width = GraphicsDevice.Viewport.Width;
            int height = GraphicsDevice.Viewport.Height;



            spriteBatch.Begin();

            Rectangle position = new Rectangle();

            position.Width = width;
            position.Height = height;
            position.X = position.Y = 0;

            spriteBatch.Draw(background, position, Color.White);

            
            position.Width = width / game.Width;
            position.Height = height / game.Height;

            foreach (Wall wall in game.GetWalls())
            {
                position.X = wall.X * position.Width;
                position.Y = wall.Y * position.Height;

                if (wall.Breakable) spriteBatch.Draw(this.wall, position, Color.White);
                else spriteBatch.Draw(this.wall, position, Color.Orange);
            }
            
            foreach (Wall w in explodedWalls.Keys)
            {
                if (game.timer.ElapsedMilliseconds - explodedWalls[w] >= 300) continue;

                position.X = w.X * position.Width;
                position.Y = w.Y * position.Height;

                Color color = Color.White;
                color.G = color.B = (byte)((300 - game.timer.ElapsedMilliseconds + explodedWalls[w]) / 2);

                spriteBatch.Draw(wall, position, color);
            }

            Rectangle bombPosition = new Rectangle();
            foreach (Bomb bomb in game.GetBombs())
            {
                double bombRatio = 0.5 + ((double)Math.Abs(25 - ((game.timer.ElapsedMilliseconds - bomb.Placed) % 2000) / 40)) / 50.0;

                bombPosition.Width = (int)(bombRatio * position.Width);
                bombPosition.Height = (int)(bombRatio * position.Height);

                if (bombPosition.Width % 2 == 0) bombPosition.Width++;
                if (bombPosition.Height % 2 == 0) bombPosition.Height++;

                position.X = bomb.X * position.Width;
                position.Y = bomb.Y * position.Height;

                bombPosition.X = position.Center.X - bombPosition.Width / 2;
                bombPosition.Y = position.Center.Y - bombPosition.Height / 2;

                spriteBatch.Draw(this.bomb, bombPosition, Color.White);
            }

            foreach (Tank tank in game.GetTanks())
            {
                if (tank.LastMovement + tank.MovementDuration <= game.timer.ElapsedMilliseconds)
                {
                    position.X = tank.X * position.Width;
                    position.Y = tank.Y * position.Height;
                }
                else
                {
                    double ratio = ((double)(game.timer.ElapsedMilliseconds - tank.LastMovement)) / tank.MovementDuration;
                    position.X = (int) ((((double)tank.X - tank.Direction.GetModX()) + ((double)tank.Direction.GetModX()) * ratio) * (double)position.Width);
                    position.Y = (int) ((((double)tank.Y - tank.Direction.GetModY()) + ((double)tank.Direction.GetModY()) * ratio) * (double)position.Height);
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

                Vector2 origin = new Vector2(tank1.Width / 2, tank1.Height / 2);
                //petit fix (ça commence...)
                position.Location = position.Center;

                spriteBatch.Draw(tank1, position, null, Color.White, rotation, origin, SpriteEffects.None, 0);
            }

            foreach (int[] t in explodedTiles.Keys)
            {
                Rectangle explosionPosition = new Rectangle();
                explosionPosition.X = t[0] * position.Width;
                explosionPosition.Y = t[1] * position.Height;

                explosionPosition.Y += (int)(0.15 * position.Height);
                explosionPosition.Width = position.Width;
                explosionPosition.Height = (int)(0.7 * position.Height);

                Vector2 origin = new Vector2(explosion.Width / 2, explosion.Height / 2);
                explosionPosition.Location = explosionPosition.Center;

                if (t[2] == (int)Direction.TOP)
                {
                    spriteBatch.Draw(explosion, explosionPosition, null, Color.White, (float)Math.PI / 2, origin, SpriteEffects.None, 0);
                }
                else if (t[2] == (int)Direction.LEFT)
                {
                    spriteBatch.Draw(explosion, explosionPosition, null, Color.White, 0, origin, SpriteEffects.None, 0);
                }
            }

            spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
