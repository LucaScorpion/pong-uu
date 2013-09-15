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
        int yDir;
        int ySpeed = 2;
        Emitter bounceEmitter = new Emitter(5f,0f,Assets.Colors.ExplodingGreen,Assets.Colors.DimmGreen,20f,3);
        Rectangle collisionRect = new Rectangle(0, 0, 10, 10);
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
            yDir = random.Next(0, 2) * 2 - 1;
            speed.X = (random.Next(0,2) * 2 - 1) * (g.Viewport.Width - 60) / 60;
            speed.Y = yDir * ySpeed;
        }
        public void update(GraphicsDevice g)
        {
            Rectangle preciousRect = rect;

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
            //Drag along emitter
            bounceEmitter.Position = new Vector2(rect.Center.X, rect.Center.Y);
            //Spray particles if bounced
            if (bounced)
            {
                bounceEmitter.shoot();
            }

            //Check if ball crossed midline
            if (preciousRect.Center.X > g.Viewport.Width / 2 && rect.Center.X <= g.Viewport.Width / 2 || preciousRect.Center.X < g.Viewport.Width / 2 && rect.Center.X >= g.Viewport.Width / 2)
            {
                //Play kick
                Assets.Audio.Kick.Play();

                //Play hat
                Assets.Audio.Hat120.Play();
            }
        }
        public void collideToPlayer(Player player)
        {
            if (rect.Intersects(player.CollisionRectangle))
            {
                collisionRect.X -= (int)speed.X;
                if (!collisionRect.Intersects(player.CollisionRectangle))
                {
                    speed.X = -speed.X;
                    bounceEmitter.shoot();
                    //play Hitsound
                    Assets.Audio.HitSound.Play();
                    //Play kick if required TODO
                    Assets.Audio.Kick.Play();
                    //Play hat
                    Assets.Audio.Hat120.Play();
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
        }
        #endregion

        #region Constructors
        public PongBall()
        {
            //Add the bounce emitter to the manager for update and draws
            ParticleManager.addEmitter(bounceEmitter);
        }
        #endregion

        #region Properties
        #endregion

    }
}
