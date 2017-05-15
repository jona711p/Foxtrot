using System.Windows;
using System.Windows.Controls;
using Foxtrot.Classes;
using Foxtrot.Classes.DB;

namespace Foxtrot.GUI.User
{
    /// <summary>
    /// Jonas Lykke
    /// </summary>
    public partial class User_Add : Page
    {
        public User_Add()
        {
            InitializeComponent();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            if (rbtn_Administrator.IsChecked == false && rbtn_Actor.IsChecked == false)
            {
                GUISortingLogic.Message("Vælg enten en Administrator eller en Aktør!");
                return;
            }
            
            if (rbtn_Administrator.IsChecked == true)
            {
                Administrator();
            }

            if (rbtn_Actor.IsChecked == true)
            {
                Actor();
            }

            MainWindow.FillComboBoxWithAdminsAndActors();
        }

        void Administrator()
        {
            Administrator tempAdministrator = new Administrator();

            tempAdministrator.FirstName = GUISortingLogic.Name(txtbox_FirstName);

            if (tempAdministrator.FirstName == null)
            {
                GUISortingLogic.Message("Du SKAL indtast et FORNAVN!");
                return;
            }

            tempAdministrator.LastName = GUISortingLogic.Name(txtbox_LastName);

            if (tempAdministrator.LastName == null)
            {
                GUISortingLogic.Message("Du SKAL indtast et EFTERNAVN!");
                return;
            }

            tempAdministrator.WorkPhone = GUISortingLogic.Number(txtbox_WorkPhone);

            if (tempAdministrator.WorkPhone == null)
            {
                GUISortingLogic.Message("Du SKAL indtast et 8-CIFRET TELEFON NR.!");
                return;
            }

            tempAdministrator.WorkEmail = GUISortingLogic.Email(txtbox_WorkEmail);

            if (tempAdministrator.WorkEmail == null)
            {
                GUISortingLogic.Message("Du SKAL indtast en gyldig EMAIL ADRESSE!");
                return;
            }

            if (txtbox_WorkFax.Text.Length != 0)
            {
                tempAdministrator.WorkFax = GUISortingLogic.Number(txtbox_WorkFax);

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

            bool dupe = DBReadLogic.DupeCheckAdmin(tempAdministrator);

            if (!dupe)
            {
                
                XMLDBWriteLogic.WriteAdministrators(tempAdministrator);

                GUISortingLogic.Message("En Administrator med navnet: '" + tempAdministrator.FirstName + " " +
                        tempAdministrator.LastName + "' er blevet oprettet i systemet!");
            }

            else
            {
                GUISortingLogic.Message("Der findes allerede en Administrator med navnet: '" + tempAdministrator.FirstName + " " +
                            tempAdministrator.LastName + "' i systemet!");
            }
        }

        void Actor()
        {
            Actor tempActor = new Actor();

            tempActor.FirstName = GUISortingLogic.Name(txtbox_FirstName);

            if (tempActor.FirstName == null)
            {
                GUISortingLogic.Message("Du SKAL indtast et FORNAVN!");
                return;
            }

            tempActor.LastName = GUISortingLogic.Name(txtbox_LastName);

            if (tempActor.LastName == null)
            {
                GUISortingLogic.Message("Du SKAL indtast et EFTERNAVN!");
                return;
            }

            tempActor.WorkPhone = GUISortingLogic.Number(txtbox_WorkPhone);

            if (tempActor.WorkPhone == null)
            {
                GUISortingLogic.Message("Du SKAL indtast et 8-CIFRET TELEFON NR.!");
                return;
            }

            tempActor.WorkEmail = GUISortingLogic.Email(txtbox_WorkEmail);

            if (tempActor.WorkEmail == null)
            {
                GUISortingLogic.Message("Du SKAL indtast en gyldig EMAIL ADRESSE!");
                return;
            }

            if (txtbox_WorkFax.Text.Length != 0)
            {
                tempActor.WorkFax = GUISortingLogic.Number(txtbox_WorkFax);

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

            tempActor.CompanyName = GUISortingLogic.Name(txtbox_CompanyName);

            if (tempActor.CompanyName == null)
            {
                GUISortingLogic.Message("Du SKAL indtast et FIRMANAVN!");
                return;
            }

            bool dupe = DBReadLogic.DupeCheckActor(tempActor);

            if (!dupe)
            {
                XMLDBWriteLogic.WriteActors(tempActor);

                GUISortingLogic.Message("En Aktør med firmanavnet: '" + tempActor.CompanyName + "' er blevet oprettet i systemet");
            }

            else
            {
                GUISortingLogic.Message("Der findes allerede en Aktør med firmanavnet: '" + tempActor.CompanyName + "' i systemet!");
            }
        }
    }
}
