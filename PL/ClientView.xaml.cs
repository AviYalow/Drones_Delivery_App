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
using BO;
using BlApi;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for ClientView.xaml
    /// </summary>
    public partial class ClientView : Window
    {
        Client client;
        IBL bl;
        bool clientMode;

        public ClientView(BlApi.IBL bL,bool clientView=false)
        {
            InitializeComponent();
            bl = bL;
            client = new Client { ToClient = null, FromClient = null, Phone = "" };
            DataContext = client;
            clientMode = clientView;
            letitudeTextBox.DataContext = client.Location;
            longditue.DataContext = client.Location;
            ListPackegeFromClient.Visibility = Visibility.Collapsed;

        }

        public ClientView(BlApi.IBL bL, ClientToList clientFromList,bool clientView= false)
        {
            InitializeComponent();
            bl = bL;
            clientMode = clientView;
            ctorUpdateClient(clientFromList.ID);

        }
        public ClientView(BlApi.IBL bL, uint clientFromList, bool clientView = false)
        {
            InitializeComponent();
            bl = bL;
            clientMode = clientView;
            ctorUpdateClient(clientFromList);

        }
        private void ctorUpdateClient(uint id)
        {
            this.client = bl.GetingClient(id);
            this.DataContext = this.client;
            letitudeTextBox.DataContext = client.Location;
            longditue.DataContext = client.Location;
            TitelClientLabel.Content = "Updte Client Window";
            ListPackegeFromClient.ItemsSource = client.FromClient;
            ListPackegeToClient.ItemsSource = client.ToClient;
            ListPackegeFromClient.Visibility = Visibility.Visible;
            OkButton.Visibility = Visibility.Collapsed;
            foreach (var c in client.Phone.Skip(3))
            {
                phoneTextBox.Text += c;
            }
            foreach (var c in client.Phone.Take(3))
            {
                
                StartPhoneCmb.Text += c;
            }
            foreach (ComboBoxItem item in StartPhoneCmb.Items)
            {
                if (item.Content.ToString() == StartPhoneCmb.Text)
                {
                    StartPhoneCmb.SelectedItem = item;
                    break;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Closing += ClientView_Closing;
            this.Close();
        }

        private void ClientView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = false;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddClient(client);
                MessageBox.Show("Add client \n" + client.ToString() + "\nsucceed!");
                ctorUpdateClient(client.Id);
            }
            catch(Exception ex)
            { MessageBox.Show(ex.ToString(), "ERROR"); }
        }
        private void PreviewKeyDownWhitNoDot(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;

            if (text == null) return;
            if (e == null) return;
            if (text.Text.All(x => x >= '0' && x <= '9'))
            {

                ((TextBox)sender).Background = Brushes.Transparent;
                ((TextBox)sender).BorderBrush = Brushes.Transparent;
                OkButton.IsEnabled = true;
            }
            if (text.Text != "0" || text.Text != "")
            {
                ((TextBox)sender).BorderBrush = Brushes.Transparent;
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
            OkButton.IsEnabled = false;
            updateButton.IsEnabled = false;

            return;
        }
        private void PreviewKeyDownWhitDot(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;

            if (text == null) return;
            if (e == null) return;
            if (text.Text.All(x => x >= '0' && x <= '9'))
            {
                ((TextBox)sender).BorderBrush = Brushes.Transparent;
                ((TextBox)sender).Background = Brushes.Transparent;
                OkButton.IsEnabled = true;
            }
            if (text.Text.Count(x => x == '.') > 1)
            {
                ((TextBox)sender).BorderBrush = Brushes.Transparent;
                ((TextBox)sender).Background = Brushes.Transparent;
                OkButton.IsEnabled = true;
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

                if (text.Text.StartsWith('.') || text.Text.Count(x => x == '.') > 1)
                {
                    ((TextBox)sender).Background = Brushes.Red;
                    OkButton.IsEnabled = false;
                    updateButton.IsEnabled = false;

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
            OkButton.IsEnabled = false;

            return;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to update the client detels?", "Update", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                try
                {
                    bl.UpdateClient(client);
                    MessageBox.Show("Update seccsed!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "ERROR");

                }
            }
        }

        private void StartPhoneCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var text = (sender as ComboBox).Text;
            foreach(var c in client.Phone.Skip(4).SkipWhile(x => x == '-'))
            text +=c;
            client.Phone = text;
        }

        private void HeaderedContentControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                HeaderedContentControl control = sender as HeaderedContentControl;
                if (control.Name.LastOrDefault() == 'S')
                {
                    control.Name = control.Name.Remove(control.Name.Count() - 1);
                    ListPackegeFromClient.ItemsSource = (bl.SortList(control.Name, ListPackegeFromClient.ItemsSource as IEnumerable<PackageAtClient>));
                }
                if (control.Name.LastOrDefault() == 'P')
                {
                    control.Name = control.Name.Remove(control.Name.Count() - 1);
                    ListPackegeToClient.ItemsSource = (bl.SortList(control.Name, ListPackegeToClient.ItemsSource as IEnumerable<PackageAtClient>));
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR");
            }
        }

        private void ListPackegeFromClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListPackegeFromClient.SelectedItem != null)
            {

                new PackageView(bl, ((PackageAtClient)ListPackegeFromClient.SelectedItem).SerialNum, StatusPackegeWindow.SendClient).ShowDialog();
                this.client = bl.GetingClient(client.Id);
                ListPackegeFromClient.SelectedItem = null;
                this.DataContext = this.client;
            }
        }

        private void ListPackegeToClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListPackegeToClient.SelectedItem!=null)
            {

                new PackageView(bl, ((PackageAtClient)ListPackegeToClient.SelectedItem).SerialNum, StatusPackegeWindow.GetingClient, clientMode).ShowDialog();
                ListPackegeToClient.SelectedItem = null;
                this.client = bl.GetingClient(client.Id);
                this.DataContext = this.client;
            }
        }

        private void AddPAckegeButton_Click(object sender, RoutedEventArgs e)
        {
            new PackageView(bl,client.Id.ToString(),StatusPackegeWindow.SendClient,clientMode).ShowDialog();
            this.client = bl.GetingClient(client.Id);
            this.DataContext = this.client;
            ListPackegeFromClient.ItemsSource = client.FromClient;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this clien?", "Update", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                try
                {
                    bl.DeleteClient(client.Id);
                    MessageBox.Show($"client {client.Id.ToString()} deleted!");
                    this.Closing += ClientView_Closing;
                    this.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "ERROR");

                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
