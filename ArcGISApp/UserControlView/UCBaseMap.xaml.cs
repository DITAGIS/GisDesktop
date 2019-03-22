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
    public partial class UCBaseMap : UserControl
    {
        
        public UCBaseMap(MapView mapView)
        {
            InitializeComponent();
            Initialize(mapView);
        }
        private void Initialize(MapView mapView)
        {
            this.wrapPanel.Children.Add(new UCBasemapElement(@"/View;component/Resources/Images/anh_be_mat_noi.jpg", Constant.BasemapTitle.ANH_BE_MAT_NOI,mapView));
            this.wrapPanel.Children.Add(new UCBasemapElement(@"/View;component/Resources/Images/bieu_do_hang_khong.jpg", Constant.BasemapTitle.BIEU_DO_HANG_KHONG, mapView));
            this.wrapPanel.Children.Add(new UCBasemapElement(@"/View;component/Resources/Images/dai_duong.jpg", Constant.BasemapTitle.DAI_DUONG, mapView));
            this.wrapPanel.Children.Add(new UCBasemapElement(@"/View;component/Resources/Images/dia_hinh.jpg", Constant.BasemapTitle.DIA_HINH, mapView));
            this.wrapPanel.Children.Add(new UCBasemapElement(@"/View;component/Resources/Images/duong_pho.jpg", Constant.BasemapTitle.DUONG_PHO, mapView));
            this.wrapPanel.Children.Add(new UCBasemapElement(@"/View;component/Resources/Images/gian_luoc_sang.jpg", Constant.BasemapTitle.GIAN_LUOC_SANG, mapView));
            this.wrapPanel.Children.Add(new UCBasemapElement(@"/View;component/Resources/Images/gian_luoc_toi.jpg", Constant.BasemapTitle.GIAN_LUOC_TOI, mapView));
            this.wrapPanel.Children.Add(new UCBasemapElement(@"/View;component/Resources/Images/hinh_anh.jpg", Constant.BasemapTitle.HINH_ANH, mapView));
            this.wrapPanel.Children.Add(new UCBasemapElement(@"/View;component/Resources/Images/open_street_map.jpg", Constant.BasemapTitle.OPEN_STREET_MAP, mapView));

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var stkPanel = ((sender as Button).Content as StackPanel);
            foreach (var child in stkPanel.Children)
            {
                if(child is TextBlock)
                {
                    switch((child as TextBlock).Text)
                    {

                    }
                }
            }
        }
    }
}
