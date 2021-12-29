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
using BO;
using System.Globalization;

using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for PackageView.xaml
    /// </summary>
    public partial class PackageView : Window
    {
        BlApi.IBL bl;
        Package package;
        PackageStatus packageStatus;


        public PackageView(BlApi.IBL bL)
        {
            InitializeComponent();
            AddGrid.Visibility = Visibility.Visible;
            bl = bL;
            package = new Package();
            addPackegeWindow();
        }
        public PackageView(BlApi.IBL bL, uint packegeNum)
        {
            InitializeComponent();

            bl = bL;
            package = bl.ShowPackage(packegeNum);
            this.DataContext = package;
            MainGrid.DataContext = package;
            DataGridPackege.DataContext = package;
            UpdateGrid.Visibility = Visibility.Visible;
            statusSelectorSurce();

        }


        public PackageView(BlApi.IBL bL, PackageToList packagefromList)
        {

            InitializeComponent();

            bl = bL;
            package = bl.ShowPackage(packagefromList.SerialNumber);
            this.DataContext = package;
            MainGrid.DataContext = package;
            DataGridPackege.DataContext = package;
            UpdateGrid.Visibility = Visibility.Visible;
            statusSelectorSurce();

        }
        void statusSelectorSurce()
        {
            if (package.PackageArrived != null)
            {
                packageStatus= PackageStatus.Arrived;
                NextModeButton.Visibility = Visibility.Collapsed;
                
            }
            else if (package.CollectPackage != null)
            {
                packageStatus=(PackageStatus.Collected);
                
            }
            else if (package.PackageAssociation != null)
            {
                packageStatus=(PackageStatus.Assign);
               

            }
            else
            {
                packageStatus=(PackageStatus.Create);
                NextModeButton.Visibility = Visibility.Collapsed;
            }
            DronestatuseLabel.DataContext = packageStatus;


        }
        void addPackegeWindow()
        {
            DataContext = package;
            ResiveClientCMB.ItemsSource = bl.ClientInPackagesList();
            SendClientCMB.ItemsSource = bl.ClientInPackagesList();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (WeightCmb.SelectedItem is null || SendClientCMB.SelectedItem is null || ResiveClientCMB.SelectedItem is null || PraiyurtyCMB.SelectedItem is null)
                return;
            try
            {
                MessageBox.Show($"Packege number:{ bl.AddPackege(package)} Add!");

            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString(), "ERROR"); }
        }

        

   

        private void NextModeButton_Click(object sender, RoutedEventArgs e)
        {
            if (packageStatus == PackageStatus.Assign)
                bl.CollectPackegForDelivery(package.Drone.SerialNum);
            else if (packageStatus == PackageStatus.Collected)
                bl.PackegArrive(package.Drone.SerialNum);
            else
                return;
            PackageView newWindow = new PackageView(bl, package.SerialNumber);
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
            this.Close();
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
