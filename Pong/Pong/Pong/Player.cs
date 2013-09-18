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
        ControlMode controlMode = ControlMode.Player;
        int moveDireciton = 0; //1 = up, -1 = down, 0 = no movement
        #endregion

        #region Methods
        public void update(GraphicsDevice g, PongBall pongBall)
        {
            if (ControlMode.Player == controlMode)
            {
                //Movement using input and adjust moveDirection for the curveball
                moveDireciton = 0;
                if (InputState.isKeyDown(up))
                {
                    rect.Y -= movementSpeed;
                    moveDireciton = 1; //Is moving up
                }
                if (InputState.isKeyDown(down))
                {
                    rect.Y += movementSpeed;
                    if(moveDireciton == 1)
                    {
                        moveDireciton = 0; //is NOT moving, the up and down keys are both pressed
                    }
                    else
                    {
                        moveDireciton = -1; //Moving down.
                    }
                }
            }
            else if(ControlMode.Ai == controlMode)
            {
                if (pongBall.Position.Y > rect.Center.Y)
                {
                    rect.Y += movementSpeed;
                }
                if (pongBall.Position.Y < rect.Center.Y)
                {
                    rect.Y -= movementSpeed;
                }
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
        public void setControls(ControlMode controlMode)
        {
            this.controlMode = controlMode;
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
        public Rectangle CollisionRectangle { get { return rect; } }
        public int MoveDirection { get { return moveDireciton; } set { moveDireciton = value; } }
        #endregion
    }
    public enum ControlMode { Ai, Player }
}
