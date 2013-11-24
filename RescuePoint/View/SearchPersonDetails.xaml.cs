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
using System.Windows.Media;

namespace RescuePoint.View
{
    public partial class SearchPersonDetails : PhoneApplicationPage
    {
        public SearchPersonDetails()
        {
            InitializeComponent();
        }
        DTOMorgue dto = new DTOMorgue();
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            dto = PhoneApplicationService.Current.State["param"] as DTOMorgue;
            LoadDTO();
        }

        void LoadDTO()
        {
            StringBuilder details = new StringBuilder();
            txtName.Text = dto.Name;
            txtCnumber.Text = dto.ContactNumber;
            txtTotalBodies.Text = dto.TotalBodies.ToString();
            txtTotalMale.Text = dto.TotalMales.ToString();
            txtTotalFemale.Text = dto.TotalFemales.ToString();
            txtTotalTeen.Text = dto.TotalTeens.ToString();
            txtTotalChild.Text = dto.TotalChilds.ToString();
            txtTotalAdult.Text = dto.TotalAdults.ToString();
            txtTotalIdentified.Text = dto.TotalIdentified.ToString();
            txtTotalUnidentified.Text = dto.TotalUnidentified.ToString();

            foreach (var item in dto.PersonList)
            {
                if (item.Identified)
                {

                    var lblName = new TextBlock();
                    var lblName2 = new TextBlock();
                    var lblGender = new TextBlock();
                    var lblGender2 = new TextBlock();
                    var lblAge = new TextBlock();
                    var lblAge2 = new TextBlock();

                    lblName.FontFamily = new FontFamily("Segoe WP");
                    lblName.FontWeight = FontWeights.Bold;
                    lblName.Text = "Name: ";

                    lblName2.FontFamily = new FontFamily("Segoe WP");
                    lblName2.FontWeight = FontWeights.Thin;
                    lblName2.Text = item.Name;

                    lblGender.FontFamily = new FontFamily("Segoe WP");
                    lblGender.FontWeight = FontWeights.Bold;
                    lblGender.Text = "Gender: ";

                    lblGender2.FontFamily = new FontFamily("Segoe WP");
                    lblGender2.FontWeight = FontWeights.Thin;
                    lblGender2.Text = item.Gender;

                    lblAge.FontFamily = new FontFamily("Segoe WP");
                    lblAge.FontWeight = FontWeights.Bold;
                    lblAge.Text = "Age: ";

                    lblAge2.FontFamily = new FontFamily("Segoe WP");
                    lblAge2.FontWeight = FontWeights.Thin;
                    lblAge2.Text = item.AgeBracket;
                    lblAge2.Margin = new Thickness(0, 0, 0, 20);


                    stackpanel.Children.Add(lblName);
                    stackpanel.Children.Add(lblName2);

                    stackpanel.Children.Add(lblGender);
                    stackpanel.Children.Add(lblGender2);

                    stackpanel.Children.Add(lblAge);
                    stackpanel.Children.Add(lblAge2);

                }
                else
                {
                    var lblGender = new TextBlock();
                    var lblGender2 = new TextBlock();
                    var lblAge = new TextBlock();
                    var lblAge2 = new TextBlock();

                    lblGender.FontFamily = new FontFamily("Segoe WP");
                    lblGender.FontWeight = FontWeights.Bold;
                    lblGender.Text = "Gender: ";

                    lblGender2.FontFamily = new FontFamily("Segoe WP");
                    lblGender2.FontWeight = FontWeights.Thin;
                    lblGender2.Text = item.Gender;

                    lblAge.FontFamily = new FontFamily("Segoe WP");
                    lblAge.FontWeight = FontWeights.Bold;
                    lblAge.Text = "Age: ";

                    lblAge2.FontFamily = new FontFamily("Segoe WP");
                    lblAge2.FontWeight = FontWeights.Thin;
                    lblAge2.Text = item.AgeBracket;
                    lblAge2.Margin = new Thickness(0, 0, 0, 20);

                    stackpanel2.Children.Add(lblGender);
                    stackpanel2.Children.Add(lblGender2);

                    stackpanel2.Children.Add(lblAge);
                    stackpanel2.Children.Add(lblAge2);
                }
               
            }

        }


    }
}