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

namespace Pong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameManager gameManager = new GameManager();
        Stats stats = new Stats();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Initialize common Assets
            Assets.init(graphics.GraphicsDevice);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Load Fonts
            Assets.MenuFont = Content.Load<SpriteFont>("menuFont");

            //Load Textures
            Assets.PongBall = Content.Load<Texture2D>("PongBall");
            Assets.Player = Content.Load<Texture2D>("Player");
            Assets.Midline = Content.Load<Texture2D>("Midline");
            Assets.Life = Content.Load<Texture2D>("Heart");
            Assets.TitleGraphic = Content.Load<Texture2D>("Title-Screen");

            //Load Audio
            Assets.Audio.Death = Content.Load<SoundEffect>("Audio/Death");
            Assets.Audio.HitSound = Content.Load<SoundEffect>("Audio/HitSound");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload all content
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
            //Update input devices
            InputState.update();
            
            //Update playtime for stats
            stats.updateGameTime(gameTime.TotalGameTime.Seconds);

            gameManager.update(graphics.GraphicsDevice);
            //Close the game if exit is clicked in the main menu
            if (gameManager.checkToQuit())
            {
                Exit();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Assets.Colors.DimmGreen);
            gameManager.draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
