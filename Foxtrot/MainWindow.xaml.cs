using System;
using System.Windows;
using System.Windows.Controls;
using Classes;
using Foxtrot.GUI;

namespace Foxtrot
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            XMLLogic.WatchXMLDir();
            InitializeComponent();
        }


        //private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        //{
        //    string tempString;
        //    sender.Header = tempString;
        //    //switch (sender.Header)
        //    //{
        //    //    case "Opret":
        //    //        MessageBox.Show("Opret");
        //    //        break;
        //    //    case "Rediger":
        //    //        MessageBox.Show("Rediger");
        //    //        break;
        //    //    case "Slet":
        //    //        MessageBox.Show("Slet");
        //    //        break;
        //    //}
        //    //MessageBox.Show("hest");
        //    //MainFrame.Source = new Uri("GUI/Users_Add.xaml", UriKind.Relative); // initialize frame with the "test1" view

        //    //Page opret = new Page();
        //    //frame.Content = Users_Add;
        //}

        private void MenuItem_Menu_Frontpage_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MenuItem_User_Create_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MenuItem_User_Edit_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MenuItem_Product_Create_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MenuItem_Product_Edit_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
