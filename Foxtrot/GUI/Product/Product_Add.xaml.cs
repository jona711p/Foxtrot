using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using Foxtrot.Classes;
using Foxtrot.Classes.DB;

namespace Foxtrot.GUI.Product
{
    /// <summary>
    /// Mikael Paaske
    /// </summary>
    public partial class Product_Add : Page
    {
        private bool availibility;
        public City tempCity = new City();
        Classes.Product tempProduct = new Classes.Product();
        Classes.User tempUser = new Classes.User();
        DateTime? tempTimeFrom = new DateTime?();
        DateTime? tempTimeTo = new DateTime?();
        OpeningHour tempTime = new OpeningHour();


        public Product_Add(Classes.User inputUser)
        {
            InitializeComponent();
            tempUser = inputUser;
        
            comboBox_Product_Add_CityID.ItemsSource = DBReadLogic.FillCityList(tempCity.CityList);
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
            //tempProduct.ExtraDescription[0] = GUISortingLogic.Name(textBox_Product_Add_ExtraDescription);
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
                MessageBox.Show("Du skal indtaste byportal på produktet");
                return;
            }
            tempProduct.Website = GUISortingLogic.Name(textBox_Product_Add_Website);
            if (textBox_Product_Add_Website.Text.Length != 0)
            {
                tempProduct.Website = textBox_Product_Add_Website.Text;
            }
            if (datePicker_Product_Add_DateFrom.Value != null && datePicker_Product_Add_DateFrom != null)
            {

                tempTimeFrom = datePicker_Product_Add_DateFrom.Value;
                tempTimeTo = datePicker_Product_Add_DateTo.Value;


                tempTime.StartDate = Convert.ToDateTime(tempTimeFrom.Value.ToString("yyyy-MM-dd"));
                tempTime.EndDate = Convert.ToDateTime(tempTimeTo.Value.ToString("yyyy-MM-dd"));
                tempTime.StartTime = Convert.ToDateTime(tempTimeFrom.Value.ToString("HH:mm:ss"));
                tempTime.EndTime = Convert.ToDateTime(tempTimeTo.Value.ToString("HH:mm:ss"));

                //tempTime.Monday = checkBox_Product_Add_Monday.IsChecked ? null : false; //hvis det er null skal den sættes til false
                //tempTime.Tuesday = checkBox_Product_Add_Tuesday.IsChecked ? null : false;
                //tempTime.Wednesday = checkBox_Product_Add_Wednesday.IsChecked ? null : false;
                //tempTime.Thursday = checkBox_Product_Add_Thursday.IsChecked ? null : false;
                //tempTime.Friday = checkBox_Product_Add_Friday.IsChecked ? null : false;
                //tempTime.Saturday = checkBox_Product_Add_Saturday.IsChecked ? null : false;
                //tempTime.Sunday = checkBox_Product_Add_Sunday.IsChecked ? null : false;

            }

            if (rbtn_Product_Add_Availability_False.IsEnabled == true ||
                rbtn_Product_Add_Availability_True.IsChecked == true)
            {
                tempProduct.Availability = availibility;
            }

            tempProduct.Cities = new City();
            tempProduct.Cities.ID = ((KeyValuePair<int, string>)comboBox_Product_Add_CityID.SelectedItem).Key;

            tempProduct.OpeningHours = tempTime;
            tempProduct.UserID = tempUser.ID;
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