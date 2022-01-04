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
        CollectionView view;
        PropertyGroupDescription groupDescription;
        BlApi.IBL bl;
        public ClientsLIst(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            view = (CollectionView)CollectionViewSource.GetDefaultView(clientListView.ItemsSource);
            groupDescription = new PropertyGroupDescription("Name");
            clientListView.ItemsSource = bl.ClientToLists();
        }

        private void HeaderedContentControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HeaderedContentControl control = sender as HeaderedContentControl;
            try
            {
                clientListView.ItemsSource = bl.SortList(control.Name, clientListView.ItemsSource as IEnumerable<BO.ClientToList>);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ClientView(bl).ShowDialog();
            clientListView.ItemsSource = bl.FilterClientList();
        }

        private void clientListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (clientListView.SelectedItem != null)
            {
                new ClientView(bl, (BO.ClientToList)clientListView.SelectedItem).ShowDialog();
                clientListView.ItemsSource = bl.FilterClientList();
                clientListView.SelectedItem = null;
            }
        }

      

        private void SenderClientCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           if(SenderClientCmb.SelectedItem is null)
            {
                clientListView.ItemsSource = bl.ClientActiveHowSendPackegesToLists(false);
            }
           else if(SenderClientCmb.SelectedItem == SendItem)
            { clientListView.ItemsSource = bl.ClientActiveHowSendPackegesToLists(); }
            else if (SenderClientCmb.SelectedItem == SendItemAndPackegeArrive)
            { clientListView.ItemsSource = bl.ClientActiveHowSendAndArrivePackegesToLists(); }
            else if (SenderClientCmb.SelectedItem == SendItemAndPackegeNotArrive)
            { clientListView.ItemsSource = bl.ClientActiveHowSendPackegesAndNotArriveToLists(); }
        }

        private void GetingClientCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (getingClientCmb.SelectedItem is null)
            {
                clientListView.ItemsSource = bl.ClientActiveHowGetingPackegesToLists(false);
            }
            else if (getingClientCmb.SelectedItem == GetItem)
            { clientListView.ItemsSource = bl.ClientActiveHowGetingPackegesToLists(); }
            else if (getingClientCmb.SelectedItem == GetItemAndPackegeArrive)
            { clientListView.ItemsSource = bl.ClientActiveHowGetingPackegesAndArriveToLists(); }
            else if (getingClientCmb.SelectedItem == GetItemAndPackegeNotArrive)
            { clientListView.ItemsSource = bl.ClientActiveHowGetingPackegesAndNotArriveToLists(); }
        }

        private void AllClient_Checked(object sender, RoutedEventArgs e)
        {
            if(!AllClient.IsChecked.Value)
            clientListView.ItemsSource = bl.FilterClientList(false);
            if (AllClient.IsChecked.Value)
                clientListView.ItemsSource = bl.FilterClientList();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Closing += ClientsLIst_Closing;
            this.Close();
        }

        private void ClientsLIst_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = false;
        }
    }
}
