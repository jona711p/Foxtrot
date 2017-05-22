﻿using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;
using Foxtrot.Classes.DB;
using System.Windows;

namespace Foxtrot.GUI.CombiProduct
{
    /// <summary>
    /// Interaction logic for CombiProduct_Add.xaml
    /// </summary>
    public partial class CombiProduct_Add : Page
    {
        Classes.User tempUser = new Classes.User();
        Classes.Product tempProduct = new Classes.Product();
        Classes.CombiProduct tempCombiProduct = new Classes.CombiProduct();

        public CombiProduct_Add(Classes.User inputUser)
        {
            tempUser = inputUser;

            tempProduct.ProductTable = new DataTable();
            tempCombiProduct.ProductTable = new DataTable();

            InitializeComponent();

            DBReadLogic.FillProductTable(tempProduct.ProductTable);

            DataContext = tempProduct;
        }

        private void CombiProduct_Add_Add_OnClick(object sender, RoutedEventArgs e)
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

                tempCombiProduct.ProductTable =
                    DBReadLogic.GetSingleProductInfo(tempProduct, tempCombiProduct.ProductTable);
            }

            dataGrid_CombiProduct_List.ItemsSource = tempCombiProduct.ProductTable.AsDataView();
        }

        private void CombiProduct_Add_Delete_OnClick(object sender, RoutedEventArgs e)
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

                foreach (DataRow row in tempCombiProduct.ProductTable.Rows)
                {
                    if (int.Parse(row[0].ToString()) == tempProduct.ID)
                    {
                        row.Delete();
                    }
                }
            }

            dataGrid_CombiProduct_List.ItemsSource = tempCombiProduct.ProductTable.AsDataView();
        }

        private void btn_Combi_Add_Add_OnClick(object sender, RoutedEventArgs e)
        {
            tempCombiProduct.UserID = tempUser.ID;

            tempCombiProduct.ProductID = new List<int?>();

            foreach (DataRow row in tempCombiProduct.ProductTable.Rows)
            {
                tempCombiProduct.ProductID.Add(int.Parse(row[0].ToString()));
            }

            tempCombiProduct.Name = GUISortingLogic.Name(textBox_Combi_Add_Name);

            if (tempCombiProduct.Name == null)
            {
                GUISortingLogic.Message("Du SKAL indtast et NAVN!");
                return;
            }
        }
    }
}
