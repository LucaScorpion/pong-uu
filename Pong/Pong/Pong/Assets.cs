using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pong
{
    /// <summary>
    /// This static class contains several commonly used assets.
    /// </summary>
    public static class Assets
    {
        #region Fields
        public static Texture2D DummyTexture;
        public static SpriteFont MenuFont;
        public static Texture2D PongBall;
        public static Texture2D Player;
        public static Texture2D Midline;
        public static Texture2D Life;
        public struct Colors
        {
            public static Color MainGreen = new Color(0, 255, 0);
            public static Color FlashyGreen = new Color(10, 255, 10);
            public static Color LuminantGreen = new Color(30, 255, 30);
            public static Color AccentGreen = new Color(0, 80, 0);
            public static Color ShadyGreen = new Color(0, 50, 0);
            public static Color DimmGreen = new Color(0, 10, 0);
        }
        #endregion

        #region Methods
        public static void init(GraphicsDevice g)
        {
            DummyTexture = new Texture2D(g, 1, 1);
            DummyTexture.SetData(new Color[] { Color.White });
        }
        #endregion
    }
}
