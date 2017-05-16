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
            tempEvent.ProductTable = new DataTable();
        }

        private void datagrid_Event_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
