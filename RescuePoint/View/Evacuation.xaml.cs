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
    public partial class EvacuationPage : PhoneApplicationPage
    {
        public EvacuationPage()
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
                AddPoint(MyMapControl, geoCord, "current");

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
                AddPoint(MyMapControl, coor, "evac");

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
                DTOEvacuation shortEvac = new DTOEvacuation();

                foreach (var item in DTOEvac)
                {
                    var coor = new GeoCoordinate();
                    coor.Latitude = Convert.ToDouble(item.Latitude);
                    coor.Longitude = Convert.ToDouble(item.Longitude);
                   

                    double distance = geoCord.GetDistanceTo(coor);
                    if (first || shortestDistance > distance)
                    {
                        shortestDistance = distance;
                        shortEvac = item;
                        first = false;
                    }

                }

                tempPoints = new List<GeoCoordinate>();
                tempPoints.Add(geoCord);
                GeoCoordinate evacCoor = new GeoCoordinate(Convert.ToDouble(shortEvac.Latitude), Convert.ToDouble(shortEvac.Longitude));
                tempPoints.Add(evacCoor);

                MappingRoute();

            }
            else
            {

            }
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
            coor.Latitude = Convert.ToDouble(DTOEvac[idx + 1].Latitude);
            coor.Longitude = Convert.ToDouble(DTOEvac[idx + 1].Longitude);
            tempPoints.Add(coor);
        }

        private void AddPoint(Map controlMap, GeoCoordinate geo, string type)
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
            mo.Content = r;
            mo.GeoCoordinate = geo;
            ml.Add(mo);
            controlMap.Layers.Add(ml);
            controlMap.Hold += controlMap_Hold;
            waypoints.Add(geo);
            tempPoints.Add(geo);

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

        private void AddRoute(object sender, System.EventArgs e)
        {
            isMappingRoute = true;
        }

        private void imgRoute_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MapRoute();
        }
    }
}