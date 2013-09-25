using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class Stats
    {
        #region Fields
        public static int totalBounces = 0;
        public static int totalCurveballs = 0;
        static int startPlaytime;
        public static int totalPlaytimeSeconds = 0;
        public static int totalPlaytimeMinutes = 0;
        static int previoust;
        static int rectYOffset = 3;
        static String stats = "Stats:";
        static String bounces = "Total bounces:";
        static String curveballs = "Total curveballs:";
        static String playtime = "Total playtime:";
        static String toMenu = "Back to start screen";
        
        static int statsTextOffset = 10;
        static int statsNumbersOffset = 250;
        #endregion

        #region Methods
        //Add a bounce
        public static void addBounce()
        {
            totalBounces++;
        }

        //Add a curveball
        public static void addCurveball()
        {
            totalCurveballs++;
        }

        public void updateGameTime(int t)
        {
            if (GameManager.playing == false)
            {
                //Set the starting playtime
                startPlaytime = t;
                previoust = t - 1;
            }
            else if (GameManager.playing == true)
            {
                //If a second has passed
                if (t != previoust)
                {
                    //Add a second
                    totalPlaytimeSeconds++;
                    //If 60 seconds have passed
                    if (totalPlaytimeSeconds == 60)
                    {
                        //Add a minute, reset seconds
                        totalPlaytimeMinutes++;
                        totalPlaytimeSeconds = 0;
                    }
                    previoust = t;
                }
            }
        }
        public static void Draw(SpriteBatch s, float d)
        {
            //Highlight toMenu text when hovering over it
            if (InputState.currentMouse.Y > Assets.MenuFont.MeasureString(toMenu).Y * 15 - rectYOffset && InputState.currentMouse.Y < Assets.MenuFont.MeasureString(toMenu).Y * 16 - rectYOffset && InputState.currentMouse.X > s.GraphicsDevice.Viewport.Width * (d / 2) && InputState.currentMouse.X < s.GraphicsDevice.Viewport.Width * (d / 2 + 0.5))
            {
                s.Draw(Assets.DummyTexture, new Rectangle((int)(s.GraphicsDevice.Viewport.Width * (d / 2)), (int)(Assets.MenuFont.MeasureString(toMenu).Y * 15 - rectYOffset), s.GraphicsDevice.Viewport.Width / 2, (int)(Assets.MenuFont.MeasureString(toMenu).Y)), Assets.Colors.ExplodingGreen);
                if (InputState.leftClick())
                {
                    GameManager.CurrentGameState = GameState.StartScreen;
                }
            }
            //Draw text
            s.DrawString(Assets.MenuFont, stats, new Vector2(s.GraphicsDevice.Viewport.Width * (d / 2) + statsTextOffset, Assets.MenuFont.MeasureString(stats).Y), Assets.Colors.FlashyGreen);
            s.DrawString(Assets.MenuFont, bounces, new Vector2(s.GraphicsDevice.Viewport.Width * (d / 2) + statsTextOffset, Assets.MenuFont.MeasureString(bounces).Y * 3), Assets.Colors.FlashyGreen);
            s.DrawString(Assets.MenuFont, curveballs, new Vector2(s.GraphicsDevice.Viewport.Width * (d / 2) + statsTextOffset, Assets.MenuFont.MeasureString(curveballs).Y * 5), Assets.Colors.FlashyGreen);
            s.DrawString(Assets.MenuFont, playtime, new Vector2(s.GraphicsDevice.Viewport.Width * (d / 2) + statsTextOffset, Assets.MenuFont.MeasureString(playtime).Y * 7), Assets.Colors.FlashyGreen);
            s.DrawString(Assets.MenuFont, toMenu, new Vector2(s.GraphicsDevice.Viewport.Width * (d / 2) + statsTextOffset, Assets.MenuFont.MeasureString(toMenu).Y * 15), Color.Green);
            //Draw stats
            s.DrawString(Assets.MenuFont, Stats.totalBounces.ToString(), new Vector2(s.GraphicsDevice.Viewport.Width * (d / 2) + statsNumbersOffset, Assets.MenuFont.MeasureString(bounces).Y * 3), Assets.Colors.FlashyGreen);
            s.DrawString(Assets.MenuFont, Stats.totalCurveballs.ToString(), new Vector2(s.GraphicsDevice.Viewport.Width * (d / 2) + statsNumbersOffset, Assets.MenuFont.MeasureString(curveballs).Y * 5), Assets.Colors.FlashyGreen);
            s.DrawString(Assets.MenuFont, Stats.totalPlaytimeMinutes.ToString() + " m. " + Stats.totalPlaytimeSeconds.ToString() + " s.", new Vector2(s.GraphicsDevice.Viewport.Width * (d / 2) + statsNumbersOffset, Assets.MenuFont.MeasureString(playtime).Y * 7), Assets.Colors.FlashyGreen);
            //Draw win text
            s.DrawString(Assets.BigMenuFont, "Player " + (2 - d) + " wins!", new Vector2(s.GraphicsDevice.Viewport.Width / 2 - Assets.BigMenuFont.MeasureString("Player " + (2 - d) + " wins!").X / 2), Assets.Colors.FlashyGreen);
        }

        //Clear all stats, should be called at the start of every game
        public static void clearStats()
        {
            totalBounces = 0;
            totalCurveballs = 0;
            totalPlaytimeSeconds = 0;
            totalPlaytimeMinutes = 0;
        }

        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion

    }
}
