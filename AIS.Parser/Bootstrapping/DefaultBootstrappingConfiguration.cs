using AIS.Parser.Contracts;
using AIS.Parser.Models;
using Microsoft.Extensions.DependencyInjection;

namespace AIS.Parser.Bootstrapping
{
    public class DefaultBootstrappingConfiguration
    {
        public void Bootstrap(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IPacketFactory, PacketFactory>();
            serviceCollection.AddSingleton<ISentenceListener, NMEASentenceListener>();
            serviceCollection.AddSingleton<ISentenceProcessor, NMEASentenceProcessor>();

            var vesselCollection = new VesselCollection();
            serviceCollection.AddSingleton<VesselCollection>(vesselCollection);
            serviceCollection.AddTransient<IReadOnlyVesselCollection, VesselCollection>((serviceProvider) => vesselCollection);
        }
    }
}
