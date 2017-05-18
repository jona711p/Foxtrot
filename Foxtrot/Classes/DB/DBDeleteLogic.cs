
using System;
using System.Collections.Generic;
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

            //try
            {
                SqlCommand command = new SqlCommand("spDeleteAdmin", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@userID", SqlDbType.Int).Value = inputAdmin.UserID;
                command.ExecuteNonQuery();

            }
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            connection = DBConnectionLogic.DisconnectFromDB(connection);
        }
        public static void DeleteActor(Actor inputActor)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("spDeleteActor", connection);
                command.CommandType = CommandType.StoredProcedure;
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
