using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Foxtrot.Classes;

namespace Foxtrot.GUI.CombiProduct
{
    /// <summary>
    /// Interaction logic for WindoCombiProduct_DisplayWindow.xaml
    /// </summary>
    public partial class WindoCombiProduct_DisplayWindow : Window
    {
        Classes.CombiProduct tempNewCombiProduct = new Classes.CombiProduct();
        Classes.Product tempProduct = new Classes.Product();

        public WindoCombiProduct_DisplayWindow(Classes.CombiProduct inputCombiProduct)
        {
            InitializeComponent();
            tempNewCombiProduct = inputCombiProduct;
            ResizeMode = ResizeMode.NoResize;
            DataContext = tempNewCombiProduct;
            FillFieldWithInfo();
            //DBReadLogic.FillProductTable(tempProduct.ProductTable);
            //dataGrid_Product_List.DataContext = tempProduct.ProductTable;
        }

        private void FillFieldWithInfo()
        {
            label_CombiProduct_DisplayWindow_Name.Content = tempNewCombiProduct.Name;

            if (tempNewCombiProduct.PackagePrice == 0 ||
                string.IsNullOrEmpty(tempNewCombiProduct.PackagePrice.ToString()))
            {
                label_CombiProduct_DisplayWindow_PackagePrice.Content = "Gratis";
            }
            else
            {
                label_CombiProduct_DisplayWindow_PackagePrice.Content = tempNewCombiProduct.PackagePrice.ToString();
            }



            label_CombiProduct_DisplayWindow_CreationDate.Content = tempNewCombiProduct.CreationDate;



            if (tempNewCombiProduct.Availability == true)
            {
                AvailabilityIndicator.Fill = Brushes.Green;
            }
            else
            {
                AvailabilityIndicator.Fill = Brushes.Red;
            }
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
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
                tempProduct = DBReadLogic.GetProductFileInfo(tempProduct);
                Product_DisplayWindow newProductDisplayWindow = new Product_DisplayWindow(tempProduct);
                newProductDisplayWindow.Show();
            }
        }
    }
}
