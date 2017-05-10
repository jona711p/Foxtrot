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
        private int permission;
        public MainWindow()
        {
            XMLLogic.WatchXMLDir();

            ResizeMode = ResizeMode.NoResize;

            InitializeComponent();

            FillComboBoxWithAdminsAndActors();

            MainFrame.Content = new Frontpage();

            textBox_LeftFooter.Text = "Skiveegnens Erhvervs - og Turistcenter" +
                "\nØsterbro 7, 7800 Skive" +
                "\nTlf: +45 9614 7677 | info@skiveet.dk";
            textBox_RightFooter.Text = "Åbningstider:" +
                "\nMandag – Torsdag kl. 09.00 - 15.00" +
                "\nFredag kl. 09.00 - 14.00";

            DataContext = user;
        }

        public static void FillComboBoxWithAdminsAndActors()
        {
            user.AdminActorDictionary.Clear();
            DBReadLogic.FillAdminActorDictionary(user.AdminActorDictionary);
        }

        private void ComboBox_Main_Usertype_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            user.ID = ((KeyValuePair<int, string>) comboBox_Main_Usertype.SelectedItem).Key;
            permission = DBReadLogic.GetUserPermission(user.ID.Value);

            if (permission == 0)
            {
                HideAll();
            }

            if (permission == 1)
            {
                HideAll();
                User_MenuItem.Visibility = Visibility.Visible;
                Product_MenuItem.Visibility = Visibility.Visible;
                CombiProduct_MenuItem.Visibility = Visibility.Visible;
                Event_MenuItem.Visibility = Visibility.Visible;
            }

            if (permission == 2)
            {
                HideAll();
                Product_MenuItem.Visibility = Visibility.Visible;
                CombiProduct_MenuItem.Visibility = Visibility.Visible;
                Event_MenuItem.Visibility = Visibility.Visible;
            }

            MainFrame.Content = new Frontpage();
        }

        void HideAll()
        {
            User_MenuItem.Visibility = Visibility.Hidden;
            Product_MenuItem.Visibility = Visibility.Hidden;
            CombiProduct_MenuItem.Visibility = Visibility.Hidden;
            Event_MenuItem.Visibility = Visibility.Hidden;
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
