using System.Data;
using System.Windows;
using System.Windows.Controls;
using Foxtrot.Classes;
using Foxtrot.Classes.DB;
using Foxtrot.GUI.Product;

namespace Foxtrot.GUI.User
{
    /// <summary>
    /// Jonas Lykke
    /// </summary>
    public partial class User_DisplayWindow : Window
    {
        Classes.User tempUser = new Classes.User();
        Administrator tempAdministrator = new Administrator();
        Actor tempActor = new Actor();
        Classes.Product tempProduct = new Classes.Product();

        public User_DisplayWindow(Classes.User inputUser)
        {
            tempUser = inputUser;

            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;

            FillFieldsWithInfo();
        }

        private void FillFieldsWithInfo()
        {
            if (tempUser.Permission == 1)
            {
                tempAdministrator.UserID = tempUser.ID;
                tempAdministrator = DBReadLogic.GetAdminInfo(tempAdministrator);

                DataContext = tempAdministrator;

                if (tempAdministrator.FirstName == null)
                {
                    label_User_DisplayWindow_FirstName.Content = "Ingen Oplysning";
                }

                if (tempAdministrator.LastName == null)
                {
                    label_User_DisplayWindow_LastName.Content = "Ingen Oplysning";
                }

                if (tempAdministrator.WorkPhone == null)
                {
                    label_User_DisplayWindow_WorkPhone.Content = "Ingen Oplysning";
                }

                if (tempAdministrator.WorkEmail == null)
                {
                    label_User_DisplayWindow_WorkEmail.Content = "Ingen Oplysning";
                }

                if (tempAdministrator.WorkFax == null)
                {
                    label_User_DisplayWindow_WorkFax.Content = "Ingen Oplysning";
                }
            }

            if (tempUser.Permission == 2)
            {
                tempActor.UserID = tempUser.ID;
                tempActor = DBReadLogic.GetActorInfo(tempActor);

                DataContext = tempActor;

                if (tempAdministrator.FirstName == null)
                {
                    label_User_DisplayWindow_FirstName.Content = "Ingen Oplysning";
                }

                if (tempAdministrator.LastName == null)
                {
                    label_User_DisplayWindow_LastName.Content = "Ingen Oplysning";
                }

                if (tempAdministrator.WorkPhone == null)
                {
                    label_User_DisplayWindow_WorkPhone.Content = "Ingen Oplysning";
                }

                if (tempAdministrator.WorkEmail == null)
                {
                    label_User_DisplayWindow_WorkEmail.Content = "Ingen Oplysning";
                }

                if (tempAdministrator.WorkFax == null)
                {
                    label_User_DisplayWindow_WorkFax.Content = "Ingen Oplysning";
                }
            }

            tempProduct.UserID = tempUser.ID;

            tempProduct.ProductTable = DBReadLogic.FillUserProductTable(tempProduct);
            dataGrid_UserProduct_DisplayWindow.ItemsSource = tempProduct.ProductTable.AsDataView();
        }

        private void MenuItem_ViewUserProductDetails(object sender, RoutedEventArgs e)
        {
            //Runs when the user selects any item on the datagrid
            //finds the selected products ID and retrieves all information about it from the database
            //the new information is stored in the object 'tempProduct' and displayed in the relavant inputfields in the GUI 
            if (dataGrid_UserProduct_DisplayWindow.SelectedItem != null)
            {
                tempProduct.ID =
                    int.Parse(
                        ((TextBlock)
                            dataGrid_UserProduct_DisplayWindow.Columns[0].GetCellContent(
                                dataGrid_UserProduct_DisplayWindow.SelectedItem))
                        .Text);

                tempProduct.MainCategories = new MainCategory();
                tempProduct.Categories = new MainCategory();
                tempProduct = DBReadLogic.GetProductInfo(tempProduct);
                tempProduct = DBReadLogic.GetProductFileInfo(tempProduct);
                Product_DisplayWindow newProductDisplayWindow = new Product_DisplayWindow(tempProduct);
                newProductDisplayWindow.Show();
            }
        }
    }
}
