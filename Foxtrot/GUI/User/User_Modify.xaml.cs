using System.Windows;
using System.Windows.Controls;
using Classes;

namespace Foxtrot.GUI.User
{
    /// <summary>
    /// Interaction logic for User_Modify.xaml
    /// </summary>
    public partial class User_Modify : Page
    {
        private static Classes.Actor actor = new Actor();
        public User_Modify(Classes.User user)
        {
            actor.User_ID = user.ID;

            DBReadLogic.GetActorInfo(actor);

            InitializeComponent();
            DataContext = actor;
        }
        private void Btn_Modify_CreateUser_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
