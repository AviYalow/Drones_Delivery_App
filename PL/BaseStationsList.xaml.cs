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
using System.Collections.ObjectModel;
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
       
     internal static  ObservableCollection<BaseStationToList> lists;
        CollectionView view;
        PropertyGroupDescription groupDescription;
        public BaseStationsList(BlApi.IBL bL)
        {
            InitializeComponent();
            bl = bL;
            lists = new ObservableCollection<BaseStationToList>(bl.BaseStationToLists());
            DataContext = lists;
            lists.CollectionChanged += Lists_CollectionChanged;
            //   BaseListView.DataContext = lists;
         
         
            view = (CollectionView)CollectionViewSource.GetDefaultView(BaseListView.ItemsSource);
            groupDescription = new PropertyGroupDescription("FreeState");
        }

        private void Lists_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            DataContext = lists;
        }

        private void BaseListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (BaseListView.SelectedItem != null)
            {
                new BaseStationView(bl, (BaseStationToList)BaseListView.SelectedItem).Show();
                
            }
        }

        private void HeaderedContentControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HeaderedContentControl control = sender as HeaderedContentControl;
            try
            {
                
             bl.SortList(control.Name,lists).ConvertIenmurbleToObserve(lists);
         
                
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
                    bl.BaseStationToLists(). ConvertIenmurbleToObserve(lists);
                   
                    
                }
                if (FiletrListCmb.SelectedItem == BaseStationWithFreeChargingStation)
                {
                    bl.BaseStationWhitFreeChargingStationToLists().ConvertIenmurbleToObserve(lists);
                  
        
                }
                else
                    bl.AllBaseStation().ConvertIenmurbleToObserve(lists);
              
             
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString(), "ERROR"); }
        }

        private void ChoceDroneCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ChoceGroupCmb.SelectedItem == ChoceGroupCmb.Items[0] || ChoceGroupCmb.SelectedItem is null)
            { view.GroupDescriptions.Clear(); }
            else
            {
                view = (CollectionView)CollectionViewSource.GetDefaultView(BaseListView.ItemsSource);
                view.GroupDescriptions.Add(groupDescription);
            }

        }

        private void AddBaseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new BaseStationView( bl).Show();
           
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Closing += BaseStationsList_Closing;
            this.Close();
        }

        private void BaseStationsList_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = false;
        }
    }


}
