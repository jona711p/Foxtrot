using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Classes;

namespace Foxtrot.GUI.Product
{
    /// <summary>
    /// Interaction logic for Product_Edit_Delete.xaml
    /// </summary>
    public partial class Product_Edit_Delete : Page
    {
        private bool Availibility;
        public City tempCity = new City();
        Classes.Product tempProduct = new Classes.Product();

        public Product_Edit_Delete()
        {
            InitializeComponent();
            tempProduct.ProductTable = new DataTable();
            DBReadLogic.FillProductTable(tempProduct.ProductTable);
            DataContext = tempProduct;

        }
        private void DataGrid_Product_List_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid_Product_List.SelectedItem != null)
            {
                tempProduct.ID =
                    int.Parse(
                        ((TextBlock)
                            dataGrid_Product_List.Columns[0].GetCellContent(
                                dataGrid_Product_List.SelectedItem))
                        .Text);
                tempProduct = DBReadLogic.GetProductInfo(tempProduct);
                
                textBox_Product_Edit_Name.Text = tempProduct.Name;
                textBox_Product_Edit_Adress.Text = tempProduct.Address;
                textBox_Product_Edit_Longtitude.Text = tempProduct.Longitude.ToString();
                textBox_Product_Edit_Latitude.Text = tempProduct.Latitude.ToString();
                textBox_Product_Edit_ContactPhone.Text = tempProduct.ContactPhone.ToString(); // fejl
                textBox_Product_Edit_ContactEmail.Text = tempProduct.ContactEmail[0];
                textBox_Product_Edit_ContactFax.Text = tempProduct.ContactFax.ToString();   // fejl
                textBox_Product_Edit_Príce.Text = tempProduct.Price.ToString();
                textBox_Product_Edit_Description.Text = tempProduct.Description;
                if (tempProduct.ExtraDescription.Count != 0)
                {
                    textBox_Product_Edit_ExtraDescription.Text = tempProduct.ExtraDescription[0].Description;   // fejl
                }
                rbtn_Product_Edit_Availability_True.IsChecked = tempProduct.Availability;
                rbtn_Product_Edit_Availability_False.IsChecked = !tempProduct.Availability;
                textBox_Product_Edit_Website.Text = tempProduct.Website;
                textBox_Product_Edit_CanonicalUrl.Text = tempProduct.CanonicalUrl;
            }
        }
    }
}
