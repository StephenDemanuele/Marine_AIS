using System;
using System.Linq;
using System.Reflection;
using AIS.Parser.Extensions;
using System.Collections.Generic;

namespace AIS.Parser.Models.Messages
{
	public class MessageType3 : IMessage
	{
		static Dictionary<string, BitsPosition> _propDict;

		static MessageType3()
		{
			_propDict = typeof(MessageType1).GetProperties().Where(x => x.CustomAttributes.Any()).Select(prop =>
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

		internal MessageType3(string bitVector)
		{
			BitVector = bitVector;
			MessageId = Convert.ToInt32(BitVector.Substring(_propDict[nameof(MessageId)].Ordinal, _propDict[nameof(MessageId)].BitCount), 2);
			RepeatIndicator = Convert.ToInt32(BitVector.Substring(_propDict[nameof(RepeatIndicator)].Ordinal, _propDict[nameof(RepeatIndicator)].BitCount), 2);
			UserId = Convert.ToInt32(BitVector.Substring(_propDict[nameof(UserId)].Ordinal, _propDict[nameof(UserId)].BitCount), 2);
			NavigationalStatus = (NavStatus)Convert.ToInt32(BitVector.Substring(_propDict[nameof(NavigationalStatus)].Ordinal, _propDict[nameof(NavigationalStatus)].BitCount), 2);
			RateOfTurn = Convert.ToInt32(BitVector.Substring(_propDict[nameof(RateOfTurn)].Ordinal, _propDict[nameof(RateOfTurn)].BitCount), 2);
			SOG = ((decimal)Convert.ToInt32(BitVector.Substring(_propDict[nameof(SOG)].Ordinal, _propDict[nameof(SOG)].BitCount), 2)) / 10;
			PositionAccuracy = Convert.ToInt32(BitVector.Substring(_propDict[nameof(PositionAccuracy)].Ordinal, _propDict[nameof(PositionAccuracy)].BitCount), 2);

			var rawLng = BitVector.Substring(_propDict[nameof(Longitude)].Ordinal, _propDict[nameof(Longitude)].BitCount).ConvertToIntFromBinary();
			Longitude = ((decimal)rawLng) / 600000;

			var rawLat = BitVector.Substring(_propDict[nameof(Latitude)].Ordinal, _propDict[nameof(Latitude)].BitCount).ConvertToIntFromBinary();
			Latitude = ((decimal)rawLat) / 600000;

			COG = ((decimal)Convert.ToInt32(BitVector.Substring(_propDict[nameof(COG)].Ordinal, _propDict[nameof(COG)].BitCount), 2)) / 10;
			TrueHeading = Convert.ToInt32(BitVector.Substring(_propDict[nameof(TrueHeading)].Ordinal, _propDict[nameof(TrueHeading)].BitCount), 2);
            if (TrueHeading == 511) TrueHeading = null;

			Timestamp = Convert.ToInt32(BitVector.Substring(_propDict[nameof(Timestamp)].Ordinal, _propDict[nameof(Timestamp)].BitCount), 2);
			SpecialManeuvreIndicator = Convert.ToInt32(BitVector.Substring(_propDict[nameof(SpecialManeuvreIndicator)].Ordinal, _propDict[nameof(SpecialManeuvreIndicator)].BitCount), 2);
			CommunicationState = Convert.ToInt32(BitVector.Substring(_propDict[nameof(CommunicationState)].Ordinal, _propDict[nameof(CommunicationState)].BitCount), 2);
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

		/// <summary>
		/// 0 = under way using engine, 1 = at anchor, 2 = not under command, 3 = restricted maneuverability, 
		/// 4 = constrained by her draught, 5 = moored, 6 = aground, 7 = engaged in fishing, 8 = under way sailing, 
		/// 9 = reserved for future amendment of navigational status for ships carrying DG, HS, or MP, 
		/// or IMO hazard or pollutant category C, high speed craft (HSC), 
		/// 10 = reserved for future amendment of navigational status for ships carrying dangerous goods (DG), 
		/// harmful substances (HS) or marine pollutants (MP) or IMO hazard or pollutant category A, wing in ground (WIG); 
		/// 11 = power-driven vessel towing astern (regional use); 
		/// 12 = power-driven vessel pushing ahead or towing alongside (regional use);
		/// 13 = reserved for future use,
		/// 14 = AIS-SART(active), MOB-AIS, EPIRB-AIS
		/// 15 = undefined = default (also used by AIS-SART, MOB-AIS and EPIRB-AIS under test)
		/// </summary>
		[BitPosition(38, 4)]
		public NavStatus NavigationalStatus { get; set; }

		/// <summary>
		///	0 to +126 = turning right at up to 708 deg per min or higher
		/// 0 to -126 = turning left at up to 708 deg per min or higher Values between 0 and 708 deg per min coded by ROTAIS = 4.733 SQRT(ROTsensor) degrees per min
		/// where ROTsensor is the Rate of Turn as input by an external Rate of Turn Indicator(TI). ROTAIS is rounded to the nearest integer value.
		/// +127 = turning right at more than 5 deg per 30 s (No TI available)
		/// -127 = turning left at more than 5 deg per 30 s(No TI available)
		/// -128 (80 hex) indicates no turn information available(default).
		/// ROT data should not be derived from COG information.
		/// </summary>
		[BitPosition(42, 8)]
		public int RateOfTurn { get; set; }

		/// <summary>
		/// Speed over ground in 1/10 knot steps (0-102.2 knots)
		/// 1 023 = not available, 1 022 = 102.2 knots or higher
		/// </summary>
		[BitPosition(50, 10)]
		public decimal SOG { get; set; }

		/// <summary>
		/// The position accuracy (PA) flag should be determined in accordance with the table below:
		/// 1 = high(<= 10 m)
		/// 0 = low(> 10 m)
		/// 0 = default
		/// </summary>
		[BitPosition(60, 1)]
		public int PositionAccuracy { get; set; }

		/// <summary>
		/// Longitude in 1/10 000 min (+/-180 deg, East = positive (as per 2's complement), West = negative (as per 2's complement).
		/// 181= (6791AC0h) = not available = default)
		/// </summary>
		[BitPosition(61, 28)]
		public decimal Longitude { get; set; }

		/// <summary>
		/// Latitude in 1/10 000 min (+/-90 deg, North = positive (as per 2's complement), South = negative (as per 2's complement). 
		/// 91deg (3412140h) = not available = default)
		/// </summary>
		[BitPosition(89, 27)]
		public decimal Latitude { get; set; }

		/// <summary>
		/// Course over ground in 1/10 = (0-3599). 3600 (E10h) = not available = default. 3 601-4 095 should not be used
		/// </summary>
		[BitPosition(116, 12)]
		public decimal COG { get; set; }

		/// <summary>
		/// Degrees (0-359) (511 indicates not available = default)
		/// </summary>
		[BitPosition(128, 9)]
		public int? TrueHeading { get; set; }

        private string TrueHeadingString => TrueHeading.HasValue ? TrueHeading.Value.ToString("N2") : "N/A";

        /// <summary>
        /// UTC second when the report was generated by the electronic position system (EPFS) (0-59, or 60 if time stamp is not available, 
        /// which should also be the default value, or 61 if positioning system is in manual input mode, 
        /// or 62 if electronic position fixing system operates in estimated (dead reckoning) mode, or 63 if the positioning system 
        /// is inoperative)
        /// </summary>
        [BitPosition(137, 6)]
		public int Timestamp { get; set; }

		/// <summary>
		/// 0 = not available = default
		/// 1 = not engaged in special maneuver
		/// 2 = engaged in special maneuver
		/// (i.e.: regional passing arrangement on Inland Waterway)
		/// </summary>
		[BitPosition(143, 2)]
		public int SpecialManeuvreIndicator { get; set; }

		/// <summary>
		/// Not used. Should be set to zero, reserved for future use.
		/// </summary>
		[BitPosition(145, 3)]
		public int Spare { get; set; }

		/// <summary>
		/// Receiver autonomous integrity monitoring (RAIM) flag of electronic position fixing device; 
		/// 0 = RAIM not in use = default; 1 = RAIM in use. See Table
		/// </summary>
		[BitPosition(148, 1)]
		public int RAIMFlag { get; set; }

		[BitPosition(149, 19)]
		public int CommunicationState { get; set; }

        public decimal Distance { get; set; }

        public override string ToString()
		{
            return $"MsgType3: MMSI {UserId}, {NavigationalStatus}, Lon {Longitude.ToString("N4")}, Lat {Latitude.ToString("N4")}, Heading {TrueHeadingString}, SOG {SOG.ToString("N2")}";
        }
	}
}
