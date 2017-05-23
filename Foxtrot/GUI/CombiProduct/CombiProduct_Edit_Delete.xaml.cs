using System;
using System.Data;
using System.Windows.Controls;
using Foxtrot.Classes;
using Foxtrot.Classes.DB;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Foxtrot.GUI.CombiProduct
{
    /// <summary>
    /// Jonas Lykke & Mikael Paaske
    /// </summary>
    public partial class CombiProduct_Edit_Delete : Page
    {
        public Classes.User tempUser = new Classes.User();
        private bool availability;
        Classes.CombiProduct tempCombiProduct = new Classes.CombiProduct();
        Classes.Product tempProduct = new Classes.Product();

        public CombiProduct_Edit_Delete(Classes.User inputUser)
        {
            tempUser = inputUser;


            tempProduct.ProductTable = new DataTable();
            tempCombiProduct.CombiProductTable = new DataTable();
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

                //textBox_CombiProduct_Edit_Name.Text = tempCombiProduct.Name;

            }
        }
        public void MakeFieldsEditable(bool input)
        {
            //textBox_CombiProduct_Edit_Name.IsEnabled = input;
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

        private void CombiProduct_Edit_Delete_Add_OnClick(object sender, RoutedEventArgs e)
        {
            tempCombiProduct.UserID = tempUser.ID;

            tempCombiProduct.ProductID = new List<int?>();

            foreach (DataRow row in tempCombiProduct.CombiProductTable.Rows)
            {
                tempCombiProduct.ProductID.Add(int.Parse(row[0].ToString()));
            }

            bool dupe = DBReadLogic.DupeCheckCombiProduct(tempCombiProduct);

            if (!dupe)
            {
                DBWriteLogic.WriteCombiProduct(tempCombiProduct);
                DBWriteLogic.WritRelCombiProducts(tempCombiProduct);

                GUISortingLogic.Message("Et nyt Combi Produkt med Navnet: '" + tempCombiProduct.Name + "' er blevet oprettet i systemet!");
            }

            else
            {
                GUISortingLogic.Message("Der Findes Allerede et Combi Produkt med Navnet: '" + tempCombiProduct.Name + "' i systemet!");
            }
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
                            dataGrid_CombiProduct_List.Columns[0].GetCellContent(
                                dataGrid_CombiProduct_List.SelectedItem))
                        .Text);

                foreach (DataRow row in tempCombiProduct.CombiProductTable.Rows)
                {
                    if (int.Parse(row[0].ToString()) == tempProduct.ID)
                    {
                        row.Delete();
                    }
                }
            }

            dataGrid_CombiProduct_List.ItemsSource = tempCombiProduct.CombiProductTable.AsDataView();
        }
    }
}
