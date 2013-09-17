using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    /// <summary>
    /// Emitter of particles. When an instance of this class is made, it will automatically be drawn by the pariclemanager. (Of ParticleManager.draw is called)
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
            for(int i = particles.Count() - 1; i >= 0; i--)
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
                particles.Add(new Particle(new Vector2((float)MathHelper.Lerp(-spawnSpeed,spawnSpeed,(float)random.NextDouble()), (float)MathHelper.Lerp(-spawnSpeed,spawnSpeed,(float)random.NextDouble())), position,beginSize,endSize));
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
}
