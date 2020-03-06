using System;
using System.Collections.Generic;
using System.Text;

namespace AIS.GPSReader.Models
{
    public class GSA : GPMessage
    {
        internal GSA(string sentence)
        {
             var parts = sentence.Split(',');
            MessageId = parts[0];

        }

        public char Mode1 { get; }

        public byte Mode2 { get; }

        public byte SatelliteUsed { get ;}

    }
}
