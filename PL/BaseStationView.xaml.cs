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
using BlApi;
using BO;
using Mapsui.Utilities;
using Mapsui.Layers;



namespace PL
{
  
    /// <summary>
    /// Interaction logic for BaseStationView.xaml
    /// </summary>
    public partial class BaseStationView : Window
    {
        BlApi.IBL bl;
        BaseStation baseStation;
        Location location;
        public BaseStationView(BlApi.IBL bL)
        {
            InitializeComponent();
            bl = bL;
            baseStation = new BaseStation();
            baseStation.Location = new Location();
            location = new Location();
            DataContext = baseStation;
            LongitudeText.DataContext = baseStation.Location;
            Latitudtext.DataContext = baseStation.Location;
       
        }
        public BaseStationView(BlApi.IBL bL,BaseStationToList base_)
        {
            InitializeComponent();
            bl = bL;
            baseStation = bl.BaseByNumber(base_.SerialNum);
            SerialText.IsEnabled = false;
            location = baseStation.Location;
            DataContext = baseStation;
            LongitudeText.DataContext = baseStation.Location;
            Latitudtext.DataContext = baseStation.Location;
            
        }
      
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddBase(baseStation);
            }catch(Exception ex)
            { MessageBox.Show(ex.ToString(), "ERROE"); }
        }

        private void PreviewKeyDownWhitNoDot(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;
            if (text.Text.All(x => x >= '0' && x <= '9'))
            {

               ((TextBox)sender).Background = Brushes.Transparent;
                AddButton.IsEnabled = true;
            }
            if (text.Text != "0" || text.Text != "")
            {

                ((TextBox)sender).BorderBrush = Brushes.Transparent;
            }

            //allow get out of the text box
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
                return;

            //allow list of system keys (add other key here if you want to allow)
            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
                e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home || e.Key == Key.End ||
                e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right || e.Key == Key.NumPad0
                || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5
                || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9
                )
                return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

            //allow control system keys
            if (Char.IsControl(c)) return;

            //allow digits (without Shift or Alt)
            if (Char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox

            //forbid letters a
            //nd signs (#,$, %, ...)

            ((TextBox)sender).Background = Brushes.Red;
            AddButton.IsEnabled = false;

            return;
        }
        private void PreviewKeyDownWhitDot(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
     
            if (text == null) return;
            if (e == null) return;
            if (text.Text.All(x => x >= '0' && x <= '9'))
            {

                ((TextBox)sender).Background = Brushes.Transparent;
                AddButton.IsEnabled = true;
            }
            if(text.Text.Count(x => x == '.') > 1)
            {
                ((TextBox)sender).Background = Brushes.Transparent;
                AddButton.IsEnabled = true;
            }
            if (text.Text != "0" || text.Text != "")
            {

                ((TextBox)sender).BorderBrush = Brushes.Transparent;
            }

            //allow get out of the text box
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
                return;

            //allow list of system keys (add other key here if you want to allow)
            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
                e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home || e.Key == Key.End ||
                e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right || e.Key == Key.NumPad0
                || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5
                || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9
                )
                return;
            if (e.Key == Key.Decimal || e.Key == Key.OemPeriod)
            {
                if(text.Text.Count()==0)
                { }
                    if(text.Text.Count(x=>x=='.')>1)
                {
                    ((TextBox)sender).Background = Brushes.Red;
                    AddButton.IsEnabled = false;

                    return;
                }
                return;
            }
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

            //allow control system keys
            if (Char.IsControl(c)) return;
            
            //allow digits (without Shift or Alt)
            if (Char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox

            //forbid letters a
            //nd signs (#,$, %, ...)

            ((TextBox)sender).Background = Brushes.Red;
            AddButton.IsEnabled = false;

            return;
        }

        private void HeaderedContentControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Latitudtext_TextChanged(object sender, TextChangedEventArgs e)
        {
           /* TextBox text = sender as TextBox;
            if (text == null) return;
            if(Double.text.Text)
            ((TextBox)sender).Background = Brushes.Red;
            AddButton.IsEnabled = false;*/
        }

        private void Latitudtext_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Text = "";
        }
    }
}
