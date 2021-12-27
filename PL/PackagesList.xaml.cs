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
        bool groupVisble = false;
        bool groupSender = false;
        BlApi.IBL bl;
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

            if (CmbDisplayOp.SelectedItem == CmbDisplayOp.Items[0])
            {
                GroupingView.Visibility = Visibility.Hidden;
                PackagesListView.Visibility = Visibility.Visible;
                groupVisble = false;


            }
            else if (CmbDisplayOp.SelectedItem == CmbDisplayOp.Items[1])
            {

                PackagesListView.Visibility = Visibility.Hidden;
                GroupingView.Visibility = Visibility.Visible;
                groupVisble = true;
                groupSender = true;
                GroupingView.ItemsSource = bl.PackageToLists();
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(GroupingView.ItemsSource);
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("SendClient");
                view.GroupDescriptions.Add(groupDescription);
            }
            else if (CmbDisplayOp.SelectedItem == CmbDisplayOp.Items[2])
            {
                PackagesListView.Visibility = Visibility.Hidden;
                GroupingView.Visibility = Visibility.Visible;
                 groupVisble = true;
                groupSender = false;
                GroupingView.ItemsSource = bl.PackageToLists();
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(GroupingView.ItemsSource);
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("RecivedClient");
                view.GroupDescriptions.Add(groupDescription);
            }
        }

        private void WeightCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WeightCombo.SelectedItem is null)
                return;
            if (WeightCombo.SelectedItem == WeightCombo.Items[0])
            {
                PackagesListView.ItemsSource = bl.PackageToLists();
                 GroupingView.ItemsSource = bl.PackageToLists();
                
            }
            else if (WeightCombo.SelectedItem != WeightCombo.Items[0])
            {
                if (!groupVisble)
                    PackagesListView.ItemsSource = bl.PackageWeightLists((BO.WeightCategories)WeightCombo.SelectedItem);
                else
                {
                    if (groupSender)
                    {
                        GroupingView.ItemsSource = bl.PackageWeightLists((BO.WeightCategories)WeightCombo.SelectedItem);
                        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(GroupingView.ItemsSource);
                        PropertyGroupDescription groupDescription = new PropertyGroupDescription("SendClient");
                        view.GroupDescriptions.Add(groupDescription);
                    }
                   else if (!groupSender)
                    {
                        GroupingView.ItemsSource = bl.PackageWeightLists((BO.WeightCategories)WeightCombo.SelectedItem);
                        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(GroupingView.ItemsSource);
                        PropertyGroupDescription groupDescription = new PropertyGroupDescription("RecivedClient");
                        view.GroupDescriptions.Add(groupDescription);
                    }
                }
            }

        }

        private void StatusCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (StatusCombo.SelectedItem is null)
                return;
            if (groupVisble == false)
            {
                if (StatusCombo.SelectedItem == StatusCombo.Items[0])
                {
                    PackagesListView.ItemsSource = bl.PackageToLists();

                }
                else if (StatusCombo.SelectedItem == StatusCombo.Items[1])
                {

                }
                else if (StatusCombo.SelectedItem == StatusCombo.Items[2])
                {

                    PackagesListView.ItemsSource = bl.PackageConnectedButNutCollectedLists();
                }
                else if (StatusCombo.SelectedItem == StatusCombo.Items[3])
                {
                    PackagesListView.ItemsSource = bl.PackageCollectedButNotArriveLists();
                }
                else if (StatusCombo.SelectedItem == StatusCombo.Items[4])
                {
                    PackagesListView.ItemsSource = bl.PackageArriveLists();
                }
            }
            else
            {
                if (StatusCombo.SelectedItem == StatusCombo.Items[0])
                {
                    GroupingView.ItemsSource = bl.PackageToLists();

                }
                else if (StatusCombo.SelectedItem == StatusCombo.Items[1])
                {

                }
                else if (StatusCombo.SelectedItem == StatusCombo.Items[2])
                {

                    GroupingView.ItemsSource = bl.PackageConnectedButNutCollectedLists();
                }
                else if (StatusCombo.SelectedItem == StatusCombo.Items[3])
                {
                    GroupingView.ItemsSource = bl.PackageCollectedButNotArriveLists();
                }
                else if (StatusCombo.SelectedItem == StatusCombo.Items[4])
                {
                    GroupingView.ItemsSource = bl.PackageArriveLists();

                }

                if(groupSender)
                {
                    CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(GroupingView.ItemsSource);
                    PropertyGroupDescription groupDescription = new PropertyGroupDescription("SendClient");
                    view.GroupDescriptions.Add(groupDescription);
                }
                else if(!groupSender)
                {
                    GroupingView.ItemsSource = bl.PackageWeightLists((BO.WeightCategories)WeightCombo.SelectedItem);
                    CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(GroupingView.ItemsSource);
                    PropertyGroupDescription groupDescription = new PropertyGroupDescription("RecivedClient");
                    view.GroupDescriptions.Add(groupDescription);
                }
            }
        }

        private void PrioCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PrioCombo.SelectedItem is null)
                return;
            if (PrioCombo.SelectedItem == WeightCombo.Items[0])
            {
                PackagesListView.ItemsSource = bl.PackageToLists();
                GroupingView.ItemsSource = bl.PackageToLists();
            }
            else
            {
                if (GroupingView.Visibility == Visibility.Hidden)
                    PackagesListView.ItemsSource = bl.PackagePriorityLists((BO.Priority)PrioCombo.SelectedItem);
                else
                {
                    if (CmbDisplayOp.SelectedItem == CmbDisplayOp.Items[1])
                    {
                        GroupingView.ItemsSource = bl.PackagePriorityLists((BO.Priority)PrioCombo.SelectedItem);
                        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(GroupingView.ItemsSource);
                        PropertyGroupDescription groupDescription = new PropertyGroupDescription("SendClient");
                        view.GroupDescriptions.Add(groupDescription);
                    }
                    if (CmbDisplayOp.SelectedItem == CmbDisplayOp.Items[2])
                    {
                        GroupingView.ItemsSource = bl.PackagePriorityLists((BO.Priority)PrioCombo.SelectedItem);
                        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(GroupingView.ItemsSource);
                        PropertyGroupDescription groupDescription = new PropertyGroupDescription("RecivedClient");
                        view.GroupDescriptions.Add(groupDescription);
                    }
                }
            }

        }
    }
}
