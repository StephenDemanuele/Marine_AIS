using System;
using System.Linq;
using System.Text;
using AIS.Parser.Models;
using AIS.Parser.Helpers;
using AIS.Parser.Contracts;
using AIS.Parser.Configuration;
using AIS.Parser.Models.Messages;

namespace AIS.Parser
{
    internal class PacketFactory : IPacketFactory
    {
        private readonly ObservationPoint _observationPoint;

        private readonly DistanceCalculator _distanceCalculator;
        
        private readonly ParserConfiguration _parserConfiguration;

        public PacketFactory(ParserConfiguration parserConfiguration)
        {
            _parserConfiguration = parserConfiguration;
            _observationPoint = _parserConfiguration.ObservationPoint;
            _distanceCalculator = new DistanceCalculator();
        }

        public Packet Get(string sentence)
        {
            var packet = new Packet(sentence);
            var values = sentence.Split(',');

            packet.Type = values[0].Replace("!", string.Empty);
            packet.Talker = new Talker((TalkerId)Enum.Parse(typeof(TalkerId), packet.Type.Substring(0, 2)));

            packet.FragmentCount = int.Parse(values[1]);
            packet.FragmentNumber = int.Parse(values[2]);
            packet.SequentialMessageId = string.IsNullOrEmpty(values[3]) ? 0 : int.Parse(values[3]);
            packet.RadioChannelCode = values[4];
            packet.Payload = values[5];
            packet.Message = ParsePayload(packet.Payload);

            var checksumPart = values[6];
            packet.NumberOfFillBits = int.Parse(checksumPart.Substring(0, 1));
            packet.DataIntegrityChecksum = checksumPart.Substring(2, 2);

            return packet;
        }

        internal IMessage ParsePayload(string payload)
        {
            var bitVector = ConvertPayload(payload);

            try
            {
                switch (payload.First())
                {
                    case '1':
                        var msg1 = new MessageType1(bitVector);
                        msg1.Distance = _distanceCalculator.Calc(_observationPoint.Longitude, _observationPoint.Latitude, msg1.Longitude, msg1.Latitude);
                        return msg1;
                    case '3':
                        var msg3 = new MessageType3(bitVector);
                        msg3.Distance = _distanceCalculator.Calc(msg3.Longitude, msg3.Latitude, _observationPoint.Longitude, _observationPoint.Latitude);
                        return msg3;
                    case '4':
                        var msg4 = new MessageType4(bitVector);
                        msg4.Distance = _distanceCalculator.Calc(msg4.Longitude, msg4.Latitude, _observationPoint.Longitude, _observationPoint.Latitude);
                        return msg4;
                    case '5':
                        return new MessageType5(bitVector);
                    default:
                        return new MessageTypeNotImplemented(payload);
                }
            }
            catch (Exception ex)
            {
                return new MessageParsingError($"Payload: {payload}, {ex.Message}", ex.StackTrace);
            }
        }

        /// <summary>
        /// Converts the AIS Encoded data into a binary string.
        /// </summary>
        /// <param name="payload">eg. 13HOI:0P0000VOHLCnHQKwvL05Ip</param>
        /// <returns>00001010010100101...</returns>
        private string ConvertPayload(string payload)
        {
            var result = new StringBuilder();
            foreach (var c in payload)
            {
                var asciiValue = (int)c;
                var reduced = asciiValue - 48;
                if (reduced > 40)
                    reduced -= 8;

                var bits = Convert.ToString(reduced, 2)
                    .PadLeft(6, '0');

                result.Append(bits);
            }

            return result.ToString();
        }
    }
}
