using AIS.Parser.Contracts;
using Microsoft.AspNetCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System;

namespace AIS.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();
            var sentenceProcessor = webHost.Services.GetService(typeof(ISentenceProcessor)) as ISentenceProcessor;

            var runningTask = Task.Factory.StartNew(() =>
            {
                sentenceProcessor.OnVesselUpdate += (sender, vessel) =>
                {
                    Console.WriteLine($"{vessel.VesselId}");
                };
                sentenceProcessor.Start().Wait();
            }, TaskCreationOptions.AttachedToParent);
            
            webHost.Run();

            Console.Write("Is sentence_processor terminated ?");
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
