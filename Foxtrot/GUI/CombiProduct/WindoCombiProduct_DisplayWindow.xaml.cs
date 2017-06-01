using System;
using System.Collections.Generic;
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
using Foxtrot.Classes;

namespace Foxtrot.GUI.CombiProduct
{
    /// <summary>
    /// Interaction logic for WindoCombiProduct_DisplayWindow.xaml
    /// </summary>
    public partial class WindoCombiProduct_DisplayWindow : Window
    {
        Classes.CombiProduct tempNewCombiProduct = new Classes.CombiProduct();

        public WindoCombiProduct_DisplayWindow(Classes.CombiProduct inputCombiProduct)
        {
            InitializeComponent();
            tempNewCombiProduct = inputCombiProduct;
            ResizeMode = ResizeMode.NoResize;
            DataContext = tempNewCombiProduct;
            FillFieldWithInfo();
        }

        public void FillFieldWithInfo()
        {
            label_CombiProduct_DisplayWindow_Name.Content = tempNewCombiProduct.Name;

            if (tempNewCombiProduct.PackagePrice == 0 || string.IsNullOrEmpty(tempNewCombiProduct.PackagePrice.ToString()) )
            {
                label_CombiProduct_DisplayWindow_PackagePrice.Content = "Gratis";
            }
            else
            {
                label_CombiProduct_DisplayWindow_PackagePrice.Content = tempNewCombiProduct.PackagePrice.ToString();
            }
           


            label_CombiProduct_DisplayWindow_CreationDate.Content = tempNewCombiProduct.CreationDate;



            if (tempNewCombiProduct.Availability == true)
            {
                AvailabilityIndicator.Fill = Brushes.Green;
            }
            else
            {
                AvailabilityIndicator.Fill = Brushes.Red;.
            }
        }
    }
}
