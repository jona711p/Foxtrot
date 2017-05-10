using System.Windows;
using System.Windows.Controls;
using Classes;

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

        private void btnCreateUser_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (rbtn_Administrator.IsChecked == true)
            {
                Administrator();
            }

            else if (rbtn_Actor.IsChecked == true)
            {
                Actor();
            }

            else
            {
                Message("Vælg enten en Administrator eller en Aktør!");
            }
        }

        void Administrator()
        {
            Administrator administrator = new Administrator();

            administrator.FirstName = Name(txtbox_FirstName);

            if (administrator.FirstName == null)
            {
                Message("Du SKAL indtast et FORNAVN!");
                return;
            }

            administrator.LastName = Name(txtbox_LastName);

            if (administrator.LastName == null)
            {
                Message("Du SKAL indtast et EFTERNAVN!");
                return;
            }

            administrator.WorkPhone = Number(txtbox_WorkPhone);

            if (administrator.WorkPhone == null)
            {
                Message("Du SKAL indtast et 8-CIFRET TELEFON NR.!");
                return;
            }

            administrator.WorkEmail = Email(txtbox_WorkEmail);

            if (administrator.WorkEmail == null)
            {
                Message("Du SKAL indtast en gyldig EMAIL ADRESSE!");
                return;
            }

            if (txtbox_WorkFax.Text.Length != 0)
            {
                administrator.WorkFax = Number(txtbox_WorkFax);

                if (administrator.WorkEmail == null)
                {
                    Message("Du SKAL indtast et 8-CIFRET TELEFON NR.!");
                    return;
                }
            }

            else
            {
                administrator.WorkFax = null;
            }

            DBWriteLogic.WriteAdministrators(administrator);

            Message("En Administrator med navnet: '" + administrator.FirstName + " " +
                            administrator.LastName + "' er blevet oprettet i systemet");
        }

        void Actor()
        {
            Actor actor = new Actor();

            actor.FirstName = Name(txtbox_FirstName);

            if (actor.FirstName == null)
            {
                Message("Du SKAL indtast et FORNAVN!");
                return;
            }

            actor.LastName = Name(txtbox_LastName);

            if (actor.LastName == null)
            {
                Message("Du SKAL indtast et EFTERNAVN!");
                return;
            }

            actor.WorkPhone = Number(txtbox_WorkPhone);

            if (actor.WorkPhone == null)
            {
                Message("Du SKAL indtast et 8-CIFRET TELEFON NR.!");
                return;
            }

            actor.WorkEmail = Email(txtbox_WorkEmail);

            if (actor.WorkEmail == null)
            {
                Message("Du SKAL indtast en gyldig EMAIL ADRESSE!");
                return;
            }

            if (txtbox_WorkFax.Text.Length != 0)
            {
                actor.WorkFax = Number(txtbox_WorkFax);

                if (actor.WorkEmail == null)
                {
                    Message("Du SKAL indtast et 8-CIFRET TELEFON NR.!");
                    return;
                }
            }

            else
            {
                actor.WorkFax = null;
            }

            actor.CompanyName = Name(txtbox_CompanyName);

            if (actor.CompanyName == null)
            {
                Message("Du SKAL indtast et FIRMANAVN!");
                return;
            }

            DBWriteLogic.WriteActors(actor);

            Message("En Aktør med firmanavnet: '" + actor.CompanyName + "' er blevet oprettet i systemet");
        }

        static void Message(string message)
        {
            MessageBox.Show(message);
        }

        static string Name(TextBox name)
        {
            if (name.Text.Length != 0)
            {
                return name.Text;
            }

            return null;
        }

        static int? Number(TextBox number)
        {
            int tempInt;

            if (int.TryParse(number.Text, out tempInt) && number.Text.Length == 8)
            {
                return tempInt;
            }

            return null;
        }

        static string Email(TextBox email)
        {
            if (email.Text.Length != 0 && email.Text.Contains("@"))
            {
                return email.Text;
            }

            return null;
        }
    }
}
