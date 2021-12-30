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
using BlApi;
using BO;
using System.Reflection;
using Microsoft.Win32;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        IBL bl;
        Drone drone;
        DroneToList droneToList;



        public DroneWindow(BlApi.IBL bl)
        {

            InitializeComponent();

            droneToList = new DroneToList();
            this.DataContext = drone;
            SirialNumberTextBox.DataContext = droneToList;
            StatusComb.Items.Add(DroneStatus.Maintenance);
            StatusComb.SelectedItem = StatusComb.Items[0];
            droneToList.DroneStatus = (DroneStatus)StatusComb.SelectedItem;
            this.bl = bl;
            WeightChoseCombo.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            ModelComboBox.ItemsSource = Enum.GetValues(typeof(DroneModel));
            BaseChosingCombo.ItemsSource = bl.BaseStationWhitFreeChargingStationToLists();
            DroneLabel.Visibility = Visibility.Hidden;
        }

        public DroneWindow(BlApi.IBL bl, uint droneFromListView)
        {
            InitializeComponent();
            ctorUpdateDronWindow(bl, droneFromListView);


        }

        public DroneWindow(BlApi.IBL bl, DroneToList droneFromListView)
        {
            InitializeComponent();
            ctorUpdateDronWindow(bl, droneFromListView.SerialNumber);


        }

        private void ctorUpdateDronWindow(BlApi.IBL bl, uint droneFromListView)
        {
            this.bl = bl;
            this.drone = bl.GetDrone(droneFromListView);

            SirialNumberTextBox.DataContext = drone;
            this.DataContext = this.drone;
            TitelDroneLabel.Content = "Updte Drone Window";
            SirialNumberTextBox.IsEnabled = false;
            SirialNumberTextBox.DataContext = droneToList;
            BaseChosingCombo.Text = this.drone.Location.ToString();

            ModelComboBox.ItemsSource = Enum.GetValues(typeof(DroneModel));
            ModelComboBox.SelectedItem = this.drone.Model;
            DroneLabel.DataContext = bl.GetDrone(droneFromListView);
            if (this.drone.DroneStatus == DroneStatus.Free)
                StatusComb.ItemsSource = Enum.GetValues(typeof(DroneStatus));
            else if (this.drone.DroneStatus == DroneStatus.Maintenance)
            {
                StatusComb.Items.Add(DroneStatus.Maintenance);
                StatusComb.Items.Add(DroneStatus.Free);

            }
            else if (this.drone.DroneStatus == DroneStatus.Work)
            {
                StatusComb.Items.Add(DroneStatus.Work);
                StatusComb.IsEnabled = false;
            }
            OkButton.Visibility = Visibility.Collapsed;
            StatusComb.SelectedItem = drone.DroneStatus;
            LocationLabel.Visibility = Visibility.Collapsed;
            BaseChosingCombo.Visibility = Visibility.Collapsed;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;

        }

        private void DronesWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = false;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {

            if (BaseChosingCombo.SelectedItem is null || ModelComboBox.SelectedItem is null || SirialNumberTextBox.Text == "" || SirialNumberTextBox.Text == "0" || WeightChoseCombo.SelectedItem is null)
            {
                if (BaseChosingCombo.SelectedItem is null)
                    InputMissingLocationLabel.Visibility = Visibility.Visible;
                if (WeightChoseCombo.SelectedItem is null)
                    InputMissingWightLabel.Visibility = Visibility.Visible;
                if (ModelComboBox.SelectedItem is null)
                {
                    InputMissingModelLabel.Visibility = Visibility.Visible;
                    ModelComboBox.BorderBrush = Brushes.Red;
                }
                if (SirialNumberTextBox.Text == "" || SirialNumberTextBox.Text == "0")
                {
                    InputMissingSirialNumberLabel.Visibility = Visibility.Visible;
                    InputMissingSirialNumberLabel.BorderBrush = Brushes.Red;
                }
                return;
            }
            try
            {
                droneToList.Model = (DroneModel)ModelComboBox.SelectedItem;
                bl.AddDrone(droneToList, ((BO.BaseStationToList)BaseChosingCombo.SelectedItem).SerialNum);
                drone = bl.GetDrone(droneToList.SerialNumber);
                MessageBox.Show(drone.ToString() + "\n add to list!", "succesful");
                ctorUpdateDronWindow(bl, drone.SerialNumber);

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
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
            if (text.Text != "0" || text.Text != "")
            {
                InputMissingSirialNumberLabel.Visibility = Visibility.Collapsed;
                SirialNumberTextBox.BorderBrush = Brushes.White;
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

            //forbid letters a
            //nd signs (#,$, %, ...)
            ErrorMassegeLabel.Visibility = Visibility.Visible;
            SirialNumberTextBox.Background = Brushes.Red;
            OkButton.IsEnabled = false;

            return;
        }

        private void WeightChoseCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InputMissingWightLabel.Visibility = Visibility.Collapsed;
            droneToList.WeightCategory = (BO.WeightCategories)WeightChoseCombo.SelectedItem;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Closing += DroneWindow_Closing;
            this.Close();
        }

        private void DroneWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = false;

        }

        private void BaseChosingCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            InputMissingLocationLabel.Visibility = Visibility.Collapsed;

        }

        private void StatusComb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (drone != null)
            {
                DroneStatus droneStatus = (DroneStatus)StatusComb.SelectedItem;
                if (droneStatus != drone.DroneStatus)
                    try
                    {
                        switch (droneStatus)
                        {
                            case DroneStatus.Free:
                                bl.FreeDroneFromCharging(drone.SerialNumber);
                                MessageBox.Show($"Drone number {drone.SerialNumber} free from charge");
                                break;
                            case DroneStatus.Maintenance:
                                bl.DroneToCharge(drone.SerialNumber);
                                MessageBox.Show($"Drone number {drone.SerialNumber} send to charge");
                                break;
                            case DroneStatus.Work:
                                bl.ConnectPackegeToDrone(drone.SerialNumber);
                                MessageBox.Show($"Drone number {drone.SerialNumber} connect to packege ");
                                break;
                            case DroneStatus.Delete:
                                bl.DeleteDrone(drone.SerialNumber);
                                MessageBox.Show($"Drone number {drone.SerialNumber} is Deleted ");
                                break;
                            default:
                                break;

                        }

                        ctorUpdateDronWindow(bl, drone.SerialNumber);


                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "ERROR");
                    }

            }
        }

        private void ModelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (drone != null)
                if (drone.Model != (DroneModel)ModelComboBox.SelectedItem)
                {
                    bl.UpdateDroneName(drone.SerialNumber, (DroneModel)ModelComboBox.SelectedItem);

                    DroneLabel.DataContext = bl.GetDrone(drone.SerialNumber);
                }
            if (drone is null)
                droneToList.Model = (DroneModel)ModelComboBox.SelectedItem;
        }

        private void TextBlock_PreviewMouseRightButtonDownPackegeWindow(object sender, MouseButtonEventArgs e)
        {
            new PackageView(bl, drone.PackageInTransfer.SerialNum).ShowDialog();
        }
    }
}
