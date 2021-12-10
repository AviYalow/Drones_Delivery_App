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
using Microsoft.VisualBasic;
using IBL;
using IBL.BO;


namespace PL
{
    /// <summary>
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        IBL.IBL bl;
        DroneToList Drone;
        
        public DroneWindow(IBL.IBL bl)
        {
            
            InitializeComponent();
            Drone = new DroneToList();
            this.DataContext = Drone;
            this.bl = bl;
            WeightChoseCombo.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatus));
            OkButton.Content = "Add Drone";
            BaseChosingCombo.ItemsSource = bl.BaseStationWhitFreeChargingStationToLists();
            
        }
        public DroneWindow(IBL.IBL bl, DroneToList drone)
        {
            InitializeComponent();
            Drone = drone;
            this.bl = bl;
            OkButton.Content = "Update";
            this.DataContext = Drone;
            TitelDroneLabel.Content = "Updte Drone Window";
            SirialNumberTextBox.IsEnabled = false;
            SirialNumberTextBox.Text = Drone.SerialNumber.ToString();
            WeightChoseCombo.IsEnabled = false;
            WeightChoseCombo.Text = drone.WeightCategory.ToString();
            BaseChosingCombo.Visibility=Visibility.Collapsed;

        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            bl.UpdateDroneName(Drone.SerialNumber,Drone.Model);
        }
    }
}
