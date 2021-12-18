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
using System.Reflection;
using Microsoft.Win32;

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


            BaseChosingCombo.ItemsSource = bl.BaseStationWhitFreeChargingStationToLists();
            sitoation = true;
            DroneLabel.Visibility = Visibility.Hidden;
        }

        public DroneWindow(IBL.IBL bl, DroneToList drone)
        {
            InitializeComponent();
            Drone = drone;
            this.bl = bl;

            this.DataContext = Drone;
            TitelDroneLabel.Content = "Updte Drone Window";
            SirialNumberTextBox.IsEnabled = false;
            SirialNumberTextBox.Text = Drone.SerialNumber.ToString();
            WeightChoseCombo.IsEnabled = false;
            WeightChoseCombo.Text = drone.WeightCategory.ToString();
            BaseChosingCombo.IsEnabled = false;
            sitoation = false;
            // DroneLabel.Content = bl.GetDrone(drone.SerialNumber);
            DroneLabel.DataContext = bl.GetDrone(drone.SerialNumber);
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


            if (!sitoation)
            {
                if (ModelTextBox.Text != Drone.Model)
                    bl.UpdateDroneName(Drone.SerialNumber, Drone.Model);

                if (connectPackage.IsChecked == true)
                {
                    try
                    {
                        bl.ConnectPackegeToDrone(Drone.SerialNumber);
                        MessageBox.Show("Connection Succeeded!", "succesful");
                    }
                    catch (IBL.BO.DroneCantMakeDliveryException ex)
                    {

                        MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (IBL.BO.ItemNotFoundException ex)
                    {
                        MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }

                if (Charge.IsChecked == true)
                {
                    try
                    {
                        if (Drone.DroneStatus == DroneStatus.Free)
                        {
                            bl.DroneToCharge(Drone.SerialNumber);
                            MessageBox.Show("Sending to Charge Succeeded!", "succesful");
                        }

                        else if (Drone.DroneStatus == DroneStatus.Maintenance)
                        {
                            bl.FreeDroneFromCharging(Drone.SerialNumber);
                            MessageBox.Show("Release from Charge Succeeded!", "succesful");
                        }

                    }
                    catch (IBL.BO.DroneStillAtWorkException ex)
                    {
                        MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (IBL.BO.NoButrryToTripException ex)
                    {
                        MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (IBL.BO.NoPlaceForChargeException ex)
                    {
                        MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (IBL.BO.ItemNotFoundException ex)
                    {
                        MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (IBL.BO.ItemFoundExeption ex)
                    {
                        MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }


                }

                MessageBox.Show(Drone.ToString() + "\n update list!", "succesful");
            }


            else
            {
                if (BaseChosingCombo.SelectedItem is null || ModelTextBox.Text == "" || SirialNumberTextBox.Text == "" || WeightChoseCombo.SelectedItem is null)
                {

                    return;
                }

                try
                {

                    bl.AddDrone(Drone, ((IBL.BO.BaseStationToList)BaseChosingCombo.SelectedItem).SerialNum);
                    MessageBox.Show(Drone.ToString() + "\n add to list!", "succesful");
                }
                catch (IBL.BO.InputErrorException ex)
                {
                    MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (IBL.BO.ItemNotFoundException ex)
                {
                    MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (IBL.BO.ItemFoundExeption ex)
                {
                    MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            this.Closing += DronesWindow_Closing;
            this.Close();
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

            //forbid letters a
            //nd signs (#,$, %, ...)
            ErrorMassegeLabel.Visibility = Visibility.Visible;
            SirialNumberTextBox.Background = Brushes.Red;
            OkButton.IsEnabled = false;

            return;
        }

        private void WeightChoseCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Drone.WeightCategory = (IBL.BO.WeightCategories)WeightChoseCombo.SelectedItem;


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


    }
}
