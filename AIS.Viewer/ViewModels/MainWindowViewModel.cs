using AIS.Parser;
using AIS.Parser.Configuration;
using AIS.Parser.Contracts;
using AIS.Parser.Models;
using System.Threading.Tasks;
using System.Windows;

namespace AIS.Viewer.ViewModels
{
    internal class MainWindowViewModel : Bindable
    {
        private ISentenceProcessor _processor;
    
        public event dlgVesselUpdate OnVesselUpdate;

        public MainWindowViewModel()
        {
            ReceptionEllipse = Visibility.Hidden;
            CanStart = true;
            CanStop = false;
        }

        public decimal ObservationPointLongitude
        {
            get
            {
                return Get<decimal>();
            }
            set
            {
                Set(value);
            }
        }

        public decimal ObservationPointLatitude
        {
            get
            {
                return Get<decimal>();
            }
            set
            {
                Set(value);
            }
        }

        public Visibility ReceptionEllipse
        {
            get
            {
                return Get<Visibility>();
            }
            private set
            {
                Set(value);
            }
        } 

        public int ListenOnPort
        {
            get
            {
                return Get<int>();
            }
            set
            {
                Set(value);
            }
        }

        public async Task StartListening()
        {
            CanStart = false;
            CanStop = true;
            var config = new ParserConfiguration(new ObservationPoint(ObservationPointLatitude, ObservationPointLongitude), ListenOnPort);
            _processor = new NMEASentenceProcessor(
                new PacketFactory(config), 
                new NMEASentenceListener(config), 
                config,
                new VesselCollection());
            _processor.OnVesselUpdate += W_OnVesselUpdate;

            await _processor.Start();
         }

        public void StopListening()
        {
            CanStart = true;
            CanStop = false;

            _processor.Stop();
            _processor.OnVesselUpdate -= W_OnVesselUpdate;
        }

        public bool CanStart
        {
            get
            {
                return Get<bool>();
            }
            private set
            {
                Set(value);
            }
        }

        public bool CanStop
        {
            get
            {
                return Get<bool>();
            }
            private set
            {
                Set(value);
            }
        }

        private void W_OnVesselUpdate(object sender, Vessel vessel)
        {
            ReceptionEllipse = Visibility.Visible;
            OnVesselUpdate?.Invoke(this, vessel);
            ReceptionEllipse = Visibility.Hidden;
        }
    }
}
