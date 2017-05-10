using System.Data;
using System.Windows.Controls;
using Classes;
using System.Windows;

namespace Foxtrot.GUI.Product
{
    /// <summary>
    /// Interaction logic for Product_Add.xaml
    /// </summary>
    public partial class Product_Add : Page
    {
        private object product;

        public Product_Add()
        {
            InitializeComponent();

            Classes.Product product = new Classes.Product();

            product.ProductTable = new DataTable();

            DBReadLogic.FillProductTable(product.ProductTable);

            DataContext = product;
        }

        private void button_Product_Add_Create_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Classes.Product products = new Classes.Product();

            if (textBox_Product_Add_Name.Text.Length != 0)
            {
                products.Name = textBox_Product_Add_Name.Text;
            }
            else
            {
                MessageBox.Show("Du SKAL indtaste et navn!");
                return;
            }

            if (textBox_Product_Add_Adress.Text.Length != 0)
            {
                products.Address = textBox_Product_Add_Adress.Text;
            }
            else
            {
                MessageBox.Show("Du skal indtaste en adresse!");
                return;
            }

            if (textBox_Product_Add_Longtitude.Text.Length != 0)
            {
                products.Longitude = textBox_Product_Add_Longtitude.Text.Length;
            }
            else
            {
                MessageBox.Show("Du skal indtaste længdegrad");
                return;
            }

            if (textBox_Product_Add_Latitude.Text.Length != 0)
            {
                products.Latitude = textBox_Product_Add_Latitude.Text.Length;
            }
            else
            {
                MessageBox.Show("Du skal indtaste breddegrad");
            }

            if (textBox_Product_Add_ContactPhone.Text.Length != 0 && textBox_Product_Add_ContactPhone.Text.Length == 8)
            {
                products.ContactPhone = textBox_Product_Add_ContactPhone.Text.Length;
            }
            else
            {
                MessageBox.Show("Du skal indtaste et gyldigt telefonnummer");
            }


        }
    }
}
