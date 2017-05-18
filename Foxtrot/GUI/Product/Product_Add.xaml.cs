using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;
using System.Windows;
using Foxtrot.Classes;
using Foxtrot.Classes.DB;

namespace Foxtrot.GUI.Product
{
    /// // Mikael
    ///             
    public partial class Product_Add : Page
    {
        private bool availibility;
        public City tempCity = new City();
        Classes.Product tempProduct = new Classes.Product();

        public Product_Add(Classes.User inputUser)
        {
            tempProduct.ProductTable = new DataTable();

            if (inputUser.Permission == 1)
            {
                tempProduct.ActorID = DBReadLogic.GetIDFromUser("Administrators", inputUser.ID.Value);
            }

            if (inputUser.Permission == 2)
            {
                tempProduct.ActorID = DBReadLogic.GetIDFromUser("Actors", inputUser.ID.Value);
            }
            InitializeComponent();
            //tempCity.CityDictionary = new Dictionary<string, int>();
            //DBReadLogic.FillCityDictionary(tempCity.CityDictionary);
            //comboBox_Product_Add_CityID.ItemsSource = tempCity.CityDictionary;
            comboBox_Product_Add_CityID.ItemsSource = DBReadLogic.FillCityDictionary(tempCity.CityDictionary);
         

            DBReadLogic.FillProductTable(tempProduct.ProductTable);
            DataContext = tempProduct;
           

        }

        private void button_Product_Add_Create_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            int tempint;

            tempProduct.Name = GUISortingLogic.Name(textBox_Product_Add_Name);
            if (textBox_Product_Add_Name.Text.Length != 0)
            {
                tempProduct.Name = textBox_Product_Add_Name.Text;
            }
            else
            {
                MessageBox.Show("Du SKAL indtaste et navn!");
                return;
            }
            tempProduct.Address = GUISortingLogic.Name(textBox_Product_Add_Adress);
            if (textBox_Product_Add_Adress.Text.Length != 0)
            {
                tempProduct.Address = textBox_Product_Add_Adress.Text;
            }
            else
            {
                MessageBox.Show("Du skal indtaste en adresse!");
                return;
            }
            tempProduct.Longitude = GUISortingLogic.Number(textBox_Product_Add_Longtitude);
            if (textBox_Product_Add_Longtitude.Text.Length != 0)
            {
                tempProduct.Longitude = float.Parse(textBox_Product_Add_Longtitude.Text);
            }
            else
            {
                MessageBox.Show("Du skal indtaste længdegrad");
                return;
            }
            tempProduct.Latitude = GUISortingLogic.Number(textBox_Product_Add_Latitude);
            if (textBox_Product_Add_Latitude.Text.Length != 0)
            {
                tempProduct.Latitude = float.Parse(textBox_Product_Add_Latitude.Text);
            }
            else
            {
                MessageBox.Show("Du skal indtaste breddegrad");
                return;
            }
            //tempProduct.ContactPhone = GUISortingLogic.Number(textBox_Product_Add_ContactPhone);
            if (int.TryParse(textBox_Product_Add_ContactPhone.Text, out tempint) &&
                textBox_Product_Add_ContactPhone.Text.Length == 8)
            {
                tempProduct.ContactPhone = new List<int?>()
                {
                   tempint
                };
            }
            else
            {
                MessageBox.Show("Du skal indtaste et gyldigt telefonnummer");
                return;
            }
            //tempProduct.ContactEmail = GUISortingLogic.Email(textBox_Product_Add_ContactEmail);
            if (textBox_Product_Add_ContactEmail.Text.Length != 0 && textBox_Product_Add_ContactEmail.Text.Contains("@"))
            {
                tempProduct.ContactEmail = new List<string>()
                    {
                        textBox_Product_Add_ContactEmail.Text
                    };

            }
            else
            {
                MessageBox.Show("Du skal indtaste en gyldig e-mail adresse!");
                return;
            }
            //tempProduct.ContactFax = GUISortingLogic.Number(textBox_Product_Add_ContactFax);
            if (int.TryParse(textBox_Product_Add_ContactFax.Text, out tempint) &&
                textBox_Product_Add_ContactFax.Text.Length == 8)
            {
                tempProduct.ContactFax = new List<int?>()
                {
                    tempint
                };
            }
            else
            {
                MessageBox.Show("Du skal indtaste et gyldigt fax nummer");
                return;
            }

            tempProduct.Price = GUISortingLogic.Number(textBox_Product_Add_Príce);
            if (textBox_Product_Add_Príce.Text.Length != 0)
            {
                tempProduct.Price = float.Parse(textBox_Product_Add_Príce.Text);
            }
            else
            {
                MessageBox.Show("Du skal indtaste en gyldig pris - Skriv 0 hvis gratis");
                return;
            }
            tempProduct.Description = GUISortingLogic.Name(textBox_Product_Add_Description);
            if (textBox_Product_Add_Description.Text.Length != 0)
            {
                tempProduct.Description = textBox_Product_Add_Description.Text;
            }
            else
            {
                MessageBox.Show("Du skal indtaste en beskrivelse af produktet");
                return;
            }
            //tempProduct.ExtraDescription = GUISortingLogic.Name(textBox_Product_Add_ExtraDescription);
            if (textBox_Product_Add_ExtraDescription.Text.Length != 0)
            {
                tempProduct.ExtraDescription = new List<ExtraDescription>()
            {
                new ExtraDescription()
                {
                    Description = textBox_Product_Add_ExtraDescription.Text
                }
            };

            }
            else
            {
                MessageBox.Show("Du skal indtaste en ekstra beskrivelse af produktet");
                return;
            }
            tempProduct.CanonicalUrl = GUISortingLogic.Name(textBox_Product_Add_CanonicalUrl);
            if (textBox_Product_Add_CanonicalUrl.Text.Length != 0)
            {
                tempProduct.CanonicalUrl = textBox_Product_Add_CanonicalUrl.Text;
            }
            else
            {
                MessageBox.Show("Du skal indtaste URL på produktet");
                return;
            }
            tempProduct.Website = GUISortingLogic.Name(textBox_Product_Add_Website);
            if (textBox_Product_Add_Website.Text.Length != 0)
            {
                tempProduct.Website = textBox_Product_Add_Website.Text;
            }

            if (rbtn_Product_Add_Availability_False.IsEnabled == true ||
                rbtn_Product_Add_Availability_True.IsChecked == true)
            {
                tempProduct.Availability = availibility;
            }

            tempProduct.Cities = new City();
            tempProduct.Cities.ID = ((KeyValuePair<string, int>)comboBox_Product_Add_CityID.SelectedItem).Value;

            DBWriteLogic.WriteNewProduct(tempProduct);
            MessageBox.Show("Et produkt med navnet: '" + tempProduct.Name + "' " + "er blevet oprettet i systemet!");
        }

        private void Rbtn_Product_Add_Availability_True_OnClick(object sender, RoutedEventArgs e)
        {
            availibility = true;
        }

        private void Rbtn_Product_Add_Availability_False_OnClick(object sender, RoutedEventArgs e)
        {
            availibility = false;
        }

        private void comboBox_Product_Add_CityID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tempProduct.Cities.Name = comboBox_Product_Add_CityID.SelectedItem.ToString();
        }
    }
}