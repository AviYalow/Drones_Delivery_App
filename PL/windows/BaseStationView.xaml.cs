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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for BaseStationView.xaml
    /// </summary>
    public partial class BaseStationView : Window
    {
        BlApi.IBL bl;

        public BaseStationView(BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;

        }
    }
}