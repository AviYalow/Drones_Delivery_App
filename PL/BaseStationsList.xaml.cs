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
using BO;
using BlApi;
using Windows.UI.Xaml.Controls.Maps;
using Microsoft.Toolkit.Forms.UI.Controls;


namespace PL
{
    /// <summary>
    /// Interaction logic for BaseStationsList.xaml
    /// </summary>
    public partial class BaseStationsList : Window
    {
        IBL bl;
        CollectionView view;
        PropertyGroupDescription groupDescription;
        public BaseStationsList(BlApi.IBL bL)
        {
            InitializeComponent();
            bl = bL;
            BaseListView.ItemsSource = bl.BaseStationToLists();
          
            view = (CollectionView)CollectionViewSource.GetDefaultView(BaseListView.ItemsSource);
            groupDescription = new PropertyGroupDescription("FreeState");
        }
    
        private void BaseListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new BaseStationView(bl,(BaseStationToList)BaseListView.SelectedItem).ShowDialog();
        }

        private void HeaderedContentControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HeaderedContentControl control = sender as HeaderedContentControl;
            try
            {
                BaseListView.ItemsSource = bl.SortList(control.Name, BaseListView.ItemsSource as IEnumerable<BO.DroneToList>);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FiletrListCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(FiletrListCmb.SelectedItem== FiletrListCmb.Items[0])
            {
                BaseListView.ItemsSource = bl.BaseStationToLists();
            }
            if (FiletrListCmb.SelectedItem == FiletrListCmb.Items[1])
            {
                BaseListView.ItemsSource = bl.BaseStationWhitFreeChargingStationToLists();
            }
            else
                BaseListView.ItemsSource = bl.AllBaseStation();
        }

        private void ChoceDroneCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ChoceGroupCmb.SelectedItem == ChoceGroupCmb.Items[0]|| ChoceGroupCmb.SelectedItem is null)
            { view.GroupDescriptions.Clear(); }
            else
            { view = (CollectionView)CollectionViewSource.GetDefaultView(BaseListView.ItemsSource);
                view.GroupDescriptions.Add(groupDescription);
            }
          
        }

        private void AddBaseButton_Click(object sender, RoutedEventArgs e)
        {
            new BaseStationView(bl).ShowDialog();
            BaseListView.ItemsSource = bl.BaseStationToLists();
        }
    }
}
