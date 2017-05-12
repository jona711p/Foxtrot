using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace Classes
{
    /// <summary>
    /// Jonas Lykke, Mikael Paaske & Thomas Nielsen
    /// </summary>
    class XMLDBReadLogic
    {
        public static int DupeCheckActors(Actor inputActor)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("SELECT ID FROM Actors WHERE CompanyName = @CompanyName", connection);

                command.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = inputActor.CompanyName;

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    return int.Parse(reader[0].ToString());
                }

                connection = DBConnectionLogic.DisconnectFromDB(connection);

                return int.Parse(XMLDBWriteLogic.WriteActors(inputActor).ToString());
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<int> DupeCheckList(string idName, string tableName)
        {
            DataTable dt = new DataTable();
            List<int> dupeCheckList = new List<int>();

            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("SELECT " + idName + " FROM " + tableName, connection);

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
    }
}