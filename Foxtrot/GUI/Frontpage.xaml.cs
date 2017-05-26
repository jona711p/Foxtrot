using System.Drawing.Text;
using Foxtrot.Classes;
using Foxtrot.Classes.DB;
using System.Windows.Controls;

namespace Foxtrot.GUI
{
    /// <summary>
    /// Mikael Paaske
    /// </summary>
    public partial class Frontpage : Page
    {
        private static Actor tempActor = new Actor();
        private  static Administrator tempAdmin = new Administrator();
        private static Classes.User tempUser = new Classes.User();
        public string User { get; set; }
        public Frontpage(Classes.User inputUser)
        {

            if (inputUser.Permission == 1)
            {
                tempAdmin.UserID = inputUser.ID;
            }
            if (inputUser.Permission == 2)
            {
                tempActor.UserID = inputUser.ID;
            }            
            

            if (tempAdmin.UserID != null && inputUser.Permission == 1)
            {
                DBReadLogic.GetAdminInfo(tempAdmin);
                User = tempAdmin.FirstName + " " + tempAdmin.LastName;
                tempActor.UserID = null;
            }

            if (tempActor.UserID != null && inputUser.Permission == 2)
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
