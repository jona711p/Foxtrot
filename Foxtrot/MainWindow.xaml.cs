﻿using System;
using System.Collections.Generic;
using System.Windows;
using Foxtrot.Classes;
using Foxtrot.Classes.DB;
using Foxtrot.GUI;
using Foxtrot.GUI.Product;
using Foxtrot.GUI.User;
using Foxtrot.GUI.XMLImport;
using Foxtrot.GUI.CombiProduct;
using System.Diagnostics;
using Foxtrot.GUI.About;
using Foxtrot.GUI.Frontpage;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace Foxtrot
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Jonas Lykke, Thoms Nielsen og Mikael Paaske
        /// This block of code is used for removeing the "Escape Hatch". Use the "Afslut" in the Menu instead
        /// </summary>
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        XMLImport xmlImport = new XMLImport();
        private static User tempUser = new User();
        public MainWindow()
        {
            Loaded += ToolWindow_Loaded;
            ResizeMode = ResizeMode.NoResize; //locks the window to its inital size (1350x735) and disables the ability to minimize
            InitializeComponent();

            HideAll();
            FillComboBoxWithAdminsAndActors();
            MainFrame.Content = new Frontpage(tempUser);
            DataContext = tempUser;

            // The footer at the bottem of the window. 
            textBox_LeftFooter.Text = "Skiveegnens Erhvervs - og Turistcenter" +
                "\nØsterbro 7, 7800 Skive" +
                "\nTlf: +45 9614 7677 | info@skiveet.dk";
            textBox_RightFooter.Text = "Åbningstider:" +
                "\nMandag – Torsdag kl. 09.00 - 15.00" +
                "\nFredag kl. 09.00 - 14.00";
        }

        /// <summary>
        /// This block of code is used for removeing the "Escape Hatch". Use the "Afslut" in the Menu instead
        /// </summary>
        void ToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
            IntPtr hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
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
            // Fill the combobox in the upperright corner with all the actors and administrators from the database
            tempUser.AdminActorList = DBReadLogic.FillAdminActorList(tempUser.AdminActorList);
        }

        private void btn_Login_User_Click(object sender, RoutedEventArgs e)
        {
            try
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
                }

                if (tempUser.Permission == 2)
                {
                    HideAll();
                    User_MenuItem.IsEnabled = true;
                    User_MenuItem_Modify.IsEnabled = true;
                    Product_MenuItem.IsEnabled = true;
                    CombiProduct_MenuItem.IsEnabled = true;
                }

                MainFrame.Content = new Frontpage(tempUser);
            }

            catch (Exception)
            {
                GUISortingLogic.Message("Vælg Venligst en Bruger!");
            } 
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
        }

        private void MenuItem_Menu_Frontpage_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Frontpage(tempUser);
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
            MainFrame.Content = new CombiProduct_Edit_Delete(tempUser);
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new AboutUs();
        }

        private void Btn_ShowWeb_OnClick(object sender, RoutedEventArgs e)
        {
            // Open a webbrowser with a website
            Process.Start("http://www.skiveet.dk/");
        }

        private void Btn_ShowFacebook_OnClick(object sender, RoutedEventArgs e)
        {
            // Open a webbrowser with a website
            Process.Start("https://www.facebook.com/skiveet/");
        }

        private void Btn_ShowLinkedIn_OnClick(object sender, RoutedEventArgs e)
        {
            // Open a webbrowser with a website
            Process.Start("https://www.linkedin.com/company-beta/2910577/");
        }
    }
}
