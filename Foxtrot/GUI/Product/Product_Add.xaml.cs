using System.Data;
using System.Windows.Controls;
using Classes;

namespace Foxtrot.GUI.Product
{
    /// <summary>
    /// Interaction logic for Product_Add.xaml
    /// </summary>
    public partial class Product_Add : Page
    {
        public Product_Add()
        {
            InitializeComponent();

            Classes.Product product = new Classes.Product();

            product.ProductTable = new DataTable();

            DBReadLogic.FillProductTable(product.ProductTable);

            DataContext = product;
        }
    }
}
