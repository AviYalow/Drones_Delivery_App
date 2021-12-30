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
    /// Interaction logic for ClientsLIst.xaml
    /// </summary>
    public partial class ClientsLIst : Window
    {
        BlApi.IBL bl;
        public ClientsLIst(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            clientListView.ItemsSource = bl.ClientActiveToLists();
        }

        private void HeaderedContentControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ClientView(bl).ShowDialog();
            clientListView.ItemsSource = bl.ClientActiveToLists();
        }

        private void clientListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (clientListView.SelectedItem != null)
            {
                new ClientView(bl, (BO.ClientToList)clientListView.SelectedItem).ShowDialog();
                clientListView.ItemsSource = bl.ClientActiveToLists();
                clientListView.SelectedItem = null;
            }
        }
    }
}
