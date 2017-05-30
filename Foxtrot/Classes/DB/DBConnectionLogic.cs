using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Foxtrot.Classes.DB
{
    /// <summary>
    /// Jonas Lykke
    /// </summary>
    class DBConnectionLogic
    {
        public static SqlConnection ConnectToDB(SqlConnection connection)
        {
            try
            {
                if (connection == null)
                    connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Foxtrot"].ConnectionString);
                connection.Open();
            }

            catch (Exception)
            {
                MessageBox.Show("Der Kunne ikke Oprettes Forbindelse til Databaseserveren!");
            }

            return connection;
        }

        public static SqlConnection DisconnectFromDB(SqlConnection connection)
        {
            try
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Der Kunne ikke Oprettes Forbindelse til Databaseserveren!");
            }

            return connection;
        }
    }
}
