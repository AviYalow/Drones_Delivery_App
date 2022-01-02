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
            try
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
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
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
            try
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
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }

        private void WeightCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
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
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }

        }




        private void StatusCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (StatusCombo.SelectedItem is null)
                    return;
                if (StatusCombo.SelectedItem == StatusCombo.Items[0])
                    PackagesListView.ItemsSource = bl.PackegeBySpsificStatus();
                else
                    PackagesListView.ItemsSource = bl.PackegeBySpsificStatus((PackageStatus)StatusCombo.SelectedItem);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }

        private void PrioCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
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
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }

        }

        private void ClearFromDate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                from.SelectedDate = null;
                PackagesListView.ItemsSource = bl.PackageFromDateLists();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }

        private void clearDateToButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                to.SelectedDate = null;
                PackagesListView.ItemsSource = bl.PackageToDateLists();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new PackageView(bl).ShowDialog();
                PackagesListView.ItemsSource = bl.PackageToLists();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }

        private void PackagesListView_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (PackagesListView.SelectedItem != null)
                {
                    new PackageView(bl, (BO.PackageToList)PackagesListView.SelectedItem).ShowDialog();
                    PackagesListView.ItemsSource = bl.PackageToLists();
                    PackagesListView.SelectedItem = null;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                new PackageView(bl).ShowDialog();
                PackagesListView.ItemsSource = bl.PackageToLists();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
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
