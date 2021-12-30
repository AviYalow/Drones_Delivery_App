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
using BO;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for ClientView.xaml
    /// </summary>
    public partial class ClientView : Window
    {
        Client client;
        
        public ClientView (BlApi.IBL bl)
        {
            InitializeComponent();
        }

        public ClientView(BlApi.IBL bl , ClientToList client)
        {
            InitializeComponent();
            this.client = bl.GetingClient(client.ID);
            this.DataContext = this.client;
            TitelClientLabel.Content = "Updte Client Window";
            ClientLabel.DataContext = bl.GetingClient(client.ID);
          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
