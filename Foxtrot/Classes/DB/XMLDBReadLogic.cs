using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Foxtrot.Classes.DB
{
    /// <summary>
    /// Jonas Lykke
    /// </summary>
    // Class to see if a data is allready in the database
    class XMLDBReadLogic
    {
        public static int DupeCheckActors(Actor inputActor)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            SqlCommand command = new SqlCommand("SELECT UserID FROM viewActors WHERE CompanyName = @CompanyName", connection);

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

        public static List<int> DupeCheckList(string idName, string tableName)
        {
            DataTable dt = new DataTable();
            List<int> dupeCheckList = new List<int>();

            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            SqlCommand command = new SqlCommand("SELECT " + idName + " FROM " + tableName + " WHERE " + idName + " IS NOT NULL", connection);

            dt.Load(command.ExecuteReader());

            foreach (DataRow row in dt.Rows)
            {
                dupeCheckList.Add(int.Parse(row[0].ToString()));
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);

            return dupeCheckList;
        }
    }
}