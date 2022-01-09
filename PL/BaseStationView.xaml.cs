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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Specialized;

namespace PL
{

    /// <summary>
    /// Interaction logic for BaseStationView.xaml
    /// </summary>
    public partial class BaseStationView : Window
    {
        BlApi.IBL bl;
        BaseStation baseStation;
        ObservableCollection<BaseStationToList> lists;

        int numberOfChargingStation;
        public BaseStationView(BlApi.IBL bL, ObservableCollection<BaseStationToList> lists)
        {
            try
            {
                InitializeComponent();
                bl = bL;
                this.lists = lists;
                baseStation = new BaseStation();
                baseStation.Location = new Location();

                DataContext = baseStation;
                LongitudeText.DataContext = baseStation.Location;
                Latitudtext.DataContext = baseStation.Location;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }



        }
        public BaseStationView(BlApi.IBL bL, BaseStationToList base_, ObservableCollection<BaseStationToList> lists)
        {
            try
            {
                InitializeComponent();
                bl = bL;
                this.lists = lists;
                ctorByItems(base_.SerialNum);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }


        }

        private void ctorByItems(uint base_)
        {
            try
            {
                baseStation = bl.BaseByNumber(base_);

                SerialText.IsEnabled = false;
                DataContext = baseStation;
                LongitudeText.DataContext = baseStation.Location;
                Latitudtext.DataContext = baseStation.Location;
                NewChrgingStatimText.Visibility = Visibility.Visible;
                NewChrgingStatimLabel.Visibility = Visibility.Visible;
                numberOfChargingStation = (int)baseStation.FreeState + baseStation.DronesInChargeList.Count();
                NewChrgingStatimText.Text = numberOfChargingStation.ToString();

                AddButton.Visibility = Visibility.Collapsed;
                TitelLabel.Content = "Base Station update";


                DroneCharge1View.Visibility = Visibility.Visible;


                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.Close();
            }


        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (SerialText.Text == "" || FreeStateText.Text == "" ||
                Latitudtext.Text == "0" || Latitudtext.Text == "" ||
                LongitudeText.Text == "0" || LongitudeText.Text == "")
            {
                if (SerialText.Text == "0" || SerialText.Text == "")
                {
                    SerialText.BorderBrush = Brushes.Red;

                }
                if (FreeStateText.Text == "0" || FreeStateText.Text == "")
                {
                    FreeStateText.BorderBrush = Brushes.Red;
                }
                if (Latitudtext.Text == "0" || Latitudtext.Text == "")
                {
                    Latitudtext.BorderBrush = Brushes.Red;
                }
                if (LongitudeText.Text == "0" || LongitudeText.Text == "")
                { LongitudeText.BorderBrush = Brushes.Red; }
                return;
            }
            try
            {
                bl.AddBase(baseStation);
                MessageBox.Show("Add base Station Succes!");
                numberOfChargingStation = (int)baseStation.FreeState;
                lists.Add(new BaseStationToList { Active = "Active", BusyState = 0, FreeState = baseStation.FreeState, Name = baseStation.Name, SerialNum = baseStation.SerialNum });
                ctorByItems(baseStation.SerialNum);



            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString(), "ERROE"); }
        }

        private void PreviewKeyDownWhitNoDot(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;

            if (text == null) return;
            if (e == null) return;
            if (text.Text.All(x => x >= '0' && x <= '9'))
            {

                ((TextBox)sender).Background = Brushes.Transparent;
                ((TextBox)sender).BorderBrush = Brushes.Transparent;
                AddButton.IsEnabled = true;
            }
            if (text.Text != "0" || text.Text != "")
            {
                ((TextBox)sender).BorderBrush = Brushes.Transparent;
                ((TextBox)sender).BorderBrush = Brushes.Transparent;
            }
            if ((text.Name) == "NewChrgingStatimText")
            {
                if (e.Key == Key.Enter)
                    if (AddButton.Visibility != Visibility.Visible)
                        if (MessageBox.Show("Do you want to update the number of charching station?", "Update", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                        {
                            try
                            {
                                bl.UpdateBase(baseStation.SerialNum, "", text.Text);
                                MessageBox.Show("Update seccsed!");
                                bl.BaseStationToLists().ConvertIenmurbleToObserve(lists);

                                ctorByItems(baseStation.SerialNum);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString(), "ERROR");
                                text.Text = numberOfChargingStation.ToString();
                            }
                        }
                        else
                            text.Text = numberOfChargingStation.ToString();
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

            ((TextBox)sender).Background = Brushes.Red;
            AddButton.IsEnabled = false;

            return;
        }

        private void PreviewKeyDownWhitDot(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;

            if (text == null) return;
            if (e == null) return;
            if (text.Text.All(x => x >= '0' && x <= '9'))
            {
                ((TextBox)sender).BorderBrush = Brushes.Transparent;
                ((TextBox)sender).Background = Brushes.Transparent;
                AddButton.IsEnabled = true;
            }
            if (text.Text.Count(x => x == '.') > 1)
            {
                ((TextBox)sender).BorderBrush = Brushes.Transparent;
                ((TextBox)sender).Background = Brushes.Transparent;
                AddButton.IsEnabled = true;
            }
            if (text.Text != "0" || text.Text != "")
            {

                ((TextBox)sender).BorderBrush = Brushes.Transparent;
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
            if (e.Key == Key.Decimal || e.Key == Key.OemPeriod)
            {

                if (text.Text.StartsWith('.') || text.Text.Count(x => x == '.') > 1)
                {
                    ((TextBox)sender).Background = Brushes.Red;
                    AddButton.IsEnabled = false;

                    return;
                }
                return;
            }
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

            //allow control system keys
            if (Char.IsControl(c)) return;

            //allow digits (without Shift or Alt)
            if (Char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox

            //forbid letters a
            //nd signs (#,$, %, ...)

            ((TextBox)sender).Background = Brushes.Red;
            AddButton.IsEnabled = false;

            return;
        }

        private void HeaderedContentControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HeaderedContentControl control = sender as HeaderedContentControl;
            try
            {
                DroneCharge1View.ItemsSource = (bl.SortList(control.Name, DroneCharge1View.ItemsSource as IEnumerable<DroneInCharge>));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR");
            }
        }

        private void Latitudtext_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == "0")
                ((TextBox)sender).Text = "";
        }

        private void LongitudeText_LostFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == "")
                ((TextBox)sender).Text = "0";
        }

        private void DroneCharge1View_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {

                new DroneWindow(bl, (((DroneInCharge)DroneCharge1View.SelectedItem).SerialNum), baseStation.DronesInChargeList).ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR");
            }
            finally
            {
                baseStation = bl.BaseByNumber(baseStation.SerialNum);
                DataContext = baseStation;
                bl.BaseStationToLists().ConvertIenmurbleToObserve(lists);




            }
        }

        private void NameText_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (AddButton.Visibility != Visibility.Visible)
                {
                    if (MessageBox.Show("Do you want to update the base name?", "Update", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        try
                        {
                            bl.UpdateBase(baseStation.SerialNum, ((TextBox)sender).Text, "");

                            MessageBox.Show("Update seccsed!");
                            bl.BaseStationToLists().ConvertIenmurbleToObserve(lists);
                            ctorByItems(baseStation.SerialNum);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "ERROR");
                        }
                    }
                    else
                        ((TextBox)sender).Text = baseStation.Name;
                }
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {

            e.Cancel = true;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Closing += BaseStationView_Closing;
            this.Close();
        }

        private void BaseStationView_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = false;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DeleteBase(baseStation.SerialNum);
                MessageBox.Show($"Base number{baseStation.SerialNum} deleted!");
                this.Closing += BaseStationView_Closing;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR");
            }
        }
    }
}
