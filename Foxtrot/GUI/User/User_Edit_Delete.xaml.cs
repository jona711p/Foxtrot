﻿using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Foxtrot.Classes;
using Foxtrot.Classes.DB;
using System.Collections.Generic;

namespace Foxtrot.GUI.User
{
    /// <summary>
    /// Interaction logic for User_Edit.xaml
    /// </summary>
    public partial class User_Edit_Delete : Page
    {
        private Classes.User tempUser = new Classes.User();
        private Actor tempActor = new Actor();
        private Administrator tempAdministrator = new Administrator();
        private int availibility;

        public User_Edit_Delete(Classes.User inputUser)
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

       

        private void Button_User_Edit_Delete_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DataGrid_User_Edit_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Runs when the user selects any item on the datagrid
            //Checks the selected users permission/usertype and and ID then runs the corresponding method to retrieves all information about it from the database
            //the new information is stored in the object 'tempAdministrator' or 'tempAdctor' depending on the usertype and displayed in the relavant inputfields in the GUI 
            if (dataGrid_User_Edit.SelectedItem != null)
            {
                if (int.Parse(((TextBlock)dataGrid_User_Edit.Columns[7].GetCellContent(dataGrid_User_Edit.SelectedItem)).Text) == 1) //If the selected row is an administrator
                {
                    {
                        tempAdministrator.ID =
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
                if (int.Parse(((TextBlock)dataGrid_User_Edit.Columns[7].GetCellContent(dataGrid_User_Edit.SelectedItem)).Text) == 2) //If the selected row is an actor
                {
                    {
                        tempActor.ID =
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
            int tempint;

            if (tempActor.Permission == 2)
            {
                tempActor.CompanyName = textBox_User_Edit_CompanyName.Text;
                tempActor.FirstName = textBox_User_Edit_Firstname.Text;
                tempActor.LastName = textBox_User_Edit_Lastname.Text;

                if (int.TryParse(textBox_User_Edit_Phone.Text, out tempint) &&
                textBox_User_Edit_Phone.Text.Length == 8)
                {
                    tempActor.WorkPhone = tempint; 
                };
                tempActor.WorkEmail = textBox_User_Edit_Email.Text;
                if (int.TryParse(textBox_User_Edit_Fax.Text, out tempint) && 
                textBox_User_Edit_Fax.Text.Length == 8)
            {
                tempActor.WorkFax = tempint;
            }
            if (rdbtn_User_Edit_Actor.IsEnabled == true)
            {
                tempActor.Permission = availibility;
            }

            if (tempActor.Permission == 1)
            {
                tempAdministrator.FirstName = textBox_User_Edit_Firstname.Text;
                tempAdministrator.LastName = textBox_User_Edit_Lastname.Text;
                if (int.TryParse(textBox_User_Edit_Phone.Text, out tempint) &&
                textBox_User_Edit_Phone.Text.Length == 8)
                {
                    tempAdministrator.WorkPhone = tempint;
                };
                tempAdministrator.WorkEmail = textBox_User_Edit_Email.Text;
                if (int.TryParse(textBox_User_Edit_Fax.Text, out tempint) &&
                        textBox_User_Edit_Fax.Text.Length == 8)
                    {
                        tempAdministrator.WorkFax = tempint;
                    }
                    if (rdbtn_User_Edit_Actor.IsChecked == true)
                    {
                        tempAdministrator.Permission = availibility;
                    }
                    

                }

        }
            
        }
    }
}
