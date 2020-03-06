namespace AIS.GPSReader.Models
{
    /// <summary>
    /// This message contains the latitude and longitude of the present position, the time of position, the fix and the status.
    /// </summary>
    public class GLL : GPMessage
    {
        internal GLL(string sentence)
        {
            var parts = sentence.Split(',');
            MessageId = parts[0];
            Latitude = decimal.Parse(parts[1]);
            if (char.TryParse(parts[2], out var northSouthIndicator)) NorthSouthIndicator = northSouthIndicator;
            Longitude = decimal.Parse(parts[3]);
            if (char.TryParse(parts[4], out var eastWestIndicator)) EastWestIndicator = eastWestIndicator;
            UTCTime = parts[5];
            Status = char.Parse(parts[6]);
            Checksum = parts[7];
        }

        public decimal Latitude { get; }

        public char NorthSouthIndicator { get; }

        public decimal Longitude { get; }

        public char EastWestIndicator { get; }

        public string UTCTime { get; }

        /// <summary>
        /// A=data valid
        /// V=data not valid
        /// </summary>
        public char Status { get; set; }

        public override string ToString()
        {
            return $"{NorthSouthIndicator} {Latitude}, {EastWestIndicator} {Longitude} -GLL";
        }
    }
}
