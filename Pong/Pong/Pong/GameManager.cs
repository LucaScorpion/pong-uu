using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class GameManager
    {
        #region Fields
        GameState gameState = GameState.StartScreen;
        Keys startButton = Keys.Space;
        PlayerManager playerManager = new PlayerManager();
        List<PongBall> pongBalls = new List<PongBall>();
        #endregion

        #region Methods
        public void update(GraphicsDevice g)
        {
            switch (gameState)
            {
                case GameState.StartScreen:
                    if (InputState.isKeyPressed(startButton))
                    {
                        startGame(g);

                    }
                    break;
                case GameState.Playing:
                    //Update playermanager
                    playerManager.update(g);
                    
                    //Update all balls
                    foreach (PongBall b in pongBalls)
                    {
                        b.update(g);
                    }
                    break;
                case GameState.GameOver:
                    break;
                case GameState.Menu:
                    break;
            }
        }
        public void draw(SpriteBatch s)
        {
            s.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
            switch (gameState)
            {
                case GameState.StartScreen:
                    drawMenu(s);
                    break;
                case GameState.Playing:
                    //Draw midline
                    s.Draw(Assets.Midline, new Rectangle(s.GraphicsDevice.Viewport.Width / 2 - Assets.Midline.Width / 2, 0, Assets.Midline.Width, s.GraphicsDevice.Viewport.Height), new Rectangle(0, 0, Assets.Midline.Width, s.GraphicsDevice.Viewport.Height), Assets.Colors.ShadyGreen);

                    //Draw players
                    playerManager.draw(s);

                    //Draw balls
                    foreach (PongBall b in pongBalls)
                    {
                        b.draw(s);
                    }
                    break;
                case GameState.GameOver:
                    break;
                case GameState.Menu:
                    break;
            }
            s.End();
        }
        public void drawMenu(SpriteBatch s)
        {
            //Draw Menu text 
            String msg = "Press Space to start the game...";
            s.DrawString(Assets.MenuFont, msg, new Vector2(s.GraphicsDevice.Viewport.Width / 2 - Assets.MenuFont.MeasureString(msg).X / 2, s.GraphicsDevice.Viewport.Height / 2 - Assets.MenuFont.MeasureString(msg).Y / 2), Color.Green);
        }
        public void drawUI(SpriteBatch s)
        {

        }
        public void startGame(GraphicsDevice g)
        {
            playerManager = new PlayerManager();

            //Create players
            Player p1 = new Player(new Rectangle(10, 0, 20, 100), Assets.Colors.FlashyGreen, 10);
            p1.setControls(Keys.Q, Keys.A);
            p1.MovementVector = new Vector2(0, 10);
            playerManager.addPlayer(p1, 1);

            Player p2 = new Player(new Rectangle(g.Viewport.Width - 30, 0, 20, 100), Assets.Colors.FlashyGreen, 10);
            p2.setControls(Keys.O, Keys.L);
            p2.MovementVector = new Vector2(0, 10);
            playerManager.addPlayer(p2, 2);

            //Spawn a ball
            PongBall ball = new PongBall(new Rectangle(0,0,10,10),Vector2.Zero);
            ball.create(g);
            pongBalls.Add(ball);

            //Change state to start game
            gameState = GameState.Playing;
        }
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion


    }
    public enum GameState
    {
        Playing,
        Menu,
        GameOver,
        StartScreen
    }
}
