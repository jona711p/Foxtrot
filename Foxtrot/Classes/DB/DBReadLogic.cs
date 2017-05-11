using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Classes
{
    /// <summary>
    /// Jonas Lykke, Mikael Paaske & Thomas Nielsen
    /// </summary>
    class DBReadLogic
    {
        public static bool DupeCheckActor(Actor actor)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Actors WHERE CompanyName = @CompanyName", connection);

                command.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = actor.CompanyName;

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

        public static int DupeCheckActors(Actor actor)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("SELECT ID FROM Actors WHERE CompanyName = @CompanyName", connection);

                command.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = actor.CompanyName;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    return int.Parse(reader[0].ToString());
                }

                connection = DBConnectionLogic.DisconnectFromDB(connection);

                return int.Parse(DBWriteLogic.WriteActors(actor).ToString());
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool DupeCheckAdmin(Administrator administrator)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE FirstName = @FirstName AND LastName = @LastName", connection);

                command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = administrator.FirstName;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = administrator.LastName;

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

        public static List<int> DupeCheckList(string id, string tableName)
        {
            DataTable dt = new DataTable();
            List<int> dupeCheckList = new List<int>();

            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("SELECT " + id + " FROM " + tableName, connection);

                dt.Load(command.ExecuteReader());

                foreach (DataRow row in dt.Rows)
                {
                    dupeCheckList.Add(int.Parse(row[0].ToString()));
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);

            return dupeCheckList;
        }

        public static int GetUserPermission(int userID)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("spGetUserPermission", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@ID", SqlDbType.Int).Value = userID;

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

        public static Dictionary<int, string> FillAdminActorDictionary(Dictionary<int, string> adminActorDictionary)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command1 = new SqlCommand("spFillAdminDictionary", connection);
                command1.CommandType = CommandType.StoredProcedure;

                dt.Load(command1.ExecuteReader());

                foreach (DataRow row in dt.Rows)
                {
                    int tempInt = int.Parse(row["FK_UserID"].ToString());
                    string tempString = row["FirstName"] + " " + row["LastName"];
                    adminActorDictionary.Add(tempInt, tempString);
                }

                SqlCommand command2 = new SqlCommand("spFillActorDictionary", connection);
                command2.CommandType = CommandType.StoredProcedure;

                dt.Clear();
                dt.Load(command2.ExecuteReader());

                foreach (DataRow row in dt.Rows)
                {
                    int tempInt = int.Parse(row["FK_UserID"].ToString());
                    string tempString = Convert.ToString(row["CompanyName"]);
                    adminActorDictionary.Add(tempInt, tempString);
                }
            }

            catch (Exception)
            {
                throw;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);

            return adminActorDictionary;
        }

        public static DataTable FillProductTable(DataTable productTable)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("spFillProductTable", connection);

                adapter.Fill(productTable);
            }

            catch (Exception)
            {
                throw;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);

            return productTable;
        }
        public static DataTable FillUserTable(DataTable userTable)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("spFillUserTable", connection);

                adapter.Fill(userTable);
            }

            catch (Exception)
            {
                throw;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);

            return userTable;
        }
        public static Dictionary<string, int> FillCityDictionary(Dictionary<string, int> cityDictionary)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("spFillCityDictionary", connection);

                dt.Load(command.ExecuteReader());

                foreach (DataRow row in dt.Rows)
                {
                    string tempString = Convert.ToString(row["Name"]);
                    int tempInt = int.Parse(row["PostalCode"].ToString());
                    cityDictionary.Add(tempString, tempInt);
                }
            }

            catch (Exception)
            {
                throw;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);

            return cityDictionary;
        }
        public static Administrator GetAdminInfo(Administrator inputAdmin)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("spGetAdminInfo", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@FK_UserID", SqlDbType.Int).Value = inputAdmin.ID;
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                inputAdmin.FirstName = reader["FirstName"].ToString();
                inputAdmin.LastName = reader["LastName"].ToString();
                inputAdmin.WorkPhone = (reader["WorkPhone"].ToString().Equals("") ? null : (int?)int.Parse(reader["WorkPhone"].ToString()));
                inputAdmin.WorkEmail = reader["WorkEmail"].ToString();
                inputAdmin.WorkFax = (reader["WorkFax"].ToString().Equals("") ? null : (int?)int.Parse(reader["WorkFax"].ToString()));
            }

            catch (Exception e)
            {
                throw;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);

            return inputAdmin;
        }
        public static Actor GetActorInfo(Actor inputActor)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("spGetActorInfo", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@FK_UserID", SqlDbType.Int).Value = inputActor.ID;
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                inputActor.CompanyName = reader["Firmanavn"].ToString();
                inputActor.FirstName = reader["Fornavn"].ToString();
                inputActor.LastName = reader["Efternavn"].ToString();
                inputActor.WorkPhone = (reader["Telefon nr."].ToString().Equals("") ? null : (int?)int.Parse(reader["Telefon nr."].ToString()));
                inputActor.WorkEmail = reader["E-Mail"].ToString();
                inputActor.WorkFax = (reader["Fax"].ToString().Equals("") ? null : (int?)int.Parse(reader["Fax"].ToString()));
            }

            catch (Exception e)
            {
                throw;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);

            return inputActor;
        }
        //public static Product GetProductInfo(Product inputProduct)
        //{
        //    SqlConnection connection = null;
        //    connection = DBConnectionLogic.ConnectToDB(connection);

        //    try
        //    {
        //        SqlCommand command = new SqlCommand("spGetProductInfo", connection);
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.Parameters.Add("@FK_ProductID", SqlDbType.Int).Value = inputProduct.ID;
        //        SqlDataReader reader = command.ExecuteReader();
        //        reader.Read();

        //        object bla = reader[0];

        //    inputProduct.ID = int.Parse(reader["ID"].ToString());
        //        inputProduct.Name = reader["Name"].ToString();
        //        inputProduct.Address = reader["Address"].ToString();
        //        inputProduct.Longitude = float.Parse(reader["Longitude"].ToString());
        //        inputProduct.Latitude = float.Parse(reader["Latitude"].ToString());
        //        inputProduct.ContactPhone = new List<int?>()
        //        {
        //            ConvertToNullableInt(reader["ContactPhone"])
        //        };
        //        inputProduct.ContactEmail = reader["ContactEmail"].ToString();

        //        inputProduct.ContactFax = (reader["Fax"].ToString().Equals("") ? null : (int?)int.Parse(reader["Fax"].ToString()));
        //        inputProduct.Price = ConvertToNullableFloat(reader["Fax"]);
        //        inputProduct.Description = reader["Description"].ToString();
        //        inputProduct.ExtraDescription = new List<ExtraDescription>();
        //        {
        //            reader["ExtraDescription"].ToString();
        //        };
        //        inputProduct.Availability = Convert.ToBoolean(reader["Fax"].ToString());
        //        inputProduct.Website = reader["Website"].ToString();
        //        inputProduct.CanonicalUrl = reader["CanonicalUrl"].ToString();
        //    }

        //    catch (Exception e)
        //    {
        //        throw;
        //    }

        //    connection = DBConnectionLogic.DisconnectFromDB(connection);

        //    return inputProduct;
        //}

        public static int? ConvertToNullableInt(object objectFromReader)
        {
            return objectFromReader.ToString().Equals("") ? null : (int?)int.Parse(objectFromReader.ToString());
        }

        public static float? ConvertToNullableFloat(object objectFromReader)
        {
            return objectFromReader.ToString().Equals("") ? null : (float?)float.Parse(objectFromReader.ToString());
        }
    }
}