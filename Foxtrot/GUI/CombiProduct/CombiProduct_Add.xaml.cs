using System;
using System.Data;
using System.Windows.Controls;
using Foxtrot.Classes;
using Foxtrot.Classes.DB;
using System.Windows;

namespace Foxtrot.GUI.CombiProduct
{
    /// <summary>
    /// Interaction logic for CombiProduct_Add.xaml
    /// </summary>
    public partial class CombiProduct_Add : Page
    {
        public Classes.User tempUser = new Classes.User();
        public City tempCity = new City();
        Classes.Product tempProduct = new Classes.Product();
        
        private DataTable productDataTable = new DataTable();
        private bool availibility;

        public CombiProduct_Add(Classes.User inputUser)
        {
            tempUser = inputUser;
            InitializeComponent();
            tempProduct.ProductTable = new DataTable();
            DBReadLogic.FillProductTable(tempProduct.ProductTable);

            DataContext = tempProduct;

            productDataTable.Columns.Add("Produkt Nr");
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
                tempProduct = DBReadLogic.GetProductFileInfo(tempProduct);
            }
        }

        private void CombiProduct_Add_Add_OnClick(object sender, RoutedEventArgs e)
        {
            int tempInt;

            tempInt = int.Parse(
                ((TextBlock)
                    dataGrid_Product_List.Columns[0].GetCellContent(
                        dataGrid_Product_List.SelectedItem))
                .Text);

            productDataTable.Rows.Add(tempInt);

            dataGrid_CombiProduct_List.ItemsSource = productDataTable.AsDataView();
        }
    }

}
