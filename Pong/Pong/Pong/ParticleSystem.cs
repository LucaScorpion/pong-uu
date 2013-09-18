using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    /// <summary>
    /// Manager of particle emitters.
    /// </summary>
    public static class ParticleManager
    {
        #region Fields
        static List<Emitter> emitters = new List<Emitter>(); //All emitter managed by this one manager
        #endregion

        #region Methods
        public static void update(GraphicsDevice g)
        {
            //Update all emitters
            foreach (Emitter emitter in emitters)
            {
                emitter.update();
            }
        }
        /// <summary>
        /// Draw the full particle manager
        /// </summary>
        /// <param name="s">SpriteBatch. WARNING! DO NOT send in an open spriteBatch. Make sure it has been ended</param>
        public static void draw(SpriteBatch s)
        {
            //Draw all emitters
            foreach (Emitter emitter in emitters)
            {
                emitter.draw(s);
            }
        }
        public static void addEmitter(Emitter emitter)
        {
            emitters.Add(emitter);
        }
        public static void removeEmitter(Emitter emitter)
        {
            emitters.Remove(emitter);
        }
        #endregion

        #region Properties
        #endregion
    }
    /// <summary>
    /// Emitter of particles
    /// </summary>
    public class Emitter
    {
        #region Fields
        List<Particle> particles = new List<Particle>();
        Vector2 position = Vector2.Zero;
        float particlesPerLoop = 10f; //Amount of particles per loop
        float waitList = 0; //Amount of particles waiting to be spawned
        Random random = new Random();
        bool paused = true;
        float beginSize;
        float endSize;
        Color beginColor;
        Color endColor;
        int spawnSpeed; //Maximum initial speed on spawn
        #endregion

        #region Methods
        /// <summary>
        /// Update Emitter. (Updates all particles sent out by the emitter)
        /// </summary>
        public void update()
        {
            //If the emitter isn't paused, it will call the shoot method.
            if (!paused)
            {
                shoot();
            }

            //Loop through all particles. Kill if the particle isn't alive, otherwise update the particle.
            for (int i = particles.Count() - 1; i >= 0; i--)
            {
                //Update all particles that are alive.
                if (particles[i].isAlive)
                {
                    particles[i].update();
                }
                else
                {
                    //Remove the dead particle
                    particles.RemoveAt(i);
                }
            }
        }
        /// <summary>
        /// Draw all particles on the screen.
        /// </summary>
        /// <param name="s">Opened Spritebatch. (Commonly opened by the ParticleManager)</param>
        public void draw(SpriteBatch s)
        {
            //Draw EVERY particle
            foreach (Particle particle in particles)
            {
                particle.draw(s);
            }
        }
        /// <summary>
        /// Start spawning particles every update.
        /// </summary>
        public void start()
        {
            paused = false;
        }
        /// <summary>
        /// Pause spawning particles.
        /// </summary>
        public void pause()
        {
            paused = true;
        }
        /// <summary>
        /// Spawn a single batch of particles. (if paused == false then shoot is called every update)
        /// </summary>
        public void shoot()
        {
            waitList += particlesPerLoop; //Add new particles to waitlist

            //Spawn particles and empty waitlist
            for (int a; waitList >= 1; waitList--)
            {
                //Spawn a particle
                particles.Add(new Particle(new Vector2((float)MathHelper.Lerp(-spawnSpeed, spawnSpeed, (float)random.NextDouble()), (float)MathHelper.Lerp(-spawnSpeed, spawnSpeed, (float)random.NextDouble())), position, beginSize, endSize));
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Emitter of particles
        /// </summary>
        /// <param name="beginSize">Size of particle on spawn</param>
        /// <param name="endSize">Size of particle on dreath (lerp)</param>
        /// <param name="beginColor">Color of particle on spawn</param>
        /// <param name="endColor">Color of particle on death (fade)</param>
        /// <param name="particlesPerLoop">Amount of particles spawned per loop. (Every time shoot() is called)</param>
        /// <param name="spawnSpeed">Maximum speed of the particle on spawn. (MAXIMUM)</param>
        public Emitter(float beginSize, float endSize, Color beginColor, Color endColor, float particlesPerLoop, int spawnSpeed)
        {
            this.beginColor = beginColor;
            this.endColor = endColor;
            this.beginSize = beginSize;
            this.endSize = endSize;
            this.spawnSpeed = spawnSpeed;
            this.particlesPerLoop = particlesPerLoop;

            ParticleManager.addEmitter(this);
        }
        #endregion

        #region Properties
        public Vector2 Position { get { return position; } set { position = value; } }
        #endregion
    }
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
        public void update()
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
                float size = MathHelper.Lerp(beginSize, endSize, lifeTime / ttl);
                s.Draw(texture, new Vector2(position.X - size / 2, position.Y - size / 2), null, Color.Lerp(beginColor, endColor, lifeTime / ttl), 0f, Vector2.Zero, size, SpriteEffects.None, 0);
            }
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
    #region SpawnShape
    abstract class SpawnShape
    {
        #region Fields
        abstract public Vector2 origin;
        abstract public float height;
        abstract public float width;
        abstract public Random random;
        #endregion

        #region Methods
        abstract public Vector2 GetPosition();
        #endregion

        #region Constructors
        public SpawnShape(Vector2 origin, float width, float height)
        {
            this.origin = origin;
            this.width = width;
            this.height = height;
            this.random = new Random();
        }
        #endregion
    }
    class PointSpawnShape : SpawnShape
    {
        public override Vector2 GetPosition()
        {
            return origin;
        }
    }
    class CircleSpawnShape : SpawnShape
    {
        public override Vector2 GetPosition()
        {
            return new Vector2((float)(origin.X + Math.Sin(random.NextDouble() * Math.PI) * width), (float)(origin.Y + Math.Sin(random.NextDouble() * Math.PI) * height));
        }
    }
    #endregion
}
