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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Foxtrot.Classes;
using Foxtrot.Classes.DB;

namespace Foxtrot.GUI.CombiProduct
{
    /// <summary>
    /// Interaction logic for CombiProduct_DisplayWindow.xaml
    /// </summary>
    public partial class CombiProduct_DisplayWindow : Page
    {
        Classes.CombiProduct tempCombiProduct = new Classes.CombiProduct();
        public CombiProduct_DisplayWindow(Classes.CombiProduct inputCombiProduct)
        {
            InitializeComponent();
            tempCombiProduct = inputCombiProduct;
            DataContext = tempCombiProduct;
            FillFieldsWithInfo();
        }

        public void FillFieldsWithInfo()
        {
            label_CombiProduct_DisplayWindow_Name.Content = tempCombiProduct.Name;
        }
    }
}
