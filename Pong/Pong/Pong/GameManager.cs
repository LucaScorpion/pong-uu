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
        PongBall pongBall = new PongBall();
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
                    
                    pongBall.update(g);
                    //Collide players to ball
                    pongBall.collideToPlayer(playerManager.playerOne);
                    pongBall.collideToPlayer(playerManager.playerTwo);
                    //Kill ball if outside screen
                    if (pongBall.Position.X > g.Viewport.Width)
                    {
                        //Player 2 loses life
                        playerManager.playerTwo.Lives--;
                        pongBall.create(g);
                        Assets.Audio.Death.Play();
                    }
                    else if (pongBall.Position.X < 0)
                    {
                        //Player 1 loses life
                        playerManager.playerOne.Lives--;
                        pongBall.create(g);
                        Assets.Audio.Death.Play();
                    }


                    //Update particles
                    ParticleManager.update(g);
                    break;
                case GameState.GameOver:
                    break;
                case GameState.Menu:
                    break;
            }
        }
        public void draw(SpriteBatch s)
        {
            
            switch (gameState)
            {
                case GameState.StartScreen:
                    s.Begin();
                    drawMenu(s);
                    s.End();
                    break;
                case GameState.Playing:
                    //draw particles
                    ParticleManager.draw(s);

                    //Spritebatch for active game
                    s.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
                    //Draw midline
                    s.Draw(Assets.Midline, new Rectangle(s.GraphicsDevice.Viewport.Width / 2 - Assets.Midline.Width / 2, 0, Assets.Midline.Width, s.GraphicsDevice.Viewport.Height), new Rectangle(0, 0, Assets.Midline.Width, s.GraphicsDevice.Viewport.Height), Assets.Colors.ShadyGreen);

                    //Draw players
                    playerManager.draw(s);

                    //Draw ball
                    pongBall.draw(s);
                    s.End();

                    break;
                case GameState.GameOver:
                    break;
                case GameState.Menu:
                    break;
            }
            
        }
        public void drawMenu(SpriteBatch s)
        {
            //Draw Menu text 
            String msg = "Press " + startButton + " to start the game...";
            s.DrawString(Assets.MenuFont, msg, new Vector2(s.GraphicsDevice.Viewport.Width / 2 - Assets.MenuFont.MeasureString(msg).X / 2, s.GraphicsDevice.Viewport.Height / 2 - Assets.MenuFont.MeasureString(msg).Y / 2), Color.Green);
        }
        public void drawUI(SpriteBatch s)
        {

        }
        public void startGame(GraphicsDevice g)
        {
            playerManager = new PlayerManager();

            //Create players
            Player p1 = new Player(new Rectangle(10, g.Viewport.Height / 2 - 50, 20, 100), Assets.Colors.FlashyGreen, 3);
            p1.setControls(Keys.W, Keys.S);
            playerManager.addPlayer(p1, 1);

            Player p2 = new Player(new Rectangle(g.Viewport.Width - 30, g.Viewport.Height / 2 - 50, 20, 100), Assets.Colors.FlashyGreen, 3);
            p2.setControls(Keys.Up, Keys.Down);
            playerManager.addPlayer(p2, 2);

            //Spawn a ball
            pongBall = new PongBall();
            pongBall.create(g);

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
