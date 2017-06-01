using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Foxtrot.Classes.DB;

namespace Foxtrot.GUI.Product
{
    /// <summary>
    /// Interaction logic for Product_DisplayWindow.xaml
    /// </summary>
    public partial class Product_DisplayWindow : Window
    {
        Classes.Product tempProduct = new Classes.Product();
        public Product_DisplayWindow(Classes.Product inputProduct)
        {
            InitializeComponent();
            tempProduct = inputProduct;
            ResizeMode = ResizeMode.NoResize;
            DataContext = tempProduct;
            FillFieldsWithInfo();
        }
        public void FillFieldsWithInfo()
        {
            //tomme felter skal skiftes ud med "ingen oplysninger"
            //extra/description skal kunne vises i fuld længde
            //canonicalURL og website skal automatisk lave om til hyperlinks
            //længde og breddegrad skal vises i googlemaps i vinduet
            


            label_Product_DisplayWindow_Name.Content = tempProduct.Name;
            string Categories = tempProduct.MainCategories.Name + " - " + tempProduct.Categories.Name;
            label_Product_DisplayWindow_Category.Content = Categories;


            if (tempProduct.Price == 0 || string.IsNullOrEmpty(tempProduct.Price.ToString()))
            {
                label_Product_DisplayWindow_Price.Content = "GRATIS";
            }
            else
            {
                label_Product_DisplayWindow_Price.Content = tempProduct.Price.ToString();
            }

            if (tempProduct.ContactFax[0] == null && string.IsNullOrEmpty(tempProduct.ContactFax[0].ToString()))
            {
                label_Product_DisplayWindow_Fax.Content = "Ingen oplysninger";
            }
            else
            {
                label_Product_DisplayWindow_Fax.Content = tempProduct.ContactFax.ToString();
            }


            string tempAdress = "Ingen oplysninger";
            if (string.IsNullOrEmpty(tempProduct.Address))
            {
                tempAdress = tempProduct.Cities.Name;
            }
            if (!string.IsNullOrEmpty(tempProduct.Address) && !string.IsNullOrEmpty(tempProduct.Cities.Name))
            {
                tempAdress = tempProduct.Address + ", " + tempProduct.Cities.Name;
            }
            label_Product_DisplayWindow_Adress.Content = tempAdress;

            string tempEmail = "Ingen Oplysninger";
            if (string.IsNullOrEmpty(tempProduct.ContactEmail[0]))
            {
                tempEmail = tempProduct.ContactEmail[0];
            }
            label_Product_DisplayWindow_Email.Content = tempEmail;
            




            label_Product_DisplayWindow_Longitude.Content = tempProduct.Longitude.ToString();
            label_Product_DisplayWindow_Latitude.Content = tempProduct.Latitude.ToString();


            if (tempProduct.ContactPhone[0] == null && string.IsNullOrEmpty(tempProduct.ContactPhone[0].ToString()))
            {
                label_Product_DisplayWindow_Phone.Content = "Ingen oplysninger";
            }
            else
            {
                label_Product_DisplayWindow_Phone.Content = tempProduct.ContactPhone.ToString();
            }

            

           





            textBlock_Product_DisplayWindow_Description.Text = tempProduct.Description;

            //textBlock_Product_DisplayWindow_ExtraDescription.Text = tempProduct.Description;
            if (tempProduct.ExtraDescription.Count != 0)
            {
                textBlock_Product_DisplayWindow_ExtraDescription.Text = tempProduct.ExtraDescription[0].Description;
            }


 

            label_Product_DisplayWindow_CanonicalURL.Content = tempProduct.CanonicalUrl;
            label_Product_DisplayWindow_Website.Content = tempProduct.Website;


            //label_Product_DisplayWindow_MainCategory.Content = tempProduct.MainCategories.Name;
            //label_Product_DisplayWindow_Category.Content = tempProduct.Categories.Name;



            //Ændre cirklen der angiver 'availibity'
            if (tempProduct.Availability == true)
            {
                AvailabilityIndicator.Fill = Brushes.Green;
            }
            else
            {
                AvailabilityIndicator.Fill = Brushes.Red;
            }

            ShowImages();
        }

        public void ShowImages()
        {
            for (int i = 0; i < 4; i++) //Resets all images
            {
                ((System.Windows.Controls.Image)grid_Images.Children[i]).Source = null;
            }

            for (int i = 0; i < tempProduct.Files.Count && i < 4; i++) //Sets the UI images to display images related to the product
            {
                if (!string.IsNullOrEmpty(tempProduct.Files[i].URI))
                {
                    ((System.Windows.Controls.Image)grid_Images.Children[i]).Source =
                        new BitmapImage(new Uri(tempProduct.Files[i].URI));

                    //((System.Windows.Controls.TextBox)grid_urlInputs.Children[i]).Text = tempProduct.Files[i].URI;
                }
            }
        }

        private void Button_GoogleWebOpen_OnClick(object sender, RoutedEventArgs e)
        {
            // Open a webbrowser with coordinates equaling to longtitude and latitude for a product which will be shown on Google Maps
            string number1, number2;
            number1 = tempProduct.Longitude.ToString().Replace(",", ".");
            number2 = tempProduct.Latitude.ToString().Replace(",", ".");
            Process.Start("https://www.google.com/maps?q=" + number2 + "," + number1);
        }
    }
}
