using System.Data;
using System.Windows.Controls;
using Foxtrot.Classes;
using Foxtrot.Classes.DB;
using System.Collections.Generic;
using System.Windows;

namespace Foxtrot.GUI.Product
{
    /// <summary>
    /// Interaction logic for Product_Edit_Delete.xaml
    /// </summary>
    public partial class Product_Edit_Delete : Page
    {
        private bool availibility;
        public City tempCity = new City();
        Classes.Product tempProduct = new Classes.Product();

        public Product_Edit_Delete()
        {
            InitializeComponent();
            tempProduct.ProductTable = new DataTable();
            DBReadLogic.FillProductTable(tempProduct.ProductTable);
            comboBox_Product_Edit_CityID.ItemsSource = DBReadLogic.FillCityDictionary(tempCity.CityDictionary);
            DataContext = tempProduct;

        }
        private void DataGrid_Product_List_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Runs when the user selects any item on the datagrid
            //finds the selected products ID and retrieves all information about it from the database
            //the new information is stored in the object 'tempProduct' and displayed in the relavant inputfields in the GUI 
            if (dataGrid_Product_List.SelectedItem != null)
            {
                tempProduct.ID =
                    int.Parse(
                        ((TextBlock)
                            dataGrid_Product_List.Columns[0].GetCellContent(
                                dataGrid_Product_List.SelectedItem))
                        .Text);
                tempProduct = DBReadLogic.GetProductInfo(tempProduct);

                textBox_Product_Edit_Name.Text = tempProduct.Name;
                textBox_Product_Edit_Adress.Text = tempProduct.Address;
                textBox_Product_Edit_Longtitude.Text = tempProduct.Longitude.ToString();
                textBox_Product_Edit_Latitude.Text = tempProduct.Latitude.ToString();
                if (tempProduct.ContactPhone.Count != 0)
                {
                    textBox_Product_Edit_ContactPhone.Text = tempProduct.ContactPhone[0].ToString();
                }
                textBox_Product_Edit_ContactEmail.Text = tempProduct.ContactEmail[0];
                if (tempProduct.ContactFax.Count != 0)
                {
                    textBox_Product_Edit_ContactFax.Text = tempProduct.ContactFax[0].ToString();
                }
                textBox_Product_Edit_ContactFax.Text = tempProduct.ContactFax[0].ToString();
                textBox_Product_Edit_Príce.Text = tempProduct.Price.ToString();
                textBox_Product_Edit_Description.Text = tempProduct.Description;
                if (tempProduct.ExtraDescription.Count != 0)
                {
                    textBox_Product_Edit_ExtraDescription.Text = tempProduct.ExtraDescription[0].Description;
                }
                rbtn_Product_Edit_Availability_True.IsChecked = tempProduct.Availability;
                rbtn_Product_Edit_Availability_False.IsChecked = !tempProduct.Availability;
                textBox_Product_Edit_Website.Text = tempProduct.Website;
                textBox_Product_Edit_CanonicalUrl.Text = tempProduct.CanonicalUrl;

                //tempProduct.Cities.ID = ((KeyValuePair<int, string>)comboBox_Product_Edit_CityID.SelectedItem).Key;
            }
        }
        private void button_Product_Edit_Edit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int tempint;

            tempProduct.Name = textBox_Product_Edit_Name.Text;
            tempProduct.Address = textBox_Product_Edit_Adress.Text;
            tempProduct.Longitude = textBox_Product_Edit_Longtitude.Text.Length;
            tempProduct.Latitude = textBox_Product_Edit_Latitude.Text.Length;

            if (int.TryParse(textBox_Product_Edit_ContactPhone.Text, out tempint) &&
                textBox_Product_Edit_ContactPhone.Text.Length == 8)
            {
                tempProduct.ContactPhone = new List<int?>()
                {
                   tempint
                };
            }
            if (textBox_Product_Edit_ContactEmail.Text.Length != 0 && textBox_Product_Edit_ContactEmail.Text.Contains("@"))
            {
                tempProduct.ContactEmail = new List<string>()
                    {
                        textBox_Product_Edit_ContactEmail.Text
                    };

            }
            if (int.TryParse(textBox_Product_Edit_ContactFax.Text, out tempint) &&
                textBox_Product_Edit_ContactFax.Text.Length == 8)
            {
                tempProduct.ContactFax = new List<int?>()
                {
                    tempint
                };
            }
            if (textBox_Product_Edit_Príce.Text.Length != 0)
            {
                tempProduct.Price = float.Parse(textBox_Product_Edit_Príce.Text);
            }
            if (textBox_Product_Edit_Description.Text.Length != 0)
            {
                tempProduct.Description = textBox_Product_Edit_Description.Text;
            }
            else
            {
                MessageBox.Show("Du skal indtaste en beskrivelse af produktet");
                return;
            }

            if (textBox_Product_Edit_ExtraDescription.Text.Length != 0)
            {
                tempProduct.ExtraDescription = new List<ExtraDescription>()
            {
                new ExtraDescription()
                {
                    Description = textBox_Product_Edit_ExtraDescription.Text
                }
            };

            }
            else
            {
                MessageBox.Show("Du skal indtaste en ekstra beskrivelse af produktet");
                return;
            }

            if (textBox_Product_Edit_CanonicalUrl.Text.Length != 0)
            {
                tempProduct.CanonicalUrl = textBox_Product_Edit_CanonicalUrl.Text;
            }
            else
            {
                MessageBox.Show("Du skal indtaste URL på produktet");
                return;
            }
            if (textBox_Product_Edit_Website.Text.Length != 0)
            {
                tempProduct.Website = textBox_Product_Edit_Website.Text;
            }

            if (rbtn_Product_Edit_Availability_False.IsEnabled == true ||
                rbtn_Product_Edit_Availability_True.IsChecked == true)
            {
                tempProduct.Availability = availibility;
            }
            tempProduct.Cities = new City();
            tempProduct.Cities.ID = ((KeyValuePair<string, int>)comboBox_Product_Edit_CityID.SelectedItem).Value;

            DBUpdateLogic.UpdateProduct(tempProduct);
            MessageBox.Show("Produktet: '" + tempProduct.Name + "' " + "er blevet redigeret i systemet!");
        }

        private void btb_Product_Search_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
