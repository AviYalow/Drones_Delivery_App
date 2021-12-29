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
        public PackageView(BlApi.IBL bL)
        {
            InitializeComponent();
            bl = bL;
            addPackegeWindow();
        }
        public PackageView(BlApi.IBL bL,uint packegeNum)
        {
            InitializeComponent();
            bl = bL;

        }


        public PackageView(BlApi.IBL bL, PackageToList package)
        {

            InitializeComponent();
            bl = bL;
        }

        void addPackegeWindow()
        {
            ResiveClientCMB.ItemsSource = bl.ClientActiveToLists();
            SendClientCMB.ItemsSource = bl.ClientActiveToLists();
        }

        private void SendClientCMB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
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
