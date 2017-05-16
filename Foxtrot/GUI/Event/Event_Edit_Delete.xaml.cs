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

            tempEvent = DBReadLogic.GetEventInfo(tempEvent); 
        }
    }
}
