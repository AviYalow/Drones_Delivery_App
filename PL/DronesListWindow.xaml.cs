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
    /// Interaction logic for DronesListWindow.xaml
    /// </summary>
    public partial class DronesListWindow : Window
    {
        IBL.IBL bl;

       
        IBL.BO.DroneToList drone;
        public DronesListWindow(IBL.IBL bl)
        {
            InitializeComponent();

            this.bl = bl;

            WeightSelctor.Items.Add("");
            StatusSelector.Items.Add("");
            foreach (var item in Enum.GetValues(typeof(IBL.BO.WeightCategories)))
                WeightSelctor.Items.Add(item);
            foreach (var item in Enum.GetValues(typeof(IBL.BO.DroneStatus)))
                StatusSelector.Items.Add(item);
            drone = new IBL.BO.DroneToList();
            DronesListView.ItemsSource = bl.FilterDronesList();
           



        }



        private void WeightSelctor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (WeightSelctor.SelectedItem == WeightSelctor.Items[0])
            {
                DronesListView.ItemsSource = bl.DroneToListsByWhight();

            }
            else
                DronesListView.ItemsSource = bl.DroneToListsByWhight((IBL.BO.WeightCategories)WeightSelctor.SelectedItem);
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StatusSelector.SelectedItem == StatusSelector.Items[0])
            {
                DronesListView.ItemsSource = bl.DroneToListsByStatus();

            }
            else
                DronesListView.ItemsSource = bl.DroneToListsByStatus((IBL.BO.DroneStatus)StatusSelector.SelectedItem);
        }


        private void ChoseDrone(object sender, MouseButtonEventArgs e)
        {

            if (DronesListView.SelectedItem != null)
            {
                new DroneWindow(bl, (IBL.BO.DroneToList)DronesListView.SelectedItem).ShowDialog();
                DronesListView.ItemsSource = bl.FilterDronesList();
                DronesListView.SelectedItem = null;
            }

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new DroneWindow(bl).ShowDialog();
            DronesListView.ItemsSource = bl.FilterDronesList();
        }

        private void Button_Return_Click(object sender, RoutedEventArgs e)
        {
            this.Closing += DronesListWindow_Closing;
            this.Close();
        }

        private void DronesListWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;

        }

   

        private void HeaderedContentControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HeaderedContentControl control = sender as HeaderedContentControl;

            DronesListView.ItemsSource = bl.DroneSortList(control.Name, DronesListView.ItemsSource as IEnumerable<IBL.BO.DroneToList>);


        }


    }
}
