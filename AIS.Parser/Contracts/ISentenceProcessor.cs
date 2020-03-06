using System.Threading.Tasks;

namespace AIS.Parser.Contracts
{
    public interface ISentenceProcessor
    {
        Task Start();

        void Stop();

        event dlgVesselUpdate OnVesselUpdate;
    }
}
