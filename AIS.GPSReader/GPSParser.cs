using System;
using System.IO.Ports;
using System.Threading;
using AIS.GPSReader.Models;

namespace AIS.GPSReader
{
    /// <summary>
    /// Role of the parser is to :
    /// 1. poll a serial port for NMEA0183 data
    /// 2. parse the data into messages
    /// 3. raise events with parsed messages
    /// </summary>
    public class GPSParser : IDisposable
    {
        private readonly SerialPort serialPort = null;

        private TimerCallback tcb = null;

        private Timer timer = null;

        private readonly int pollPeriod = 1000;

        public event OnMessageReceived OnSentenceReceived;

        public GPSParser(string comPort)
        {
            serialPort = new SerialPort(comPort);
            serialPort.BaudRate = 9600;
            tcb = new TimerCallback(HandleSentences);
        }

        public void Start()
        {
            serialPort.Open();
            timer = new Timer(tcb, null, 1000, pollPeriod);
        }

        private void HandleSentences(object state)
        {
            var existingData = ((SerialPort)serialPort).ReadExisting();
            var sentences = existingData.Split("$");
            foreach (var sentence in sentences)
            {
                var _sentence = sentence.Replace("\r\n", string.Empty);
                if (string.IsNullOrEmpty(sentence))
                    continue;

                var message = Get(sentence);
                OnSentenceReceived?.Invoke(message);
            }
        }

        private GPMessage Get(string sentence)
        {
            if (string.IsNullOrEmpty(sentence))
                return null;

            if (sentence.StartsWith("GPGGA"))
                return new GGA(sentence);

            if (sentence.StartsWith("GPGLL"))
                return new GLL(sentence);

            return new MessageNotImplemented(sentence);
        }

        public void Dispose()
        {
            timer.Change(0, Timeout.Infinite);
            timer.Dispose();
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
            serialPort.Dispose();
        }
    }

    public delegate void OnMessageReceived(GPMessage message);
}
