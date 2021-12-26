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
        DroneToList Drone;
        bool sitoation;

        public DroneWindow(BlApi.IBL bl)
        {

            InitializeComponent();

            Drone = new DroneToList();
            this.DataContext = Drone;
            this.bl = bl;
            WeightChoseCombo.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            ModelComboBox.ItemsSource = Enum.GetValues(typeof(DroneModel));
            BaseChosingCombo.ItemsSource = bl.BaseStationWhitFreeChargingStationToLists();
            sitoation = true;
            DroneLabel.Visibility = Visibility.Hidden;
        }

        public DroneWindow(BlApi.IBL bl, DroneToList drone)
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
            BaseChosingCombo.Text = Drone.Location.ToString();
            sitoation = false;
            ModelComboBox.ItemsSource = Enum.GetValues(typeof(DroneModel));
            ModelComboBox.SelectedItem = Drone.Model;
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
            else if(Drone.DroneStatus == DroneStatus.Work)
            {
                var packege = bl.ShowPackage(Drone.NumPackage);
                if (packege.collect_package is null)
                {
                    connectPackage.Content = "Take packege to delviry";
                    connectPackage.Visibility = Visibility.Visible;
                }
               else if (packege.package_arrived is null)
                {
                    connectPackage.Content = "Take packege to location";
                    connectPackage.Visibility = Visibility.Visible;
                }
             
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
               
                if (connectPackage.IsChecked == true)
                {
                    
                    try
                    {
                        if (connectPackage.Content.ToString() == "Assignment to package")
                        {
                            bl.ConnectPackegeToDrone(Drone.SerialNumber);
                            MessageBox.Show("Connection Succeeded!", "succesful");
                        }
                        else if(connectPackage.Content.ToString() == "Take packege to location")
                        {
                            bl.PackegArrive(Drone.SerialNumber);
                            MessageBox.Show("Packege get to dstinetion!", "succesful");
                        }
                        else if(connectPackage.Content.ToString() == "Take packege to delviry")
                        {
                            bl.CollectPackegForDelivery(Drone.SerialNumber);
                            MessageBox.Show("Packege collected by drone!", "succesful");
                        }
                    }
                       
                    catch (BO.DroneCantMakeDliveryException ex)
                    {

                        MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (BO.ItemNotFoundException ex)
                    {
                        MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch(Exception ex)
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
                    catch (BO.DroneStillAtWorkException ex)
                    {
                        MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (BO.NoButrryToTripException ex)
                    {
                        MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (BO.NoPlaceForChargeException ex)
                    {
                        MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (BO.ItemNotFoundException ex)
                    {
                        MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (BO.ItemFoundExeption ex)
                    {
                        MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }


                }
                if ((DroneModel)ModelComboBox.SelectedItem != Drone.Model)
                {
                    Drone.Model = (DroneModel)ModelComboBox.SelectedItem;
                    bl.UpdateDroneName(Drone.SerialNumber, (DroneModel)ModelComboBox.SelectedItem);
                    MessageBox.Show(Drone.ToString() + "\nUpdate succsed!");
                }
              
            }


            else
            {
                if (BaseChosingCombo.SelectedItem is null || ModelComboBox.SelectedItem is null || SirialNumberTextBox.Text == ""|| SirialNumberTextBox.Text == "0" || WeightChoseCombo.SelectedItem is null)
                {
                    if (BaseChosingCombo.SelectedItem is null)
                        InputMissingLocationLabel.Visibility = Visibility.Visible;
                    if(WeightChoseCombo.SelectedItem is null)
                        InputMissingWightLabel.Visibility = Visibility.Visible;
                    if (ModelComboBox.SelectedItem is null)
                    {
                        InputMissingModelLabel.Visibility = Visibility.Visible;
                        ModelComboBox.BorderBrush = Brushes.Red;
                    }
                    if(SirialNumberTextBox.Text == "" || SirialNumberTextBox.Text == "0")
                    {
                        InputMissingSirialNumberLabel.Visibility = Visibility.Visible;
                        InputMissingSirialNumberLabel.BorderBrush = Brushes.Red;
                    }
                    return;
                }

                try
                {
                    Drone.Model = (DroneModel)ModelComboBox.SelectedItem;
                    bl.AddDrone(Drone, ((BO.BaseStationToList)BaseChosingCombo.SelectedItem).SerialNum);
                    MessageBox.Show(Drone.ToString() + "\n add to list!", "succesful");
                }
                catch (BO.InputErrorException ex)
                {
                    MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (BO.ItemNotFoundException ex)
                {
                    MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (BO.ItemFoundExeption ex)
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
            Drone.WeightCategory = (BO.WeightCategories)WeightChoseCombo.SelectedItem;


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

       
    }
}
