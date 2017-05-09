using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Classes;
using Foxtrot.Classes;
using Foxtrot.GUI.Product;
using Foxtrot.GUI.User;

namespace Foxtrot
{
    public partial class MainWindow : Window
    {
        
        User tempUser = new User();
        Product temProduct = new Product();
        private int GlobalPermission;
        Product_Add test = new Product_Add();

        public MainWindow()
        {
            ResizeMode = ResizeMode.NoResize;            
            XMLLogic.WatchXMLDir();
            InitializeComponent();

            //Fills a list with actors and admins
            tempUser.UserList = DBReadLogic.FillAdminList(tempUser);
            tempUser.UserList = DBReadLogic.FillActorList(tempUser);
            comboBox_Main_Usertype.ItemsSource = tempUser.UserList; //skal kun opdateres hvis 'button_User_Add_CreateUser' bliver trykket

            //Smider tekst 'footer'
            textBox_LeftFooter.Text = @"Skiveegnens Erhvervs - og Turistcenter Østerbro 7, 7800 Skive
Tlf: +45 9614 7677 | info@skiveet.dk
Turistinformation læs her";
            textBox_RightFooter.Text = @"Åbningstider:
Mandag – torsdag kl. 09.00 - 15.00
Fredag kl. 09.00 - 14.00";
            
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
            //DBShowProducts.FillTable(temProduct);
            //test.dataGrid_Product_List.ItemsSource = temProduct.ProductTable.AsDataView();
        }

        private void MenuItem_Product_Edit_Delete_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Product_Edit_Delete();
        }
       
        private void ComboBox_Main_Usertype_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GlobalPermission = ((KeyValuePair<string, int>)comboBox_Main_Usertype.SelectedItem).Value;
            MessageBox.Show(GlobalPermission.ToString());
        }
    }
}
