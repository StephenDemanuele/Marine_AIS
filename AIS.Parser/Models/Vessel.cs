using AIS.Parser.Models.Messages;
using System;

namespace AIS.Parser.Models
{
    public class Vessel
    {
        /// <summary>
        ///     Creates a vessel instance.
        /// </summary>
        /// <param name="userId">Also referred to as MMSI.</param>
        public Vessel(int userId, Talker talker)
        {
            UserId = userId;
            Talker = talker;
        }

        public Talker Talker { get; }

        public int UserId { get; }

        public string Name { get; private set; }

        public string CallSign { get; private set; }

        public NavStatus NavigationalStatus { get; private set; }

        public decimal Longitude { get; private set; }

        public decimal Latitude { get; private set; }

        public decimal SOG { get; private set; }

        public decimal COG { get; private set; }

        public double TrueHeading { get; private set; }

        public ShipCargoTypes ShipCargoType { get; private set; }

        public decimal MaxStaticDraught { get; private set; }

        public DateTime LastUpdate { get; private set; }

        public decimal DistanceFromObservationPoint { get; private set; }

        public bool HasHeader { get; private set; } = false;

        public void UpdateWith<TMessage>(TMessage message) where TMessage : IMessage
        {
            LastUpdate = DateTime.Now;
            if (message is MessageType1)
            {
                var _message = message as MessageType1;
                Longitude = _message.Longitude;
                Latitude = _message.Latitude;
                NavigationalStatus = _message.NavigationalStatus;
                SOG = _message.SOG;
                COG = _message.COG;
                DistanceFromObservationPoint = _message.Distance;

                TrueHeading = _message.TrueHeading.HasValue? 
                    (double)_message.TrueHeading.Value: 0;
            }
            else if (message is MessageType3)
            {
                var _message = message as MessageType3;
                Longitude = _message.Longitude;
                Latitude = _message.Latitude;
                NavigationalStatus = _message.NavigationalStatus;
                SOG = _message.SOG;
                COG = _message.COG;
                DistanceFromObservationPoint = _message.Distance;
                TrueHeading = _message.TrueHeading.HasValue ?
                    (double)_message.TrueHeading.Value : 0;
            }
            else if (message is MessageType4)
            {
                var _message = message as MessageType4;
                Longitude = _message.Longitude;
                Latitude = _message.Latitude;
            }
            else if (message is MessageType5)
            {
                HasHeader = true;
                var _message = message as MessageType5;
                Name = _message.Name;
                CallSign = _message.CallSign;
                ShipCargoType = _message.ShipCargoType;
                MaxStaticDraught = _message.MaxStaticDraught;
            }
        }

        public string VesselId => string.IsNullOrEmpty(Name) ? $"{Talker.Value} MMSI: {UserId}" : $"{Talker.Value} {Name}/{ShipCargoType}";

        public override string ToString()
        {
            return $"{VesselId}, {NavigationalStatus}. SOG: {SOG.ToString("N2")}. Draught: {MaxStaticDraught}. {DistanceFromObservationPoint.ToString("N2")}Km.";
        }
    }
}
