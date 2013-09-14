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
        Dictionary<int, Player> players = new Dictionary<int, Player>();
        #endregion

        #region Methods
        public void update(GraphicsDevice g)
        {
            foreach (KeyValuePair<int, Player> p in players)
            {
                p.Value.update(g);
            }
        }
        public void draw(SpriteBatch s)
        {
            foreach (KeyValuePair<int, Player> p in players)
            {
                //If player is alive, draw it
                p.Value.draw(s);
            }
        }
        public void addPlayer(Player p, int playerIndex)
        {
            players.Add(playerIndex, p);
        }
        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion
    }
}
