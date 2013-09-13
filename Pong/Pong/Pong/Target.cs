using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class Target
    {
        #region Fields
        Rectangle rect = new Rectangle();
        Texture2D texture = Assets.DummyTexture;
        Color color = Color.White;
        Boolean visible = true;
        int owner = 0;
        #endregion

        #region Methods
        public void update()
        {

        }
        public void draw(SpriteBatch s)
        {
            s.Draw(texture, rect, color);
        }
        public void setColor(Color c)
        {
            this.color = c;
        }
        public void setTexture(Texture2D t)
        {
            texture = t;
        }
        #endregion

        #region Constructor
        public Target()
        {
        }
        public Target(Rectangle rect, Texture2D texture, Color color)
        {
            this.rect = rect;
            this.texture = texture;
            this.color = color;
        }
        #endregion

        #region Properties
        public Boolean Hidden { get { return !visible; } set { visible = !value; } }
        #endregion

    }
}
