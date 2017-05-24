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
        public static void DeleteFile(File inputFile)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                    SqlCommand command = new SqlCommand("DELETE FROM Files WHERE ID = @FileID", connection);

                    command.Parameters.Add("@FileID", SqlDbType.Int).Value = inputFile.ID;

                    command.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);
        }
        public static void DeleteProduct(Product inputProduct)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM Products WHERE ID = @ProductID", connection);

                command.Parameters.Add("@ProductID", SqlDbType.Int).Value = inputProduct.ID;

                command.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);
        }
        public static void DeleteOpeningHour(OpeningHour inputTimes)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM OpeningHours WHERE ID = @OpeningHourID", connection);

                command.Parameters.Add("@OpeningHourID", SqlDbType.Int).Value = inputTimes.ID;

                command.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);
        }

        public static void DeleteRelCombiProducts(CombiProduct inputCombiProduct)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM rel_CombiProducts WHERE FK_CombiProductID = @CombiProductID", connection);

                command.Parameters.Add("@CombiProductID", SqlDbType.Int).Value = inputCombiProduct.ID;

                command.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteCombiProducts(CombiProduct inputCombiProduct)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM CombiProducts WHERE ID = @CombiProductID", connection);

                command.Parameters.Add("@CombiProductID", SqlDbType.Int).Value = inputCombiProduct.ID;

                command.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
