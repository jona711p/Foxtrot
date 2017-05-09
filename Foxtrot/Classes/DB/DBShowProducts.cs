﻿using Classes;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Foxtrot.Classes
{
    class DBShowProducts
    {
        // Mikael
        public static SqlConnection connection = null;

        public static Product FillTable(Product product)
        {
            connection = DBConnectionLogic.ConnectToDB(connection);
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("spFillProductList", connection);
                adapter.Fill(product.ProductTable);
            }
            catch (Exception)
            {

                throw;
            }
            connection = DBConnectionLogic.DisconnectFromDB(connection);
            return product;
        }
    }
}
