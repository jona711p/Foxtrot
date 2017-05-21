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
        public static bool DupeCheckAdmin(Administrator inputAdministrator)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM viewAdmins WHERE FirstName = @FirstName AND LastName = @LastName", connection);

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

        public static bool DupeCheckActor(Actor inputActor)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM viewActors WHERE CompanyName = @CompanyName", connection);

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

        public static List<KeyValuePair<int, string>> FillAdminActorList(List<KeyValuePair<int, string>> inputList)
        {
            if (inputList != null)
            {
                inputList = new List<KeyValuePair<int, string>>();
            }

            inputList.Clear();
            DataTable dt = new DataTable();

            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command1 = new SqlCommand("SELECT UserID, FirstName, LastName FROM viewAdmins ORDER BY FirstName", connection);

                dt.Load(command1.ExecuteReader());

                foreach (DataRow row in dt.Rows)
                {
                    int tempInt = int.Parse(row["UserID"].ToString());
                    string tempString = row["FirstName"] + " " + row["LastName"];
                    inputList.Add(new KeyValuePair<int, string>(tempInt, tempString));
                }

                SqlCommand command2 = new SqlCommand("SELECT UserID, CompanyName FROM viewActors ORDER BY CompanyName", connection);

                dt.Clear();
                dt.Load(command2.ExecuteReader());

                foreach (DataRow row in dt.Rows)
                {
                    int tempInt = int.Parse(row["UserID"].ToString());
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

        public static User GetUserInfo(User inputUser)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE ID = @ID", connection);

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
                inputUser.WorkPhone = reader["WorkPhone"].ToString() == null || reader["WorkPhone"].ToString().Length == 0 ? null : (int?)int.Parse(reader["WorkPhone"].ToString());
                inputUser.WorkEmail = reader["WorkEmail"].ToString();
                inputUser.WorkFax = reader["WorkFax"].ToString() == null || reader["WorkFax"].ToString().Length == 0 ? null : (int?)int.Parse(reader["WorkFax"].ToString());
            }

            catch (Exception ex)
            {
                throw ex;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);

            return inputUser;
        }

        public static Administrator GetAdminInfo(Administrator inputAdmin)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("spGetAdminInfo", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@UserID", SqlDbType.Int).Value = inputAdmin.UserID;

                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                inputAdmin.FirstName = reader["Fornavn"].ToString();
                inputAdmin.LastName = reader["Efternavn"].ToString();
                inputAdmin.WorkPhone = reader["Arbejds Telefon"].ToString() == null || reader["Arbejds Telefon"].ToString().Length == 0 ? null : (int?)int.Parse(reader["Arbejds Telefon"].ToString());
                inputAdmin.WorkEmail = reader["Arbejds Email"].ToString();
                inputAdmin.WorkFax = reader["Arbejds Fax"].ToString() == null || reader["Arbejds Telefon"].ToString().Length == 0 ? null : (int?)int.Parse(reader["Arbejds Fax"].ToString());
            }

            catch (Exception ex)
            {
                throw ex;
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

                command.Parameters.Add("@UserID", SqlDbType.Int).Value = inputActor.UserID;

                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                inputActor.CompanyName = reader["Firmanavn"].ToString();
                inputActor.FirstName = reader["Fornavn"].ToString();
                inputActor.LastName = reader["Efternavn"].ToString();
                inputActor.WorkPhone = reader["Arbejds Telefon"].ToString() == null || reader["Arbejds Telefon"].ToString().Length == 0 ? null : (int?)int.Parse(reader["Arbejds Telefon"].ToString());
                inputActor.WorkEmail = reader["Arbejds Email"].ToString();
                inputActor.WorkFax = reader["Arbejds Fax"].ToString() == null || reader["Arbejds Telefon"].ToString().Length == 0 ? null : (int?)int.Parse(reader["Arbejds Fax"].ToString());
            }

            catch (Exception ex)
            {
                throw ex;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);

            return inputActor;
        }

        public static List<KeyValuePair<int, string>> FillCityList(List<KeyValuePair<int, string>> inputList)
        {
            if (inputList != null)
            {
                inputList = new List<KeyValuePair<int, string>>();
            }

            inputList.Clear();
            DataTable dt = new DataTable();

            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Cities ORDER BY Name", connection);

                dt.Load(command.ExecuteReader());

                foreach (DataRow row in dt.Rows)
                {
                    int tempInt = int.Parse(row["ID"].ToString());
                    string tempString = Convert.ToString(row["Name"]);
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

        public static DataTable FillUserTable(DataTable userTable)
        {
            userTable.Clear();

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

        public static DataTable FillProductTable(DataTable productTable)
        {
            productTable.Clear();

            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM viewProductsEditTable", connection);

                adapter.Fill(productTable);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);

            return productTable;
        }

        public static Product GetProductInfo(Product inputProduct)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("spGetProductInfo", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@ProductID", SqlDbType.Int).Value = inputProduct.ID;

                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                inputProduct.ID = int.Parse(reader["ProductID"].ToString());
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
                
                inputProduct.Availability = bool.Parse(reader["Availability"].ToString());
                inputProduct.Website = reader["Website"].ToString();
                inputProduct.CanonicalUrl = reader["CanonicalUrl"].ToString();
                inputProduct.UserID = DBSortingLogic.ConvertToNullableInt(reader["UserID"]);

                inputProduct.Cities = new City();
                inputProduct.Cities.Name = reader["City"].ToString();
            }

            catch (Exception ex)
            {
                throw ex;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);

            return inputProduct;
        }

        public static Product GetProductFileInfo(Product inputProduct)
        {
            DataTable dt = new DataTable();
            inputProduct.Files = new List<File>();

            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("spGetProductFiles", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@ProductID", SqlDbType.Int).Value = inputProduct.ID;
                dt.Load(command.ExecuteReader());

                foreach (DataRow row in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(row["FileID"].ToString()) &&
                        !string.IsNullOrEmpty(row["URI"].ToString()))
                    {

                        File tempFile = new File();
                        tempFile.ID = int.Parse(row["FileID"].ToString());
                        tempFile.URI = Convert.ToString(row["URI"]);
                        inputProduct.Files.Add(tempFile);
                    }
                }
           }

            catch (Exception ex)
            {
                throw ex;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);

            return inputProduct;
        }
    }
}