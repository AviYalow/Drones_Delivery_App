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
        IEnumerable<IBL.BO.DroneToList> dronesSelectByStatusIenumrble;
        IEnumerable<IBL.BO.DroneToList> dronesSelectByWeightIenumrble;
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
            DronesListView.ItemsSource = dronesSelectByStatusIenumrble= dronesSelectByWeightIenumrble= bl.DroneToLists();
            DronesListView.DataContext = drone;
        }



        private void WeightSelctor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WeightSelctor.SelectedItem == WeightSelctor.Items[0])
            {
                dronesSelectByWeightIenumrble = bl.DroneToLists();
            }
            else
            {
                dronesSelectByWeightIenumrble = bl.DroneToListsByWhight((IBL.BO.WeightCategories)WeightSelctor.SelectedItem);
            }
            DronesListView.ItemsSource = dronesSelectByStatusIenumrble.Intersect(dronesSelectByWeightIenumrble);
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StatusSelector.SelectedItem == StatusSelector.Items[0])
            {
                dronesSelectByStatusIenumrble = bl.DroneToLists();

            }
            else
            {
                dronesSelectByStatusIenumrble = (bl.DroneToListsByStatus((IBL.BO.DroneStatus)StatusSelector.SelectedItem));


            }
            DronesListView.ItemsSource = dronesSelectByStatusIenumrble.Intersect(dronesSelectByWeightIenumrble);
        }

        private void ChoseDrone(object sender, MouseButtonEventArgs e)
        {
           
            
            new DroneWindow( bl, (IBL.BO.DroneToList)DronesListView.SelectedItem).ShowDialog();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new DroneWindow(bl).Show();
        }

        private void Button_Return_Click(object sender, RoutedEventArgs e)
        {
            this.Closing += DronesListWindow_Closing;
            this.Close();
        }

        private void DronesListWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel=false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;

        }

       
    }
}
