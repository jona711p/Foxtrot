﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Foxtrot.Classes;
using Foxtrot.Classes.DB;
using Foxtrot.GUI;
using Foxtrot.GUI.Product;
using Foxtrot.GUI.User;
using Foxtrot.GUI.XMLImport;

namespace Foxtrot
{
    public partial class MainWindow : Window
    {
        private static XMLImport xmlImport = new XMLImport();
        private static User tempUser = new User();
        public MainWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize; //locks the window to its inital size (1350x735) and disables the ability to minimize
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

        public static void FillComboBoxWithAdminsAndActors()
        {
            tempUser.AdminActorObservableCollection = DBReadLogic.FillAdminActorObservableCollection(tempUser.AdminActorObservableCollection);
        }

        private void ComboBox_Main_Usertype_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
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

        void HideAll()
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
            MainFrame.Content = new User_Edit_Delete(tempUser);
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
            MainFrame.Content = new Product_Edit_Delete();
        }
    }
}
