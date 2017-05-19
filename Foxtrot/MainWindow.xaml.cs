using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using Foxtrot.Classes;
using Foxtrot.Classes.DB;
using Foxtrot.GUI;
using Foxtrot.GUI.Product;
using Foxtrot.GUI.User;
using Foxtrot.GUI.XMLImport;
using Foxtrot.GUI.CombiProduct;
using Foxtrot.GUI.Event;
using System.Diagnostics;
using System.Windows.Navigation;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace Foxtrot
{
    public partial class MainWindow : Window
    {
        XMLImport xmlImport = new XMLImport();
        private static User tempUser = new User();
        public MainWindow()
        {
            ResizeMode = ResizeMode.NoResize; //locks the window to its inital size (1350x735) and disables the ability to minimize
            InitializeComponent();
            this.Closing += WindowClosing;

            HideAll();
            FillComboBoxWithAdminsAndActors();
            MainFrame.Content = new Frontpage();
            DataContext = tempUser;

            textBox_LeftFooter.Text = "Skiveegnens Erhvervs - og Turistcenter" + // skal opdateres med relevant information og hyperlink og sæættes ind i xamlkoden 
                "\nØsterbro 7, 7800 Skive" +
                "\nTlf: +45 9614 7677 | info@skiveet.dk";
            textBox_RightFooter.Text = "Åbningstider:" +
                "\nMandag – Torsdag kl. 09.00 - 15.00" +
                "\nFredag kl. 09.00 - 14.00";
        }

        void WindowClosing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_Menu_Exit_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult response = MessageBox.Show("Er du Sikker på du vil Afslutte?", "Afslut?",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (response == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        public static void FillComboBoxWithAdminsAndActors()
        {
            tempUser.AdminActorList = DBReadLogic.FillAdminActorList(tempUser.AdminActorList);
        }

        private void btn_Login_User_Click(object sender, RoutedEventArgs e)
        {
            tempUser.ID = ((KeyValuePair<int, string>)comboBox_Main_Usertype.SelectedItem).Key;

            tempUser = DBReadLogic.GetUserInfo(tempUser);

            if (tempUser.Permission == 1)
            {
                HideAll();
                XML_Import_MenuItem.IsEnabled = true;
                User_MenuItem.IsEnabled = true;
                User_MenuItem_Add.IsEnabled = true;
                User_MenuItem_Edit_Delete.IsEnabled = true;
                Product_MenuItem.IsEnabled = true;
                CombiProduct_MenuItem.IsEnabled = true;
                Event_MenuItem.IsEnabled = true;
            }

            if (tempUser.Permission == 2)
            {
                HideAll();
                User_MenuItem.IsEnabled = true;
                User_MenuItem_Modify.IsEnabled = true;
                Product_MenuItem.IsEnabled = true;
                CombiProduct_MenuItem.IsEnabled = true;
                Event_MenuItem.IsEnabled = true;
            }

            MainFrame.Content = new Frontpage();
        }

        private void HideAll()
        {
            XML_Import_MenuItem.IsEnabled = false;
            User_MenuItem.IsEnabled = false;
            User_MenuItem_Add.IsEnabled = false;
            User_MenuItem_Edit_Delete.IsEnabled = false;
            User_MenuItem_Modify.IsEnabled = false;
            Product_MenuItem.IsEnabled = false;
            CombiProduct_MenuItem.IsEnabled = false;
            Event_MenuItem.IsEnabled = false;
        }

        private void MenuItem_Menu_Frontpage_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Frontpage();
        }

        private void MenuItem_Menu_XML_Import_OnClick(object sender, RoutedEventArgs e)
        {
            xmlImport.Show();
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
            MainFrame.Content = new User_Modify(tempUser);
        }

        private void MenuItem_Product_Add_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Product_Add(tempUser);
        }

        private void MenuItem_Product_Edit_Delete_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Product_Edit_Delete(tempUser);
        }

        private void MenuItem_CombiProduct_Add_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new CombiProduct_Add(tempUser);
        }

        private void MenuItem_CombiProduct_Edit_Delete_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new CombiProduct_Edit_Delete();
        }

        private void MenuItem_Event_Add_Onclick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Event_Add(tempUser);
        }

        private void MenuItem_Event_Edit_Delete_Onclick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Event_Edit_Delete(tempUser);
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
