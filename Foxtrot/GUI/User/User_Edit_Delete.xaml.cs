using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Foxtrot.Classes;
using Foxtrot.Classes.DB;

namespace Foxtrot.GUI.User
{
    /// <summary>
    /// Interaction logic for User_Edit.xaml
    /// </summary>
    public partial class User_Edit_Delete : Page
    {
        private Classes.User tempUser = new Classes.User();
        private Administrator tempAdministrator = new Administrator();
        private Actor tempActor = new Actor();

        public User_Edit_Delete()
        {
            InitializeComponent();
            DataContext = tempUser;

            tempUser.UserTable = new DataTable();

            DBReadLogic.FillUserTable(tempUser.UserTable);
            DataContext = tempUser;

        }

        private void DataGrid_User_Edit_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Runs when the user selects any item on the datagrid
            //Checks the selected users permission/usertype and and ID then runs the corresponding method to retrieves all information about it from the database
            //the new information is stored in the object 'tempAdministrator' or 'tempAdctor' depending on the usertype and displayed in the relavant inputfields in the GUI 
            if (dataGrid_User_Edit.SelectedItem != null)
            {
                if (((TextBlock) dataGrid_User_Edit.Columns[1].GetCellContent(dataGrid_User_Edit.SelectedItem)).Text == "Administrator") //If the selected row is an administrator
                {
                    {
                        tempAdministrator.User_ID =
                            int.Parse(
                                ((TextBlock)
                                    dataGrid_User_Edit.Columns[0].GetCellContent(
                                        dataGrid_User_Edit.SelectedItem))
                                .Text);
                        tempAdministrator = DBReadLogic.GetAdminInfo(tempAdministrator);

                        textBox_User_Edit_Firstname.Text = tempAdministrator.FirstName;
                        textBox_User_Edit_Lastname.Text = tempAdministrator.LastName;
                        textBox_User_Edit_Phone.Text = tempAdministrator.WorkPhone.ToString();
                        textBox_User_Edit_Email.Text = tempAdministrator.WorkEmail;
                        textBox_User_Edit_Fax.Text = tempAdministrator.WorkFax.ToString();

                        rdbtn_User_Edit_Admin.IsChecked = true;
                        rdbtn_User_Edit_Actor.IsChecked = false;
                        textBox_User_Edit_CompanyName.Visibility = Visibility.Collapsed;
                    }
                }

                if (((TextBlock) dataGrid_User_Edit.Columns[1].GetCellContent(dataGrid_User_Edit.SelectedItem)).Text == "Aktør") //If the selected row is an actor
                {
                    {
                        tempActor.User_ID =
                            int.Parse(
                                ((TextBlock)
                                    dataGrid_User_Edit.Columns[0].GetCellContent(
                                        dataGrid_User_Edit.SelectedItem))
                                .Text);
                        tempActor = DBReadLogic.GetActorInfo(tempActor);

                        textBox_User_Edit_CompanyName.Text = tempActor.CompanyName;
                        textBox_User_Edit_Firstname.Text = tempActor.FirstName;
                        textBox_User_Edit_Lastname.Text = tempActor.LastName;
                        textBox_User_Edit_Phone.Text = tempActor.WorkPhone.ToString();
                        textBox_User_Edit_Email.Text = tempActor.WorkEmail;
                        textBox_User_Edit_Fax.Text = tempActor.WorkFax.ToString();

                        rdbtn_User_Edit_Admin.IsChecked = false;
                        rdbtn_User_Edit_Actor.IsChecked = true;
                        textBox_User_Edit_CompanyName.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void Button_User_Edit_Edit_OnClick(object sender, RoutedEventArgs e)
        {
            int tempInt;

            if (rdbtn_User_Edit_Actor.IsChecked == true)
            {
                tempActor.CompanyName = textBox_User_Edit_CompanyName.Text;
                tempActor.FirstName = textBox_User_Edit_Firstname.Text;
                tempActor.LastName = textBox_User_Edit_Lastname.Text;

                if (int.TryParse(textBox_User_Edit_Phone.Text, out tempInt) && textBox_User_Edit_Phone.Text.Length == 8)
                {
                    tempActor.WorkPhone = tempInt;
                }

                tempActor.WorkEmail = textBox_User_Edit_Email.Text;

                if (int.TryParse(textBox_User_Edit_Fax.Text, out tempInt) && textBox_User_Edit_Fax.Text.Length == 8)
                {
                    tempActor.WorkFax = tempInt;
                }

                DBUpdateLogic.UpdateActor(tempActor);
            }

            if (rdbtn_User_Edit_Admin.IsChecked == true)
            {
                tempAdministrator.FirstName = textBox_User_Edit_Firstname.Text;
                tempAdministrator.LastName = textBox_User_Edit_Lastname.Text;

                if (int.TryParse(textBox_User_Edit_Phone.Text, out tempInt) && textBox_User_Edit_Phone.Text.Length == 8)
                {
                    tempAdministrator.WorkPhone = tempInt;
                }

                tempAdministrator.WorkEmail = textBox_User_Edit_Email.Text;
                if (int.TryParse(textBox_User_Edit_Fax.Text, out tempInt) && textBox_User_Edit_Fax.Text.Length == 8)
                {
                    tempAdministrator.WorkFax = tempInt;
                }

                DBUpdateLogic.UpdateAdmin(tempAdministrator);
            }

            DBReadLogic.FillUserTable(tempUser.UserTable);
            MainWindow.FillComboBoxWithAdminsAndActors();
        }

        private void Button_User_Edit_Delete_OnClick(object sender, RoutedEventArgs e)
        {
            if (
                int.Parse(
                    ((TextBlock) dataGrid_User_Edit.Columns[1].GetCellContent(dataGrid_User_Edit.SelectedItem)).Text) ==
                1)
            {
                DBDeleteLogic.DeleteAdmin(tempAdministrator);
            } //If the selected row is an administrator

            if (
                int.Parse(
                    ((TextBlock) dataGrid_User_Edit.Columns[1].GetCellContent(dataGrid_User_Edit.SelectedItem)).Text) ==
                2)
            {
                DBDeleteLogic.DeleteActor(tempActor);
            } //If the selected row is an actor

            DBReadLogic.FillUserTable(tempUser.UserTable);
            MainWindow.FillComboBoxWithAdminsAndActors();
        }
    }
}
