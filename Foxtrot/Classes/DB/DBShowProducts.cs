using Classes;
using System;
using System.Data.SqlClient;

namespace Foxtrot.Classes
{
    class DBShowProducts
    {
        private static SqlConnection connection = null;

        public static Product FillTable(Product product)
        {
            product.ProductTable.Clear();
            connection = DBConnectionLogic.ConnectToDB(connection);
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(
                    "SELECT * FROM Products", connection);
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
