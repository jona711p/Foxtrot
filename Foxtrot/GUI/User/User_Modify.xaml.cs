using System.Windows;
using System.Windows.Controls;
using Classes;

namespace Foxtrot.GUI.User
{
    /// <summary>
    /// Jonas Lykke
    /// </summary>
    public partial class User_Modify : Page
    {
        private static Actor tempActor = new Actor();
        public User_Modify(global::Classes.User inputUser)
        {
            tempActor.User_ID = inputUser.ID;
            tempActor.ID = DBReadLogic.GetIDFromUser("Actors", inputUser.ID.Value);

            DBReadLogic.GetActorInfo(tempActor);

            InitializeComponent();
            DataContext = tempActor;
        }
        private void Btn_ModifyUser_OnClick(object sender, RoutedEventArgs e)
        {
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

            DBUpdateLogic.UpdateActors(tempActor);

            GUISortingLogic.Message("Informationerne er blevet ændret til:" +
                                    "\nFirmanavn: '" + tempActor.CompanyName + "'" +
                                    "\nFornavn: '" + tempActor.FirstName + "'" +
                                    "\nEfternavn: '" + tempActor.LastName + "'" +
                                    "\nArbejds Telefon: '" + tempActor.WorkPhone + "'" +
                                    "\nArbejds Email: '" + tempActor.WorkEmail + "'" +
                                    "\nArbejds Fax: '" + tempActor.WorkFax + "'");
        }
    }
}
