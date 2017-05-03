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

        public static void WriteActorsToDB(List<Actor> actors)
        {
            foreach (Actor actor in actors)
            {
                int notCoingToBeUsedAtThisPoint = WriteActorToDB(actor);
            }
        }

        public static int WriteActorToDB(Actor actor)
        {
            SqlConnection connection = null;

            connection = ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("spWriteActorsToDB", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter outputID = new SqlParameter("@ID", SqlDbType.Int);
                command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = actor.FirstName;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = actor.LastName;
                command.Parameters.Add("@WorkPhone", SqlDbType.Int).Value = actor.WorkPhone;
                command.Parameters.Add("@WorkEmail", SqlDbType.NVarChar).Value = actor.WorkEmail;
                command.Parameters.Add("@WorkFax", SqlDbType.Int).Value = actor.WorkFax;

                command.Parameters.Add("@Permission", SqlDbType.Int).Value = actor.Permission;
                command.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = actor.CompanyName;

                command.Parameters.Add(outputID);
                outputID.Direction = ParameterDirection.Output;

                command.ExecuteNonQuery();

                return int.Parse(outputID.Value.ToString());
            }

            catch (Exception ex)
            {
                throw ex;
            }

            connection = DisconnectFromDB(connection);
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

        public static void WriteFilesToDB(List<File> files)
        {
            SqlConnection connection = null;

            connection = ConnectToDB(connection);

            foreach (File file in files)
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

        public static void WriteOpeningHoursToDB(List<OpeningHour> openingHours)
        {
            SqlConnection connection = null;

            connection = ConnectToDB(connection);

            foreach (OpeningHour openingHour in openingHours)
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
                    command.Parameters.Add("@FK_ActorID", SqlDbType.Int).Value = product.ActorID;  //Er en foreign Key , Skal referere aktørID hvilket vil sige aktøren skal oprettes først
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
                    command.Parameters.Add("@EventID", SqlDbType.Int).Value = product.Categories.ID; //Er en foreign Key 

                    command.ExecuteNonQuery();

                    foreach (OpeningHour OpeningHour in product.OpeningHours)
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

        public static void WriteRelFileTable(List<Product> input_product)
        {
            SqlConnection connection = null;
            connection = ConnectToDB(connection);

            foreach (Product product in input_product)
            {
                foreach (File file in product.Files)
                {
                    try
                    {
                        SqlCommand command = new SqlCommand("spWriteRelFiles", connection);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@FK_ProductID", SqlDbType.Int).Value = product.ID;
                        command.Parameters.Add("@FK_FileID", SqlDbType.Int).Value = file.ID;

                        command.ExecuteNonQuery();
                    }

                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            connection = DisconnectFromDB(connection);
        }
        public static void WriteRelOpeningHoursTable(List<Product> input_product)
        {
            SqlConnection connection = null;

            connection = ConnectToDB(connection);

            foreach (Product product in input_product)
            {
                foreach (OpeningHour time in product.OpeningHours)
                {
                    try
                    {
                        SqlCommand command = new SqlCommand("spWriteRelOpeningHours", connection);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@FK_ProductID", SqlDbType.Int).Value = product.ID;
                        command.Parameters.Add("@FK_OpeningHoursID", SqlDbType.Int).Value = time.ID;
                        command.Parameters.Add("FK_EventID", SqlDbType.Int).Value = product.Event;

                        command.ExecuteNonQuery();
                    }

                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            connection = DisconnectFromDB(connection);
        }
        public static void WriteAdministratorToDB(List<User> users)
        {
            SqlConnection connection = null;

            connection = ConnectToDB(connection);

            foreach (User user in users)
            {
                try
                {
                    SqlCommand command = new SqlCommand("spWriteUsersToDB", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter outputID = new SqlParameter("@ID", SqlDbType.Int);
                    command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = user.FirstName;
                    command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = user.LastName;
                    command.Parameters.Add("@WorkPhone", SqlDbType.Int).Value = user.WorkPhone;
                    command.Parameters.Add("@WorkEmail", SqlDbType.NVarChar).Value = user.WorkEmail;
                    command.Parameters.Add("@WorkFax", SqlDbType.Int).Value = user.WorkFax;

                    command.Parameters.Add(outputID);
                    outputID.Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

            connection = DisconnectFromDB(connection);
        }

        public static int CheckActorDuplicatesInDB(Actor actor)
        {
            SqlConnection connection = null;

            connection = ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("SELECT ID FROM Actors WHERE CompanyName = @CompanyName", connection);

                command.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = actor.CompanyName;
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                string check = reader[0].ToString();

                return check == "" ? WriteActorToDB(actor) : int.Parse(check);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            connection = DisconnectFromDB(connection);
        }

    }
}