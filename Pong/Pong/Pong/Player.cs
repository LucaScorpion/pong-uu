using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Pong
{
    public class Player
    {
        #region Fields
        Rectangle rect = new Rectangle(0,0,0,0);
        int lives = 0;
        Texture2D texture = Assets.Player;
        Color color = Color.White;
        Keys up = Keys.W;
        Keys down = Keys.S;
        int movementSpeed = 7;
        SoundEffectInstance hitSound;
        #endregion

        #region Methods
        public void update(GraphicsDevice g)
        {
            //Movement using input
            if (InputState.isKeyDown(up))
            {
                rect.Y -= movementSpeed;
            }
            if (InputState.isKeyDown(down))
            {
                rect.Y += movementSpeed;
            }
            //Restrict moment to viewport
            if (rect.Y < 0)
            {
                rect.Y = 0;
            }
            else if (rect.Bottom > g.Viewport.Height)
            {
                rect.Y = g.Viewport.Height - rect.Height;
            }
            if (rect.X < 0)
            {
                rect.X = 0;
            }
            if (rect.Right > g.Viewport.Width)
            {
                rect.X = g.Viewport.Width - rect.Width;
            }
        }
        public void draw(SpriteBatch s)
        {
            s.Draw(texture, rect, color);
        }
        public void setControls(Keys u, Keys d)
        {
            this.up = u;
            this.down = d;
        }
        public void playSound()
        {
            if (hitSound != null)
            {
                hitSound.Play();
            }
        }
        #endregion

        #region Constructors
        public Player()
        {
        }
        public Player(Rectangle rect, Color color)
        {
            this.rect = rect;
            this.color = color;
        }
        public Player(Rectangle rect, Color color, int lives, SoundEffectInstance hitSound)
        {
            this.rect = rect;
            this.color = color;
            this.lives = lives;
            this.hitSound = hitSound;
        }
        #endregion

        #region Properties
        public int Lives { get { return lives; } set { lives = value; } }
        public Rectangle CollisionRectangle { get { return rect; } }
        #endregion
    }
}
