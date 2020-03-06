using System;

namespace AIS.Viewer
{
    internal class OpacityCalculator
    {
        public double Get(DateTime lastTimestamp)
        {
            var x = DateTime.Now.Subtract(lastTimestamp);
            if (x.TotalMinutes > 10)
            {
                return 0.25;
            }
            if (x.TotalMinutes > 5)
            {
                return 0.5;
            }
            if (x.TotalMinutes > 3)
            {
                return 0.7;
            }

            return 1;
        }
    }
}
