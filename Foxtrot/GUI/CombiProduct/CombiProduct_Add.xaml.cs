using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;
using Foxtrot.Classes.DB;
using System.Windows;
using Foxtrot.GUI.About;

namespace Foxtrot.GUI.CombiProduct
{
    /// <summary>
    /// Jonas Lykke & Mikael Paaske
    /// </summary>
    // Add a new combi product to the database
    public partial class CombiProduct_Add : Page
    {
        Classes.User tempUser = new Classes.User();
        Classes.Product tempProduct = new Classes.Product();
        Classes.CombiProduct tempCombiProduct = new Classes.CombiProduct();
        
        public CombiProduct_Add(Classes.User inputUser)
        {
            tempUser = inputUser;

            tempProduct.ProductTable = new DataTable();
            tempCombiProduct.CombiProductTable = new DataTable();

            InitializeComponent();

            DBReadLogic.FillProductTable(tempProduct.ProductTable);

            DataContext = tempProduct;
        }

        private void CombiProduct_Add_Add_OnClick(object sender, RoutedEventArgs e)
        {
            // The button "Tilføj" which add a product to the CombiProduct list
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

                tempCombiProduct.CombiProductTable =
                    DBReadLogic.GetProductInfoAndCupeCheck(tempProduct.ID, tempCombiProduct.CombiProductTable);
            }

            dataGrid_CombiProduct_List.ItemsSource = tempCombiProduct.CombiProductTable.AsDataView();
        }

        private void CombiProduct_Add_Delete_OnClick(object sender, RoutedEventArgs e)
        {
            // The button "Tilføj" which delete a product from the CombiProduct list
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
                    if (row.RowState != DataRowState.Deleted)
                    {
                        if (row[0].ToString().Contains(tempProduct.ID.ToString()))
                        {
                            row.Delete();
                        }
                    }
                }

                tempCombiProduct.CombiProductTable.AcceptChanges();
            }

            dataGrid_CombiProduct_List.ItemsSource = tempCombiProduct.CombiProductTable.AsDataView();
        }

        private void btn_Combi_Add_Add_OnClick(object sender, RoutedEventArgs e)
        {
            // The button "Tilføj" which add a product to the database
            tempCombiProduct.UserID = tempUser.ID;

            tempCombiProduct.ProductID = new List<int?>();

            foreach (DataRow row in tempCombiProduct.CombiProductTable.Rows)
            {
                tempCombiProduct.ProductID.Add(int.Parse(row[0].ToString()));
            }

            if (tempCombiProduct.ProductID.Count == 0)
            {
                GUISortingLogic.Message("Du SKAL vælge mindst 1 Produkt til et Kombi Produkt!");
                return;
            }

            tempCombiProduct.Name = GUISortingLogic.Name(textBox_Combi_Add_Name);

            if (tempCombiProduct.Name == null)
            {
                GUISortingLogic.Message("Du SKAL indtast et NAVN!");
                return;
            }

            if (textBox_Combi_Add_PackagePrice.Text.Length != 0)
            {
                tempCombiProduct.PackagePrice = GUISortingLogic.Float(textBox_Combi_Add_PackagePrice);

                if (tempCombiProduct.PackagePrice == null)
                {
                    GUISortingLogic.Message("Du SKAL indtast en SAMLET PRIS for hele Combi Produkt Pakken!");
                    return;
                }
            }

            else
            {
                tempCombiProduct.PackagePrice = null;
            }

            if (rdbtn_Combi_Add_Availibility_True.IsChecked == true)
            {
                tempCombiProduct.Availability = true;
            }

            if (rdbtn_Combi_Add_Availibility_False.IsChecked == true)
            {
                tempCombiProduct.Availability = false;
            }

            bool dupe = DBReadLogic.DupeCheckCombiProduct(tempCombiProduct);

            if (!dupe)
            {
                Declaration_of_Consent DOC = new Declaration_of_Consent();
                DOC.ShowDialog();

                if (DOC.Accept)
                {
                    DBWriteLogic.WriteCombiProduct(tempCombiProduct);
                    DBWriteLogic.WriteRelCombiProducts(tempCombiProduct);

                    GUISortingLogic.Message("Et nyt Combi Produkt med Navnet: '" + tempCombiProduct.Name + "' er blevet oprettet i systemet!");
                }
            }
            else
            {
                GUISortingLogic.Message("Der Findes Allerede et Combi Produkt med Navnet: '" + tempCombiProduct.Name + "' i systemet!");
            }
        }
    }
}
