using System.Data;
using System.Windows.Controls;
using Classes;

namespace Foxtrot.GUI.Product
{
    /// <summary>
    /// Interaction logic for Product_Edit_Delete.xaml
    /// </summary>
    public partial class Product_Edit_Delete : Page
    {
        private bool availibility;
        public City tempCity = new City();
        global::Classes.Product tempProduct = new global::Classes.Product();

        public Product_Edit_Delete()
        {
            InitializeComponent();
            tempProduct.ProductTable = new DataTable();
            DBReadLogic.FillProductTable(tempProduct.ProductTable);
            DataContext = tempProduct;

        }
        private void DataGrid_Product_List_OnSelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            //Runs when the user selects any item on the datagrid
            //finds the selected products ID and retrieves all information about it from the database
            //the new information is stored in the object 'tempProduct' and displayed in the relavant inputfields in the GUI 
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
                if (tempProduct.ContactPhone.Count != 0)
                {
                    textBox_Product_Edit_ContactPhone.Text = tempProduct.ContactPhone[0].ToString();
                }
                textBox_Product_Edit_ContactEmail.Text = tempProduct.ContactEmail[0];
                if (tempProduct.ContactFax.Count != 0)
                {
                    textBox_Product_Edit_ContactFax.Text = tempProduct.ContactFax[0].ToString();
                }
                textBox_Product_Edit_ContactFax.Text = tempProduct.ContactFax[0].ToString();   // fejl
                textBox_Product_Edit_Príce.Text = tempProduct.Price.ToString();
                textBox_Product_Edit_Description.Text = tempProduct.Description;
                if (tempProduct.ExtraDescription.Count != 0) //Ikke lavet ordenligt
                {
                    textBox_Product_Edit_ExtraDescription.Text = tempProduct.ExtraDescription[0].Description;   
                }
                rbtn_Product_Edit_Availability_True.IsChecked = tempProduct.Availability;
                rbtn_Product_Edit_Availability_False.IsChecked = !tempProduct.Availability;
                textBox_Product_Edit_Website.Text = tempProduct.Website;
                textBox_Product_Edit_CanonicalUrl.Text = tempProduct.CanonicalUrl;
            }
        }
    }
}
