using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class Player
    {
        #region Fields
        Rectangle rect = new Rectangle(0,0,0,0);
        int lives = 0;
        Texture2D texture = Assets.DummyTexture;
        Color color = Color.White;
        Keys left = Keys.A;
        Keys right = Keys.S;
        Vector2 movementVector = Vector2.Zero; //Movement direction of 'right' key
        #endregion

        #region Methods
        public void update(GraphicsDevice g)
        {
            //Movement using input
            if (InputState.isKeyDown(left))
            {
                rect.X -= (int)(movementVector.X);
                rect.Y -= (int)(movementVector.Y);
            }
            if (InputState.isKeyDown(right))
            {
                rect.X += (int)(movementVector.X);
                rect.Y += (int)(movementVector.Y);
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
        public void setControls(Keys l, Keys r)
        {
            this.left = l;
            this.right = r;
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
        public Player(Rectangle rect, Color color, int lives)
        {
            this.rect = rect;
            this.color = color;
            this.lives = lives;
        }
        #endregion

        #region Properties
        public int Lives { get { return lives; } set { lives = value; } }
        public Vector2 MovementVector { get { return movementVector; } set { movementVector = value; } }
        public Rectangle CollisionRectangle { get { return rect; } }
        #endregion
    }
}
