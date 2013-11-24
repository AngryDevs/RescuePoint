using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Text;
using System.Device.Location;

namespace RescuePoint.View
{
    public partial class EvacuationDetails : PhoneApplicationPage
    {
        public EvacuationDetails()
        {
            InitializeComponent();
        }

        DTOEvacuation dto = new DTOEvacuation();
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            dto = PhoneApplicationService.Current.State["param"] as DTOEvacuation;
            LoadDTO();
        }

        void LoadDTO()
        {
            StringBuilder details = new StringBuilder();
            txtName.Text = dto.Name;
            txtCurrentPeople.Text = dto.CurrentPeople.ToString();
            txtMaxPeople.Text = dto.MaxPeople.ToString();
            txtFood.Text = string.Format("Good for {0} days", dto.Food.ToString());
            txtWater.Text = string.Format("Good for {0} days", dto.Water.ToString());  
            txtMedicine.Text = string.Format("Good for {0} days", dto.Medicine.ToString());  
            



        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //GeoCoordinate coor = new GeoCoordinate();
            //coor.Latitude = Convert.ToDouble(dto.Latitude);
            //coor.Longitude = Convert.ToDouble(dto.Longitude);

            PhoneApplicationService.Current.State["dto"] = dto;
            NavigationService.Navigate(new Uri("/View/Donations.xaml", UriKind.Relative));
        }
    }
}