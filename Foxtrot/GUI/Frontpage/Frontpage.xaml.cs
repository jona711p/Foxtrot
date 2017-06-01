using System.Windows.Controls;
using Foxtrot.Classes;
using Foxtrot.Classes.DB;

namespace Foxtrot.GUI.Frontpage
{
    /// <summary>
    /// Mikael Paaske
    /// </summary>
    public partial class Frontpage : Page
    {
        private static Administrator tempAdministrator = new Administrator();
        private static Actor tempActor = new Actor();
        public string User { get; set; }

        public Frontpage(Classes.User inputUser)
        {

            if (inputUser.Permission == 1)
            {
                tempAdministrator.UserID = inputUser.ID;
            }
            if (inputUser.Permission == 2)
            {
                tempActor.UserID = inputUser.ID;
            }


            if (tempAdministrator.UserID != null && inputUser.Permission == 1)
            {
                DBReadLogic.GetAdminInfo(tempAdministrator);
                User = tempAdministrator.FirstName + " " + tempAdministrator.LastName;
                tempActor.UserID = null;
            }

            if (tempActor.UserID != null && inputUser.Permission == 2)
            {
                DBReadLogic.GetActorInfo(tempActor);
                User = tempActor.CompanyName;
                tempAdministrator.UserID = null;
            }

            InitializeComponent();
            DataContext = this;
        }
    }
}
