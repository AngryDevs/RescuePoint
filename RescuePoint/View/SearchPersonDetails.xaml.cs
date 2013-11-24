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


            //details.AppendLine(string.Format("Name: {0}", dto.Name));
           // details.AppendLine(string.Format("Contact Number: {0}", dto.ContactNumber));
            //details.AppendLine(string.Format("Total Bodies: {0}", dto.TotalBodies));
            //details.AppendLine(string.Format("Total Male: {0}", dto.TotalMales));
           // details.AppendLine(string.Format("Total Female: {0}", dto.TotalFemales));
            //details.AppendLine(string.Format("Total Teen: {0}", dto.TotalTeens));
           // details.AppendLine(string.Format("Total Child: {0}", dto.TotalChilds));
            //details.AppendLine(string.Format("Total Adult: {0}", dto.TotalAdults));
           // details.AppendLine(string.Format("Total Identified: {0}", dto.TotalIdentified));
           // details.AppendLine(string.Format("Total Unidentified: {0}", dto.TotalUnidentified));

           // txtDetails.Text = details.ToString();

            StringBuilder person;

            foreach (var item in dto.PersonList)
	        {
                person = new StringBuilder();

                person.AppendLine(string.Format("Name: {0}", item.Name));
                person.AppendLine(string.Format("Gender: {0}", item.Gender));
                person.AppendLine(string.Format("Age Bracket: {0}", item.AgeBracket));
                Identified.Items.Add(person.ToString());
		 
	        }
            
        }


    }
}