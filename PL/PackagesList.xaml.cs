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
using  BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for PackagesList.xaml
    /// </summary>
    public partial class PackagesList : Window
    {
      
        BlApi.IBL bl;
        CollectionView view;
        PropertyGroupDescription groupDescription;
        public PackagesList(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;

            PackagesListView.ItemsSource = bl.PackageToLists();


            WeightCombo.Items.Add("");
            StatusCombo.Items.Add("");
            PrioCombo.Items.Add("");
            foreach (var item in Enum.GetValues(typeof(BO.WeightCategories)))
                WeightCombo.Items.Add(item);

            foreach (var item in Enum.GetValues(typeof(BO.PackageStatus)))
                    StatusCombo.Items.Add(item);

            foreach (var item in Enum.GetValues(typeof(BO.Priority)))
                PrioCombo.Items.Add(item);

            view = (CollectionView)CollectionViewSource.GetDefaultView(PackagesListView.ItemsSource);
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

        private void CmbDisplayOp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            view = (CollectionView)CollectionViewSource.GetDefaultView(PackagesListView.ItemsSource);
            if (CmbDisplayOp.SelectedItem == CmbDisplayOp.Items[0])
            {
                view.GroupDescriptions.Clear();


            }
            else if (CmbDisplayOp.SelectedItem == CmbDisplayOp.Items[1])
            {
                view.GroupDescriptions.Clear();
                groupDescription = new PropertyGroupDescription("SendClient");
                view.GroupDescriptions.Add(groupDescription);
            }
            else if (CmbDisplayOp.SelectedItem == CmbDisplayOp.Items[2])
            {
                view.GroupDescriptions.Clear();
                groupDescription = new PropertyGroupDescription("RecivedClient");
                
                view.GroupDescriptions.Add(groupDescription);
            }
        }

        private void WeightCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WeightCombo.SelectedItem is null)
                return;
            if (WeightCombo.SelectedItem == WeightCombo.Items[0])
            {
                PackagesListView.ItemsSource = bl.PackageWeightLists();


            }
            else if (WeightCombo.SelectedItem != WeightCombo.Items[0])
            {
               
                    PackagesListView.ItemsSource = bl.PackageWeightLists((BO.WeightCategories)WeightCombo.SelectedItem);
                
            }

        }




        private void StatusCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (StatusCombo.SelectedItem is null)
                return;
            if (StatusCombo.SelectedItem == StatusCombo.Items[0])
              PackagesListView.ItemsSource=  bl.PackegeBySpsificStatus();
            else
                PackagesListView.ItemsSource = bl.PackegeBySpsificStatus((PackageStatus)StatusCombo.SelectedItem);

        }

        private void PrioCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PrioCombo.SelectedItem is null)
                return;
            if (PrioCombo.SelectedItem == PrioCombo.Items[0])
            {
             
                    PackagesListView.ItemsSource = bl.PackagePriorityLists();
          
            }
          else
            {
                PackagesListView.ItemsSource = bl.PackagePriorityLists((Priority)PrioCombo.SelectedItem);
            }
        

        }

        private void ClearFromDate_Click(object sender, RoutedEventArgs e)
        {
            from.SelectedDate = null;
            PackagesListView.ItemsSource = bl.PackageFromDateLists();
        }

        private void clearDateToButton_Click(object sender, RoutedEventArgs e)
        {
           to.SelectedDate = null;
            PackagesListView.ItemsSource = bl.PackageToDateLists();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            new PackageView(bl).ShowDialog();
            PackagesListView.ItemsSource = bl.PackageToLists();
        }

        private void PackagesListView_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            if (PackagesListView.SelectedItem != null)
            {
                new PackageView(bl, (BO.PackageToList)PackagesListView.SelectedItem).ShowDialog();
                PackagesListView.ItemsSource = bl.PackageToLists();
                PackagesListView.SelectedItem = null;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new PackageView(bl).ShowDialog();
            PackagesListView.ItemsSource = bl.PackageToLists();
        }

        private void from_DataContextChanged(object sender, RoutedEventArgs e)
        {
            PackagesListView.ItemsSource = bl.PackageFromDateLists(from.SelectedDate);
        }

        private void to_DataContextChanged(object sender, RoutedEventArgs e)
        {
            PackagesListView.ItemsSource = bl.PackageToDateLists(to.SelectedDate);
        }
    }
}
