using Esri.ArcGISRuntime;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Tasks.Geocoding;
using Model;
using Proccessor;
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
    /// Interaction logic for UC_Map.xaml
    /// </summary>
    public partial class UC_Map : UserControl
    {
        private enum EditState
        {
            NotReady, // Geodatabase has not yet been generated.
            Editing, // A feature is in the process of being moved.
            Ready // The geodatabase is ready for synchronization or further edits.
        }

        private LocatorTask geocodeTask;
        private FeatureCollectionLayer FeatCollectionTable;
        private MapViewHandlingEvent MapViewHandlingEvent;
        public UC_Map()
        {
            InitializeComponent();
            InitializeAsync();
        }
        private void InitializeAsync()
        {
            //geocodeTask = await LocatorTask.CreateAsync(new Uri(geocodeServiceUrl));
            //GetFeaturesFromQuery();
            MapViewHandlingEvent = new MapViewHandlingEvent(MyMapView);
            MapViewHandlingEvent.AddLayer();

            MyMapView.GeoViewTapped += async (s, e) =>
            {
                progressBar.Visibility = Visibility.Visible;
                Point tapScreenPoint = e.Position;
                var details = await MapViewHandlingEvent.IdentifyLayerAsync(tapScreenPoint);
                details.ForEach(f =>
                {
                    UCTest ucTest = new UCTest(MyMapView);
                    ucTest.dataGrid.ItemsSource = f;
                    MapPoint mapLocation = MyMapView.ScreenToLocation(e.Position);
                    MyMapView.ShowCalloutAt(mapLocation, ucTest);
                });
                progressBar.Visibility = Visibility.Collapsed;
            };
            SetLicense();

           
        }
        private void SetLicense()
        {
            ArcGISRuntimeEnvironment.SetLicense(Constant.License);
        }

        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            MapViewHandlingEvent.Zoom(true);
        }

        private void GoHome_Click(object sender, RoutedEventArgs e)
        {
            this.MapViewHandlingEvent.GoHome();
        }

        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            MapViewHandlingEvent.Zoom(false);
        }

        private void BtnBasemap_Click(object sender, RoutedEventArgs e)
        {
            //temp
            var UCBaseMap = new UCBaseMap(this.MyMapView);
            
            UCBaseMap.Height = wrapPanelDetail.ActualHeight;
            UCBaseMap.Width = wrapPanelDetail.ActualWidth;
            if (wrapPanelDetail.Children.Count == 1)
                wrapPanelDetail.Children.RemoveAt(0);
            wrapPanelDetail.Children.Add(UCBaseMap);
        }
    }
}
