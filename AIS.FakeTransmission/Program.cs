using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace AIS.FakeTransmission
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CancelKeyPress += (sender, cancelEventArgs) => { Environment.Exit(0); };
            var waitForMilliseconds = 5000;

            var filename = Path.Combine(Environment.CurrentDirectory, "nmea-sample");
            var lines = File.ReadAllLines(filename);
            Console.WriteLine($"Starting AIS fake transmission after {waitForMilliseconds/1000} seconds. Source:{filename}");
            using (var udpClient = new UdpClient())
            {
                udpClient.Connect(new IPEndPoint(IPAddress.Loopback, 12345));
                Thread.Sleep(waitForMilliseconds);

                foreach (var line in lines)
                {
                    Console.WriteLine($"Transmitting line {line}");
                    var bytes = Encoding.UTF8.GetBytes(line);
                    udpClient.Send(bytes, bytes.Length);

                    Thread.Sleep(2000);
                }
                udpClient.Close();
            }
        }
    }
}
