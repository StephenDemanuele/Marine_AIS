using System;

namespace AIS.Parser.Models
{
	[AttributeUsage(AttributeTargets.Property)]
	internal class BitPositionAttribute : Attribute
	{
		public int OrdinalPosition { get; }

		public int BitCount { get; }

		/// <param name="ordinalPosition">
		/// The zero-based starting position of 
		/// the field in the bit vector.
		/// </param>
		/// <param name="bitCount">The number of bits.</param>
		public BitPositionAttribute(int ordinalPosition, int bitCount)
		{
			OrdinalPosition = ordinalPosition;
			BitCount = bitCount;
		}
	}

	internal class BitsPosition
	{
		public BitsPosition(string propertyName, int ordinal, int bitCount)
		{
			PropertyName = propertyName;
			Ordinal = ordinal;
			BitCount = bitCount;
		}

		public string PropertyName { get; }

		public int Ordinal { get; }

		public int BitCount { get; }
	}
}
