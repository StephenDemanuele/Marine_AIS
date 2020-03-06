namespace AIS.Parser.Models
{
    public class ObservationPoint
    {
        public ObservationPoint(decimal lat, decimal lon)
        {
            Latitude = lat;
            Longitude = lon;
        }

        public decimal Latitude { get ;}

        public decimal Longitude { get ;}
    }
}
