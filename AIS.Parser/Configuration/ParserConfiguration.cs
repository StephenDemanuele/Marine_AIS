using System;
using AIS.Parser.Models;
using AIS.Parser.Exceptions;
using Microsoft.Extensions.Configuration;

namespace AIS.Parser.Configuration
{
    public class ParserConfiguration
    {
        private const string ListenOnPort_key = "ParserConfiguration:ListenOnPort";
        private const string ObsPntLat_key = "ParserConfiguration:ObservationPointLat";
        private const string ObsPntLon_key = "ParserConfiguration:ObservationPointLon";

        public ParserConfiguration(ObservationPoint observationPoint, int listenOnPort)
        {
            ObservationPoint = observationPoint;
            ListenOnPort = listenOnPort;
        }

        public ObservationPoint ObservationPoint { get; }

        public int ListenOnPort { get; }

        public static ParserConfiguration Get(IConfiguration configuration)
        {
            try
            {
                var listenOnPort = Int32.Parse(configuration[ListenOnPort_key]);
                var lat = decimal.Parse(configuration[ObsPntLat_key]);
                var lon = decimal.Parse(configuration[ObsPntLon_key]);

                return new ParserConfiguration(
                    new ObservationPoint(lat, lon), 
                    listenOnPort);
            }
            catch (Exception ex)
            {
                throw new ConfigurationLoadingException(ex);
            }
        }
    }
}
