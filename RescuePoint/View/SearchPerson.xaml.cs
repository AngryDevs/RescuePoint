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
using System.Device.Location;
using System.Text;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Xml.Linq;
using System.IO;

namespace RescuePoint.View
{
    public partial class Page2 : PhoneApplicationPage
    {
        public Page2()
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
        //DTOEvacuationList DTOEvac = new DTOEvacuationList();
        DTOMorgueList DTOMorgue = new DTOMorgueList();

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

        //same in Evacuation.xaml.cs
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

                PopulateMorgues();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        void PopulateMorgues()
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

            DTOMorgue = parser.PopulateMorgue(XDoc);

            foreach (var item in DTOMorgue)
            {
                var coor = new GeoCoordinate();
                coor.Latitude = Convert.ToDouble(item.Latitude);
                coor.Longitude = Convert.ToDouble(item.Longitude);
                AddPoint(MyMapControl, coor, "morgue", item.Name);
            }
        }

        void MapRoute()
        {
            if (geoCord != null)
            {

                tempPoints = new List<GeoCoordinate>();
                tempPoints.Add(geoCord);

                double shortestDistance = 0;

                bool first = true;
                DTOMorgue shortMorgue = new DTOMorgue();

                foreach (var item in DTOMorgue)
                {
                    var coor = new GeoCoordinate();
                    coor.Latitude = Convert.ToDouble(item.Latitude);
                    coor.Longitude = Convert.ToDouble(item.Longitude);


                    double distance = geoCord.GetDistanceTo(coor);
                    if (first || shortestDistance > distance)
                    {
                        shortestDistance = distance;
                        shortMorgue = item;
                        first = false;
                    }

                }

                tempPoints = new List<GeoCoordinate>();
                tempPoints.Add(geoCord);
                GeoCoordinate evacCoor = new GeoCoordinate(Convert.ToDouble(shortMorgue.Latitude), Convert.ToDouble(shortMorgue.Longitude));
                tempPoints.Add(evacCoor);

                MappingRoute();

            }
            else
            {

            }
        }

        private void MappingRoute()
        {
            isMappingRoute = false;
            routeQuery.Waypoints = tempPoints;
            routeQuery.QueryAsync();

        }

        void GetShortestPath()
        {
            double min = 0;
            int idx = 0;
            for (int i = 0; i < DurationList.Count - 1; i++)
            {
                if (DurationList[i] < min)
                {
                    min = DurationList[i];
                    idx = i;
                }
            }
            tempPoints = new List<GeoCoordinate>();
            tempPoints.Add(waypoints[0]);

            var coor = new GeoCoordinate();
            coor.Latitude = Convert.ToDouble(DTOMorgue[idx + 1].Latitude);
            coor.Longitude = Convert.ToDouble(DTOMorgue[idx + 1].Longitude);
            tempPoints.Add(coor);
        }

        private void AddPoint(Map controlMap, GeoCoordinate geo, string type, string name)
        {
            MapLayer ml = new MapLayer();
            MapOverlay mo = new MapOverlay();

            Ellipse r = new Ellipse();

            switch (type)
            {
                case "current": //red
                    r.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 0, 5));
                    break;
                case "evac": //orange
                    r.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 140, 0));
                    break;
                case "relief": //green
                    r.Fill = new SolidColorBrush(Color.FromArgb(255, 0, 128, 0));
                    break;
                case "morgue": //black
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


            //controlMap.Hold += controlMap_Hold;
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
                var o = DTOMorgue.Find(x => x.Name == name);

                PhoneApplicationService.Current.State["param"] = o;
                NavigationService.Navigate(new Uri("/View/SearchPersonDetails.xaml", UriKind.Relative));
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
         
        }

        void routeQuery_QueryCompleted(object sender, QueryCompletedEventArgs<Route> e)
        {
            if (null == e.Error)
            {
                Route MyRoute = e.Result;
                MapRoute MyMapRoute = new MapRoute(MyRoute);

                MyMapControl.AddRoute(MyMapRoute);
                MyMapControl.SetView(MyMapRoute.Route.BoundingBox);


                //var i = 0;
                //sb.AppendFormat("Estimated time: {0} minutes\n", e.Result.EstimatedDuration.TotalMinutes.ToString("#,###.##"));
                //foreach (var leg in e.Result.Legs)
                //    foreach (var maneuver in leg.Maneuvers)
                //    {
                //        sb.AppendFormat("{0}. {1}: {2}\n", ++i, maneuver.InstructionKind.ToString(), maneuver.InstructionText);
                //    }
            }

            else
                MessageBox.Show("Error occured:\n" + e.Error.Message);

        }


    }
}