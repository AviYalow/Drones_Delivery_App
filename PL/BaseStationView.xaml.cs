using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BlApi;
using BO;
using Mapsui.Utilities;
using Mapsui.Layers;
using Windows.UI.Xaml.Controls.Maps;

using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;


namespace PL
{
  
    /// <summary>
    /// Interaction logic for BaseStationView.xaml
    /// </summary>
    public partial class BaseStationView : Window
    {
        BlApi.IBL bl;
        BaseStation baseStation;
        Location location;
        public BaseStationView(BlApi.IBL bL)
        {
            InitializeComponent();
            bl = bL;
            baseStation = new BaseStation();
            baseStation.Location = new Location();
            location = new Location();
            DataContext = baseStation;
            LongitudeText.DataContext = baseStation.Location;
            Latitudtext.DataContext = baseStation.Location;
       
        }
        public BaseStationView(BlApi.IBL bL,BaseStationToList base_)
        {
            InitializeComponent();
            bl = bL;
            baseStation = bl.BaseByNumber(base_.SerialNum);
            SerialText.IsEnabled = false;
            location = baseStation.Location;
            DataContext = baseStation;
            LongitudeText.DataContext = baseStation.Location;
            Latitudtext.DataContext = baseStation.Location;
            
        }
        private async void MapControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Specify a known location.
            BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = 47.604, Longitude = -122.329 };
            var cityCenter = new Geopoint(cityPosition);
          
            // Set the map location.
            await (sender as Microsoft.Toolkit.Wpf.UI.Controls. MapControl).TrySetViewAsync(cityCenter, 12);
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddBase(baseStation);
            }catch(Exception ex)
            { MessageBox.Show(ex.ToString(), "ERROE"); }
        }
    }
}
