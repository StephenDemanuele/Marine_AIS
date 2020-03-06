using System;
using System.Text;

namespace AIS.Parser.Extensions
{
	public static class TwosComplementExtension
	{
		public static string ComplementIfNecessary(this string bin)
		{
			if (bin[0] == '0')
				return bin;

			var sb = new StringBuilder();
			//flip all bits
			foreach (var c in bin)
			{
				if (c == '0') sb.Append('1');
				else sb.Append('0');
			}

			var onesComplement = sb.ToString().ToCharArray();
			var i = onesComplement.Length - 1;
			for (; i >= 0; i--)
			{
				if (onesComplement[i] == '1')
				{
					onesComplement[i] = '0';
				}
				else
				{
					onesComplement[i] = '1';
					break;
				}
			}

			var result = string.Join("", onesComplement);
			if (i == -1)
			{
				//did not break cos loop went all the way
				//then 
				result.Insert(0, "1");
			}

			return result;
		}

		public static int ConvertToIntFromBinary(this string bin)
		{
			var complimented = bin.ComplementIfNecessary();
			var x = Convert.ToInt32(complimented, 2);
			if (bin[0] == '1')
				x = 0 - x;

			return x;
		}

	}
}
