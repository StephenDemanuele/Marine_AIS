using System.Net;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using AIS.Parser.Contracts;
using System.Threading.Tasks;
using AIS.Parser.Configuration;

namespace AIS.Parser
{
    /// <summary>
    ///     This starts a task which listens on an IPEndPoint
    ///     and blocks until receives a message.
    ///     Raises event <see cref="OnSentenceReceived"/> with the nmea
    ///     sentence when one is received.
    /// </summary>
    internal class NMEASentenceListener : ISentenceListener
    {
        private readonly int _port;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public event dlgString OnSentenceReceived;

        public NMEASentenceListener(ParserConfiguration parserConfiguration)
        {
            _port = parserConfiguration.ListenOnPort;
        }

        public async Task Start()
        {
            var token = _cancellationTokenSource.Token;

            await Task.Factory.StartNew(async () =>
            {
                var udpClient = new UdpClient(new IPEndPoint(IPAddress.Any, _port));
                while (true)
                {
                    var msg = await udpClient.ReceiveAsync();
                    OnSentenceReceived?.Invoke(this, Encoding.UTF8.GetString(msg.Buffer));

                    if (token.IsCancellationRequested)
                    {
                        udpClient.Close();
                        udpClient.Dispose();

                        break;
                    }
                }
            },
            token);
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }
    }

    public delegate void dlgString(object sender, string s);
}
