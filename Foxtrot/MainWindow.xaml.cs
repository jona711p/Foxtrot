using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Classes;
using Foxtrot.GUI;
using Foxtrot.GUI.Product;
using Foxtrot.GUI.User;

namespace Foxtrot
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ResizeMode = ResizeMode.NoResize;

            XMLLogic.WatchXMLDir();
            InitializeComponent();
            User tempUser = new User();

            // Fills a list with actors and admins 
            tempUser.UserList = DBReadLogic.FillActorList(tempUser);
            tempUser.UserList = DBReadLogic.FillAdminList(tempUser);
            comboBox_Main_Usertype.ItemsSource = tempUser.UserList; //skal kun opdateres hvis 'button_User_Add_CreateUser' bliver trykket

            //Smider tekst 'footer'
            textBox.Text = @"Skiveegnens Erhvervs - og Turistcenter Østerbro 7, 7800 Skive
Tlf: +45 9614 7677 | info@skiveet.dk
Åbningstider:
Mandag – torsdag kl. 09.00 - 15.00
Fredag kl. 09.00 - 14.00
Turistinformation læs her";

        }
        private void MenuItem_Menu_Frontpage_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("FORSIDE!");
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
