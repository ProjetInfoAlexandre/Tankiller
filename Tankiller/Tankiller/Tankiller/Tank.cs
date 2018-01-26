using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace Tankiller
{
    /// <summary>
    /// Singleton
    /// </summary>
    class Tank
    {
        private static Tank instance = null;

        private Stopwatch watch = new Stopwatch();
        private float X, Y;
        private float from_x, from_y;
        private float to_x = -1, to_y = -1;
        private long move_duration = -1;

        private Tank()
        {
            X = Y = 100;
        }

        public float getX() {
            if (to_x == X) to_x = -1;
            else if (to_x > -1)
            {
                long current_time = watch.ElapsedMilliseconds;

                if (current_time >= move_duration)
                {
                    X = to_x;
                    watch.Stop();
                }
                else
                {
                    X = from_x + ((float)to_x - (float)from_x) * (float)current_time / (float)move_duration;
                }
            }

            return X;
        }

        public float getY()
        {
            if (to_y == Y) to_y = -1;
            else if (to_y > -1)
            {
                long current_time = watch.ElapsedMilliseconds;

                if (current_time >= move_duration)
                {
                    Y = to_y;
                    watch.Stop();
                }
                else Y = from_y + ((float)to_y - (float)from_y) * (float)current_time / (float)move_duration;
            }

            return Y;
        }

        public static Tank getTank()
        {
            if (instance == null) instance = new Tank();
            return instance;
        }

        public void moveTo(float x, float y, int duration_millis)
        {
            from_x = X;
            from_y = Y;

            if (duration_millis <= 0)
            {
                X = x;
                Y = y;
            }
            else
            {
                move_duration = duration_millis;

                if (watch != null) watch.Stop();

                watch.Reset();
                watch.Start();

                from_x = X;
                from_y = Y;

                to_x = x;
                to_y = y;
            }
        }
    }
}
