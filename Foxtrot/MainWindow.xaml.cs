using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Classes;
using Foxtrot.GUI;
using Foxtrot.GUI.Product;
using Foxtrot.GUI.User;

namespace Foxtrot
{
    public partial class MainWindow : Window
    {
        private static User user = new User();
        private int permission = 0;
        public MainWindow()
        {
            XMLLogic.WatchXMLDir();

            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            HideAll();
            FillComboBoxWithAdminsAndActors();
            MainFrame.Content = new Frontpage();
            DataContext = user;

            textBox_LeftFooter.Text = "Skiveegnens Erhvervs - og Turistcenter" +
                "\nØsterbro 7, 7800 Skive" +
                "\nTlf: +45 9614 7677 | info@skiveet.dk";
            textBox_RightFooter.Text = "Åbningstider:" +
                "\nMandag – Torsdag kl. 09.00 - 15.00" +
                "\nFredag kl. 09.00 - 14.00";
        }

        public static void FillComboBoxWithAdminsAndActors()
        {
            user.AdminActorObservableCollection = DBReadLogic.FillAdminActorObservableCollection(user.AdminActorObservableCollection);
        }

        private void ComboBox_Main_Usertype_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            user.ID = ((KeyValuePair<int, string>)comboBox_Main_Usertype.SelectedItem).Key;

            permission = DBReadLogic.GetUserPermission(user.ID.Value);

            if (permission == 1)
            {
                HideAll();
                User_MenuItem.IsEnabled = true;
                User_MenuItem_Add.IsEnabled = true;
                User_MenuItem_Edit_Delete.IsEnabled = true;
                User_MenuItem_Modify.IsEnabled = false;
                Product_MenuItem.IsEnabled = true;
                CombiProduct_MenuItem.IsEnabled = true;
                Event_MenuItem.IsEnabled = true;
            }

            if (permission == 2)
            {
                HideAll();
                User_MenuItem.IsEnabled = true;
                User_MenuItem_Add.IsEnabled = false;
                User_MenuItem_Edit_Delete.IsEnabled = false;
                User_MenuItem_Modify.IsEnabled = true;
                Product_MenuItem.IsEnabled = true;
                CombiProduct_MenuItem.IsEnabled = true;
                Event_MenuItem.IsEnabled = true;
            }

            MainFrame.Content = new Frontpage();
        }

        void HideAll()
        {
            User_MenuItem.IsEnabled = false;
            Product_MenuItem.IsEnabled = false;
            CombiProduct_MenuItem.IsEnabled = false;
            Event_MenuItem.IsEnabled = false;
        }

        private void MenuItem_Menu_Frontpage_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Frontpage();
        }

        private void MenuItem_User_Add_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new User_Add();
        }

        private void MenuItem_User_Edit_Delete_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new User_Edit_Delete();
        }

        private void MenuItem_User_Modify_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new User_Modify(user.ID.Value);
        }

        private void MenuItem_Product_Add_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Product_Add(user.ID.Value);
        }

        private void MenuItem_Product_Edit_Delete_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Product_Edit_Delete();
        }
    }
}
