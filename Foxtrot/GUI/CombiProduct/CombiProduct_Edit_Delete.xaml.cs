using System.Data;
using System.Windows.Controls;
using Foxtrot.Classes.DB;
using System.Windows;

namespace Foxtrot.GUI.CombiProduct
{
    /// <summary>
    /// Jonas Lykke & Mikael Paaske
    /// </summary>
    public partial class CombiProduct_Edit_Delete : Page
    {
        public Classes.User tempUser = new Classes.User();
        Classes.CombiProduct tempCombiProduct = new Classes.CombiProduct();
        Classes.Product tempProduct = new Classes.Product();

        public CombiProduct_Edit_Delete(Classes.User inputUser)
        {
            tempUser = inputUser;


            tempProduct.ProductTable = new DataTable();
            tempCombiProduct.CombiProductTable = new DataTable();
            tempCombiProduct.CombiProductTable1 = new DataTable();
            InitializeComponent();
            DBReadLogic.FillProductTable(tempProduct.ProductTable);
            DBReadLogic.FillCombiProductTable(tempCombiProduct.CombiProductTable);
            DataContext = tempProduct;
            dataGrid_CombiProduct_List.ItemsSource = tempCombiProduct.CombiProductTable.AsDataView();
        }

        private void DataGrid_CombiProduct_List_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Runs when the user selects any item on the datagrid
            //finds the selected products ID and retrieves all information about it from the database
            //the new information is stored in the object 'tempProduct' and displayed in the relavant inputfields in the GUI 
            if (dataGrid_CombiProduct_List.SelectedItem != null)
            {
                tempCombiProduct.ID =
                    int.Parse(
                        ((TextBlock)
                            dataGrid_CombiProduct_List.Columns[0].GetCellContent(
                                dataGrid_CombiProduct_List.SelectedItem))
                        .Text);
                tempCombiProduct = DBReadLogic.GetCombiProductInfo(tempCombiProduct);
                MakeFieldsEditable(false);

                if (tempCombiProduct.UserID == tempUser.ID || tempUser.Permission == 1)
                {
                    MakeFieldsEditable(true);
                }

                DBReadLogic.FillCombiProductProductList(tempCombiProduct);

                FillDataGridWithProductsForCombiProduct(tempCombiProduct);
            }
        }
        private void MakeFieldsEditable(bool input)
        {

        }

        private void Btb_CombiProduct_Search_OnClick(object sender, RoutedEventArgs e)
        {
            DBReadLogic.FillCombiProductTable(tempCombiProduct.CombiProductTable);
            if (TextBox_CombiProduct_Search_CombiProductName.Text != null)
            {
                foreach (DataRow row in tempCombiProduct.CombiProductTable.Rows)
                {
                    if (row.RowState != DataRowState.Deleted)
                    {
                        if (!row["Navn"].ToString().Contains(TextBox_CombiProduct_Search_CombiProductName.Text))
                            row.Delete();
                    }
                }
            }

            if (TextBox_CombiProduct_Search_ProductName.Text != null) // Skal laves til produkt navn
            {
                foreach (DataRow row in tempCombiProduct.CombiProductTable.Rows)
                {
                    if (row.RowState != DataRowState.Deleted)
                    {
                        if (!row["ProductName"].ToString().Contains(TextBox_CombiProduct_Search_ProductName.Text))
                            row.Delete();
                    }
                }
            }
        }

        private void FillDataGridWithProductsForCombiProduct(Classes.CombiProduct inputCombiProduct)
        {
            foreach (int productID in inputCombiProduct.ProductID)
            {
                tempCombiProduct.CombiProductTable =
                    DBReadLogic.GetProductInfoAndCupeCheck(productID, tempCombiProduct.CombiProductTable);
            }
        }

        private void CombiProduct_Edit_Delete_Add_OnClick(object sender, RoutedEventArgs e)
        {
            if (dataGrid_Product_List.SelectedItem == null)
            {
                GUISortingLogic.Message("Du skal Først Vælge et Produkt fra Listen!");
            }

            else
            {
                tempProduct.ID =
                    int.Parse(
                        ((TextBlock)
                            dataGrid_Product_List.Columns[0].GetCellContent(
                                dataGrid_Product_List.SelectedItem))
                        .Text);

                    tempCombiProduct.CombiProductTable1 =
                    DBReadLogic.GetProductInfoAndCupeCheck(tempProduct, tempCombiProduct.CombiProductTable1);

            }

            dataGrid_CombiProduct_List1.ItemsSource = tempCombiProduct.CombiProductTable1.AsDataView();
        }

        private void CombiProduct_Edit_Delete_Delete_OnClick(object sender, RoutedEventArgs e)
        {
            if (dataGrid_CombiProduct_List1.SelectedItem == null)
            {
                GUISortingLogic.Message("Du skal Først Vælge et Produkt fra Listen!");
            }

            else
            {
                tempProduct.ID =
                    int.Parse(
                        ((TextBlock)
                            dataGrid_CombiProduct_List1.Columns[0].GetCellContent(
                                dataGrid_CombiProduct_List1.SelectedItem))
                        .Text);

                foreach (DataRow row in tempCombiProduct.CombiProductTable1.Rows)
                {
                    if (int.Parse(row[0].ToString()) == tempProduct.ID)
                    {
                        row.Delete();
                    }
                }
            }

            dataGrid_CombiProduct_List1.ItemsSource = tempCombiProduct.CombiProductTable1.AsDataView();
        }
    }
}
