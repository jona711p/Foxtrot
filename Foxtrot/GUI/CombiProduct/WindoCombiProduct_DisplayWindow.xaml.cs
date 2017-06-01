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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Foxtrot.Classes;
using Foxtrot.Classes.DB;
using Foxtrot.GUI.Product;

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

            DBReadLogic.FillCombiProductProductList(tempNewCombiProduct);
            dataGrid_CombiProduct_ProductList.ItemsSource = tempNewCombiProduct.CombiProductTable.AsDataView();
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



            if (tempNewCombiProduct.Availability)
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
            if (dataGrid_CombiProduct_ProductList.SelectedItem != null)
            {
                tempProduct.ID =
                    int.Parse(
                        ((TextBlock)
                            dataGrid_CombiProduct_ProductList.Columns[0].GetCellContent(
                                dataGrid_CombiProduct_ProductList.SelectedItem))
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
