using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Foxtrot.GUI.User
{
    /// <summary>
    /// Interaction logic for User_Modify.xaml
    /// </summary>
    public partial class User_Modify : Page
    {
        private Classes.User tempUser = new Classes.User();
        private Classes.Actor tempActor = new Classes.Actor();
        public User_Modify()
        {
            InitializeComponent();
        }
        private void Btn_Modify_CreateUser_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
