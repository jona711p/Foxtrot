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
        private static Classes.Actor tempActor = new Actor();
        public User_Modify(Classes.User inputUser)
        {
            tempActor.User_ID = inputUser.ID;

            DBReadLogic.GetActorInfo(tempActor);

            InitializeComponent();
            DataContext = tempActor;
        }
        private void Btn_ModifyUser_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
