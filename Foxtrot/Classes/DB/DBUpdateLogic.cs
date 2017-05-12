using System;
using System.Data;
using System.Data.SqlClient;

namespace Classes
{
    class DBUpdateLogic
    {
        public static void UpdateActors(Actor inputActor)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("spUpdateActors", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@ID", SqlDbType.Int).Value = inputActor.ID;
                command.Parameters.Add("@FK_UserID", SqlDbType.Int).Value = inputActor.User_ID;
                command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = inputActor.FirstName;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = inputActor.LastName;
                command.Parameters.Add("@WorkPhone", SqlDbType.Int).Value = inputActor.WorkPhone;
                command.Parameters.Add("@WorkEmail", SqlDbType.NVarChar).Value = inputActor.WorkEmail;
                command.Parameters.Add("@WorkFax", SqlDbType.Int).Value = inputActor.WorkFax;
                command.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = inputActor.CompanyName;

                command.ExecuteNonQuery();

                connection = DBConnectionLogic.DisconnectFromDB(connection);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
