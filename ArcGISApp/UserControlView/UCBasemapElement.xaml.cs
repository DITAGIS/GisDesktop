using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.UI.Controls;
using Model;
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
    /// Interaction logic for UCBaseMap.xaml
    /// </summary>
    public partial class UCBasemapElement : UserControl
    {
        private MapView MapView;
        public UCBasemapElement(string source, string title, MapView mapView)
        {
            InitializeComponent();

            this.imgBasemap.Source = new BitmapImage(new Uri(source, UriKind.Relative));
            this.txtBasemap.Text = title;

            this.MapView = mapView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var stkPanel = ((sender as Button).Content as StackPanel);
            foreach (var child in stkPanel.Children)
            {
                if (child is TextBlock)
                {
                    switch ((child as TextBlock).Text)
                    {
                        case Constant.BasemapTitle.ANH_BE_MAT_NOI:
                            this.MapView.Map = new Map(Basemap.CreateTerrainWithLabels());
                            break;
                        case Constant.BasemapTitle.BIEU_DO_HANG_KHONG:
                            this.MapView.Map = new Map(Basemap.CreateNavigationVector());
                            break;
                        case Constant.BasemapTitle.DAI_DUONG:
                            this.MapView.Map = new Map(Basemap.CreateOceans());
                            break;
                        case Constant.BasemapTitle.DIA_HINH:
                            this.MapView.Map = new Map(Basemap.CreateTopographic());
                            break;
                        case Constant.BasemapTitle.DUONG_PHO:
                            this.MapView.Map = new Map(Basemap.CreateStreets());
                            break;
                        case Constant.BasemapTitle.GIAN_LUOC_SANG:
                            this.MapView.Map = new Map(Basemap.CreateLightGrayCanvasVector());
                            break;
                        case Constant.BasemapTitle.GIAN_LUOC_TOI:
                            this.MapView.Map = new Map(Basemap.CreateDarkGrayCanvasVector());
                            break;
                        case Constant.BasemapTitle.HINH_ANH:
                            this.MapView.Map = new Map(Basemap.CreateImageryWithLabels());
                            break;
                        case Constant.BasemapTitle.OPEN_STREET_MAP:
                            this.MapView.Map = new Map(Basemap.CreateOpenStreetMap());
                            break;
                    }
                }
            }
        }
    }
}
