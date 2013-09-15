using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
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
        public void update(GraphicsDevice g)
        {
            if (!paused)
            {
                shoot();
            }
            for(int i = particles.Count() - 1; i >= 0; i--)
            {
                //Update all particles that are alive.
                if (particles[i].isAlive)
                {
                    particles[i].update(g);
                }
                else
                {
                    //Remove the dead particle
                    particles.RemoveAt(i);
                }
            }
        }
        public void draw(SpriteBatch s)
        {
            foreach (Particle particle in particles)
            {
                particle.draw(s);
            }
        }
        /// <summary>
        /// Start spawning particles
        /// </summary>
        public void start()
        {
            paused = false;
        }
        /// <summary>
        /// Pause spawning particles
        /// </summary>
        public void pause()
        {
            paused = true;
        }
        /// <summary>
        /// Spawn a single loop of particles
        /// </summary>
        public void shoot()
        {
            waitList += particlesPerLoop; //Add new particles to waitlist
            for (int a; waitList >= 1; waitList--)
            {
                //Spawn a particle
                particles.Add(new Particle(new Vector2(random.Next(0, 2 * spawnSpeed) - spawnSpeed, random.Next(0, 2 * spawnSpeed ) - spawnSpeed), position,beginSize,endSize));
            }
        }
        #endregion

        #region Constructors
        public Emitter(float beginSize, float endSize, Color beginColor, Color endColor, float particlesPerLoop, int spawnSpeed)
        {
            this.beginColor = beginColor;
            this.endColor = endColor;
            this.beginSize = beginSize;
            this.endSize = endSize;
            this.spawnSpeed = spawnSpeed;
            this.particlesPerLoop = particlesPerLoop;
        }
        #endregion

        #region Properties
        public Vector2 Position { get { return position; } set { position = value; } }
        #endregion
    }
}
