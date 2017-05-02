using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Classes
{
    public class DBLogic
    {
        static SqlConnection ConnectToDB(SqlConnection connection)
        {
            try
            {
                if (connection == null)
                    connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Foxtrot"].ConnectionString);
                connection.Open();
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return connection;
        }

        static SqlConnection DisconnectFromDB(SqlConnection connection)
        {
            try
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return connection;
        }

        public static void WriteCitiesToDB(List<City> cities)
        {
            SqlConnection connection = null;

            connection = ConnectToDB(connection);

            foreach (City city in cities)
            {
                try
                {
                    SqlCommand command = new SqlCommand("spWriteCitiesToDB", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@ID", SqlDbType.Int).Value = city.ID;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = city.Name;
                    command.Parameters.Add("@PostalCode", SqlDbType.Int).Value = city.PostalCode;

                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

            connection = DisconnectFromDB(connection);
        }

        public static void WriteOpeningHoursToDB(List<OpeningHours> openingHours)
        {
            SqlConnection connection = null;

            connection = ConnectToDB(connection);

            foreach (OpeningHours openingHour in openingHours)
            {
                try
                {
                    SqlCommand command = new SqlCommand("spWriteOpeningHoursToDB", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@ID", SqlDbType.Int).Value = openingHour.ID;
                    command.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = openingHour.StartDate;
                    command.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = openingHour.EndDate;
                    command.Parameters.Add("@StartTime", SqlDbType.DateTime).Value = openingHour.StartTime;
                    command.Parameters.Add("@Endtime", SqlDbType.DateTime).Value = openingHour.EndTime;
                    command.Parameters.Add("@Monday", SqlDbType.Bit).Value = openingHour.Monday;
                    command.Parameters.Add("@Tuesday", SqlDbType.Bit).Value = openingHour.Tuesday;
                    command.Parameters.Add("@Wednesday", SqlDbType.Bit).Value = openingHour.Wednesday;
                    command.Parameters.Add("@Thursday", SqlDbType.Bit).Value = openingHour.Thursday;
                    command.Parameters.Add("@Friday", SqlDbType.Bit).Value = openingHour.Friday;
                    command.Parameters.Add("@Saturday", SqlDbType.Bit).Value = openingHour.Saturday;
                    command.Parameters.Add("@Sunday", SqlDbType.Bit).Value = openingHour.Sunday;

                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

            connection = DisconnectFromDB(connection);
        }

        public static void WriteMainCategoriesToDB(List<MainCategory> mainCategories)
        {
            SqlConnection connection = null;

            connection = ConnectToDB(connection);

            foreach (MainCategory maincategory in mainCategories)
            {
                try
                {
                    SqlCommand command = new SqlCommand("spWriteMainCategoriesToDB", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@ID", SqlDbType.Int).Value = maincategory.ID;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = maincategory.Name;

                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

            connection = DisconnectFromDB(connection);
        }

        public static void WriteCategoriesToDB(List<Category> categories)
        {
            SqlConnection connection = null;

            connection = ConnectToDB(connection);

            foreach (Category category in categories)
            {
                try
                {
                    SqlCommand command = new SqlCommand("spWriteCategoriesToDB", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@ID", SqlDbType.Int).Value = category.ID;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = category.Name;

                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

            connection = DisconnectFromDB(connection);
        }

        public static void WriteFilesToDB(List<File> Files)
        {
            SqlConnection connection = null;

            connection = ConnectToDB(connection);

            foreach (File file in Files)
            {
                try
                {
                    SqlCommand command = new SqlCommand("spWriteFilesToDB", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@ID", SqlDbType.Int).Value = file.ID;
                    command.Parameters.Add("@Uri", SqlDbType.NVarChar).Value = file.URI;

                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

            connection = DisconnectFromDB(connection);
        }

        public static void WriteProductsToDB(List<Product> products)
        {
            SqlConnection connection = null;

            connection = ConnectToDB(connection);

            foreach (Product product in products)
            {
                int? currentProductID = product.ID; // Gets the ID foreach product, so the FK can be correct!

                try
                {
                    SqlCommand command = new SqlCommand("spWriteProductsToDB", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@ID", SqlDbType.Int).Value = product.ID;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = product.Name;
                    command.Parameters.Add("@FK_ActorID", SqlDbType.NVarChar).Value = product.Actor;  //Er en foreign Key , Skal referere aktørID hvilket vil sige aktøren skal oprettes først
                    command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = product.Address;
                    command.Parameters.Add("@Latitude", SqlDbType.Float).Value = product.Latitude;
                    command.Parameters.Add("@Longitude", SqlDbType.Float).Value = product.Longitude;

                    command.Parameters.Add("@ContactPhone", SqlDbType.Int).Value = product.ContactPhone[0].Value;
                    command.Parameters.Add("@ContactEmail", SqlDbType.NVarChar).Value = product.ContactEmail[0];
                    command.Parameters.Add("@ContactFax", SqlDbType.Int).Value = product.ContactFax[0].Value;

                    command.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = product.CreationDate;
                    command.Parameters.Add("@Price", SqlDbType.Float).Value = product.Price;

                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = product.Description;
                    command.Parameters.Add("@ExtraDescription", SqlDbType.NVarChar).Value = product.ExtraDesription;

                    command.Parameters.Add("@Website", SqlDbType.NVarChar).Value = product.Website;
                    command.Parameters.Add("@CanonicalUrl", SqlDbType.NVarChar).Value = product.CanonicalUrl;

                    command.Parameters.Add("@FK_CityID", SqlDbType.Int).Value = product.Cities.ID; //Er en foreign Key 

                    command.Parameters.Add("@FK_MainCategoryID", SqlDbType.Int).Value = product.MainCategories.ID; //Er en foreign Key 
                    command.Parameters.Add("@FK_CategoryID", SqlDbType.Int).Value = product.Categories.ID; //Er en foreign Key 
                    //command.Parameters.Add("@EventID", SqlDbType.Int).Value = product.Categories.ID; //Er en foreign Key 

                    command.ExecuteNonQuery();

                    foreach (OpeningHours OpeningHour in product.OpeningHours)
                    {
                        command.Parameters.Add("@FK_OpeningHours", SqlDbType.Int).Value = OpeningHour.ID;
                        command.Parameters.Add("@FK_ProductID", SqlDbType.Int).Value = currentProductID;

                        command.ExecuteNonQuery();
                    }

                    foreach (File file in product.Files)
                    {
                        command.Parameters.Add("@FK_Files", SqlDbType.Int).Value = file.ID;
                        command.Parameters.Add("@FK_ProductID", SqlDbType.Int).Value = currentProductID;

                        command.ExecuteNonQuery();
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

            connection = DisconnectFromDB(connection);
        }
    }
}