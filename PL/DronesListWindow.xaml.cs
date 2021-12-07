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
        IEnumerable<IBL.BO.DroneToList> dronesIenumrble;
       
       
        public DronesListWindow(IBL.IBL bl)
        {
            InitializeComponent();
            dronesIenumrble= bl.DroneToLists();
            this.bl = bl;
           
            WeightSelctor.Items.Add("");
            StatusSelector.Items.Add("");
            foreach (var item in Enum.GetValues(typeof(IBL.BO.WeightCategories)))
            WeightSelctor.Items.Add( item  );
            foreach (var item in Enum.GetValues(typeof(IBL.BO.DroneStatus)))
                StatusSelector.Items.Add(item);
            DronesListView.ItemsSource = dronesIenumrble;
            
        }

       

        private void WeightSelctor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WeightSelctor.SelectedItem == "")
            {
                dronesIenumrble = bl.DroneToLists();
                 DronesListView.ItemsSource = dronesIenumrble; }
            else
            {
                
                dronesIenumrble = dronesIenumrble.Intersect(bl.DroneToListsByWhight((IBL.BO.WeightCategories)WeightSelctor.SelectedItem));
                DronesListView.ItemsSource = dronesIenumrble;


            }
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StatusSelector.SelectedItem == "")
            {
                dronesIenumrble = bl.DroneToLists();
                DronesListView.ItemsSource = dronesIenumrble;
            }
            else
            {
                dronesIenumrble= dronesIenumrble.Intersect(bl.DroneToListsByStatus((IBL.BO.DroneStatus)StatusSelector.SelectedItem));
                DronesListView.ItemsSource = dronesIenumrble;

            }
        }

        private void ChoseDrone(object sender, MouseButtonEventArgs e)
        {
            new DroneWindow((IBL.BO.DroneToList)sender).Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new DroneWindow().Show();
        }
    }
}
