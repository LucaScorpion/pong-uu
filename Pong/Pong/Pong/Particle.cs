using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    /// <summary>
    /// Single particle managed by an emitter
    /// </summary>
    public class Particle
    {
        #region Fields
        Vector2 position = Vector2.Zero;
        float beginSize = 1f;
        float endSize = 0f;
        Vector2 speed = Vector2.Zero;
        Texture2D texture = Assets.DummyTexture;
        Color beginColor = Assets.Colors.ExplodingGreen; //Color on spawn
        Color endColor = Assets.Colors.DimmGreen; //Color on death. (Fades from begin to end color)
        float ttl = 30; //Amount of frames to live (60 = 1 sec)
        float lifeTime = 0; //The age of the particle in frames
        #endregion

        #region Methods
        public void update(GraphicsDevice g)
        {
            //Change Lifetime
            lifeTime++;

            //Move particle
            position += speed;
        }
        public void draw(SpriteBatch s)
        {
            if (isAlive)
            {
                //Draw particle
                s.Draw(texture, generateRectangle(), Color.Lerp(beginColor, endColor, lifeTime / ttl));
            }
        }
        Rectangle generateRectangle()
        {
            float size = MathHelper.Lerp(beginSize, endSize, lifeTime / ttl);
            Rectangle rect = new Rectangle((int)(position.X - size / 2), (int)(position.Y - size / 2), (int)size, (int)size);
            return rect;
        }
        #endregion

        #region Constructors
        public Particle(Vector2 speed, Vector2 position, float beginSize, float endSize)
        {
            this.position = position;
            this.beginSize = beginSize;
            this.endSize = endSize;
            this.speed = speed;
        }
        #endregion

        #region Properties
        public bool isAlive { get { if (ttl > lifeTime) { return true; } else { return false; } } }
        #endregion
    }
}
