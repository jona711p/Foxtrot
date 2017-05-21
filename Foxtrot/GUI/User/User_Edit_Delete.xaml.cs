using System.Data;
using System.Windows;
using System.Windows.Controls;
using Foxtrot.Classes;
using Foxtrot.Classes.DB;

namespace Foxtrot.GUI.User
{
    /// <summary>
    /// Jonas Lykke & Thomas Nielsen
    /// </summary>
    public partial class User_Edit_Delete : Page
    {
        private Classes.User tempUser = new Classes.User();
        private Administrator tempAdministrator = new Administrator();
        private Actor tempActor = new Actor();

        public User_Edit_Delete()
        {
            tempUser.UserTable = new DataTable();
            DBReadLogic.FillUserTable(tempUser.UserTable);

            InitializeComponent();
            
            DataContext = tempUser;
        }

        private void DataGrid_User_Edit_OnSelectionChanged(object sender, SelectionChangedEventArgs e) //griddet bliver tømt efter man vælger en (mulighedvis pga. datacontexten bliver ændret)
        {
            //Runs when the user selects any item on the datagrid
            //Checks the selected users permission/usertype and and ID then runs the corresponding method to retrieves all information about it from the database
            //the new information is stored in the object 'tempAdministrator' or 'tempAdctor' depending on the usertype and displayed in the relavant inputfields in the GUI 
            if (dataGrid_User_Edit.SelectedItem != null)
            {
                if (((TextBlock) dataGrid_User_Edit.Columns[1].GetCellContent(dataGrid_User_Edit.SelectedItem)).Text == "Administrator") //If the selected row is an administrator
                {
                    {
                        tempAdministrator.UserID =
                            int.Parse(
                                ((TextBlock)
                                    dataGrid_User_Edit.Columns[0].GetCellContent(
                                        dataGrid_User_Edit.SelectedItem))
                                .Text);

                        tempAdministrator = DBReadLogic.GetAdminInfo(tempAdministrator);

                        textBox_User_Edit_CompanyName.Visibility = Visibility.Collapsed;

                        DataContext = tempAdministrator;
                    }
                }

                if (((TextBlock) dataGrid_User_Edit.Columns[1].GetCellContent(dataGrid_User_Edit.SelectedItem)).Text == "Aktør") //If the selected row is an actor
                {
                    {
                        tempActor.UserID =
                            int.Parse(
                                ((TextBlock)
                                    dataGrid_User_Edit.Columns[0].GetCellContent(
                                        dataGrid_User_Edit.SelectedItem))
                                .Text);

                        tempActor = DBReadLogic.GetActorInfo(tempActor);

                        textBox_User_Edit_CompanyName.Visibility = Visibility.Visible;

                        DataContext = tempActor;
                    }
                }
            }
        }

        private void Button_User_Edit_Edit_OnClick(object sender, RoutedEventArgs e)
        {
            int tempInt;

            if (((TextBlock) dataGrid_User_Edit.Columns[1].GetCellContent(dataGrid_User_Edit.SelectedItem)).Text == "Administrator")
            {
                tempAdministrator.FirstName = GUISortingLogic.Name(textBox_User_Edit_FirstName);

                if (tempAdministrator.FirstName == null)
                {
                    GUISortingLogic.Message("Du SKAL indtast et FORNAVN!");
                    return;
                }

                tempAdministrator.LastName = GUISortingLogic.Name(textBox_User_Edit_LastName);

                if (tempAdministrator.LastName == null)
                {
                    GUISortingLogic.Message("Du SKAL indtast et EFTERNAVN!");
                    return;
                }

                tempAdministrator.WorkPhone = GUISortingLogic.Number(textBox_User_Edit_Phone);

                if (tempAdministrator.WorkPhone == null)
                {
                    GUISortingLogic.Message("Du SKAL indtast et 8-CIFRET TELEFON NR.!");
                    return;
                }

                tempAdministrator.WorkEmail = GUISortingLogic.Email(textBox_User_Edit_Email);

                if (tempAdministrator.WorkEmail == null)
                {
                    GUISortingLogic.Message("Du SKAL indtast en gyldig EMAIL ADRESSE!");
                    return;
                }

                if (textBox_User_Edit_Fax.Text.Length != 0)
                {
                    tempAdministrator.WorkFax = GUISortingLogic.Number(textBox_User_Edit_Fax);

                    if (tempAdministrator.WorkEmail == null)
                    {
                        GUISortingLogic.Message("Du SKAL indtast et 8-CIFRET TELEFON NR.!");
                        return;
                    }
                }

                else
                {
                    tempAdministrator.WorkFax = null;
                }

                DBUpdateLogic.UpdateAdmin(tempAdministrator);

                GUISortingLogic.Message("Brugeroplysninger til: " + tempAdministrator.FirstName + " " + tempAdministrator.LastName + " er blevet Ændret!");
            }

            if (((TextBlock) dataGrid_User_Edit.Columns[1].GetCellContent(dataGrid_User_Edit.SelectedItem)).Text == "Aktør")
            {
                tempActor.FirstName = GUISortingLogic.Name(textBox_User_Edit_FirstName);

                if (tempActor.FirstName == null)
                {
                    GUISortingLogic.Message("Du SKAL indtast et FORNAVN!");
                    return;
                }

                tempActor.LastName = GUISortingLogic.Name(textBox_User_Edit_LastName);

                if (tempActor.LastName == null)
                {
                    GUISortingLogic.Message("Du SKAL indtast et EFTERNAVN!");
                    return;
                }

                tempActor.WorkPhone = GUISortingLogic.Number(textBox_User_Edit_Phone);

                if (tempActor.WorkPhone == null)
                {
                    GUISortingLogic.Message("Du SKAL indtast et 8-CIFRET TELEFON NR.!");
                    return;
                }

                tempActor.WorkEmail = GUISortingLogic.Email(textBox_User_Edit_Email);

                if (tempActor.WorkEmail == null)
                {
                    GUISortingLogic.Message("Du SKAL indtast en gyldig EMAIL ADRESSE!");
                    return;
                }

                if (textBox_User_Edit_Fax.Text.Length != 0)
                {
                    tempActor.WorkFax = GUISortingLogic.Number(textBox_User_Edit_Fax);

                    if (tempActor.WorkEmail == null)
                    {
                        GUISortingLogic.Message("Du SKAL indtast et 8-CIFRET TELEFON NR.!");
                        return;
                    }
                }

                else
                {
                    tempActor.WorkFax = null;
                }

                tempActor.CompanyName = GUISortingLogic.Name(textBox_User_Edit_CompanyName);

                if (tempActor.CompanyName == null)
                {
                    GUISortingLogic.Message("Du SKAL indtast et FIRMANAVN!");
                    return;
                }

                DBUpdateLogic.UpdateActor(tempActor);

                GUISortingLogic.Message("Brugeroplysninger til: " + tempActor.CompanyName + " er blevet Ændret!");
            }

            DBReadLogic.FillUserTable(tempUser.UserTable);

            MainWindow.FillComboBoxWithAdminsAndActors();
        }

        private void Button_User_Edit_Delete_OnClick(object sender, RoutedEventArgs e)
        {
            if (((TextBlock) dataGrid_User_Edit.Columns[1].GetCellContent(dataGrid_User_Edit.SelectedItem)).Text == "Administrator")
            {
                DBDeleteLogic.DeleteAdmin(tempAdministrator);

                GUISortingLogic.Message(tempAdministrator.FirstName + " " + tempAdministrator.LastName + " er blevet Slettet!");
            } //If the selected row is an administrator

            if (((TextBlock)dataGrid_User_Edit.Columns[1].GetCellContent(dataGrid_User_Edit.SelectedItem)).Text == "Aktør")
            {
                DBDeleteLogic.DeleteActor(tempActor);

                GUISortingLogic.Message(tempActor.CompanyName + " er blevet Slettet!");
            } //If the selected row is an actor

            DBReadLogic.FillUserTable(tempUser.UserTable);

            MainWindow.FillComboBoxWithAdminsAndActors();
        }
    }
}
