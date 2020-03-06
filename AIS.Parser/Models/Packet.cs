using AIS.Parser.Models.Messages;

namespace AIS.Parser.Models
{
	public class Packet
	{
		private readonly string rawSentence;

		public Packet(string sentence)
		{
			rawSentence = sentence;
		}

		//pos0
		public string Type { get; set; } //AIVDM or AIVDO

		//pos1
		public int FragmentCount { get; set; }

		//pos2
		public int FragmentNumber { get; set; }

		//pos3
		public int SequentialMessageId { get; set; }

		//pos4
		public string RadioChannelCode{ get; set; }

		//pos5
		public string Payload { get; set; }

		//message obtained after parsing BinaryPayload (aka BitVector)
		public IMessage Message { get; set; }

		//pos6
		public int NumberOfFillBits { get; set; }

		//pos6 after *
		public string DataIntegrityChecksum { get; set; }

		//inferred from Type property.
		public Talker Talker { get; set; }

		public override string ToString()
		{
			return $"{Type},Talker:{Talker.Value}, [{Message.ToString()}]";
		}
	}
}
