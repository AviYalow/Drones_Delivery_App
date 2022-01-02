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
            if (BaseListView.SelectedItem != null)
            {
                new BaseStationView(bl, (BaseStationToList)BaseListView.SelectedItem).ShowDialog();
                BaseListView.ItemsSource = bl.BaseStationToLists();
            }
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
            try
            {
                if (FiletrListCmb.SelectedItem == BaseStationActive)
                {
                    BaseListView.ItemsSource = bl.BaseStationToLists();
                }
                if (FiletrListCmb.SelectedItem == BaseStationWithFreeChargingStation)
                {
                    BaseListView.ItemsSource = bl.BaseStationWhitFreeChargingStationToLists();
                }
                else
                    BaseListView.ItemsSource = bl.AllBaseStation();
            }
            catch(Exception ex)
            { MessageBox.Show(ex.ToString(), "ERROR"); }
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
            try
            {
                new BaseStationView(bl).ShowDialog();
                BaseListView.ItemsSource = bl.BaseStationToLists();
            }
            catch(Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }
    }
}
