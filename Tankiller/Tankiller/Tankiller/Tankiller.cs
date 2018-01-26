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

        private Vector2 mouse_location;

        private Texture2D tank;

        public Tankiller()
        {
            graphics = new GraphicsDeviceManager(this);
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

			//taille fenetre
            graphics.PreferredBackBufferWidth = 720;
            graphics.PreferredBackBufferWidth = 480;

			//visibilité souris
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
            //tank = this.Content.Load<Texture2D>("tank");
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
            // Allows the game to exit
            KeyboardState keyboard_state = Keyboard.GetState();
            if (keyboard_state.IsKeyDown(Keys.Escape)) this.Exit();

            // TODO: Add your update logic here
            if (keyboard_state.IsKeyDown(Keys.Left))
            {
                //Joueur2 gauche
            }
            else if (keyboard_state.IsKeyDown(Keys.Right))
            {
                //Joueur2 droite
            }
            else if (keyboard_state.IsKeyDown(Keys.Up))
            {
                //Joueur2 haut
            }
            else if (keyboard_state.IsKeyDown(Keys.Down))
            {
                //Joueur2 bas
            }

            if (keyboard_state.IsKeyDown(Keys.Q))
            {
                //Joueur1 gauche
            }
            else if (keyboard_state.IsKeyDown(Keys.D))
            {
                //Joueur1 droite
            }
            else if (keyboard_state.IsKeyDown(Keys.Z))
            {
                //Joueur1 haut
            }
            else if (keyboard_state.IsKeyDown(Keys.S))
            {
                //Joueur1 bas
            }



            MouseState mouse_state = Mouse.GetState();

            if (mouse_state.LeftButton == ButtonState.Pressed)
            {
				
            }


			//A LAISSER (par defaut)
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
            //spriteBatch.Begin();
            //spritBatch.draw
            //spriteBatch.End();

            //Console.WriteLine(Tank.getTank().getX());

            base.Draw(gameTime);
        }
    }
}