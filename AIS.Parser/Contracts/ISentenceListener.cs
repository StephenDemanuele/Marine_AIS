using System.Threading.Tasks;

namespace AIS.Parser.Contracts
{
    public interface ISentenceListener
    {
        event dlgString OnSentenceReceived;

        Task Start();

        void Stop();
    }
}
