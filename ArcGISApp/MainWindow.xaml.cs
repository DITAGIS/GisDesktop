using View.UserControlView;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Tasks.Geocoding;
using Esri.ArcGISRuntime.Tasks.Offline;
using Esri.ArcGISRuntime.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using View.Model;
using Proccessor;
using Model;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UC_Map UC_Map;
        public MainWindow()
        {
            InitializeComponent();


        }
        private void Initialize()
        {
            if (UC_Map == null)
                UC_Map = new UC_Map();
            else
            {
                foreach (var operationalLayer in UC_Map.MyMapView.Map.OperationalLayers)
                    operationalLayer.LoadAsync();

            }
            UC_Map.Height = stkMain.ActualHeight;
            UC_Map.Width = stkMain.ActualWidth;
            if (stkMain.Children.Count == 1)
                stkMain.Children.RemoveAt(0);
            stkMain.Children.Add(UC_Map);
        }

        private void RibbonButton_Click(object sender, RoutedEventArgs e)
        {
            Initialize();
            //MapViewHandlingEvent.ChangeGridVisible();
        }




    }
}
