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
        static int startPlaytime;
        public static int totalPlaytime = 0;

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

        public void updateGameTime(int t)
        {
            if (GameManager.playing == false)
            {
                //Calculate the starting playtime
                startPlaytime = t;
            }
            else if (GameManager.playing == true)
            {
                //Calculate the total playtime
                totalPlaytime = t - startPlaytime;
            }
        }

        //Clear all stats, should be called at the start of every game
        public static void clearStats()
        {
            totalBounces = 0;
            totalCurveballs = 0;
            totalPlaytime = 0;
        }

        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion

    }
}
