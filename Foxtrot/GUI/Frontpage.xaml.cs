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
        public Frontpage(Classes.User inputUser)
        {
            tempActor.UserID = inputUser.ID;

            DBReadLogic.GetActorInfo(tempActor);

            InitializeComponent();
            DataContext = this;
        }
    }
}
