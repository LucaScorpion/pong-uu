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
        public static int totalPlaytimeSeconds = 0;
        public static int totalPlaytimeMinutes = 0;
        static int previoust;

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
                //Set the starting playtime
                startPlaytime = t;
                previoust = t - 1;
            }
            else if (GameManager.playing == true)
            {
                //If a second has passed
                if (t != previoust)
                {
                    //Add a second
                    totalPlaytimeSeconds++;
                    //If 60 seconds have passed
                    if (totalPlaytimeSeconds == 60)
                    {
                        //Add a minute, reset seconds
                        totalPlaytimeMinutes++;
                        totalPlaytimeSeconds = 0;
                    }
                    previoust = t;
                }
            }
        }

        //Clear all stats, should be called at the start of every game
        public static void clearStats()
        {
            totalBounces = 0;
            totalCurveballs = 0;
            totalPlaytimeSeconds = 0;
            totalPlaytimeMinutes = 0;
        }

        #endregion

        #region Constructors
        #endregion

        #region Properties
        #endregion

    }
}
