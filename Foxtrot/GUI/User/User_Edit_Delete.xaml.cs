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
using Classes;

namespace Foxtrot.GUI.User
{
    /// <summary>
    /// Interaction logic for User_Edit.xaml
    /// </summary>
    public partial class User_Edit_Delete : Page
    {
        private Classes.User tempUser = new Classes.User();
        private Classes.Actor tempActor = new Classes.Actor();
        private Classes.Administrator tempAdministrator = new Classes.Administrator();

        public User_Edit_Delete()
        {
            InitializeComponent();
            DataContext = tempUser;

            tempUser.UserTable = new DataTable();

            DBReadLogic.FillUserTable(tempUser.UserTable);

            DataContext = tempUser;
        }

        private void Rdbtn_User_Edit_Admin_OnClick(object sender, RoutedEventArgs e)
        {
            textBox_User_Edit_CompanyName.Visibility = Visibility.Collapsed;
        }

        private void Rdbtn_User_Edit_Actor_OnClick(object sender, RoutedEventArgs e)
        {
            textBox_User_Edit_CompanyName.Visibility = Visibility.Visible;
        }

        private void Button_User_Edit_Edit_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Button_User_Edit_Delete_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DataGrid_User_Edit_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (dataGrid_User_Edit.SelectedItem != null)
            //{
            //    if (int.Parse(((TextBlock)dataGrid_User_Edit.Columns[7].GetCellContent(dataGrid_User_Edit.SelectedItem)).Text) == 1) //If the selected row is an administrator
            //    {
            //        {
            //            tempAdministrator.ID =
            //                int.Parse(
            //                    ((TextBlock)
            //                            dataGrid_User_Edit.Columns[0].GetCellContent(
            //                                dataGrid_User_Edit.SelectedItem))
            //                                    .Text);
            //            DBReadLogic.GetInfo(tempAdministrator);
            //        }
            //    }
            //    else if (int.Parse(((TextBlock)dataGrid_User_Edit.Columns[7].GetCellContent(dataGrid_User_Edit.SelectedItem)).Text) == 2) //If the selected row is an actor
            //    {
            //        {
            //            tempActor.ID =
            //                int.Parse(
            //                    ((TextBlock)
            //                            dataGrid_User_Edit.Columns[0].GetCellContent(
            //                                dataGrid_User_Edit.SelectedItem))
            //                                    .Text);
            //            DBReadLogic.GetInfo(tempActor);
            //        }
            //    }
            //}
        }
    }
}
