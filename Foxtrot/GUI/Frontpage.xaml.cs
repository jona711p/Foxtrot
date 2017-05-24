using Foxtrot.Classes;
using Foxtrot.Classes.DB;
using System.Windows.Controls;

namespace Foxtrot.GUI
{
    /// <summary>
    /// Interaction logic for Frontpage.xaml
    /// </summary>
    public partial class Frontpage : Page
    {
        private static Actor tempActor = new Actor();
        private  static Administrator tempAdmin = new Administrator();
        public Frontpage(Classes.User inputUser)
        {
            string User;
         
            tempActor.UserID = inputUser.ID;
            tempAdmin.UserID = inputUser.ID;
            

            if (tempAdmin.UserID != null && tempAdmin.Permission == 1)
            {
                DBReadLogic.GetAdminInfo(tempAdmin);
                User = tempAdmin.FirstName + "" + tempAdmin.LastName;
                tempActor.UserID = null;
            }

            if (tempActor.UserID != null && tempActor.Permission == 2)
            {
                DBReadLogic.GetActorInfo(tempActor);
                User = tempActor.CompanyName;
                tempAdmin.UserID = null;
            }

            InitializeComponent();
            DataContext = this;
        }
    }
}
