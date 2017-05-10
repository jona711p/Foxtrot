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

                return int.Parse(DBWriteLogic.WriteActors(actor).ToString());

            }

            catch (Exception ex)
            {
                throw ex;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);
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

        public static Dictionary<string, int> FillAdminActorDictionary(Dictionary<string, int> adminActorDictionary)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command1 = new SqlCommand("spFillAdminDictionary", connection);
                command1.CommandType = CommandType.StoredProcedure;

                dt.Load(command1.ExecuteReader());

                adminActorDictionary.Add("=== ADMINISTRATOR ===", 0);

                foreach (DataRow row in dt.Rows)
                {
                    string tempString = row["FirstName"] + " " + row["LastName"];
                    int tempInt = int.Parse(row["Permission"].ToString());
                    adminActorDictionary.Add(tempString, tempInt);
                }

                SqlCommand command2 = new SqlCommand("spFillActorDictionary", connection);
                command2.CommandType = CommandType.StoredProcedure;

                dt.Clear();
                dt.Load(command2.ExecuteReader());

                adminActorDictionary.Add("=== AKTØR ===", 0);

                foreach (DataRow row in dt.Rows)
                {
                    string tempString = Convert.ToString(row["CompanyName"]);
                    int tempInt = int.Parse(row["Permission"].ToString());
                    adminActorDictionary.Add(tempString, tempInt);
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
        public static Dictionary<string,int> FillCityDictionary(Dictionary<string,int> cityDictionary)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("SELECT Name, PostalCode FROM Cities", connection);
                dt.Load(command.ExecuteReader());

                foreach (DataRow row in dt.Rows)
                {
                    string tempString = Convert.ToString(row["Name"]);
                    int tempInt = int.Parse(row["PostalCode"].ToString());
                    cityDictionary.Add(tempString,tempInt);
                }
            }

            catch (Exception)
            {
                throw;
            }
            connection = DBConnectionLogic.DisconnectFromDB(connection);

            return cityDictionary;
        }
    }
}