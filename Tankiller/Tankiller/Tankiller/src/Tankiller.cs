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
                game.Tank1.Direction = src.Direction.TOP;
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                game.Tank1.Direction = src.Direction.BOT;
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                game.Tank1.Direction = src.Direction.LEFT;
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                game.Tank1.Direction = src.Direction.RIGHT;
            }


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
            position.Width = width / game.Width;
            position.Height = height / game.Height;

            for (int i = 0; i < game.Width; ++i)
            {
                for (int j = 0; j < game.Height; ++j)
                {
                    Entity e = game.getEntity(i, j);


                    if (e == null) continue;

                    position.X = i * position.Width;
                    position.Y = j * position.Height;

                    if (e is Wall)
                    {
                        spriteBatch.Draw(wall, position, Color.White);
                    }
                    else if (e is Tank)
                    {
                        float rotation = 0;
                        switch (((Tank)e).Direction)
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
                }
            }

            spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
