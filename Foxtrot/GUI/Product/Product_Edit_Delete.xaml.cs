﻿using System;
using System.Collections.Generic;
using System.Data;
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
using Classes;

namespace Foxtrot.GUI.Product
{
    /// <summary>
    /// Interaction logic for Product_Edit_Delete.xaml
    /// </summary>
    public partial class Product_Edit_Delete : Page
    {
        private bool Availibility;
        public City tempCity = new City();
        Classes.Product tempProduct = new Classes.Product();

        public Product_Edit_Delete()
        {
            InitializeComponent();
            tempProduct.ProductTable = new DataTable();
            DBReadLogic.FillProductTable(tempProduct.ProductTable);
            DataContext = tempProduct;

        }
    }
}
