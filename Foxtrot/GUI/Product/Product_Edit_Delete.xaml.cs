using System;
using System.Data;
using System.Windows.Controls;
using Foxtrot.Classes;
using Foxtrot.Classes.DB;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using MessageBox = System.Windows.MessageBox;

namespace Foxtrot.GUI.Product
{
    /// <summary>
    /// Interaction logic for Product_Edit_Delete.xaml
    /// </summary>
    public partial class Product_Edit_Delete : Page
    {
        public  Classes.User tempUser = new Classes.User();
        private bool availibility;
        public City tempCity = new City();
        Classes.Product tempProduct = new Classes.Product();
        DateTime? tempTimeFrom = new DateTime?();
        DateTime? tempTimeTo = new DateTime?();
        OpeningHour tempTime = new OpeningHour();
        Category tempCategory = new Category();
        MainCategory tempMainCategory = new MainCategory();

        public Product_Edit_Delete(Classes.User inputUser) 
        {
            tempUser = inputUser;
            InitializeComponent();
            tempProduct.ProductTable = new DataTable();
            tempProduct.MainCategories = new MainCategory();
            tempProduct.Categories = new MainCategory();
            DBReadLogic.FillProductTable(tempProduct.ProductTable);
            comboBox_Product_Edit_CityID.ItemsSource = DBReadLogic.FillCityList(tempCity.CityList);
            comboBox_Product_Edit_MainCategory.ItemsSource = DBReadLogic.FillMainCategoryList(tempMainCategory.MainCategoryList);
            comboBox_Product_Edit_Category.ItemsSource = DBReadLogic.FillCategoryList(tempCategory.CategoryList);
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
                tempProduct = DBReadLogic.GetProductFileInfo(tempProduct);
                MakeFieldsEditable(false);

                if (tempProduct.UserID == tempUser.ID || tempUser.Permission == 1) //Checks if the current user is an admin or the creator of the product 
                {
                    MakeFieldsEditable(true);
                }
                tempTime.ID = tempProduct.OpeningHours.ID;
                textBox_Product_Edit_Name.Text = tempProduct.Name;
                textBox_Product_Edit_Adress.Text = tempProduct.Address;
                textBox_Product_Edit_Longtitude.Text = tempProduct.Longitude.ToString();
                textBox_Product_Edit_Latitude.Text = tempProduct.Latitude.ToString();
                textBox_Product_Edit_ContactEmail.Text = tempProduct.ContactEmail[0];
                textBox_Product_Edit_ContactFax.Text = tempProduct.ContactFax[0].ToString();
                textBox_Product_Edit_Príce.Text = tempProduct.Price.ToString();
                textBox_Product_Edit_Description.Text = tempProduct.Description;
                rbtn_Product_Edit_Availability_True.IsChecked = tempProduct.Availability;
                rbtn_Product_Edit_Availability_False.IsChecked = !tempProduct.Availability;
                textBox_Product_Edit_Website.Text = tempProduct.Website;
                textBox_Product_Edit_CanonicalUrl.Text = tempProduct.CanonicalUrl;
                comboBox_Product_Edit_CityID.Text = tempProduct.Cities.Name;
                comboBox_Product_Edit_MainCategory.Text = tempProduct.MainCategories.Name;
                comboBox_Product_Edit_Category.Text = tempProduct.Categories.Name;

                if (tempProduct.ContactPhone.Count != 0)
                {
                    textBox_Product_Edit_ContactPhone.Text = tempProduct.ContactPhone[0].ToString();
                }

                if (tempProduct.ContactFax.Count != 0)
                {
                    textBox_Product_Edit_ContactFax.Text = tempProduct.ContactFax[0].ToString();
                }

                if (tempProduct.ExtraDescription.Count != 0)
                {
                    textBox_Product_Edit_ExtraDescription.Text = tempProduct.ExtraDescription[0].Description;
                }
                for (int i = 0; i <  4; i++) //Resets all images
                {
                    ((System.Windows.Controls.Image)product_imageGrid.Children[i]).Source = null;
                    ((System.Windows.Controls.TextBox)grid_urlInputs.Children[i]).Text = null;
                }

                for (int i = 0; i < tempProduct.Files.Count && i < 4; i++) //Sets the UI images to display images related to the product
                {
                    if (!string.IsNullOrEmpty(tempProduct.Files[i].URI))
                    {
                        ((System.Windows.Controls.Image) product_imageGrid.Children[i]).Source =
                            new BitmapImage(new Uri(tempProduct.Files[i].URI));

                        ((System.Windows.Controls.TextBox)grid_urlInputs.Children[i]).Text = tempProduct.Files[i].URI;
                    }
                }
                checkBox_Product_Edit_Monday.IsChecked = tempProduct.OpeningHours.Monday;
                checkBox_Product_Edit_Tuesday.IsChecked = tempProduct.OpeningHours.Tuesday;
                checkBox_Product_Edit_Wednesday.IsChecked = tempProduct.OpeningHours.Wednesday;
                checkBox_Product_Edit_Thursday.IsChecked = tempProduct.OpeningHours.Thursday;
                checkBox_Product_Edit_Friday.IsChecked = tempProduct.OpeningHours.Friday;
                checkBox_Product_Edit_Saturday.IsChecked = tempProduct.OpeningHours.Saturday;
                checkBox_Product_Edit_Sunday.IsChecked = tempProduct.OpeningHours.Sunday;

                if (tempProduct.OpeningHours.StartDate != null && tempProduct.OpeningHours.StartTime != null)
                {
                    DateTime? tempStartDateTime = new DateTime(tempProduct.OpeningHours.StartDate.Value.Year, tempProduct.OpeningHours.StartDate.Value.Month, tempProduct.OpeningHours.StartDate.Value.Day,
                        tempProduct.OpeningHours.StartTime.Value.Hour, tempProduct.OpeningHours.StartTime.Value.Minute, tempProduct.OpeningHours.StartTime.Value.Second);
                    datePicker_Product_Edit_DateFrom.Value = tempStartDateTime;
                }
                else
                {
                    datePicker_Product_Edit_DateFrom.Value = tempProduct.OpeningHours.StartDate;
                }
                if (tempProduct.OpeningHours.EndDate != null && tempProduct.OpeningHours.EndTime != null)
                {
                    DateTime? tempEndDateTime = new DateTime(tempProduct.OpeningHours.EndDate.Value.Year, tempProduct.OpeningHours.EndDate.Value.Month, tempProduct.OpeningHours.EndDate.Value.Day,
                        tempProduct.OpeningHours.EndTime.Value.Hour, tempProduct.OpeningHours.EndTime.Value.Minute, tempProduct.OpeningHours.EndTime.Value.Second);
                    datePicker_Product_Edit_DateTo.Value = tempEndDateTime;
                }
                else
                {
                    datePicker_Product_Edit_DateTo.Value = tempProduct.OpeningHours.EndDate;
                }
            }
        }

        public void MakeFieldsEditable(bool input)
        {
            textBox_Product_Edit_Name.IsEnabled = input;
            textBox_Product_Edit_Adress.IsEnabled = input;
            textBox_Product_Edit_Longtitude.IsEnabled = input;
            textBox_Product_Edit_Latitude.IsEnabled = input;
            textBox_Product_Edit_ContactPhone.IsEnabled = input;
            textBox_Product_Edit_ContactEmail.IsEnabled = input;
            textBox_Product_Edit_ContactFax.IsEnabled = input;
            textBox_Product_Edit_ContactFax.IsEnabled = input;
            textBox_Product_Edit_Príce.IsEnabled = input;
            textBox_Product_Edit_Description.IsEnabled = input;
            textBox_Product_Edit_ExtraDescription.IsEnabled = input;
            rbtn_Product_Edit_Availability_True.IsEnabled = input;
            rbtn_Product_Edit_Availability_False.IsEnabled = input;
            textBox_Product_Edit_Website.IsEnabled = input;
            textBox_Product_Edit_CanonicalUrl.IsEnabled = input;
            comboBox_Product_Edit_CityID.IsEnabled = input;
            comboBox_Product_Edit_MainCategory.IsEnabled = input;
            comboBox_Product_Edit_Category.IsEnabled = input;
            datePicker_Product_Edit_DateFrom.IsEnabled = input;
            datePicker_Product_Edit_DateTo.IsEnabled = input;
            textBox_Product_Edit_Url1.IsEnabled = input;
            textBox_Product_Edit_Url2.IsEnabled = input;
            textBox_Product_Edit_Url3.IsEnabled = input;
            textBox_Product_Edit_Url4.IsEnabled = input;


            checkBox_Product_Edit_Monday.IsEnabled = input;
            checkBox_Product_Edit_Tuesday.IsEnabled = input;
            checkBox_Product_Edit_Wednesday.IsEnabled = input;
            checkBox_Product_Edit_Thursday.IsEnabled = input;
            checkBox_Product_Edit_Friday.IsEnabled = input;
            checkBox_Product_Edit_Saturday.IsEnabled = input;
            checkBox_Product_Edit_Sunday.IsEnabled = input;

            button_Product_Edit_Edit.IsEnabled = input;
            button_Product_Edit_Delete.IsEnabled = input;

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

            if (datePicker_Product_Edit_DateFrom.Value != null && datePicker_Product_Edit_DateTo != null)
            {
                if (datePicker_Product_Edit_DateFrom.Value < datePicker_Product_Edit_DateTo.Value)
                {
                    tempTimeFrom = datePicker_Product_Edit_DateFrom.Value;
                    tempTimeTo = datePicker_Product_Edit_DateTo.Value;


                    tempTime.StartDate = Convert.ToDateTime(tempTimeFrom.Value.ToString("yyyy-MM-dd"));
                    tempTime.EndDate = Convert.ToDateTime(tempTimeTo.Value.ToString("yyyy-MM-dd"));
                    tempTime.StartTime = Convert.ToDateTime(tempTimeFrom.Value.ToString("HH:mm:ss"));
                    tempTime.EndTime = Convert.ToDateTime(tempTimeTo.Value.ToString("HH:mm:ss"));

                    tempTime.Monday = checkBox_Product_Edit_Monday.IsChecked == true;
                    tempTime.Tuesday = checkBox_Product_Edit_Tuesday.IsChecked == true;
                    tempTime.Wednesday = checkBox_Product_Edit_Wednesday.IsChecked == true;
                    tempTime.Thursday = checkBox_Product_Edit_Thursday.IsChecked == true;
                    tempTime.Friday = checkBox_Product_Edit_Friday.IsChecked == true;
                    tempTime.Saturday = checkBox_Product_Edit_Saturday.IsChecked == true;
                    tempTime.Sunday = checkBox_Product_Edit_Sunday.IsChecked == true;
                }
                else
                {
                    MessageBox.Show("Du kan ikke vælge en startdato der er senere end slutdatoen!");
                    return;
                }
            }
            tempProduct.OpeningHours = tempTime;

            tempProduct.Cities = new City();
            tempProduct.Cities.ID = ((KeyValuePair<int, string>)comboBox_Product_Edit_CityID.SelectedItem).Key;
            tempProduct.MainCategories.ID = ((KeyValuePair<int, string>)comboBox_Product_Edit_MainCategory.SelectedItem).Key;
            tempProduct.Categories.ID = ((KeyValuePair<int, string>)comboBox_Product_Edit_Category.SelectedItem).Key;


            if (tempProduct.Files.Count == 0)
            {
                File tempFile1 = new File();
                tempProduct.Files.Add(tempFile1);
                File tempFile2 = new File();
                tempProduct.Files.Add(tempFile2);
                File tempFile3 = new File();
                tempProduct.Files.Add(tempFile3);
                File tempFile4 = new File();
                tempProduct.Files.Add(tempFile4);
            }
           else if (tempProduct.Files.Count == 1)
            {
                File tempFile1 = new File();
                tempProduct.Files.Add(tempFile1);
                File tempFile2 = new File();
                tempProduct.Files.Add(tempFile2);
                File tempFile3 = new File();
                tempProduct.Files.Add(tempFile3);
            }
           else if (tempProduct.Files.Count == 2)
            {
                File tempFile1 = new File();
                tempProduct.Files.Add(tempFile1);
                File tempFile2 = new File();
                tempProduct.Files.Add(tempFile2);
            }
           else if (tempProduct.Files.Count == 3)
            {
                File tempFile = new File();
                tempProduct.Files.Add(tempFile);
            }
            if (tempProduct.Files.Count == 4)
            {
                tempProduct.Files[0].URI = textBox_Product_Edit_Url1.Text;
                tempProduct.Files[1].URI = textBox_Product_Edit_Url2.Text;
                tempProduct.Files[2].URI = textBox_Product_Edit_Url3.Text;
                tempProduct.Files[3].URI = textBox_Product_Edit_Url4.Text;
            }

            DBUpdateLogic.UpdateFiles(tempProduct);

            if (tempTime.ID != null)
            {
                DBUpdateLogic.UpdateOpeningHours(tempTime);
            }
            else if (tempTime.StartDate != null && tempTime.EndDate != null && tempProduct.OpeningHours.ID == null)
            {
                tempProduct.OpeningHours.ID = DBWriteLogic.WriteOpeningHours(tempProduct);
            }

            DBUpdateLogic.UpdateProduct(tempProduct);
            DBReadLogic.FillProductTable(tempProduct.ProductTable);
            MessageBox.Show("Produktet: '" + tempProduct.Name + "' " + "er blevet redigeret i systemet!");
        }

        private void btb_Product_Search_Click(object sender, RoutedEventArgs e)
        {
            DBReadLogic.FillProductTable(tempProduct.ProductTable);
            if (textBox_Product_SearchName.Text != "")
            {
                foreach (DataRow row in tempProduct.ProductTable.Rows)
                {
                    if (row.RowState != DataRowState.Deleted)
                    {
                        if (!row["Navn"].ToString().Contains(textBox_Product_SearchName.Text))
                            row.Delete();
                    }

                }
            }
            if (textBox_Product_SearchCategory.Text != "")
            {
                foreach (DataRow row in tempProduct.ProductTable.Rows)
                {
                    if (row.RowState != DataRowState.Deleted)
                        if (!row["Kategori"].ToString().Contains(textBox_Product_SearchCategory.Text))
                            row.Delete();
                }
            }
            if (textBox_Product_SearchCity.Text != "")
            {
                foreach (DataRow row in tempProduct.ProductTable.Rows)
                {
                    if (row.RowState != DataRowState.Deleted)
                        if (!row["By"].ToString().Contains(textBox_Product_SearchCity.Text))
                            row.Delete();
       
                }
            }

            if (datePicker__Product_Search_Date_From.SelectedDate != null && datePicker_Product_Search_Date_To != null)
            {
                if (datePicker__Product_Search_Date_From.SelectedDate > datePicker_Product_Search_Date_To.SelectedDate)
                {
                    GUISortingLogic.Message("Du kan ikke vælge en start dato der er efter slut datoen.");
                    datePicker_Product_Search_Date_To.SelectedDate = null;
                    datePicker__Product_Search_Date_From.SelectedDate = null;
                }
                foreach (DataRow row in tempProduct.ProductTable.Rows)
                {
                    if (row.RowState != DataRowState.Deleted)
                        if (Convert.ToDateTime(row["Start Dato"]) < datePicker__Product_Search_Date_From.SelectedDate || Convert.ToDateTime(row["Slut Dato"]) > datePicker_Product_Search_Date_To.SelectedDate)
                            row.Delete();
                }
            }            
        }

        private void Button_Product_Edit_Delete_OnClick(object sender, RoutedEventArgs e)
        {
            DBDeleteLogic.DeleteProduct(tempProduct);

            for (int i = 0; i < tempProduct.Files.Count; i++)
            {
                DBDeleteLogic.DeleteFile(tempProduct.Files[i]);
            }
            DBDeleteLogic.DeleteOpeningHour(tempProduct.OpeningHours);
            DBReadLogic.FillProductTable(tempProduct.ProductTable);
            

            MessageBox.Show("Produktet: '" + tempProduct.Name + "' " + "er blevet slettet fra systemet!");

        }
    }
}
