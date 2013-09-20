using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong
{
    public static class Stats
    {
        #region Fields
        static int totalBounces = 0;
        static int totalCurveballs = 0;
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
        //Use these to display the stats
        public int TotalBounces { get { return totalBounces; } }
        public int TotalCurveballs { get { return totalCurveballs; } }
        #endregion

    }
}
