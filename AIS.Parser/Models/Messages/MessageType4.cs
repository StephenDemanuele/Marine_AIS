using AIS.Parser.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AIS.Parser.Models.Messages
{
    public class MessageType4 : IMessage
    {
        static Dictionary<string, BitsPosition> _propDict;

        static MessageType4()
        {
            _propDict = typeof(MessageType4).GetProperties().Where(x => x.CustomAttributes.Any()).Select(prop =>
            {
                var attr = prop.GetCustomAttribute<BitPositionAttribute>();
                return new BitsPosition(
                    prop.Name,
                    attr.OrdinalPosition,
                    attr.BitCount);
            })
            .ToDictionary(entry => entry.PropertyName, entry => entry);
        }

        public readonly string BitVector;

        internal MessageType4(string bitVector)
        {
            BitVector = bitVector;
            MessageId = Convert.ToInt32(BitVector.Substring(_propDict[nameof(MessageId)].Ordinal, _propDict[nameof(MessageId)].BitCount), 2);
            RepeatIndicator = Convert.ToInt32(BitVector.Substring(_propDict[nameof(RepeatIndicator)].Ordinal, _propDict[nameof(RepeatIndicator)].BitCount), 2);
            UserId = Convert.ToInt32(BitVector.Substring(_propDict[nameof(UserId)].Ordinal, _propDict[nameof(UserId)].BitCount), 2);

            var rawLng = BitVector.Substring(_propDict[nameof(Longitude)].Ordinal, _propDict[nameof(Longitude)].BitCount).ConvertToIntFromBinary();
            Longitude = ((decimal)rawLng) / 600000;

            var rawLat = BitVector.Substring(_propDict[nameof(Latitude)].Ordinal, _propDict[nameof(Latitude)].BitCount).ConvertToIntFromBinary();
            Latitude = ((decimal)rawLat) / 600000;
        }

        [BitPosition(0, 6)]
        public int MessageId { get; set; }

        /// <summary>
        /// Used by the repeater to indicate how many times a message has been repeated. 
        /// See Section 4.6.1, Annex 2; 0-3; 0 = default; 3 = do not repeat any more.
        /// </summary>
        [BitPosition(6, 2)]
        public int RepeatIndicator { get; set; }

        /// <summary>
        /// MMSI
        /// </summary>
        [BitPosition(8, 30)]
        public int UserId { get; set; }

        [BitPosition(38, 14)]
        public int UTCYear { get; set; }

        [BitPosition(52, 4)]
        public int UTCMonth { get; set; }

        [BitPosition(56, 5)]
        public int UTCDay { get; set; }

        [BitPosition(61, 5)]
        public int UTCHour { get; set; }

        [BitPosition(66, 6)]
        public int UTCMinute { get; set; }

        [BitPosition(72, 6)]
        public int UTCSecond { get; set; }

        [BitPosition(78, 1)]
        public int PositionAccuracy { get; set; }

        [BitPosition(79, 28)]
        public decimal Longitude { get; set; }

        [BitPosition(107, 27)]
        public decimal Latitude { get; set; }

        [BitPosition(134, 4)]
        public PositionFixingDeviceType PositionFixingDeviceType { get;set;}

        [BitPosition(138, 1)]
        public int TransmissionControl { get; set; }

        [BitPosition(139, 9)]
        public int Spare { get ;set;}

        public decimal Distance { get; set; }
    }
}
