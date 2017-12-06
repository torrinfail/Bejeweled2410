using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace Bejeweled
{
    /// <summary>
    /// Author: Aidan Hubert
    /// Represents all the Timers in the game. Does a specifc action after alloted time
    /// </summary>
    class Timer
    {
        public static IList<Timer> Timers { get; set; }
        Action onComplete;
        double remainingTime;
        static Timer()
        {
            Timers = new List<Timer>();
        }
        Timer(double time, Action onComplete)
        {
            this.onComplete = onComplete;
            remainingTime = time;
        }
        /// <summary>
        /// creates a new timer and adds it to the list of timers 
        /// </summary>
        /// <param name="time">
        /// Time till action is run
        /// </param>
        /// <param name="onComplete">
        /// action to run after alloted Time
        /// </param>
        /// <returns></returns>
        public static Timer NewTimer(double time, Action onComplete)
        {
            Timer nT = new Timer(time, onComplete);
            Timers.Add(nT);
            return nT;
        }
        /// <summary>
        /// Should only be called by the Update() method in the display class.
        /// updates the timer based off the current framerate.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if(remainingTime <= 0d)
            {
                onComplete();
                Timers.Remove(this);
                return;
            }
            remainingTime -= gameTime.ElapsedGameTime.TotalSeconds;
        }



    }
}
