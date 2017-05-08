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
            textBox.Text = @"
                            Skiveegnens Erhvervs - og Turistcenter Østerbro 7, 7800 Skive
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

        private void MenuItem_User_Create_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Source = new Uri("GUI/User/User_Add.xaml", UriKind.Relative); // initialize frame with the "test1" view        
        }

        private void MenuItem_User_Edit_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Source = new Uri("GUI/User/User_Edit.xaml", UriKind.Relative); // initialize frame with the "test1" view        
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
