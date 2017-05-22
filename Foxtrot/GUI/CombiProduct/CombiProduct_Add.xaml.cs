using System;
using System.Data;
using System.Windows.Controls;
using Foxtrot.Classes;
using Foxtrot.Classes.DB;
using System.Collections.Generic;
using System.Windows;
using System.Data.SqlClient;
using System.Windows.Media.Imaging;

namespace Foxtrot.GUI.CombiProduct
{
    /// <summary>
    /// Interaction logic for CombiProduct_Add.xaml
    /// </summary>
    public partial class CombiProduct_Add : Page
    {
        public Classes.User tempUser = new Classes.User();
        private bool availibility;
        public City tempCity = new City();
        Classes.Product tempProduct = new Classes.Product();

        public CombiProduct_Add(Classes.User inputUser)
        {
            tempUser = inputUser;
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
                tempProduct = DBReadLogic.GetProductFileInfo(tempProduct);
            }
        }

        private void CombiProduct_Add_Add_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }

}
