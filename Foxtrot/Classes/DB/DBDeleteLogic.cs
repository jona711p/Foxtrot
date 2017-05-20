using System;
using System.Data;
using System.Data.SqlClient;

namespace Foxtrot.Classes.DB
{
    class DBDeleteLogic
    {
        public static void DeleteAdmin(Administrator inputAdmin)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM Administrators WHERE FK_UserID = @UserID", connection);

                command.Parameters.Add("@UserID", SqlDbType.Int).Value = inputAdmin.UserID;

                command.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);
        }

        public static void DeleteActor(Actor inputActor)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM Actors WHERE FK_UserID = @UserID", connection);

                command.Parameters.Add("@userID", SqlDbType.Int).Value = inputActor.UserID;

                command.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);
        }
    }
}
