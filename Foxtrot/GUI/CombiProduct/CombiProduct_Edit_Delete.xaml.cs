using System.Data;
using System.Windows.Controls;
using Foxtrot.Classes.DB;
using System.Windows;
using System.Windows.Forms;

namespace Foxtrot.GUI.CombiProduct
{
    /// <summary>
    /// Jonas Lykke & Mikael Paaske
    /// </summary>
    public partial class CombiProduct_Edit_Delete : Page
    {
        public Classes.User tempUser = new Classes.User();
        Classes.Product tempProduct = new Classes.Product();
        Classes.CombiProduct tempOldCombiProduct = new Classes.CombiProduct();
        Classes.CombiProduct tempNewCombiProduct = new Classes.CombiProduct();

        public CombiProduct_Edit_Delete(Classes.User inputUser)
        {
            tempUser = inputUser;

            tempProduct.ProductTable = new DataTable();
            tempOldCombiProduct.CombiProductTable = new DataTable();
            tempNewCombiProduct.CombiProductTable = new DataTable();

            InitializeComponent();

            DBReadLogic.FillProductTable(tempProduct.ProductTable);
            DBReadLogic.FillCombiProductTable(tempOldCombiProduct.CombiProductTable);

            DataContext = tempOldCombiProduct;
            dataGrid_Product_List.ItemsSource = tempProduct.ProductTable.AsDataView();
        }

        private void DataGrid_CombiProduct_List_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid_CombiProduct_List.SelectedItem != null)
            {
                tempOldCombiProduct.ID =
                    int.Parse(
                        ((TextBlock)
                            dataGrid_CombiProduct_List.Columns[0].GetCellContent(
                                dataGrid_CombiProduct_List.SelectedItem))
                        .Text);
                tempOldCombiProduct = DBReadLogic.GetCombiProductInfo(tempOldCombiProduct);
                MakeFieldsEditable(false);

                if (tempOldCombiProduct.UserID == tempUser.ID || tempUser.Permission == 1)
                {
                    MakeFieldsEditable(true);
                }

                FillCombiProductWithProducts();
            }
        }

        private void MakeFieldsEditable(bool input)
        {

        }

        private void Btb_CombiProduct_Search_OnClick(object sender, RoutedEventArgs e)
        {
            DBReadLogic.FillCombiProductTable(tempOldCombiProduct.CombiProductTable);
            if (TextBox_CombiProduct_Search_CombiProductName.Text != null)
            {
                foreach (DataRow row in tempOldCombiProduct.CombiProductTable.Rows)
                {
                    if (row.RowState != DataRowState.Deleted)
                    {
                        if (!row["Navn"].ToString().Contains(TextBox_CombiProduct_Search_CombiProductName.Text))
                            row.Delete();
                    }
                }
            }

            if (TextBox_CombiProduct_Search_FirstName.Text != null) 
            {
                foreach (DataRow row in tempCombiProduct.CombiProductTable.Rows)
                {
                    if (row.RowState != DataRowState.Deleted)
                    {
                        if (!row["Fornavn"].ToString().Contains(TextBox_CombiProduct_Search_FirstName.Text))
                            row.Delete();
                    }
                }
            }
            if (TextBox_CombiProduct_Search_LastName.Text != null)
            {
                foreach (DataRow row in tempOldCombiProduct.CombiProductTable.Rows)
                {
                    if (row.RowState != DataRowState.Deleted)
                    {
                        if (!row["Efternavn"].ToString().Contains(TextBox_CombiProduct_Search_LastName.Text))
                            row.Delete();
                    }
                }
            }
        }

        private void FillCombiProductWithProducts()
        {
            if (tempOldCombiProduct.ProductID != null && tempNewCombiProduct.CombiProductTable != null)
            {
                tempOldCombiProduct.ProductID.Clear();
                tempNewCombiProduct.CombiProductTable.Clear();
            }

            DBReadLogic.FillCombiProductProductList(tempOldCombiProduct);

            foreach (int productID in tempOldCombiProduct.ProductID)
            {
                tempNewCombiProduct.CombiProductTable =
                    DBReadLogic.GetProductInfoAndCupeCheck(productID, tempNewCombiProduct.CombiProductTable);
            }

            dataGrid_CombiProduct_ProductList.ItemsSource = tempNewCombiProduct.CombiProductTable.AsDataView();

            textBox_Combi_Edit_Delete_Name.Text = tempOldCombiProduct.Name;
            textBox_Combi_Edit_Delete_PackagePrice.Text = tempOldCombiProduct.PackagePrice.ToString();

            if (tempNewCombiProduct.Availability)
            {
                rdbtn_Combi_Edit_Delete_Availibility_True.IsChecked = true;
            }

            if (!tempNewCombiProduct.Availability)
            {
                rdbtn_Combi_Edit_Delete_Availibility_False.IsChecked = true;
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
                DBReadLogic.GetProductInfoAndCupeCheck(tempProduct.ID, tempCombiProduct.CombiProductTable1);

            }

            dataGrid_CombiProduct_ProductList.ItemsSource = tempNewCombiProduct.CombiProductTable.AsDataView();
        }

        private void CombiProduct_Edit_Delete_Delete_OnClick(object sender, RoutedEventArgs e)
        {
            if (dataGrid_CombiProduct_List.SelectedItem == null)
            {
                GUISortingLogic.Message("Du skal Først Vælge et Produkt fra Listen!");
            }

            else
            {
                tempProduct.ID =
                    int.Parse(
                        ((TextBlock)
                            dataGrid_CombiProduct_ProductList.Columns[0].GetCellContent(
                                dataGrid_CombiProduct_ProductList.SelectedItem))
                        .Text);

                foreach (DataRow row in tempNewCombiProduct.CombiProductTable.Rows)
                {
                    if (row.RowState != DataRowState.Deleted)
                    {
                        if (row[0].ToString().Contains(tempProduct.ID.ToString()))
                        {
                            row.Delete();
                        }
                    }
                }

                tempNewCombiProduct.CombiProductTable.AcceptChanges();
            }

            dataGrid_CombiProduct_ProductList.ItemsSource = tempNewCombiProduct.CombiProductTable.AsDataView();
        }
    }
}
