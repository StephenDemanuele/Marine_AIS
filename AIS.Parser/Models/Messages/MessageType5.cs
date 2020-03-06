using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AIS.Parser.Extensions;
using AIS.Parser.Models;

namespace AIS.Parser.Models.Messages
{
    public class MessageType5 : IMessage
    {
        static Dictionary<string, BitsPosition> _propDict;

        static MessageType5()
        {
            _propDict = typeof(MessageType5).GetProperties().Select(prop =>
            {
                var attr = prop.GetCustomAttribute<BitPositionAttribute>();
                return new BitsPosition(
                    prop.Name,
                    attr.OrdinalPosition,
                    attr.BitCount);
            })
            .ToDictionary(entry => entry.PropertyName, entry => entry);
        }

        public readonly string BitVector ;

        internal MessageType5(string bitVector)
        {
            BitVector = bitVector;
            MessageId = Convert.ToInt32(BitVector.Substring(_propDict[nameof(MessageId)].Ordinal, _propDict[nameof(MessageId)].BitCount), 2);
            RepeatIndicator = Convert.ToInt32(BitVector.Substring(_propDict[nameof(RepeatIndicator)].Ordinal, _propDict[nameof(RepeatIndicator)].BitCount), 2);
            UserId = Convert.ToInt32(BitVector.Substring(_propDict[nameof(UserId)].Ordinal, _propDict[nameof(UserId)].BitCount), 2);
            ShipCargoType = (ShipCargoTypes)Convert.ToInt32(BitVector.Substring(_propDict[nameof(ShipCargoType)].Ordinal, _propDict[nameof(ShipCargoType)].BitCount), 2);
            Name = BitVector.Substring(_propDict[nameof(Name)].Ordinal, _propDict[nameof(Name)].BitCount).ToCharacterString();
            CallSign = BitVector.Substring(_propDict[nameof(CallSign)].Ordinal, _propDict[nameof(CallSign)].BitCount).ToCharacterString();
            MaxStaticDraught = ((decimal)Convert.ToInt32(BitVector.Substring(_propDict[nameof(MaxStaticDraught)].Ordinal, _propDict[nameof(MaxStaticDraught)].BitCount), 2))/10;
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

        [BitPosition(38, 2)]
        public int AISVersionIndicator { get ;set;}

        [BitPosition(40, 30)]
        public int IMONumber { get; set; }

        [BitPosition(70, 42)]
        public string CallSign { get; set; }

        [BitPosition(112, 120)]
        public string Name { get; set; }

        [BitPosition(232, 8)]
        public ShipCargoTypes ShipCargoType { get; set; }

        [BitPosition(240, 30)]
        public  int OverallDimension { get; set; }

        [BitPosition(270, 4)]
        public int PositionFixingDeviceType { get; set; }

        [BitPosition(274, 20)]
        public DateTime ETA { get; set; }

        [BitPosition(294, 8)]
        public decimal MaxStaticDraught { get; set; }

        [BitPosition(302, 120)]
        public string Destination { get; set; }

        [BitPosition(422, 1)]
        public int DTE { get;set;}

        [BitPosition(423, 1)]
        public int Spare { get; set; }

        public override string ToString()
        {
            return $"MsgType5: MMSI {UserId}, Name:{Name} ({CallSign}), Classification:{ShipCargoType}, Draught:{MaxStaticDraught}";
        }
    }
}
