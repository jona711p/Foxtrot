using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Classes
{
    class DBReadLogic
    {
        public static int DupeCheckActorsInDB(Actor actor)
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

                return int.Parse(DBWriteLogic.WriteActorToDB(actor).ToString());

            }

            catch (Exception ex)
            {
                throw ex;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);
        }

        public static List<int> DupeCheckListFromDB(string tableName)
        {
            List<int> dupeCheckList = new List<int>();

            SqlConnection connection = null;

            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("SELECT ID FROM " + tableName, connection);

                DataTable dt = new DataTable();
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
