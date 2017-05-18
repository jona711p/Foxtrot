using Foxtrot.Classes;
using Foxtrot.Classes.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Foxtrot.GUI.Event
{
    /// <summary>
    /// Interaction logic for Event_Edit_Delete.xaml
    /// </summary>
    public partial class Event_Edit_Delete : Page
    {
        Classes.Event tempEvent = new Classes.Event();
        Classes.User tempUser = new Classes.User();

        private bool availability;
        private City tempCity = new City();

        public Event_Edit_Delete(Classes.User inputUser)
        {
            InitializeComponent();
            tempEvent.EventTable = new DataTable();
            tempUser = inputUser;
            comboBox_Event_Edit_Delete_CityID.ItemsSource = DBReadLogic.FillCityDictionary(tempCity.CityDictionary);
            DBReadLogic.FillEventTable(tempEvent.EventTable);
            DataContext = tempEvent;
        }

        private void datagrid_Event_list_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Runs when the user selects any item on the datagrid
            //finds the selected products ID and retrieves all information about it from the database
            //the new information is stored in the object 'tempEvent' and displayed in the relavant inputfields in the GUI 
            if (datagrid_Event_list.SelectedItem != null)
            {
                tempEvent.ID =
                int.Parse(
                    ((TextBlock)
                    datagrid_Event_list.Columns[0].GetCellContent(
                        datagrid_Event_list.SelectedItem))
                        .Text);
                tempEvent = DBReadLogic.GetEventInfo(tempEvent);
                tempEvent = DBReadLogic.GetEventFileInfo(tempEvent);

                MakeFieldsEditable(true);

                textBox_Event_Edit_Name.Text = tempEvent.Name;
                textBox_Event_Edit_Adress.Text = tempEvent.Address;
                textBox_Event_Edit_CanonicalUrl.Text = tempEvent.CanonicalUrl;
                textBox_Event_Edit_Description.Text = tempEvent.Description;
                if (tempEvent.ExtraDescription.Count != 0)
                {
                    textBox_Event_Edit_ExtraDescription.Text = tempEvent.ExtraDescription[0].Description;
                }
                textBox_Event_Edit_Latitude.Text = tempEvent.Latitude.ToString();
                textBox_Event_Edit_Longtitude.Text = tempEvent.Longitude.ToString();
                textBox_Event_Edit_Website.Text = tempEvent.Website;
                rbtn_Event_Edit_Availability_True.IsChecked = tempEvent.Availability;
                rbtn_Event_Edit_Availability_False.IsChecked = !tempEvent.Availability;

                //for (int i = 0; i < 4; i++) //Resets all images
                //{
                //    ((System.Windows.Controls.Image)event_imageGrid.Children[i]).Source = null;
                //}
                //for (int i = 0; i < tempEvent.Files.Count && i < 4; i++) //Sets the UI images to display images related to the product
                //{
                //    ((System.Windows.Controls.Image)event_imageGrid.Children[i]).Source = new BitmapImage(new Uri(tempEvent.Files[i].URI));
                //}
                if (tempEvent.Files.Count > 0 && tempEvent.Files[0].URI != "")
                {
                    event_imageDisplay01.Source = new BitmapImage(new Uri(tempEvent.Files[0].URI));
                }
                if (tempEvent.Files.Count > 0 && tempEvent.Files[1].URI != "")
                {
                    event_imageDisplay01.Source = new BitmapImage(new Uri(tempEvent.Files[1].URI));
                }
                if (tempEvent.Files.Count > 0 && tempEvent.Files[2].URI != "")
                {
                    event_imageDisplay01.Source = new BitmapImage(new Uri(tempEvent.Files[2].URI));
                }
                if (tempEvent.Files.Count > 0 && tempEvent.Files[3].URI != "")
                {
                    event_imageDisplay01.Source = new BitmapImage(new Uri(tempEvent.Files[3].URI));
                }
                image.Source = new BitmapImage(new Uri(tempEvent.Files[0].URI));
                //comboBox_Event_Edit_Delete_CityID.Text = tempEvent.Cities.Name;
            }
        }

        public void MakeFieldsEditable(bool input)
        {
            textBox_Event_Edit_Name.IsEnabled = input;
            textBox_Event_Edit_Adress.IsEnabled = input;
            textBox_Event_Edit_CanonicalUrl.IsEnabled = input;
            textBox_Event_Edit_Description.IsEnabled = input;
            textBox_Event_Edit_ExtraDescription.IsEnabled = input;
            textBox_Event_Edit_Latitude.IsEnabled = input;
            textBox_Event_Edit_Longtitude.IsEnabled = input;
            textBox_Event_Edit_Website.IsEnabled = input;
            rbtn_Event_Edit_Availability_False.IsEnabled = input;
            rbtn_Event_Edit_Availability_True.IsEnabled = input;
        }

        private void button_Event_Edit_Click(object sender, RoutedEventArgs e)
        {
            tempEvent.Address = textBox_Event_Edit_Adress.Text;
            if (rbtn_Event_Edit_Availability_True.IsChecked == true || rbtn_Event_Edit_Availability_False.IsChecked == true)
            {
                tempEvent.Availability = availability;
            }
            tempEvent.CanonicalUrl = textBox_Event_Edit_CanonicalUrl.Text;
            tempEvent.Description = textBox_Event_Edit_Description.Text;

            if (textBox_Event_Edit_ExtraDescription.Text.Length != 0)
            {
                tempEvent.ExtraDescription = new List<ExtraDescription>()
                    {
                        new ExtraDescription()
                        {
                            Description = textBox_Event_Edit_ExtraDescription.Text
                        }
                    };
            }
            tempEvent.Longitude = float.Parse(textBox_Event_Edit_Latitude.Text.ToString());
            tempEvent.Latitude = float.Parse(textBox_Event_Edit_Longtitude.Text.ToString());
            tempEvent.Name = textBox_Event_Edit_Name.Text;
            tempEvent.Website = textBox_Event_Edit_Website.Text;
            if (comboBox_Event_Edit_Delete_CityID.SelectedItem != null)
            {
                tempEvent.Cities = new City();
                tempEvent.Cities.ID = ((KeyValuePair<string, int>)comboBox_Event_Edit_Delete_CityID.SelectedItem).Value;
            }

            DBUpdateLogic.UpdateEvent(tempEvent);
            MessageBox.Show("Eventet: '" + tempEvent.Name + "' " + "er blevet redigeret i systemet!");
        }

        private void btb_Event_Search_Click(object sender, RoutedEventArgs e)
        {
            DBReadLogic.FillEventTable(tempEvent.EventTable);
            if (textBox_Event_Search_Name.Text != "")
            {

            }
        }

        private void rbtn_Event_Edit_Availability_False_Click(object sender, RoutedEventArgs e)
        {
            availability = false;
        }

        private void rbtn_Event_Edit_Availability_True_Click(object sender, RoutedEventArgs e)
        {
            availability = true;
        }
    }
}
