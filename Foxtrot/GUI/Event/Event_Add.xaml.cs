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
using Foxtrot.Classes;
using Foxtrot.Classes.DB;

namespace Foxtrot.GUI.Event
{
    /// <summary>
    /// Interaction logic for Event_Add.xaml
    /// </summary>
    public partial class Event_Add : Page
    {
        private Classes.Event tempEvent = new Classes.Event();
        private Classes.User tempUser = new Classes.User();
        public City tempCity = new City();
        
        public Event_Add(Classes.User inputUser) //FK_ActorID til userID
        {
            InitializeComponent();

            tempUser = inputUser;
            comboBox_Event_Add_CityID.ItemsSource = DBReadLogic.FillCityDictionary(tempCity.CityDictionary);
    }

        private void Button_Event_Add_Create_OnClick(object sender, RoutedEventArgs e)
        {
            if (textBox_Event_Add_Name.Text != "")
            {
                tempEvent.Name = GUISortingLogic.Name(textBox_Event_Add_Name); 
            }
            if (textBox_Event_Add_Address.Text != "")
            {
                tempEvent.Address = GUISortingLogic.Name(textBox_Event_Add_Address);
            }
            if (textBox_Event_Add_Longitude.Text != "")
            {
                tempEvent.Longitude = float.Parse(textBox_Event_Add_Longitude.Text);
            }
            if (textBox_Event_Add_Latitude.Text != "")
            {
                tempEvent.Latitude = float.Parse(textBox_Event_Add_Latitude.Text);
            }
            if (textBox_Event_Add_Description.Text != "")
            {
                tempEvent.Description = textBox_Event_Add_Description.Text;
            }
            if (textBox_Event_Add_ExtraDescription.Text != "")
            {
                tempEvent.ExtraDescription = new List<ExtraDescription>()
            {
                new ExtraDescription()
                {
                    Description = textBox_Event_Add_ExtraDescription.Text
                }
            };
            }
            if (rbtn_Event_Add_Availability_True.IsChecked == true || rbtn_Event_Add_Availability_False.IsChecked == true)
            {
                tempEvent.Availability = rbtn_Event_Add_Availability_True.IsChecked;
            }
            if (comboBox_Event_Add_CityID.SelectedItem != null)
            {
                tempEvent.Cities = new City();
                tempEvent.Cities.ID = ((KeyValuePair<string, int>)comboBox_Event_Add_CityID.SelectedItem).Value;
            }




            tempEvent.Website = textBox_Event_Add_Website.Text;
            tempEvent.CanonicalUrl = textBox_Event_Add_CanonicalUrl.Text;

            tempEvent.ActorID = tempUser.ID; // skal rettes til så userid

            GUISortingLogic.Message("Arrangementet: '" + tempEvent.Name + "' er nu oprettet.");
        }
       
    }
}
