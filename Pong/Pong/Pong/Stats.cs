using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong
{
    public class Stats
    {
        #region Fields
        public static int totalBounces = 0;
        public static int totalCurveballs = 0;
        #endregion

        #region Methods
        //Add a bounce
        public static void addBounce()
        {
            totalBounces++;
        }

        //Add a curveball
        public static void addCurveball()
        {
            totalCurveballs++;
        }

        //Clear all stats, should be called at the start of every game
        public static void clearStats()
        {
            totalBounces = 0;
            totalCurveballs = 0;
        }

        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion

    }
}
