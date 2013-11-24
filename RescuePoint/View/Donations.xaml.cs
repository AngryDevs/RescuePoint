using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Maps.Services;
using Windows.Devices.Geolocation;
using Microsoft.Phone.Maps.Controls;
using System.Device.Location;
using System.Windows.Shapes;
using System.Windows.Media;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Text;

namespace RescuePoint.View
{
    public partial class Page3 : PhoneApplicationPage
    {
        public Page3()
        {
            InitializeComponent();
        }

        RouteQuery routeQuery = new RouteQuery();

        Geolocator geolocator = new Geolocator();
        List<GeoCoordinate> waypoints = new List<GeoCoordinate>();
        List<GeoCoordinate> tempPoints = new List<GeoCoordinate>();
        StringBuilder sb = new StringBuilder();
        List<double> DurationList = new List<double>();
        GeoCoordinate geoCord;

        bool isMappingRoute = false;
        bool isShortest = false;
        //int index = 0;
        DTOEvacuationList DTOEvac = new DTOEvacuationList();


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            routeQuery.TravelMode = TravelMode.Driving;
            routeQuery.QueryCompleted += routeQuery_QueryCompleted;
            GetCurrentLocation();
            MyMapControl.CartographicMode = MapCartographicMode.Road;
            MyMapControl.LandmarksEnabled = true;
            MyMapControl.ZoomLevel = 16;

        }

        private async void GetCurrentLocation()
        {
            try
            {
                geolocator.DesiredAccuracyInMeters = 70;
                // If the cached location is over 30 minutes old, get a new one
                TimeSpan maximumAge = new TimeSpan(0, 30, 0);
                // If we're trying for more than 45 seconds, abandon the operation
                TimeSpan timeout = new TimeSpan(0, 0, 60);
                Geoposition myLocation = await geolocator.GetGeopositionAsync(maximumAge, timeout);

                geoCord = new GeoCoordinate(myLocation.Coordinate.Latitude, myLocation.Coordinate.Longitude);

                MyMapControl.Center = geoCord;
                AddPoint(MyMapControl, geoCord, "current", "current");

                PopulateEvac();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        void PopulateEvac()
        {
            var parser = new XMLParser();
            var XDoc = new XDocument();
            string content = "";
            var ResourceStream = Application.GetResourceStream(new Uri("LocationXML.xml", UriKind.Relative));

            if (ResourceStream != null)
            {
                Stream myFileStream = ResourceStream.Stream;
                if (myFileStream.CanRead)
                {
                    StreamReader myStreamReader = new StreamReader(myFileStream);

                    //read the content here
                    content = myStreamReader.ReadToEnd();
                    content = content.Replace("\r\n", string.Empty);
                    XDoc = XDocument.Parse(content);
                }
            }
            DTOEvac = parser.PopulateEvacuation(XDoc);

            foreach (var item in DTOEvac)
            {
                var coor = new GeoCoordinate();
                coor.Latitude = Convert.ToDouble(item.Latitude);
                coor.Longitude = Convert.ToDouble(item.Longitude);
                AddPoint(MyMapControl, coor, "evac", item.Name);

            }



        }

        private void AddPoint(Map controlMap, GeoCoordinate geo, string type, string name)
        {
            MapLayer ml = new MapLayer();
            MapOverlay mo = new MapOverlay();

            Ellipse r = new Ellipse();

            switch (type)
            {
                case "current":
                    r.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 0, 5));
                    break;
                case "evac":
                    r.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 140, 0));
                    break;
                case "relief":
                    r.Fill = new SolidColorBrush(Color.FromArgb(255, 0, 128, 0));
                    break;
                case "morgue":
                    r.Fill = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                    break;
                default:
                    r.Fill = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                    break;
            }

            r.Width = r.Height = 20;
            r.Margin = new Thickness(-6, -6, 0, 0);
            r.Name = name;
            r.DoubleTap += r_DoubleTap;
            mo.Content = r;
            mo.GeoCoordinate = geo;
            ml.Add(mo);
            controlMap.Layers.Add(ml);
            controlMap.Hold += controlMap_Hold;
            waypoints.Add(geo);
            tempPoints.Add(geo);

        }

        void r_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var el = (Ellipse)sender;
            if (el.Name != "current")
                GetDetails(el.Name);
        }

        void GetDetails(string name)
        {
            try
            {
                var o = DTOEvac.Find(x => x.Name == name);

                PhoneApplicationService.Current.State["param"] = o;
                NavigationService.Navigate(new Uri("/View/EvacuationDetails.xaml", UriKind.Relative));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void controlMap_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //e.
            MessageBox.Show("wah");
        }

        private void MappingRoute()
        {
            isMappingRoute = false;
            routeQuery.Waypoints = tempPoints;
            routeQuery.QueryAsync();

        }


        void routeQuery_QueryCompleted(object sender, QueryCompletedEventArgs<Route> e)
        {
            if (null == e.Error)
            {
                Route MyRoute = e.Result;
                MapRoute MyMapRoute = new MapRoute(MyRoute);

                MyMapControl.AddRoute(MyMapRoute);
                MyMapControl.SetView(MyMapRoute.Route.BoundingBox);         
            }

            else
                MessageBox.Show("Error occured:\n" + e.Error.Message);

        }
    }
}