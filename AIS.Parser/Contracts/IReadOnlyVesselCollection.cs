using AIS.Parser.Models;
using System.Collections.Generic;

namespace AIS.Parser.Contracts
{
    public interface IReadOnlyVesselCollection
    {
        IReadOnlyCollection<Vessel> AsReadOnly();
    }
}
