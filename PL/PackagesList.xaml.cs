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

namespace PL
{
    /// <summary>
    /// Interaction logic for PackagesList.xaml
    /// </summary>
    public partial class PackagesList : Window
    {

        BlApi.IBL bl;
        public PackagesList(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;

           
            PackagesListView.ItemsSource = bl.PackageToLists();
        }

        private void HeaderedContentControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HeaderedContentControl control = sender as HeaderedContentControl;
            try
            {
                PackagesListView.ItemsSource = bl.SortList(control.Name, PackagesListView.ItemsSource as IEnumerable<BO.PackageToList>);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


    }
}
