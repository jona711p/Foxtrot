using Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Foxtrot.Classes
{
    public class DBShowProducts
    {
        private static SqlConnection connection = null;

        public static Product FillTable(Product product)
        {
            product.ProductTable.Clear();
            connection = DBConnectionLogic.ConnectToDB(connection);
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(
                    @"SELECT * FROM Products",
                    connection);
                adapter.Fill(product.ProductTable);
            }
            catch (Exception)
            {

                throw;
            }
            connection = DBConnectionLogic.ConnectToDB(connection);
            return product;
        }

    }
}
