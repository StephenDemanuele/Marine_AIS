using System.Linq;
using System.Windows;
using AIS.Parser.Models;
using System.Windows.Media;
using AIS.Viewer.ViewModels;
using Microsoft.Maps.MapControl.WPF;

namespace AIS.Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly OpacityCalculator _opacityCalculator = new OpacityCalculator();

        public MainWindow()
        {
            InitializeComponent();
            DC = new MainWindowViewModel()
            {
                ObservationPointLatitude = 35.89166667m,
                ObservationPointLongitude = 14.508333m,
                ListenOnPort = 12345
            };
            DC.OnVesselUpdate += OnVesselUpdate;
            map.Mode = new AerialMode(true);
        }

        internal MainWindowViewModel DC
        {
            get
            {
                return this.DataContext as MainWindowViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        private void OnVesselUpdate(object sender, Vessel vessel)
        {
            Dispatcher.Invoke(() =>
            {
                var existingVesselPin = map.Children.OfType<Pushpin>().FirstOrDefault(x => x.Uid == vessel.UserId.ToString());
                var location = new Location((double)vessel.Latitude, (double)vessel.Longitude);
                if (existingVesselPin == null)
                {
                    var pin = new Pushpin
                    {
                        Uid = vessel.UserId.ToString(),
                        Location = location,
                        Background = (vessel.NavigationalStatus == Parser.NavStatus.Moored? Brushes.Red: Brushes.Green),
                        BorderThickness = new Thickness(1),
                        Content = vessel.Name,
                        Heading = vessel.TrueHeading,
                        FontSize = 4,
                        ToolTip = vessel.ToString()
                    };
                    map.Children.Add(pin);

                    return;
                }

                existingVesselPin.Location = location;
                existingVesselPin.Heading = vessel.TrueHeading;
                existingVesselPin.BorderBrush = (vessel.NavigationalStatus == Parser.NavStatus.Moored ? Brushes.Red : Brushes.Green);
                existingVesselPin.Opacity = _opacityCalculator.Get(vessel.LastUpdate);
                existingVesselPin.ToolTip = vessel.ToString();
            });
        }
        
        private async void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            await DC.StartListening();
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            DC.StopListening();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var pin = new Pushpin
            {
                Uid = "observationPoint",
                Location = new Location((double)DC.ObservationPointLatitude, (double)DC.ObservationPointLongitude),
                Background = Brushes.MediumPurple,
                BorderThickness = new Thickness(1),
                Content = "you",
                Heading = 0
            };
            map.Children.Add(pin);
        }

        private void BtnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            map.ZoomLevel--;
        }

        private void BtnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            map.ZoomLevel++;
        }
    }
}
