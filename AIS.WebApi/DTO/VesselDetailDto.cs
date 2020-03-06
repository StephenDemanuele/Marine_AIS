using AIS.Parser.Models;
using Newtonsoft.Json;
using System;

namespace AIS.WebApi.DTO
{
    public class VesselDetailDto
    {
        public VesselDetailDto(Vessel vessel)
        {
            MMSI = vessel.UserId;
            FriendlyName = string.IsNullOrEmpty(vessel.Name) ?
                $"MMSI {vessel.UserId}" :
                vessel.Name;
            Longitude = vessel.Longitude;
            Latitude = vessel.Latitude;
            TrueHeading = vessel.TrueHeading;
            SOG = vessel.SOG;
            VesselType = $"{vessel.ShipCargoType.ToString()} {vessel.NavigationalStatus.ToString()}";
            LastUpdate = vessel.LastUpdate;
        }

        [JsonProperty("Id")]
        public string FriendlyName { get; set; }

        public int MMSI { get; }

        [JsonProperty("Lon")]
        public decimal Longitude { get; }

        [JsonProperty("Lat")]
        public decimal Latitude { get; }

        [JsonProperty("Heading")]
        public double TrueHeading { get; }

        public decimal SOG { get; }

        [JsonProperty("Type")]
        public string VesselType { get; }

        [JsonIgnore]
        public DateTime LastUpdate { get ;}

        public string LastUpdateDescription
        {
            get
            {
                var minutes = (int)DateTime.Now.Subtract(LastUpdate).TotalMinutes;
                if (minutes <= 1) return "Now";

                if (minutes <= 59) return $"{minutes} mins ago";

                var quotient = Math.DivRem(minutes, 60, out int remainder);
                return $"{quotient}hrs {remainder} mins ago";
            }
        }
    }
}
