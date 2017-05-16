using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace Foxtrot.Classes.DB
{
    /// <summary>
    /// Jonas Lykke, Mikael Paaske & Thomas Nielsen
    /// </summary>
    class DBReadLogic
    {
        public static bool DupeCheckActor(Actor inputActor)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Actors WHERE CompanyName = @CompanyName", connection);

                command.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = inputActor.CompanyName;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    return true;
                }

                connection = DBConnectionLogic.DisconnectFromDB(connection);

                return false;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool DupeCheckAdmin(Administrator inputAdministrator)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command =
                    new SqlCommand("SELECT * FROM Users WHERE FirstName = @FirstName AND LastName = @LastName",
                        connection);

                command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = inputAdministrator.FirstName;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = inputAdministrator.LastName;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    return true;
                }

                connection = DBConnectionLogic.DisconnectFromDB(connection);

                return false;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int GetIDFromUser(string tableName, int userID)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("SELECT ID FROM " + tableName + " WHERE FK_UserID = " + userID, connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    return int.Parse(reader[0].ToString());
                }

                connection = DBConnectionLogic.DisconnectFromDB(connection);

                return 0;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static User GetUserInfo(User inputUser)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("spGetUserInfo", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@ID", SqlDbType.Int).Value = inputUser.ID;

                SqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    return null;
                }

                reader.Read();
                inputUser.Permission = int.Parse(reader["Permission"].ToString());
                inputUser.FirstName = reader["FirstName"].ToString();
                inputUser.LastName = reader["LastName"].ToString();
                inputUser.WorkPhone = int.Parse(reader["Permission"].ToString());
                inputUser.WorkEmail = reader["Permission"].ToString();
                inputUser.WorkFax = int.Parse(reader["Permission"].ToString());

                connection = DBConnectionLogic.DisconnectFromDB(connection);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return inputUser;
        }

        public static List<KeyValuePair<int, string>> FillAdminActorList(
            List<KeyValuePair<int, string>> inputList)
        {
            if (inputList != null)
            {
                inputList = new List<KeyValuePair<int, string>>();
            }

            DataTable dt = new DataTable();

            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command1 = new SqlCommand("spFillAdminList", connection);
                command1.CommandType = CommandType.StoredProcedure;

                dt.Load(command1.ExecuteReader());

                foreach (DataRow row in dt.Rows)
                {
                    int tempInt = int.Parse(row["FK_UserID"].ToString());
                    string tempString = row["FirstName"] + " " + row["LastName"];
                    inputList.Add(new KeyValuePair<int, string>(tempInt, tempString));
                }

                SqlCommand command2 = new SqlCommand("spFillActorList", connection);
                command2.CommandType = CommandType.StoredProcedure;

                dt.Clear();
                dt.Load(command2.ExecuteReader());

                foreach (DataRow row in dt.Rows)
                {
                    int tempInt = int.Parse(row["FK_UserID"].ToString());
                    string tempString = Convert.ToString(row["CompanyName"]);
                    inputList.Add(new KeyValuePair<int, string>(tempInt, tempString));
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);

            return inputList;
        }

        public static Dictionary<string, int> FillCityDictionary(Dictionary<string, int> inputDictionary)
        {
            DataTable dt = new DataTable();
            inputDictionary = new Dictionary<string, int>();

            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("spFillCityDictionary", connection);

                dt.Load(command.ExecuteReader());

                foreach (DataRow row in dt.Rows)
                {
                    string tempString = Convert.ToString(row["Name"]);
                    int tempInt = int.Parse(row["ID"].ToString());
                    inputDictionary.Add(tempString, tempInt);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);

            return inputDictionary;
        }

        public static DataTable FillProductTable(DataTable productTable)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);
            productTable.Clear();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("spFillProductTable", connection);

                adapter.Fill(productTable);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);

            return productTable;
        }
        //public static DataTable FillEventTable(DataTable eventtable)
        //{
        //    SqlConnection connection = null;
        //    connection = DBConnectionLogic.ConnectToDB(connection);
        //    //Producttable clear
        //    try
        //    {
        //        SqlDataAdapter adapter = new SqlDataAdapter("", connection); // Skal laves stored procedure
        //        adapter.Fill();
                
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public static DataTable FillUserTable(DataTable userTable)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("spFillUserTable", connection);

                adapter.Fill(userTable);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);

            return userTable;
        }

        public static Actor GetActorInfo(Actor inputActor)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("spGetActorInfo", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@FK_UserID", SqlDbType.Int).Value = inputActor.User_ID;

                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                inputActor.CompanyName = reader["Firmanavn"].ToString();
                inputActor.FirstName = reader["Fornavn"].ToString();
                inputActor.LastName = reader["Efternavn"].ToString();
                //implenter me convertnullable metoder
                inputActor.WorkPhone = (reader["Telefon nr."].ToString().Equals("")
                    ? null
                    : (int?)int.Parse(reader["Telefon nr."].ToString()));
                inputActor.WorkEmail = reader["E-Mail"].ToString();
                //implenter me convertnullable metoder
                inputActor.WorkFax = (reader["Fax"].ToString().Equals("")
                    ? null
                    : (int?)int.Parse(reader["Fax"].ToString()));
            }

            catch (Exception ex)
            {
                throw ex;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);

            return inputActor;
        }

        public static Administrator GetAdminInfo(Administrator inputAdmin)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("spGetAdminInfo", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@FK_UserID", SqlDbType.Int).Value = inputAdmin.User_ID;
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                inputAdmin.FirstName = reader["FirstName"].ToString();
                inputAdmin.LastName = reader["LastName"].ToString();
                //implenter me convertnullable metoder
                inputAdmin.WorkPhone = (reader["WorkPhone"].ToString().Equals("")
                    ? null
                    : (int?)int.Parse(reader["WorkPhone"].ToString()));
                inputAdmin.WorkEmail = reader["WorkEmail"].ToString();
                //implenter me convertnullable metoder
                inputAdmin.WorkFax = (reader["WorkFax"].ToString().Equals("")
                    ? null
                    : (int?)int.Parse(reader["WorkFax"].ToString()));
            }

            catch (Exception ex)
            {
                throw ex;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);

            return inputAdmin;
        }

        public static Product GetProductInfo(Product inputProduct)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("spGetProductInfo", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@FK_ProductID", SqlDbType.Int).Value = inputProduct.ID;
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                inputProduct.ID = int.Parse(reader["ID"].ToString());
                inputProduct.Name = reader["Name"].ToString();
                inputProduct.Address = reader["Address"].ToString();
                inputProduct.Longitude = DBSortingLogic.ConvertToNullableFloat(reader["Longitude"]);
                inputProduct.Latitude = DBSortingLogic.ConvertToNullableFloat(reader["Latitude"]);

                inputProduct.ContactPhone = new List<int?>()
                {
                    DBSortingLogic.ConvertToNullableInt(reader["ContactPhone"])
                };

                inputProduct.ContactEmail = new List<string>()
                {
                    reader["ContactEmail"].ToString()
                };

                inputProduct.ContactFax = new List<int?>()
                {
                    DBSortingLogic.ConvertToNullableInt(reader["ContactFax"])
                };

                inputProduct.Price = DBSortingLogic.ConvertToNullableFloat(reader["Price"]);
                inputProduct.Description = reader["Description"].ToString();

                inputProduct.ExtraDescription = new List<ExtraDescription>();
                ExtraDescription tempExtraDescription = new ExtraDescription();
                inputProduct.ExtraDescription.Add(tempExtraDescription);
                tempExtraDescription.Description = reader["ExtraDescription"].ToString();


                inputProduct.Availability = Convert.ToBoolean(reader["Availability"].ToString());
                inputProduct.Website = reader["Website"].ToString();
                inputProduct.CanonicalUrl = reader["CanonicalUrl"].ToString();
                inputProduct.ActorID = DBSortingLogic.ConvertToNullableInt(reader["userID"]);

                inputProduct.Cities = new City();
                inputProduct.Cities.Name = reader["CityName"].ToString();
            }

            catch (Exception ex)
            {
                throw ex;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);

            return inputProduct;
        }
        public static Product GetFileInfo(Product inputProduct)
        {
            DataTable dt = new DataTable();
            inputProduct.Files = new List<File>();

            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            // try
            {
                SqlCommand command = new SqlCommand("spGetProductFiles", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ProductID", SqlDbType.Int).Value = inputProduct.ID;
                dt.Load(command.ExecuteReader());

                foreach (DataRow row in dt.Rows) // skipper altid
                {
                    File tempFile = new File();
                    tempFile.ID = int.Parse(row["ID"].ToString());
                    tempFile.URI = Convert.ToString(row["URI"]);
                    inputProduct.Files.Add(tempFile);
                }
            }

            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            connection = DBConnectionLogic.DisconnectFromDB(connection);

            return inputProduct;
        }
    }
}