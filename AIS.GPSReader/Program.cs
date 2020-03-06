using AIS.GPSReader.Models;
using System;
using System.Text;

namespace AIS.GPSReader
{
    class Program
    {
        static StringBuilder sb = new StringBuilder();

        static void Main(string[] args)
        {
            using (var parser = new GPSParser("COM4"))
            {
                parser.OnSentenceReceived += (message) =>
                {
                    if (message is GLL || message is GGA)
                        Console.WriteLine(message);
                };

                parser.Start();
                Console.Read();
                Console.WriteLine("Exiting...");
            }
        }
    }
}
