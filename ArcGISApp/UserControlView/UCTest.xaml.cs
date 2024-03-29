﻿using Esri.ArcGISRuntime.UI.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace View.UserControlView
{
    /// <summary>
    /// Interaction logic for UCTest.xaml
    /// </summary>
    public partial class UCTest : UserControl
    {
        private MapView MapView;
        public UCTest(MapView mapView)
        {
            InitializeComponent();
            this.MapView = mapView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MapView.DismissCallout();
        }
    }
}
