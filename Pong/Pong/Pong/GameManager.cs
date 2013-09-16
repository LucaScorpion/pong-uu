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
        MouseState mouseState;
        int selected = 0;
        int rectYOffset = 3;
        String sp = "1 player";
        String mp = "2 players";
        #endregion

        #region Methods
        public void update(GraphicsDevice g)
        {
            switch (gameState)
            {
                case GameState.StartScreen:
                    if (InputState.isKeyPressed(startButton))
                    {
                        gameState = GameState.Menu;
                    }
                    break;
                case GameState.Playing:
                    //Update playermanager
                    playerManager.update(g, pongBall);
                    
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
                    mouseState = Mouse.GetState();
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (mouseState.Y > Assets.TitleGraphic.Height + 3 * Assets.MenuFont.MeasureString(sp).Y - 3 && mouseState.Y < Assets.TitleGraphic.Height + 4 * Assets.MenuFont.MeasureString(sp).Y - rectYOffset)
                        {
                            startGame(GameMode.Singleplayer, g);
                        }
                        else if (mouseState.Y > Assets.TitleGraphic.Height + 5 * Assets.MenuFont.MeasureString(mp).Y - 3 && mouseState.Y < Assets.TitleGraphic.Height + 6 * Assets.MenuFont.MeasureString(mp).Y - rectYOffset)
                        {
                            startGame(GameMode.Multiplayer, g);
                        }
                    }
                    if (mouseState.Y > Assets.TitleGraphic.Height + 3 * Assets.MenuFont.MeasureString(sp).Y - 3 && mouseState.Y < Assets.TitleGraphic.Height + 4 * Assets.MenuFont.MeasureString(sp).Y - rectYOffset)
                    {
                        selected = 1;
                    }
                    else if (mouseState.Y > Assets.TitleGraphic.Height + 5 * Assets.MenuFont.MeasureString(mp).Y - 3 && mouseState.Y < Assets.TitleGraphic.Height + 6 * Assets.MenuFont.MeasureString(mp).Y - rectYOffset)
                    {
                        selected = 2;
                    }
                    else
                    {
                        selected = 0;
                    }
                    break;
            }
        }
        public void draw(SpriteBatch s)
        {
            
            switch (gameState)
            {
                case GameState.StartScreen:
                    s.Begin();
                    drawStartScreen(s);
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
                    s.Begin();
                    drawMenu(s);
                    s.End();
                    break;
            }
            
        }
        public void drawStartScreen(SpriteBatch s)
        {
            //Draw logo
            s.Draw(Assets.TitleGraphic, new Rectangle((s.GraphicsDevice.Viewport.Width - Assets.TitleGraphic.Width) / 2, (s.GraphicsDevice.Viewport.Height - Assets.TitleGraphic.Height) / 2 - 100, Assets.TitleGraphic.Width, Assets.TitleGraphic.Height), Color.White);
            //Draw Startscreen text 
            String msg = "Press " + startButton + " to start the game...";
            s.DrawString(Assets.MenuFont, msg, new Vector2(s.GraphicsDevice.Viewport.Width / 2 - Assets.MenuFont.MeasureString(msg).X / 2, s.GraphicsDevice.Viewport.Height / 2 - Assets.MenuFont.MeasureString(msg).Y / 2 + 50), Color.Green);
        }
        public void drawMenu(SpriteBatch s)
        {
            //Draw logo
            s.Draw(Assets.TitleGraphic, new Rectangle((s.GraphicsDevice.Viewport.Width - Assets.TitleGraphic.Width) / 2, (s.GraphicsDevice.Viewport.Height - Assets.TitleGraphic.Height) / 2 - 100, Assets.TitleGraphic.Width, Assets.TitleGraphic.Height), Color.White);
            //Draw rectangle around selected text
            if (selected == 1)
            {
                s.Draw(Assets.DummyTexture, new Rectangle(0, (int)(Assets.TitleGraphic.Height + 3 * Assets.MenuFont.MeasureString(sp).Y - rectYOffset), s.GraphicsDevice.Viewport.Width, (int)(Assets.MenuFont.MeasureString(sp).Y)), Assets.Colors.ExplodingGreen);
            }
            else if (selected == 2)
            {
                s.Draw(Assets.DummyTexture, new Rectangle(0, (int)(Assets.TitleGraphic.Height + 5 * Assets.MenuFont.MeasureString(mp).Y - rectYOffset), s.GraphicsDevice.Viewport.Width, (int)(Assets.MenuFont.MeasureString(mp).Y)), Assets.Colors.ExplodingGreen);
            }
            //Draw Menu text
            s.DrawString(Assets.MenuFont, sp, new Vector2(s.GraphicsDevice.Viewport.Width / 2 - Assets.MenuFont.MeasureString(sp).X / 2, Assets.TitleGraphic.Height + 3 * Assets.MenuFont.MeasureString(sp).Y), Color.Green);
            s.DrawString(Assets.MenuFont, mp, new Vector2(s.GraphicsDevice.Viewport.Width / 2 - Assets.MenuFont.MeasureString(mp).X / 2, Assets.TitleGraphic.Height + 5 * Assets.MenuFont.MeasureString(mp).Y), Color.Green);
        }
        public void drawUI(SpriteBatch s)
        {

        }
        public void startGame(GameMode gameMode, GraphicsDevice g)
        {
            playerManager = new PlayerManager();

            if (gameMode == GameMode.Multiplayer)
            {
                //Create players
                Player p1 = new Player(new Rectangle(10, g.Viewport.Height / 2 - 50, 20, 100), Assets.Colors.FlashyGreen, 3);
                p1.setControls(Keys.W,Keys.S);
                playerManager.addPlayer(p1, 1);

                Player p2 = new Player(new Rectangle(g.Viewport.Width - 30, g.Viewport.Height / 2 - 50, 20, 100), Assets.Colors.FlashyGreen, 3);
                p2.setControls(Keys.Up, Keys.Down);
                playerManager.addPlayer(p2, 2);
            }
            else if (gameMode == GameMode.Singleplayer)
            {
                //Create players
                Player p1 = new Player(new Rectangle(10, g.Viewport.Height / 2 - 50, 20, 100), Assets.Colors.FlashyGreen, 3);
                p1.setControls(ControlMode.Ai);
                playerManager.addPlayer(p1, 1);

                Player p2 = new Player(new Rectangle(g.Viewport.Width - 30, g.Viewport.Height / 2 - 50, 20, 100), Assets.Colors.FlashyGreen, 3);
                p2.setControls(Keys.Up, Keys.Down);
                playerManager.addPlayer(p2, 2);
            }

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
    public enum GameMode
    {
        Singleplayer,
        Multiplayer
    }
}
