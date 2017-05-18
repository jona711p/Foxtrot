using Foxtrot.Classes;
using Foxtrot.Classes.DB;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Foxtrot.GUI.Event
{
    /// <summary>
    /// Interaction logic for Event_Edit_Delete.xaml
    /// </summary>
    public partial class Event_Edit_Delete : Page
    {
        Classes.Event tempEvent = new Classes.Event();
        private bool availability;
        public Event_Edit_Delete()
        {
            InitializeComponent();
            tempEvent.EventTable = new DataTable();
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
                datepicker_Event_Edit_CreationDate.Text = tempEvent.CreationDate.ToString();
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
            datepicker_Event_Edit_CreationDate.IsEnabled = input;
        }

        private void button_Event_Edit_Click(object sender, RoutedEventArgs e)
        {
            tempEvent.Address = textBox_Event_Edit_Adress.Text;
            if (rbtn_Event_Edit_Availability_False.IsEnabled == true ||
                rbtn_Event_Edit_Availability_False.IsChecked == true)
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
    }
}
