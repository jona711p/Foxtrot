using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Classes
{
    class DBReadLogic
    {
        public static int DupeCheckActorsFromDB(Actor actor)
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

        public static List<int> DupeCheckListFromDB(string id, string tableName)
        {
            List<int> dupeCheckList = new List<int>();

            SqlConnection connection = null;

            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("SELECT " + id + " FROM " + tableName, connection);

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
        public static Dictionary<string, int> FillAdminList(User inputUser)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);
            inputUser.UserList = new Dictionary<string, int>();
            inputUser.UserList.Clear();

            try
            {
                SqlCommand command = new SqlCommand("Select FirstName, LastName, Permission from Administrators inner join Users on FK_UserID = Users.ID", connection);
                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());

                foreach (DataRow row in dt.Rows)
                {
                    inputUser.UserList.Add("Admin - " + (string)row[0] + " " + (string)row[1], (int)row[2]); //{0} er 'FirstName' {1} er 'LastName' {2} er 'permission'
                }
            }
            catch (Exception)
            {
                throw;
            }
            connection = DBConnectionLogic.DisconnectFromDB(connection);
            return inputUser.UserList;
        }
        public static Dictionary<string,int> FillActorList(User inputUser)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("SELECT CompanyName, Permission FROM Actors", connection);
                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());

                foreach (DataRow row in dt.Rows)
                {
                    inputUser.UserList.Add("Aktør - " + (string)row[0], (int)row[1]); //{0} er 'companyName' {1} er 'permission'
                }
            }
            catch (Exception)
            {
                throw;
            }
            connection = DBConnectionLogic.DisconnectFromDB(connection);
            return inputUser.UserList;
        }
    }
}