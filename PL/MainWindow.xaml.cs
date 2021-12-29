﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
       internal readonly IBL bL= BlFactory.GetBl();


        public MainWindow()
        {
            
            InitializeComponent();
        }

        private void DroneMainButton_Click(object sender, RoutedEventArgs e)
        {
            
            new DronesListWindow( bL).ShowDialog();
            
        }

        private void BaseStationsButton_Click(object sender, RoutedEventArgs e)
        {
            new BaseStationsList(bL).ShowDialog();
        }

        private void PackagesButton_Click(object sender, RoutedEventArgs e)
        {
            new PackagesList(bL).ShowDialog();
        }

        private void ClientsButton_Click(object sender, RoutedEventArgs e)
        {
            new ClientsLIst(bL).ShowDialog();
        }
    }
}
