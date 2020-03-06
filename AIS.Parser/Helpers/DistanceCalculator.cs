using System;

namespace AIS.Parser.Helpers
{
    /// <summary>
    /// ref geodatasource.com
    /// </summary>
    internal class DistanceCalculator
    {
        public decimal Calc(decimal lon1, decimal lat1, decimal lon2, decimal lat2)
        {
            var r = 6371;
            var _lat1 = deg2rad((double)lat1);
            var _lat2 = deg2rad((double)lat2);
            var deltaLat = deg2rad((double)(lat2-lat1));
            var deltaLon = deg2rad((double)(lon2 - lon1));

            var a= Math.Sin(deltaLat/2) * Math.Sin(deltaLat/2) +
                Math.Cos(_lat1) * Math.Cos(_lat2) * Math.Sin(deltaLon/2) * Math.Sin(deltaLon/2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a));

            return (decimal)(r * c); 
        }

        private static double deg2rad(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        private static double rad2deg(double radians)
        {
            return radians / Math.PI * 180;
        }
    }
}
