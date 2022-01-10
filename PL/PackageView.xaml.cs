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
using System.Collections.ObjectModel;
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
        ObservableCollection<PackageToList> lists;
        bool clientMode;

        public PackageView(BlApi.IBL bL,string SendClient="", StatusPackegeWindow change =StatusPackegeWindow. NotClient, bool clientMode = false)
        {
            try
            {
                InitializingCtor(bL, clientMode, change);
                DataToCmb(SendClient);
                
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }
        public PackageView(BlApi.IBL bL, uint packegeNum, StatusPackegeWindow change = StatusPackegeWindow.NotClient ,bool clientMode=false)
        {
            try
            {
                InitializingCtor(bL,clientMode,change);
                packegeFromDialog(packegeNum);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }

        }
        public PackageView(BlApi.IBL bL, ObservableCollection<PackageToList> lists, uint packegeNum=0)
        {
            try
            {
                InitializingCtor(bL);
                this.lists = lists;

                if (packegeNum == 0)
                    DataToCmb("");
                else
                    packegeFromDialog(packegeNum);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }
        private void InitializingCtor(IBL bL, bool mode = false,StatusPackegeWindow status=StatusPackegeWindow.NotClient)
        {
            InitializeComponent();
            this.clientMode = false;
            changFromClient = StatusPackegeWindow.NotClient;
           this. bl = bL;
        }


        private void packegeFromDialog(uint serialNumber)
        {
            try
            {
                
                package = bl.ShowPackage(serialNumber);
                WeightCmb.ItemsSource = Enum.GetValues(typeof(WeightCategories));
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
                WeightCmb.ItemsSource = Enum.GetValues(typeof(WeightCategories));
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
                if(lists!=null)
               lists.Add(new PackageToList { Drone = false, packageStatus = PackageStatus.Create, priority = package.Priority, RecivedClient = package.RecivedClient.Name, SendClient = package.SendClient.Name, SerialNumber = packegeNumber, WeightCategories = package.WeightCatgory });
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
                if(lists != null)
                bl.PackageToLists().ConvertIenmurbleToObserve(lists);
               
                packegeFromDialog(package.SerialNumber);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR");
            }

        }

        private void SirialNumberDroneLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
          
          if (changFromClient != StatusPackegeWindow.NotClient|| clientMode)
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
                bl.PackageToLists().ConvertIenmurbleToObserve(lists);
                this.Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR");
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Closing += PackageView_Closing;
            this.Close();
        }

        private void PackageView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
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
