using AIS.Parser.Models;
using AIS.Parser.Contracts;
using AIS.Parser.Exceptions;
using System.Threading.Tasks;
using AIS.Parser.Configuration;

namespace AIS.Parser
{
    public class NMEASentenceProcessor : ISentenceProcessor
    {
        private VesselCollection _vessels;

        private readonly IPacketFactory _packetFactory;

        private readonly ISentenceListener _listener;

        private readonly ParserConfiguration _configuration;

        private bool isRunning = false;
        private object padlock = new object();

        public event dlgVesselUpdate OnVesselUpdate;

        public NMEASentenceProcessor(
            IPacketFactory packetFactory, 
            ISentenceListener sentenceListener, 
            ParserConfiguration parserConfiguration,
            VesselCollection vesselCollection)
        {
            _packetFactory = packetFactory;
            _listener = sentenceListener;
            _configuration = parserConfiguration;
            _vessels = vesselCollection;

            _listener.OnSentenceReceived += _listener_OnSentenceReceived;
        }

        public async Task Start()
        {
            if (!isRunning)
            {
                lock(padlock)
                {
                    isRunning = true;
                }
                await _listener.Start();
            }
            else throw new ListenerAlreadyRunningException();
        }

        public void Stop()
        {
            if (!isRunning)
                return;

            lock(padlock)
            {
                isRunning = false;
                _listener.Stop();
            }
        }

        private void _listener_OnSentenceReceived(object sender, string s)
        {
            var packet = _packetFactory.Get(s);
            var updatedVessel = _vessels.Add(packet.Message, packet.Talker);
            OnVesselUpdate?.Invoke(this, updatedVessel);
        }
    }

    public delegate void dlgVesselUpdate(object sender, Vessel vessel);
}
