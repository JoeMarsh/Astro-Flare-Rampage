using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace AstroFlare
{
    class Timer
    {
        public static List<Timer> Timers = new List<Timer>();

        private double timeRemaining;
        private double interval;

        public static float TimerSpeedModifier = 1f;
        public event NotifyHandler Fire;

        public void Start(double interval)
        {
            Timers.Remove(this);
            Timers.Add(this);
            this.interval = interval;
            this.timeRemaining = interval;
        }

        public void Stop()
        {
            Timers.Remove(this);
        }

        public static void Update(GameTime gameTime)
        {
            for (int i = Timers.Count - 1; i >= 0; i--)
            {
                Timer timer = Timers[i];

                timer.timeRemaining -= gameTime.ElapsedGameTime.TotalSeconds * TimerSpeedModifier;
                if (timer.timeRemaining <= 0)
                {
                    timer.Fire();
                    timer.timeRemaining = timer.interval;
                }
            }
        }
    }
}
