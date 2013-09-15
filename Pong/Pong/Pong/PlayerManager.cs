using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class PlayerManager
    {
        #region Fields
        public Player playerOne = new Player();
        public Player playerTwo = new Player();

        #endregion

        #region Methods
        public void update(GraphicsDevice g)
        {
            //Update players
            playerOne.update(g);
            playerTwo.update(g);
        }
        public void draw(SpriteBatch s)
        {
            //Draw players
            playerOne.draw(s);
            playerTwo.draw(s);

            //Draw player One Lives
            for (int i = 0; i < playerOne.Lives; i++)
            {
                s.Draw(Assets.Life, new Rectangle(s.GraphicsDevice.Viewport.Width / 2 - Assets.Life.Width - 20, (int)((s.GraphicsDevice.Viewport.Height / 3 - Assets.Life.Height) / 2 + s.GraphicsDevice.Viewport.Height / 3 * i), Assets.Life.Width, Assets.Life.Height), Assets.Colors.AccentGreen);
            }

            //Draw player Two Lives
            for (int i = 0; i < playerTwo.Lives; i++)
            {
                s.Draw(Assets.Life, new Rectangle(s.GraphicsDevice.Viewport.Width / 2 + 20, (int)((s.GraphicsDevice.Viewport.Height / 3 - Assets.Life.Height) / 2 + s.GraphicsDevice.Viewport.Height / 3 * i), Assets.Life.Width, Assets.Life.Height), Assets.Colors.AccentGreen);
            }
        }
        public void addPlayer(Player p, int playerIndex)
        {
            if (playerIndex == 1)
            {
                playerOne = p;
            }
            else if (playerIndex == 2)
            {
                playerTwo = p;
            }
        }
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion
    }
}
