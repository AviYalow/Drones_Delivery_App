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
using BO;
using System.Windows.Shapes;

using System.Globalization;

using BlApi;

namespace PL
{
  public  enum StatusPackegeWindow {NotClient,SendClient,GetingClient }
    /// <summary>
    /// Interaction logic for PackageView.xaml
    /// </summary>
    public partial class PackageView : Window
    {
        BlApi.IBL bl;
        Package package;
        PackageStatus packageStatus;
        StatusPackegeWindow changFromClient;

        public PackageView(BlApi.IBL bL,string SendClient="", StatusPackegeWindow change =StatusPackegeWindow. NotClient)
        {
            try
            {
                InitializeComponent();

                bl = bL;
                changFromClient = change;
                DataToCmb(SendClient);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }
        public PackageView(BlApi.IBL bL, uint packegeNum, StatusPackegeWindow change = StatusPackegeWindow.NotClient)
        {
            try
            {
                InitializeComponent();
                changFromClient = change;
                bl = bL;
                packegeFromDialog(packegeNum);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }

        }


        public PackageView(BlApi.IBL bL, PackageToList packagefromList, StatusPackegeWindow change = StatusPackegeWindow.NotClient)
        {
            try
            {
                InitializeComponent();
                bl = bL;
                changFromClient = change;
                packegeFromDialog(packagefromList.SerialNumber);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }

        }
        private void packegeFromDialog(uint serialNumber)
        {
            try
            {
                package = bl.ShowPackage(serialNumber);
                this.DataContext = package;
                MainGrid.DataContext = package;
                DataGridPackege.DataContext = package;
                AddGrid.Visibility = Visibility.Collapsed;
                UpdateGrid.Visibility = Visibility.Visible;
                statusSelectorSurce();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR");

            }
        }
        void statusSelectorSurce()
        {
            if (package.PackageArrived != null)
            {
                packageStatus = PackageStatus.Arrived;
                NextModeButton.Visibility = Visibility.Collapsed;
                
               
            }
            else if (package.CollectPackage != null)
            {
                packageStatus = (PackageStatus.Collected);
                if(changFromClient==StatusPackegeWindow.SendClient)
                {
                    NextModeButton.Visibility = Visibility.Collapsed;
                }

            }
            else if (package.PackageAssociation != null)
            {
                packageStatus = (PackageStatus.Assign);
                if (changFromClient==StatusPackegeWindow.GetingClient)
                {
                    NextModeButton.Visibility = Visibility.Collapsed;
                }

            }
            else
            {
                packageStatus = (PackageStatus.Create);
                NextModeButton.Visibility = Visibility.Collapsed;
                if(changFromClient != StatusPackegeWindow.GetingClient)
                DeleteButton.Visibility = Visibility.Visible;
            }
            DronestatuseLabel.DataContext = packageStatus;


        }
        void DataToCmb(string SendClient)
        {
            try
            {
                AddGrid.Visibility = Visibility.Visible;
                package = new Package { SendClient = new ClientInPackage(), RecivedClient = new ClientInPackage() };
                if(SendClient!="")
                {
                    uint id;
                    uint.TryParse(SendClient,out id);
                    package.SendClient.Id = id;
                    SendClientCMB.ItemsSource = bl.ClientInPackagesList().Where(x => x.Id == id);
                    SendClientCMB.SelectedItem = SendClientCMB.Items[0];



                }
                else
                    SendClientCMB.ItemsSource = bl.ClientInPackagesList();
                DataContext = package;
                ResiveClientCMB.ItemsSource = bl.ClientInPackagesList();
               
                PraiyurtyCMB.ItemsSource = Enum.GetValues(typeof(Priority));
                WeightCmb.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR");
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (WeightCmb.SelectedItem is null || SendClientCMB.SelectedItem is null || ResiveClientCMB.SelectedItem is null || PraiyurtyCMB.SelectedItem is null)
                return;
            try
            {
                uint packegeNumber = bl.AddPackege(package);
                MessageBox.Show($"Packege number:{packegeNumber } Add!");
                packegeFromDialog(packegeNumber);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString(), "ERROR"); }
        }





        private void NextModeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (packageStatus == PackageStatus.Assign)
                {
                    bl.CollectPackegForDelivery(package.Drone.SerialNum);
                    MessageBox.Show("The packege collected!");
                }
                else if (packageStatus == PackageStatus.Collected)
                {
                    bl.PackegArrive(package.Drone.SerialNum);
                    MessageBox.Show("The packege arrive!");
                }
                else
                    return;
                packegeFromDialog(package.SerialNumber);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR");
            }

        }

        private void SirialNumberDroneLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
          if (changFromClient != StatusPackegeWindow.NotClient)
                {
                return;
                }
            try
            {
                new DroneWindow(bl, package.Drone.SerialNum).ShowDialog();
                packegeFromDialog(package.SerialNumber);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DeletePackege(package.SerialNumber);
                MessageBox.Show($"Packge number{package.SerialNumber} deleted!");
                this.Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR");
            }
        }
    }
    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return string.IsNullOrWhiteSpace((value ?? "").ToString())
                ? new ValidationResult(false, "Field is required.")
                : ValidationResult.ValidResult;
        }
    }
}
