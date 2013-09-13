using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class PongBall
    {
        #region Fields
        Rectangle rect = new Rectangle();
        Texture2D texture = Assets.DummyTexture;
        Vector2 speed = Vector2.Zero;
        Color color = Color.White;
        Random random = new Random();
        int xDir, yDir;
        int minXSpeed = 2;
        int maxXSpeed = 4;
        int minYSpeed = 2;
        int maxYSpeed = 4;
        #endregion

        #region Methods
        public void setColor(Color c)
        {
            this.color = c;
        }
        public void create(GraphicsDevice g)
        {
            rect.X = (g.Viewport.Width / 2) - (rect.Width / 2);
            rect.Y = (g.Viewport.Height / 2) - (rect.Height / 2);
            xDir = random.Next(0, 2) * 2 - 1;
            yDir = random.Next(0, 2) * 2 - 1;
            speed.X = xDir * random.Next(minXSpeed, maxXSpeed + 1);
            speed.Y = yDir * random.Next(minYSpeed, maxYSpeed + 1);
        }
        public void update(GraphicsDevice g)
        {
            rect.X += (int)speed.X;
            rect.Y += (int)speed.Y;

            if (rect.Y < 0)
            {
                speed.Y = -speed.Y;
                rect.Y = -rect.Y;
            }

            if (rect.Bottom > g.Viewport.Height)
            {
                speed.Y = -speed.Y;
                rect.Y = 2 * g.Viewport.Height - rect.Bottom - rect.Height;
            }
        }
        public void draw(SpriteBatch s)
        {
            s.Draw(texture, rect, color);
        }
        #endregion

        #region Constructors
        public PongBall()
        {

        }
        public PongBall(Rectangle rect, Vector2 speed)
        {
            this.rect = rect;
            this.speed = speed;
        }
        public PongBall(Rectangle rect, Vector2 speed, Texture2D texture)
        {
            this.texture = texture;
            this.rect = rect;
            this.speed = speed;
        }
        #endregion

        #region Properties
        #endregion

    }
}
