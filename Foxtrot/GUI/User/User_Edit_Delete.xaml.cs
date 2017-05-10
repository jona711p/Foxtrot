using System;
using System.Collections.Generic;
using System.Data;
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
using Classes;

namespace Foxtrot.GUI.User
{
    /// <summary>
    /// Interaction logic for User_Edit.xaml
    /// </summary>
    public partial class User_Edit_Delete : Page
    {
        private Classes.User tempUser = new Classes.User();
        public User_Edit_Delete()
        {
            InitializeComponent();
            DataContext = tempUser;

            tempUser.UserTable = new DataTable();

            DBReadLogic.FillUserTable(tempUser.UserTable);

            DataContext = tempUser;
        }

        private void Rdbtn_User_Edit_Admin_OnClick(object sender, RoutedEventArgs e)
        {
            textBox_User_Edit_CompanyName.Visibility = Visibility.Collapsed;
        }

        private void Rdbtn_User_Edit_Actor_OnClick(object sender, RoutedEventArgs e)
        {
            textBox_User_Edit_CompanyName.Visibility = Visibility.Visible;
        }
    }
}
