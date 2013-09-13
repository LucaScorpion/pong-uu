using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    /// <summary>
    /// Keeps the up-to-date input data
    /// </summary>
    public static class InputState
    {
        #region Fields
        static KeyboardState currentKeyboard;
        static KeyboardState previousKeyboard;
        #endregion

        #region Methods
        public static void update()
        {
            previousKeyboard = currentKeyboard;
            currentKeyboard = Keyboard.GetState();
        }
        public static Boolean isKeyDown(Keys k)
        {
            if (currentKeyboard.IsKeyDown(k))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static Boolean isKeyUp(Keys k)
        {
            if (currentKeyboard.IsKeyUp(k))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static Boolean isKeyPressed(Keys k)
        {
            if (currentKeyboard.IsKeyDown(k) && previousKeyboard.IsKeyUp(k))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Properties
        #endregion
    }
}
