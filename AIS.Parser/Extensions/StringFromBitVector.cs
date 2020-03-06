using System;
using System.Text;

namespace AIS.Parser.Extensions
{
    public static class StringFromBitVector
    {
        static char[] trimChars = new char[] { '@', ' '};

        /// <summary>
        /// Converts a bit vector string consisting of only 0s and 1s
        /// into a string, using 6-bit ASCII as per
        /// https://gpsd.gitlab.io/gpsd/AIVDM.html Table3.
        /// </summary>
        /// <param name="bitVector">The bit vector for the string character-string field.</param>
        /// <returns>The character string.</returns>
        public static string ToCharacterString(this string bitVector)
        {
            var sb = new StringBuilder();
            for(var i = 0 ; i < bitVector.Length ; i+=6)
            {
                var x = Convert.ToInt32(bitVector.Substring(i, 6), 2);
                if (x <= 31) x+= 64;

                sb.Append((char)x);
            }
            var result = sb.ToString();

            return result.TrimEnd(trimChars);
        }
    }
}
