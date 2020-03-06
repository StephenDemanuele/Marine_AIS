using AIS.Parser.Contracts;
using AIS.Parser.Models.Messages;
using System.Collections;
using System.Collections.Generic;

namespace AIS.Parser.Models
{
    public class VesselCollection : IReadOnlyVesselCollection
    {
        private readonly Dictionary<int, Vessel> Vessels;

        public VesselCollection()
        {
            Vessels = new Dictionary<int, Vessel>();
        }

        public Vessel Add(IMessage message, Talker talker)
        {
            if (!Vessels.ContainsKey(message.UserId))
            {
                Vessels.Add(message.UserId, new Vessel(message.UserId, talker));
            }

            Vessels[message.UserId].UpdateWith(message);

            return Vessels[message.UserId];
        }

        public IReadOnlyCollection<Vessel> AsReadOnly()
        {
            return Vessels.Values;
        }
    }
}
