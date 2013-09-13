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
        static Texture2D dummyTexture;
        static SpriteFont menuFont;
        #endregion

        #region Methods
        public static void init(GraphicsDevice g)
        {
            dummyTexture = new Texture2D(g, 1, 1);
            dummyTexture.SetData(new Color[] { Color.White });
        }
        #endregion

        #region Properties
        public static Texture2D DummyTexture
        {
            get { return dummyTexture; }
            set { dummyTexture = value; }
        }
        public static SpriteFont MenuFont
        {
            get { return menuFont; }
            set { menuFont = value; }
        }
        #endregion
    }
}
