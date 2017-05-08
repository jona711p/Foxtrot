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
        private void MenuItem_Menu_Frontpage_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("FORSIDE!");
        }

        private void MenuItem_User_Create_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Source = new Uri("GUI/User/User_Add.xaml", UriKind.Relative); // initialize frame with the "test1" view        
        }

        private void MenuItem_User_Edit_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MenuItem_Product_Create_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Source = new Uri("GUI/Product/Product_Add.xaml", UriKind.Relative); // initialize frame with the "test1" view        }
        }

        private void MenuItem_Product_Edit_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
