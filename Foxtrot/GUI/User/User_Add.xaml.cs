using System.Windows;
using System.Windows.Controls;
using Classes;

namespace Foxtrot.GUI.User
{
    /// <summary>
    /// Interaction logic for User_Add.xaml
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
                Administrator administrator = new Administrator();

                int tempInt;

                if (txtbox_FirstName.Text.Length != 0)
                {
                    administrator.FirstName = txtbox_FirstName.Text;
                }

                else
                {
                    MessageBox.Show("Du SKAL indtast et FORNAVN!");
                    return;
                }

                if (txtbox_LastName.Text.Length != 0)
                {
                    administrator.LastName = txtbox_LastName.Text;
                }

                else
                {
                    MessageBox.Show("Du SKAL indtast et EFTERNAVN!");
                    return;
                }

                if (int.TryParse(txtbox_WorkPhone.Text, out tempInt) && txtbox_WorkPhone.Text.Length == 8)
                {
                    administrator.WorkPhone = tempInt;
                }

                else
                {
                    MessageBox.Show("Du SKAL indtast et 8-CIFRET TELEFON NR.!");
                    return;
                }

                if (txtbox_WorkEmail.Text.Length != 0 && txtbox_WorkEmail.Text.Contains("@"))
                {
                    administrator.WorkEmail = txtbox_WorkEmail.Text;
                }

                else
                {
                    MessageBox.Show("Du SKAL indtast en gyldig EMAIL ADRESSE");
                    return;
                }

                if (txtbox_WorkFax.Text.Length != 0)
                {
                    if (int.TryParse(txtbox_WorkFax.Text, out tempInt) && txtbox_WorkFax.Text.Length == 8)
                    {
                        administrator.WorkFax = tempInt;
                    }

                    else
                    {
                        MessageBox.Show("Du SKAL indtast et 8-CIFRET TELEFON NR.!");
                        return;
                    }
                }

                else
                {
                    administrator.WorkFax = null;
                }

                DBWriteLogic.WriteAdministrators(administrator);

                MessageBox.Show("En Administrator med navnet: '" + administrator.FirstName + " " + administrator.LastName + "' er blevet oprettet i systemet");

                return;
            }

            if (rbtn_Actor.IsChecked == true)
            {
                Actor actor = new Actor();

                int tempInt;

                if (txtbox_FirstName.Text.Length != 0)
                {
                    actor.FirstName = txtbox_FirstName.Text;
                }

                else
                {
                    MessageBox.Show("Du SKAL indtast et FORNAVN!");
                    return;
                }

                if (txtbox_LastName.Text.Length != 0)
                {
                    actor.LastName = txtbox_LastName.Text;
                }

                else
                {
                    MessageBox.Show("Du SKAL indtast et EFTERNAVN!");
                    return;
                }

                if (int.TryParse(txtbox_WorkPhone.Text, out tempInt) && txtbox_WorkPhone.Text.Length == 8)
                {
                    actor.WorkPhone = tempInt;
                }

                else
                {
                    MessageBox.Show("Du SKAL indtast et 8-CIFRET TELEFON NR.!");
                    return;
                }

                if (txtbox_WorkEmail.Text.Length != 0 && txtbox_WorkEmail.Text.Contains("@"))
                {
                    actor.WorkEmail = txtbox_WorkEmail.Text;
                }

                else
                {
                    MessageBox.Show("Du SKAL indtast en gyldig EMAIL ADRESSE");
                    return;
                }

                if (txtbox_WorkFax.Text.Length != 0)
                {
                    if (int.TryParse(txtbox_WorkFax.Text, out tempInt) && txtbox_WorkFax.Text.Length == 8)
                    {
                        actor.WorkFax = tempInt;
                    }

                    else
                    {
                        MessageBox.Show("Du SKAL indtast et 8-CIFRET TELEFON NR.!");
                        return;
                    }
                }

                else
                {
                    actor.WorkFax = null;
                }

                if (txtbox_CompanyName.Text.Length != 0)
                {
                    actor.CompanyName = txtbox_CompanyName.Text;
                }

                else
                {
                    MessageBox.Show("Du SKAL indtast et FIRMANAVN!");
                    return;
                }

                DBWriteLogic.WriteActors(actor);

                MessageBox.Show("En Aktør med firmanavnet: '" + actor.CompanyName + "' er blevet oprettet i systemet");

                return;
            }

            else
            {
                MessageBox.Show("Vælg enten en Administrator eller en Aktør!");
            }
        }
    }
}
