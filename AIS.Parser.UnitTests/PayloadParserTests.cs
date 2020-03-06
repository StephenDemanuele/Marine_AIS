using Xunit;
using System;
using System.Linq;
using AIS.Parser.Models;
using System.Reflection;
using AIS.Parser.Extensions;
using AIS.Parser.Models.Messages;
using AIS.Parser.Configuration;

namespace AIS.Parser.UnitTests
{
	public class PayloadParserTests
	{
		private PacketFactory _sut;

		public PayloadParserTests()
		{
            var observationPoint = new ObservationPoint(35.89167m, 14.5075M);
            var parserConfiguration = new ParserConfiguration(observationPoint, 12345);
			_sut = new PacketFactory(parserConfiguration);
		}

		[Theory]
		[InlineData("13HOI:0P0000VOHLCnHQKwvL05Ip", 227006760, 0, NavStatus.UnderwayUsingEngine, 0.13138, 49.47558, 36.7)]
		[InlineData("14eGL:@000o8oQ`LMjOchmG@08HK", 316005417, 0, NavStatus.UnderwayUsingEngine, -123.89197, 49.74698, 301.1)]
		[InlineData("14eG;o@034o8sd<L9i:a;WF>062D", 316001245, 19.6, NavStatus.UnderwayUsingEngine, -123.87775, 49.20028, 235)]
		[InlineData("177KQJ5000G?tO`K>RA1wUbN0TKH", 477553000, 0, NavStatus.Moored, -122.34583, 47.58283, 51)]
		public void PayloadConversion_toBits(
			string payload, int userId, decimal sog, NavStatus navStatus, decimal lng, decimal lat, decimal cog)
		{
			var packet = _sut.ParsePayload(payload) as MessageType1;
			Assert.Equal(168, packet.BitVector.Length);
			Assert.Equal(userId, packet.UserId);
			Assert.Equal(sog, packet.SOG);
			Assert.Equal(navStatus, packet.NavigationalStatus);
			Assert.Equal(lng, Math.Round(packet.Longitude, 5));
			Assert.Equal(lat, Math.Round(packet.Latitude, 5));
			Assert.Equal(cog, packet.COG);
		}

		[Fact]
		public void NumberOfBitsInMessageType_should_equal_168()
		{
			var properties = typeof(MessageType1).GetProperties();
			var totalNumberOfBits = 0;
			properties.ToList().ForEach(prop =>
			{
				var bitPositionAttribute = prop.GetCustomAttribute<BitPositionAttribute>();
				totalNumberOfBits += bitPositionAttribute.BitCount;
			});

			Assert.Equal(169, totalNumberOfBits);
		}

        [Theory]
        [InlineData("000101010101010010001111000110000101010010010010011001100000001101000001001100010100000001000000000000000000000000000000", "EUROFERRY MALTA")]
        public void BitVectorForCharacterString_should_parse(string bitVector, string expected)
        {
            var result = bitVector.ToCharacterString();
            Assert.Equal(expected, result);
        }
	}
}
