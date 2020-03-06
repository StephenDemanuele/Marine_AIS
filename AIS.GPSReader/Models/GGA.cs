namespace AIS.GPSReader.Models
{
    /// <summary>
    /// This message contains time, position and fix related data. Fixed Data.
    /// </summary>
    public class GGA : GPMessage
    {
        internal GGA(string sentence)
        {
            var parts = sentence.Split(',');
            MessageId = parts[0];
            UTCTime = parts[1];
            Latitude = decimal.Parse(parts[2]);
            if (char.TryParse(parts[3], out var northSouthIndicator)) NorthSouthIndicator = northSouthIndicator;
            Longitude = decimal.Parse(parts[4]);
            if (char.TryParse(parts[5], out var eastWestIndicator)) EastWestIndicator = eastWestIndicator;
            FixQualityIndicator = byte.Parse(parts[6]);
            SatellitesUsed = byte.Parse(parts[7]);
            HDOP = decimal.Parse(parts[8]);
            Altitude = decimal.Parse(parts[9]);
            if (char.TryParse(parts[10], out char altitudeUnits)) AltitudeUnits = altitudeUnits;
            GeoidSeparation = decimal.Parse(parts[11]);
            if (char.TryParse(parts[12], out char geoidSeparationUnits)) GeoidSeparationUnits = geoidSeparationUnits;
            if (!string.IsNullOrEmpty(parts[13])) AgeOfDifferentialCorrections = decimal.Parse(parts[13]);
            var lastFieldParts = parts[14].Split('*');
            DifferentialReferenceStationId = int.Parse(lastFieldParts[0]);
            Checksum = lastFieldParts[1];
        }

        public string UTCTime { get; }

        public decimal Latitude { get; }

        public char NorthSouthIndicator { get; }

        public decimal Longitude { get; }

        public char EastWestIndicator { get; }

        /// <summary>
        /// 0: fix not available or invalid
        /// 1: GPS SPS mode, fix valid
        /// 2: Differential GPS, SPS mode, fix valid
        /// </summary>
        public byte FixQualityIndicator { get; }

        public byte SatellitesUsed { get; }

        public decimal HDOP { get; }

        public decimal Altitude { get; }

        public char AltitudeUnits { get; }

        public decimal GeoidSeparation { get; }

        public char GeoidSeparationUnits { get; set; }

        public decimal AgeOfDifferentialCorrections { get; }

        public int DifferentialReferenceStationId { get; }

        public override string ToString()
        {
            return $"{NorthSouthIndicator} {Latitude}, {EastWestIndicator} {Longitude}, Alt {Altitude} {AltitudeUnits} (from {SatellitesUsed} satellites) -GGA";
        }
    }
}
