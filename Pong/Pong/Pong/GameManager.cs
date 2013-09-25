using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    /// <summary>
    /// This class is the main manager of the game. It handles gamestate, gamemode and calls the actions to the game.
    /// </summary>
    public class GameManager
    {
        #region Fields
        static GameState gameState = GameState.StartScreen;
        Keys startButton = Keys.Space;
        Keys pauseButton = Keys.Escape;
        PlayerManager playerManager = new PlayerManager();
        PongBall pongBall = new PongBall();
        int menuSelected = 0;
        int pauseSelected = 0;
        int statsSelected = 0;
        int rectYOffset = 3;
        bool checkQuitClicked = false;
        String sp = "1 player";
        String mp = "2 players";
        String quit = "Exit game";
        String paused = "Game paused";
        String pausedContinue = "Continue";
        String toMenu = "Back to main menu";
        public static bool playing = false;
        float playerDead = 0;
        #endregion

        #region Methods
        /// <summary>
        /// Update the game manager.
        /// This method has to be called every loop. It updates a part of the game after determining the gamestate.
        /// </summary>
        /// <param name="g">GraphicsDevice used for screen size calculations.</param>
        public void update(GraphicsDevice g)
        {
            //Check what the gamestate is.
            switch (gameState)
            {
                case GameState.StartScreen: //The game is on the startscreen. Not in the menu yet.
                    if (InputState.isKeyPressed(startButton))
                    {
                        //If the startbutton is pressed, the game will move on to the menu.
                        gameState = GameState.Menu;
                    }
                    break;
                case GameState.Playing: //The game is playing. All game logic is done here. 
                    //Update playermanager
                    playerManager.update(g, pongBall);
                    //Update the ball
                    pongBall.Update(g);

                    //Collide players to ball
                    pongBall.CollideToPlayer(playerManager.playerOne);
                    pongBall.CollideToPlayer(playerManager.playerTwo);

                    //Kill ball if outside screen (point scored)
                    if (pongBall.Position.X > g.Viewport.Width)
                    {
                        //Player 2 loses life
                        playerManager.playerTwo.Lives--;
                        pongBall.Create(g);
                        Assets.Audio.Death.Play();

                        //Reset players
                        playerManager.reset(g);
                    }
                    else if (pongBall.Position.X < 0)
                    {
                        //Player 1 loses life
                        playerManager.playerOne.Lives--;
                        pongBall.Create(g);
                        Assets.Audio.Death.Play();
                        //Reset players
                        playerManager.reset(g);
                    }

                    //Update particles
                    ParticleManager.update(g);

                    //Pause game
                    if (InputState.isKeyPressed(pauseButton))
                    {
                        gameState = GameState.Paused;
                        pongBall.Pause();
                    }
                    //Game Over when a player is dead
                    if (playerManager.playerOne.Lives <= 0)
                    {
                        gameState = GameState.GameOver;
                        playerDead = 0;
                        playing = false;
                        //Kill ball
                        pongBall = null;
                    }
                    else if (playerManager.playerTwo.Lives <= 0)
                    {
                        gameState = GameState.GameOver;
                        playerDead = 1;
                        playing = false;
                        //Kill ball
                        pongBall = null;
                    }
                    else
                    {
                        playing = true;
                    }
                    break;
                case GameState.Paused:
                    //Unpause game
                    if (InputState.isKeyPressed(pauseButton))
                    {
                        gameState = GameState.Playing;
                    }
                    //Highlight text when hovering over it
                    if (InputState.currentMouse.Y > g.Viewport.Height / 3 + 2 * Assets.MenuFont.MeasureString(pausedContinue).Y - rectYOffset && InputState.currentMouse.Y < g.Viewport.Height / 3 + 3 * Assets.MenuFont.MeasureString(pausedContinue).Y - rectYOffset)
                    {
                        pauseSelected = 1;
                    }
                    else if (InputState.currentMouse.Y > g.Viewport.Height / 3 + 4 * Assets.MenuFont.MeasureString(toMenu).Y - rectYOffset && InputState.currentMouse.Y < g.Viewport.Height / 3 + 5 * Assets.MenuFont.MeasureString(toMenu).Y - rectYOffset)
                    {
                        pauseSelected = 2;
                    }
                    else
                    {
                        pauseSelected = 0;
                    }
                    //Check for clicking
                    if (InputState.leftClick())
                    {
                        if (InputState.currentMouse.Y > g.Viewport.Height / 3 + 2 * Assets.MenuFont.MeasureString(pausedContinue).Y - rectYOffset && InputState.currentMouse.Y < g.Viewport.Height / 3 + 3 * Assets.MenuFont.MeasureString(pausedContinue).Y - rectYOffset)
                        {
                            gameState = GameState.Playing;
                        }
                        else if (InputState.currentMouse.Y > g.Viewport.Height / 3 + 4 * Assets.MenuFont.MeasureString(toMenu).Y - rectYOffset && InputState.currentMouse.Y < g.Viewport.Height / 3 + 5 * Assets.MenuFont.MeasureString(toMenu).Y - rectYOffset)
                        {
                            gameState = GameState.Menu;
                        }
                        pauseSelected = 0;
                    }
                    break;
                case GameState.GameOver:
                    
                    //Check for clicking
                    if (InputState.leftClick() && statsSelected == 1)
                    {
                        gameState = GameState.Menu;
                    }
                    break;
                case GameState.Menu:
                    //Highlight text when hovering over it
                    if (InputState.currentMouse.Y > g.Viewport.Height / 2 + 50 && InputState.currentMouse.Y < g.Viewport.Height / 2 + 80)
                    {
                        menuSelected = 1;
                    }
                    else if (InputState.currentMouse.Y > g.Viewport.Height / 2 + 80 && InputState.currentMouse.Y < g.Viewport.Height / 2 + 110)
                    {
                        menuSelected = 2;
                    }
                    else if (InputState.currentMouse.Y > g.Viewport.Height / 2 + 110 && InputState.currentMouse.Y < g.Viewport.Height / 2 + 140)
                    {
                        menuSelected = 3;
                    }
                    else
                    {
                        menuSelected = 0;
                    }
                    //Check for clicking
                    if (InputState.leftClick())
                    {
                        if (menuSelected == 1)
                        {
                            //Start a singleplayer game, clear stats
                            startGame(GameMode.Singleplayer, g);
                            Stats.clearStats();
                        }
                        else if (menuSelected == 2)
                        {
                            //Start a multiplayer game, clear stats
                            startGame(GameMode.Multiplayer, g);
                            Stats.clearStats();
                        }
                        else if (menuSelected == 3)
                        {
                            checkQuitClicked = true;
                        }
                        menuSelected = 0;
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

                    //If playing or game over
                case GameState.GameOver:
                case GameState.Playing:
                    //Spritebatch for active game
                    s.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
                    //Draw midline
                    s.Draw(Assets.Midline, new Rectangle(s.GraphicsDevice.Viewport.Width / 2 - Assets.Midline.Width / 2, 0, Assets.Midline.Width, s.GraphicsDevice.Viewport.Height), new Rectangle(0, 0, Assets.Midline.Width, s.GraphicsDevice.Viewport.Height), Assets.Colors.ShadyGreen);

                    //Draw players
                    playerManager.draw(s);

                    //Game over logic
                    if (gameState == GameState.GameOver)
                    {
                        Stats.Draw(s, playerDead);
                    }
                    else
                    {
                        //Draw ball if game is not over
                        pongBall.Draw(s);
                        //Draw particles if not game over
                        ParticleManager.draw(s);
                    }
                    
                    s.End();
                    break;
                case GameState.Paused:
                    s.Begin();
                    drawPauseMenu(s);
                    s.End();
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
            s.Draw(Assets.TitleGraphic, new Rectangle(s.GraphicsDevice.Viewport.Width / 2 - Assets.TitleGraphic.Width / 2, s.GraphicsDevice.Viewport.Height / 2 - Assets.TitleGraphic.Height / 2, Assets.TitleGraphic.Width, Assets.TitleGraphic.Height), Color.White);
            //Draw Startscreen text 
            String msg = "Press " + startButton + " to start the game...";
            s.DrawString(Assets.MenuFont, msg, new Vector2(s.GraphicsDevice.Viewport.Width / 2 - 350, s.GraphicsDevice.Viewport.Height / 2 + 50), Assets.Colors.LuminantGreen);
        }
        public void drawMenu(SpriteBatch s)
        {
            //Draw logo
            s.Draw(Assets.TitleGraphic, new Rectangle(s.GraphicsDevice.Viewport.Width / 2 - Assets.TitleGraphic.Width / 2, s.GraphicsDevice.Viewport.Height / 2 - Assets.TitleGraphic.Height / 2, Assets.TitleGraphic.Width, Assets.TitleGraphic.Height), Color.White);
            //Highlight selected text
            if (menuSelected == 1)
            {
                s.Draw(Assets.DummyTexture, new Rectangle(s.GraphicsDevice.Viewport.Width / 2 - 350 - rectYOffset, s.GraphicsDevice.Viewport.Height / 2 + 50 - rectYOffset, 380, (int)(Assets.MenuFont.MeasureString(sp).Y + rectYOffset)), Assets.Colors.ShadyGreen);
            }
            else if (menuSelected == 2)
            {
                s.Draw(Assets.DummyTexture, new Rectangle(s.GraphicsDevice.Viewport.Width / 2 - 350 - rectYOffset, s.GraphicsDevice.Viewport.Height / 2 + 80 - rectYOffset, 380, (int)(Assets.MenuFont.MeasureString(sp).Y + rectYOffset)), Assets.Colors.ShadyGreen);
            }
            else if (menuSelected == 3)
            {
                s.Draw(Assets.DummyTexture, new Rectangle(s.GraphicsDevice.Viewport.Width / 2 - 350 - rectYOffset, s.GraphicsDevice.Viewport.Height / 2 + 110 - rectYOffset, 380, (int)(Assets.MenuFont.MeasureString(sp).Y + rectYOffset)), Assets.Colors.ShadyGreen);
            }
            //Draw Menu text
            s.DrawString(Assets.MenuFont, sp, new Vector2(s.GraphicsDevice.Viewport.Width / 2 - 350, s.GraphicsDevice.Viewport.Height / 2  + 50), Assets.Colors.LuminantGreen);
            s.DrawString(Assets.MenuFont, mp, new Vector2(s.GraphicsDevice.Viewport.Width / 2 - 350, s.GraphicsDevice.Viewport.Height / 2 + 80), Assets.Colors.LuminantGreen);
            s.DrawString(Assets.MenuFont, quit, new Vector2(s.GraphicsDevice.Viewport.Width / 2 - 350, s.GraphicsDevice.Viewport.Height / 2 + 110), Assets.Colors.LuminantGreen);
        }
        public void drawPauseMenu(SpriteBatch s)
        {
            //Draw paused text
            s.DrawString(Assets.MenuFont, paused, new Vector2(s.GraphicsDevice.Viewport.Width / 2 - Assets.MenuFont.MeasureString(paused).X / 2, s.GraphicsDevice.Viewport.Height / 3), Assets.Colors.FlashyGreen);
            //Highlight selected text
            if (pauseSelected == 1)
            {
                s.Draw(Assets.DummyTexture, new Rectangle(0, (int)(s.GraphicsDevice.Viewport.Height / 3 + 2 * Assets.MenuFont.MeasureString(pausedContinue).Y - rectYOffset), s.GraphicsDevice.Viewport.Width, (int)(Assets.MenuFont.MeasureString(pausedContinue).Y)), Assets.Colors.ExplodingGreen);
            }
            else if (pauseSelected == 2)
            {
                s.Draw(Assets.DummyTexture, new Rectangle(0, (int)(s.GraphicsDevice.Viewport.Height / 3 + 4 * Assets.MenuFont.MeasureString(toMenu).Y - rectYOffset), s.GraphicsDevice.Viewport.Width, (int)(Assets.MenuFont.MeasureString(toMenu).Y)), Assets.Colors.ExplodingGreen);
            }
            //Draw pause menu text
            s.DrawString(Assets.MenuFont, pausedContinue, new Vector2(s.GraphicsDevice.Viewport.Width / 2 - Assets.MenuFont.MeasureString(pausedContinue).X / 2, s.GraphicsDevice.Viewport.Height / 3 + 2 * Assets.MenuFont.MeasureString(pausedContinue).Y), Color.Green);
            s.DrawString(Assets.MenuFont, toMenu, new Vector2(s.GraphicsDevice.Viewport.Width / 2 - Assets.MenuFont.MeasureString(toMenu).X / 2, s.GraphicsDevice.Viewport.Height / 3 + 4 * Assets.MenuFont.MeasureString(toMenu).Y), Color.Green);
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
            pongBall.Create(g);

            //Change state to start game
            gameState = GameState.Playing;
        }
        //Exit game
        public bool checkToQuit()
        {
            if (checkQuitClicked == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Constructors
        #endregion

        #region Properties
        public static GameState CurrentGameState { get { return gameState; } set { gameState = value; } }
        #endregion


    }
    public enum GameState
    {
        Playing,
        Paused,
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
