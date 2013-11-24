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
            txtCurrentPeople.Text = dto.Medicine.ToString();
            txtFood.Text = dto.Food.ToString();
            txtWater.Text = dto.Water.ToString();
            txtMedicine.Text = dto.Medicine.ToString();
            StringBuilder person;



        }
    }
}