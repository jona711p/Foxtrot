using Classes;
using System;
using System.Data.SqlClient;

namespace Foxtrot.Classes
{
    class DBShowProducts
    {
        public static SqlConnection connection = null;

        public static Product FillTable(Product product)
        {
            connection = DBConnectionLogic.ConnectToDB(connection);

            SqlCommand command = new SqlCommand(
                @"SELECT * FROM PRODUCTS", connection);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            connection = DBConnectionLogic.DisconnectFromDB(connection);
            return product;
        }
    }
}
