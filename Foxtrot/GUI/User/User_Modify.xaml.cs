﻿using System.Windows;
using System.Windows.Controls;
using Foxtrot.Classes;
using Foxtrot.Classes.DB;

namespace Foxtrot.GUI.User
{
    /// <summary>
    /// Jonas Lykke
    /// </summary>
    // Edit a actor in the database
    public partial class User_Modify : Page
    {
        private static Actor tempActor = new Actor();
        public User_Modify(Classes.User inputUser)
        {
            tempActor.UserID = inputUser.ID;
            
            DBReadLogic.GetActorInfo(tempActor);

            InitializeComponent();
            DataContext = tempActor;
        }
        private void Btn_ModifyUser_OnClick(object sender, RoutedEventArgs e)
        {
            // Edit your actor
            tempActor.CompanyName = GUISortingLogic.Name(txtbox_Modify_CompanyName);

            if (tempActor.CompanyName == null)
            {
                GUISortingLogic.Message("Du SKAL indtast et FIRMANAVN!");
                return;
            }

            tempActor.FirstName = GUISortingLogic.Name(txtbox_Modify_FirstName);

            if (tempActor.FirstName == null)
            {
                GUISortingLogic.Message("Du SKAL indtast et FORNAVN!");
                return;
            }

            tempActor.LastName = GUISortingLogic.Name(txtbox_Modify_LastName);

            if (tempActor.LastName == null)
            {
                GUISortingLogic.Message("Du SKAL indtast et EFTERNAVN!");
                return;
            }

            tempActor.WorkPhone = GUISortingLogic.Number(txtbox_Modify_WorkPhone);

            if (tempActor.WorkPhone == null)
            {
                GUISortingLogic.Message("Du SKAL indtast et 8-CIFRET TELEFON NR.!");
                return;
            }

            tempActor.WorkEmail = GUISortingLogic.Email(txtbox_Modify_WorkEmail);

            if (tempActor.WorkEmail == null)
            {
                GUISortingLogic.Message("Du SKAL indtast en gyldig EMAIL ADRESSE!");
                return;
            }

            tempActor.WorkFax = GUISortingLogic.Number(txtbox_Modify_WorkFax);

            if (txtbox_Modify_WorkFax.Text.Length != 0)
            {
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

            MessageBoxResult response = MessageBox.Show("Er du Sikker på du vil Ændre dine Brugeroplysninger?", "Ændre?",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (response == MessageBoxResult.Yes)
            {
                DBUpdateLogic.UpdateActor(tempActor);

                GUISortingLogic.Message("Dine Brugeroplysninger er blevet Ændret!");
            }

            MainWindow.FillComboBoxWithAdminsAndActors();
        }
    }
}
