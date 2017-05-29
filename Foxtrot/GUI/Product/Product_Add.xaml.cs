using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using Foxtrot.Classes;
using Foxtrot.Classes.DB;

namespace Foxtrot.GUI.Product
{
    /// <summary>
    /// Mikael Paaske og Thomas Nielsen
    /// </summary>
    public partial class Product_Add : Page
    {
        City tempCity = new City();
        Classes.Product tempProduct = new Classes.Product();
        Classes.User tempUser = new Classes.User();
        DateTime? tempTimeFrom = new DateTime?();
        DateTime? tempTimeTo = new DateTime?();
        OpeningHour tempTime = new OpeningHour();
        Category tempCategory = new Category();
        MainCategory tempMainCategory = new MainCategory();

        public Product_Add(Classes.User inputUser)
        {
            InitializeComponent();
            tempUser = inputUser;
            comboBox_Product_Add_CityID.ItemsSource = DBReadLogic.FillCityList(tempCity.CityList);
            comboBox_Product_Add_MainCategory.ItemsSource = DBReadLogic.FillMainCategoryList(tempMainCategory.MainCategoryList);
            comboBox_Product_Add_Category.ItemsSource = DBReadLogic.FillCategoryList(tempCategory.CategoryList);
            DataContext = tempProduct;
        }

        private void button_Product_Add_Create_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int tempInt;

            if (textBox_Product_Add_Name.Text.Length != 0)
            {
                tempProduct.Name = GUISortingLogic.Name(textBox_Product_Add_Name);
            }

            else
            {
                MessageBox.Show("Du SKAL indtaste et navn!");
                return;
            }

            if (textBox_Product_Add_Adress.Text.Length != 0)
            {
                tempProduct.Address = GUISortingLogic.Name(textBox_Product_Add_Adress);
            }

            else
            {
                MessageBox.Show("Du skal indtaste en adresse!");
                return;
            }

            if (textBox_Product_Add_Longtitude.Text.Length != 0)
            {
                tempProduct.Longitude = GUISortingLogic.Float(textBox_Product_Add_Longtitude);
            }

            if (textBox_Product_Add_Latitude.Text.Length != 0)
            {
                tempProduct.Latitude = GUISortingLogic.Float(textBox_Product_Add_Latitude);
            }

            if (int.TryParse(textBox_Product_Add_ContactPhone.Text, out tempInt) &&
                textBox_Product_Add_ContactPhone.Text.Length == 8)
            {
                tempProduct.ContactPhone = new List<int?>()
                    {
                        tempInt
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
            if (int.TryParse(textBox_Product_Add_ContactFax.Text, out tempInt) &&
                textBox_Product_Add_ContactFax.Text.Length == 8)
            {
                tempProduct.ContactFax = new List<int?>()
                    {
                        tempInt
                    };
                }

            if (textBox_Product_Add_Príce.Text.Length != 0)
            {
                tempProduct.Price = float.Parse(textBox_Product_Add_Príce.Text);
            }

            if (textBox_Product_Add_Description.Text.Length != 0)
            {
                tempProduct.Description = textBox_Product_Add_Description.Text;
                tempProduct.Description = GUISortingLogic.Name(textBox_Product_Add_Description);
            }
            else
            {
                MessageBox.Show("Du skal indtaste en beskrivelse af produktet");
                return;
            }
            if (textBox_Product_Add_ExtraDescription.Text.Length != 0)
            {
                tempProduct.ExtraDescription = new List<ExtraDescription>()
                {
                    new ExtraDescription()
                    {
                        Description = GUISortingLogic.Name(textBox_Product_Add_ExtraDescription)
                    }
                };
            }

            if (textBox_Product_Add_CanonicalUrl.Text.Length != 0)
            {
                tempProduct.CanonicalUrl = textBox_Product_Add_CanonicalUrl.Text;
                tempProduct.CanonicalUrl = GUISortingLogic.Name(textBox_Product_Add_CanonicalUrl);
            }

            if (textBox_Product_Add_Website.Text.Length != 0)
            {
                tempProduct.Website = textBox_Product_Add_Website.Text;
                tempProduct.Website = GUISortingLogic.Name(textBox_Product_Add_Website);
            }

            if (datePicker_Product_Add_DateFrom.Value != null && datePicker_Product_Add_DateFrom != null)
            {

                tempTimeFrom = datePicker_Product_Add_DateFrom.Value;
                tempTimeTo = datePicker_Product_Add_DateTo.Value;


                tempTime.StartDate = Convert.ToDateTime(tempTimeFrom.Value.ToString("yyyy-MM-dd"));
                tempTime.EndDate = Convert.ToDateTime(tempTimeTo.Value.ToString("yyyy-MM-dd"));
                tempTime.StartTime = Convert.ToDateTime(tempTimeFrom.Value.ToString("HH:mm:ss"));
                tempTime.EndTime = Convert.ToDateTime(tempTimeTo.Value.ToString("HH:mm:ss"));

                //tempTime.Monday = checkBox_Product_Add_Monday.IsChecked == true;
                //tempTime.Tuesday = checkBox_Product_Add_Tuesday.IsChecked == true;
                //tempTime.Wednesday = checkBox_Product_Add_Wednesday.IsChecked == true;
                //tempTime.Thursday = checkBox_Product_Add_Thursday.IsChecked == true;
                //tempTime.Friday = checkBox_Product_Add_Friday.IsChecked == true;
                //tempTime.Saturday = checkBox_Product_Add_Saturday.IsChecked == true;
                //tempTime.Sunday = checkBox_Product_Add_Sunday.IsChecked == true;
            }

            tempProduct.Files = new List<File>();
            File tempFile1 = new File();
            File tempFile2 = new File();
            File tempFile3 = new File();
            File tempFile4 = new File();

            if (!string.IsNullOrEmpty(textBox_Product_Add_Url1.Text))
            {
                tempFile1.URI = textBox_Product_Add_Url1.Text;
                tempProduct.Files.Add(tempFile1);
            }
            if (!string.IsNullOrEmpty(textBox_Product_Add_Url2.Text))
            {
                tempFile2.URI = textBox_Product_Add_Url2.Text;
                tempProduct.Files.Add(tempFile2);
            }
            if (!string.IsNullOrEmpty(textBox_Product_Add_Url3.Text))
            {
                tempFile3.URI = textBox_Product_Add_Url3.Text;
                tempProduct.Files.Add(tempFile3);
            }
            if (!string.IsNullOrEmpty(textBox_Product_Add_Url4.Text))
            {
                tempFile4.URI = textBox_Product_Add_Url4.Text;
                tempProduct.Files.Add(tempFile4);
            }

            tempProduct.MainCategories = new MainCategory();
            tempProduct.Categories = new MainCategory();
            tempProduct.Cities = new City();

            if (comboBox_Product_Add_CityID.SelectedItem == null)
            {
                MessageBox.Show("Du skal vælge en by!");
                return;
            }
            else
            {
                tempProduct.Cities.ID = ((KeyValuePair<int, string>)comboBox_Product_Add_CityID.SelectedItem).Key;
            }
            if (comboBox_Product_Add_MainCategory.SelectedItem == null)
            {
                MessageBox.Show("Du skal vælge en hovedkategori!");
                return;
            }
            else
            {
                tempProduct.MainCategories.ID = ((KeyValuePair<int, string>)comboBox_Product_Add_MainCategory.SelectedItem).Key;
            }
            if (comboBox_Product_Add_Category.SelectedItem == null)
            {
                MessageBox.Show("Du skal vælge en kategori!");
                return;
            }
            else
            {
                tempProduct.Categories.ID = ((KeyValuePair<int, string>)comboBox_Product_Add_Category.SelectedItem).Key;
            }
            if (rbtn_Product_Add_Availability_True.IsChecked == false && rbtn_Product_Add_Availability_False.IsChecked == false)
            {
                MessageBox.Show("Du skal vælge om produktet skal være tilgængelig!");
                return;
            }
            tempProduct.OpeningHours = tempTime;
            tempProduct.UserID = tempUser.ID;

            tempProduct.Files = DBWriteLogic.WriteNewFiles(tempProduct.Files);
            tempProduct.ID = DBWriteLogic.WriteNewProduct(tempProduct);
            DBWriteLogic.WriteNewRelFiles(tempProduct);

            MessageBox.Show("Et produkt med navnet: '" + tempProduct.Name + "' " + "er blevet oprettet i systemet!");
        }

        private void Rbtn_Product_Add_Availability_True_OnClick(object sender, RoutedEventArgs e)
        {
            tempProduct.Availability = true;
        }

        private void Rbtn_Product_Add_Availability_False_OnClick(object sender, RoutedEventArgs e)
        {
            tempProduct.Availability = false;
        }
    }
}