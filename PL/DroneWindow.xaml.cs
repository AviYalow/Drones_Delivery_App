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
        bool sitoation;
        
        public DroneWindow(IBL.IBL bl)
        {
            
            InitializeComponent();
            Drone = new DroneToList();
            this.DataContext = Drone;
            this.bl = bl;
            WeightChoseCombo.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
            OkButton.Content = "Add Drone";
            
            BaseChosingCombo.ItemsSource = bl.BaseStationWhitFreeChargingStationToLists();
            sitoation = true;
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
            sitoation = false;
            DroneLabel.Content = bl.GetDrone(drone.SerialNumber);
            if (Drone.DroneStatus == DroneStatus.Free)
            {
                connectPackage.Visibility = Visibility.Visible;
                Charge.Visibility = Visibility.Visible;
            }
            else if (Drone.DroneStatus == DroneStatus.Maintenance)
            {
                Charge.Content = "Release from charge";
                Charge.Visibility = Visibility.Visible;
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!sitoation)
            {
                bl.UpdateDroneName(Drone.SerialNumber, Drone.Model);
                MessageBox.Show(Drone.ToString() + "\n update list!", "succesful");
            }
            else
            {
                bl.AddDrone(Drone, ((IBL.BO.BaseStationToList)BaseChosingCombo.SelectedItem).SerialNum);
                MessageBox.Show(Drone.ToString() + "\n add to list!", "succesful");
            }
        }

       

       

        private void SirialNumberTextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;
            if (text.Text.All(x => x >= '0' && x <= '9'))
            {
                ErrorMassegeLabel.Visibility = Visibility.Collapsed;
                SirialNumberTextBox.Background = Brushes.White;
                OkButton.IsEnabled = true;
            }

            //allow get out of the text box
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
                return;

            //allow list of system keys (add other key here if you want to allow)
            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
                e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home || e.Key == Key.End ||
                e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right || e.Key == Key.NumPad0
                || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5
                || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9
                )
                return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

            //allow control system keys
            if (Char.IsControl(c)) return;

            //allow digits (without Shift or Alt)
            if (Char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox

            //forbid letters and signs (#,$, %, ...)
            ErrorMassegeLabel.Visibility = Visibility.Visible;
            SirialNumberTextBox.Background = Brushes.Red;
            OkButton.IsEnabled = false;

            return;
        }

        private void WeightChoseCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            Drone.WeightCategory =(IBL.BO.WeightCategories) WeightChoseCombo.SelectedItem;
            

        }

        private void Charge_Click(object sender, RoutedEventArgs e)
        {
            if (Drone.DroneStatus == DroneStatus.Free)
                bl.DroneToCharge(Drone.SerialNumber);
            else if(Drone.DroneStatus == DroneStatus.Maintenance)
                bl.FreeDroneFromCharging(Drone.SerialNumber)


        }

        private void connectPackage_Click(object sender, RoutedEventArgs e)
        {
            bl.ConnectPackegeToDrone(Drone.SerialNumber);
        }
    }
}
