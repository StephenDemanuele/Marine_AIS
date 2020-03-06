using AIS.Parser.Models;

namespace AIS.Parser.Contracts
{
    public interface IPacketFactory
    {
        Packet Get(string sentence);
    }
}
