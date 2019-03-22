using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.UI.Controls;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View.Model;

namespace Proccessor
{
    public class MapViewHandlingEvent
    {
        private MapView MyMapView;
        public MapViewHandlingEvent(MapView mapView)
        {
            this.MyMapView = mapView;

            // create a grid for showing Latitude and Longitude (Meridians and Parallels)
            LatitudeLongitudeGrid grid = new LatitudeLongitudeGrid();

            // display the grid on the map view
            MyMapView.Grid = grid;
            MyMapView.Grid.IsVisible = false;
            MyMapView.Grid.IsLabelVisible = true;
            

        }

        public void ChangeGridVisible()
        {
            this.MyMapView.Grid.IsVisible = !MyMapView.Grid.IsVisible;
        }
        public void AddLayer()
        {
            FeatureLayer featureLayer = new FeatureLayer(new Uri(Constant.FeatureLayerUrl));
            
            MyMapView.Map.OperationalLayers.Add(featureLayer);
        }
        public void showCalloutLatLong(GeoViewInputEventArgs e)
        {
            // Get the user-tapped location
            MapPoint mapLocation = e.Location;

            // Project the user-tapped map point location to a geometry
            Esri.ArcGISRuntime.Geometry.Geometry myGeometry = GeometryEngine.Project(mapLocation, SpatialReferences.Wgs84);

            // Convert to geometry to a traditional Lat/Long map point
            MapPoint projectedLocation = (MapPoint)myGeometry;

            // Format the display callout string based upon the projected map point (example: "Lat: 100.123, Long: 100.234")
            string mapLocationDescription = string.Format("Lat: {0:F3} Long:{1:F3}", projectedLocation.Y, projectedLocation.X);

            // Create a new callout definition using the formatted string
            CalloutDefinition myCalloutDefinition = new CalloutDefinition("Location:", mapLocationDescription);

            // Display the callout

            MyMapView.ShowCalloutAt(mapLocation, myCalloutDefinition);

        }
        public async Task<List<List<FeatureInfo>>> IdentifyLayerAsync(System.Windows.Point tapScreenPoint)
        {
            List<List<FeatureInfo>> result = new List<List<FeatureInfo>>();
            IdentifyLayerResult idResults = await MyMapView.IdentifyLayerAsync(MyMapView.Map.OperationalLayers[0], tapScreenPoint, 5, false, 5);
            FeatureLayer idLayer = idResults.LayerContent as FeatureLayer;
            idLayer.ClearSelection();
            // iterate each identified GeoElement in the results
            foreach (GeoElement idElement in idResults.GeoElements)
            {
                // cast the result GeoElement to Feature
                Feature idFeature = idElement as Feature;

                // select this feature in the feature layer
                idLayer.SelectFeature(idFeature);

                result.Add(GetDetailFromAttributes(idFeature));

            }
            return result;
        }

        private List<FeatureInfo> GetDetailFromAttributes(Feature feature)
        {

            var featInfos = new List<FeatureInfo>();
            try
            {
                var attributes = feature.Attributes;
                foreach (var field in feature.FeatureTable.Fields)
                {
                    var value = attributes[field.Name];
                    if (value != null)
                    {

                        switch (field.FieldType)
                        {
                            case FieldType.Date:
                                featInfos.Add(new FeatureInfo()
                                {
                                    Alias = field.Alias,
                                    Value = ((DateTimeOffset)value).ToLocalTime()
                                });
                                break;
                            case FieldType.Float32:
                            case FieldType.Float64:
                            case FieldType.Int16:
                            case FieldType.Int32:
                            case FieldType.OID:
                            case FieldType.Text:
                                featInfos.Add(new FeatureInfo()
                                {
                                    Alias = field.Alias,
                                    Value = value
                                });
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            return featInfos;
        }

        public void Zoom(bool isZoomIn)
        {
            var viewPoint = MyMapView.GetCurrentViewpoint(ViewpointType.CenterAndScale);
            var mapLocation = viewPoint.TargetGeometry.Extent.GetCenter();
            // Project the user-tapped map point location to a geometry
            Esri.ArcGISRuntime.Geometry.Geometry myGeometry = GeometryEngine.Project(mapLocation, SpatialReferences.Wgs84);

            // Convert to geometry to a traditional Lat/Long map point
            MapPoint projectedLocation = (MapPoint)myGeometry;
            double scale = isZoomIn ? (MyMapView.MapScale * 0.7) : (MyMapView.MapScale * 1.3);
            MyMapView.SetViewpointCenterAsync(projectedLocation.Y, projectedLocation.X, scale);
        }
        public void GoHome()
        {
            this.MyMapView.LocationDisplay.IsEnabled = !this.MyMapView.LocationDisplay.IsEnabled;
            if (this.MyMapView.LocationDisplay.IsEnabled)
            {
                this.MyMapView.SetViewpointCenterAsync(this.MyMapView.LocationDisplay.MapLocation);
            }
        }
    }
}
