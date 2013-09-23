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
        Rectangle rect = new Rectangle(0, 0, 10, 10);
        Texture2D texture = Assets.PongBall;
        Vector2 speed = Vector2.Zero;
        Color color = Assets.Colors.FlashyGreen;
        Random random = new Random();
        float spawnSpeed = 10;
        Emitter bounceEmitter = new Emitter(5f,0f,Assets.Colors.ExplodingGreen,Assets.Colors.DimmGreen,20f,3);
        Emitter curveEmitter = new Emitter(5f, 0f, Assets.Colors.ExplodingGreen, Assets.Colors.DimmGreen, 3f, 1);
        Rectangle collisionRect = new Rectangle(0, 0, 10, 10);
        float curve =  0.1f; //Amount of curve when curveball is hit
        bool paused = true;
        int curveDirection = 0; //Direction from player for the curveball
        #endregion

        #region Methods
        public void setColor(Color c)
        {
            this.color = c;
        }
        public void create(GraphicsDevice g)
        {
            //Put the ball in the middle of the screen
            rect.X = (g.Viewport.Width / 2) - (rect.Width / 2);
            rect.Y = (g.Viewport.Height / 2) - (rect.Height / 2);
            //Set a random speed
            speed = new Vector2(MathHelper.Lerp(0.5f, 1, (float)random.NextDouble()) * (random.Next(0,2) * 2 - 1), MathHelper.Lerp(-1, 1, (float)random.NextDouble()));
            speed = speed / speed.Length() * spawnSpeed;
            //Press space to start (unpause)
            paused = true;
            curveDirection = 0;
        }
        public void update(GraphicsDevice g)
        {
            if (!paused)
            {
                //Curve speed
                 Vector2 newSpeed = new Vector2(speed.X, speed.Y + curveDirection * curve); //New direction, but needs speed (vector length) correction;
                 speed = newSpeed / newSpeed.Length() * speed.Length();

                //Move ball
                rect.X += (int)speed.X;
                rect.Y += (int)speed.Y;

                //Move collisionRect
                collisionRect.X = rect.X;
                collisionRect.Y = rect.Y;

                bool bounced = false; //Boolean to check of ball bounced this loop

                //Collision with top of window
                if (rect.Y < 0)
                {
                    speed.Y = -speed.Y;
                    rect.Y = -rect.Y;
                    bounced = true;
                }
                //Collision with bottom of window
                if (rect.Bottom > g.Viewport.Height)
                {
                    speed.Y = -speed.Y;
                    rect.Y = 2 * g.Viewport.Height - rect.Bottom - rect.Height;
                    bounced = true;
                }
                //Drag along emitters
                bounceEmitter.Position = new Vector2(rect.Center.X, rect.Center.Y);
                curveEmitter.Position = new Vector2(rect.Center.X, rect.Center.Y);
                //Spray particles if bounced
                if (bounced)
                {
                    bounceEmitter.shoot();
                }
                if (curveDirection != 0)
                {
                    curveEmitter.shoot();
                }
            }
            else
            {
                //Press space to start (unpause)
                if (InputState.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
                {
                    paused = false;
                }
            }
        }
        public void collideToPlayer(Player player)
        {
            if (rect.Intersects(player.CollisionRectangle))
            {
                //Add a bounce in the stats
                Stats.addBounce();

                collisionRect.X -= (int)speed.X;
                if (!collisionRect.Intersects(player.CollisionRectangle))
                {
                    Assets.Audio.HitSound.Play();
                    bounceEmitter.shoot();

                    //Put the ball in the right position
                    if (speed.X > 0)
                    {
                        rect.X = player.CollisionRectangle.X - rect.Width - (rect.Right - player.CollisionRectangle.X);
                    }
                    else if(speed.X < 0)
                    {
                        rect.X = player.CollisionRectangle.X + player.CollisionRectangle.Width + player.CollisionRectangle.Right - rect.X;
                    }
                    speed.X = -speed.X;
                    //Curve effect when playerdirection is opposite of ball direction
                    if ((speed.Y >= 0 && player.MoveDirection == 1) || (speed.Y <= 0 && player.MoveDirection == -1))
                    {
                        //Add a curveball in the stats
                        Stats.addCurveball();
                        
                        curveDirection = player.MoveDirection;
                    }
                    else
                    {
                        curveDirection = 0;
                    }
                }
                else
                {
                    collisionRect.X += (int)speed.X;
                    collisionRect.Y -= (int)speed.Y;
                    if (!collisionRect.Intersects(player.CollisionRectangle))
                    {
                        speed.Y = -speed.Y;
                    }
                }
            }
        }
        public void draw(SpriteBatch s)
        {
            s.Draw(texture, rect, color);
            if (paused)
            {
                //Draw the text saying <SPACE>
                String msg = "<SPACE>";
                s.DrawString(Assets.MenuFont, msg, new Vector2(rect.X + rect.Width / 2 - Assets.MenuFont.MeasureString(msg).X / 2, rect.Y + rect.Height * 2), Assets.Colors.LuminantGreen);
            }
        }
        public void pause()
        {
            paused = true;
        }
        #endregion

        #region Constructors
        public PongBall()
        {
        }
        #endregion

        #region Properties
        public Vector2 Position { get { return new Vector2(rect.Center.X, rect.Center.Y); } }
        public bool IsPaused { get { return paused; } set { paused = value; } }
        public Vector2 Speed { get { return speed; } }
        #endregion

    }
}
