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
        private User user;
        private int globalPermission;
        public MainWindow()
        {
            XMLLogic.WatchXMLDir();

            ResizeMode = ResizeMode.NoResize;

            user = new User();

            user.AdminActorDictionary = new Dictionary<string, int>();

            DBReadLogic.FillAdminActorDictionary(user.AdminActorDictionary);

            InitializeComponent();

            MainFrame.Content = new Frontpage();

            textBox_LeftFooter.Text = "Skiveegnens Erhvervs - og Turistcenter" +
                "\nØsterbro 7, 7800 Skive" +
                "\nTlf: +45 9614 7677 | info@skiveet.dk";
            textBox_RightFooter.Text = "Åbningstider:" +
                "\nMandag – Torsdag kl. 09.00 - 15.00" +
                "\nFredag kl. 09.00 - 14.00";

            DataContext = user;
        }

        private void ComboBox_Main_Usertype_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            globalPermission = ((KeyValuePair<string, int>)comboBox_Main_Usertype.SelectedItem).Value;

            if (globalPermission == 0)
            {
                HideAll();
            }

            if (globalPermission == 1)
            {
                User_MenuItem.Visibility = Visibility.Visible;
                Product_MenuItem.Visibility = Visibility.Visible;
                CombiProduct_MenuItem.Visibility = Visibility.Visible;
                Event_MenuItem.Visibility = Visibility.Visible;
            }

            if (globalPermission == 2)
            {
                User_MenuItem.Visibility = Visibility.Hidden;
                Product_MenuItem.Visibility = Visibility.Visible;
                CombiProduct_MenuItem.Visibility = Visibility.Visible;
                Event_MenuItem.Visibility = Visibility.Visible;
            }

            user.AdminActorDictionary.Clear();

            DBReadLogic.FillAdminActorDictionary(user.AdminActorDictionary);
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
            MainFrame.Content = new Product_Add();
        }

        private void MenuItem_Product_Edit_Delete_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Product_Edit_Delete();
        }
    }
}
