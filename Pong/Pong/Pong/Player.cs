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
        int moveDirection = 0; //1 = up, -1 = down, 0 = no movement
        #endregion

        #region Methods
        public void update(GraphicsDevice g, PongBall pongBall)
        {
            if (ControlMode.Player == controlMode)
            {
                //Movement using input and adjust moveDirection for the curveball
                moveDirection = 0;
                if (InputState.isKeyDown(up))
                {
                    rect.Y -= movementSpeed;
                    moveDirection = 1; //Is moving up
                }
                if (InputState.isKeyDown(down))
                {
                    rect.Y += movementSpeed;
                    if(moveDirection == 1)
                    {
                        moveDirection = 0; //is NOT moving, the up and down keys are both pressed
                    }
                    else
                    {
                        moveDirection = -1; //Moving down.
                    }
                }
            }
            else if(ControlMode.Ai == controlMode)
            {
                //Move towards the ball if the ball is moving towards the AI
                if ((pongBall.Position.X > rect.Center.X && pongBall.Speed.X < 0) || (pongBall.Position.X < rect.Center.X && pongBall.Speed.X > 0))
                {
                    //Move towards the ball if the ball is on the AI's half of the screen
                    if (pongBall.Position.X < g.Viewport.Width / 2)
                    {
                        if (pongBall.Position.Y > rect.Center.Y)
                        {
                            rect.Y += movementSpeed - lives;
                        }
                        if (pongBall.Position.Y < rect.Center.Y)
                        {
                            rect.Y -= movementSpeed - lives;
                        }
                    }
                }
            }
            //Restrict movement to viewport
            if (rect.Y < 0)
            {
                rect.Y = 0;
                moveDirection = 0;
            }
            else if (rect.Bottom > g.Viewport.Height)
            {
                rect.Y = g.Viewport.Height - rect.Height;
                moveDirection = 0;
            }
        }
        public void draw(SpriteBatch s)
        {
            if (lives > 0)
            {
                s.Draw(texture, rect, color);
            }
        }
        //Set the controls
        public void setControls(Keys u, Keys d)
        {
            this.up = u;
            this.down = d;
        }
        //AI or human player
        public void setControls(ControlMode controlMode)
        {
            this.controlMode = controlMode;
        }
        //Reset the paddle to its starting position
        public void reset(GraphicsDevice g)
        {
            rect.Y = (int)(g.Viewport.Height - rect.Height)/2;
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
        public int MoveDirection { get { return moveDirection; } set { moveDirection = value; } }
        #endregion
    }
    public enum ControlMode { Ai, Player }
}
