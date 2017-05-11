﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Controls;
using Classes;
using System.Windows;

namespace Foxtrot.GUI.Product
{
    /// // Mikael
    ///             
    public partial class Product_Add : Page
    {
        private bool Availibility;
        public City tempCity = new City();
        Classes.Product tempProduct = new Classes.Product();

        public Product_Add(int userID)
        {
            InitializeComponent();
            tempProduct.ProductTable = new DataTable();
            tempCity.CityDictionary = new Dictionary<string, int>();

            comboBox_Product_Add_CityID.ItemsSource = DBReadLogic.FillCityDictionary(tempCity.CityDictionary);
            DBReadLogic.FillProductTable(tempProduct.ProductTable);
            DataContext = tempProduct;
    
        }

        private void button_Product_Add_Create_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Classes.Product products = new Classes.Product();
            int tempint;

            if (textBox_Product_Add_Name.Text.Length != 0)
            {
                products.Name = textBox_Product_Add_Name.Text;
            }
            else
            {
                MessageBox.Show("Du SKAL indtaste et navn!");
                return;
            }

            if (textBox_Product_Add_Adress.Text.Length != 0)
            {
                products.Address = textBox_Product_Add_Adress.Text;
            }
            else
            {
                MessageBox.Show("Du skal indtaste en adresse!");
                return;
            }

            if (textBox_Product_Add_Longtitude.Text.Length != 0)
            {
                products.Longitude = textBox_Product_Add_Longtitude.Text.Length;
            }
            else
            {
                MessageBox.Show("Du skal indtaste længdegrad");
                return;
            }

            if (textBox_Product_Add_Latitude.Text.Length != 0)
            {
                products.Latitude = textBox_Product_Add_Latitude.Text.Length;
            }
            else
            {
                MessageBox.Show("Du skal indtaste breddegrad");
                return;
            }

            if (int.TryParse(textBox_Product_Add_ContactPhone.Text, out tempint) && 
                textBox_Product_Add_ContactPhone.Text.Length == 8)
            {
                products.ContactPhone = new List<int?>()
                {
                   tempint
                };
            }
            else
            {
                MessageBox.Show("Du skal indtaste et gyldigt telefonnummer");
                return;
            }

            if (textBox_Product_Add_ContactEmail.Text.Length != 0 && textBox_Product_Add_ContactEmail.Text.Contains("@")) 
            {
                    products.ContactEmail = new List<string>()
                    {
                        textBox_Product_Add_ContactEmail.Text
                    };
                
            }
            else
            {
                MessageBox.Show("Du skal indtaste en gyldig e-mail adresse!");
                return;
            }

            if (int.TryParse(textBox_Product_Add_ContactFax.Text, out tempint) && 
                textBox_Product_Add_ContactFax.Text.Length == 8)
            {
                products.ContactFax = new List<int?>()
                {
                    tempint
                };
            }
            else
            {
                MessageBox.Show("Du skal indtaste et gyldigt fax nummer");
                return;
            }


            if (textBox_Product_Add_Príce.Text.Length != 0)
            {
                products.Price = textBox_Product_Add_Príce.Text.Length;
            }
            else
            {
                MessageBox.Show("Du skal indtaste en gyldig pris - Skriv 0 hvis gratis");
                return;
            }

            if (textBox_Product_Add_Description.Text.Length != 0)
            {
                products.Description = textBox_Product_Add_Description.Text;
            }
            else
            {
                MessageBox.Show("Du skal indtaste en beskrivelse af produktet");
                return;
            }

            if (textBox_Product_Add_ExtraDescription.Text.Length != 0)
            {
                products.ExtraDescription = new List<ExtraDescription>()
                {
                    textBox_Product_Add_ExtraDescription.Text.ToString()
                };

                // Fejl
            }
            else
            {
                MessageBox.Show("Du skal indtaste en ekstra beskrivelse af produktet");
                return;
            }

            if (textBox_Product_Add_CanonicalUrl.Text.Length != 0)
            {
                products.CanonicalUrl = textBox_Product_Add_CanonicalUrl.Text;
            }
            else
            {
                MessageBox.Show("Du skal indtaste URL på produktet");
                return;
            }

            if (rbtn_Product_Add_Availability_False.IsEnabled == true ||
                rbtn_Product_Add_Availability_True.IsChecked == true)
            {
                products.Availability = Availibility;
            }

            List<Classes.Product> hest = new List<Classes.Product>();
            hest.Add(tempProduct);
            DBWriteLogic.WriteProducts(hest);
            MessageBox.Show("Et produkt med navnet: '" + tempProduct.Name + " " + "er blevet oprettet i systemet!");
        }

        private void Rbtn_Product_Add_Availability_True_OnClick(object sender, RoutedEventArgs e)
        {
            Availibility = true;
        }

        private void Rbtn_Product_Add_Availability_False_OnClick(object sender, RoutedEventArgs e)
        {
            Availibility = false;
        }
        

    }
}