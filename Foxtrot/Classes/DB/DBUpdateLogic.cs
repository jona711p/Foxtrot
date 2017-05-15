using System;
using System.Data;
using System.Data.SqlClient;

namespace Foxtrot.Classes.DB
{
    class DBUpdateLogic
    {
        public static void UpdateActor(Actor inputActor)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("spUpdateActor", connection);
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
        public static void UpdateProduct(Product inputProduct)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("spUpdateProduct", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@ID", SqlDbType.Int).Value = inputProduct.ID;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = inputProduct.Name;
                command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = inputProduct.Address;
                command.Parameters.Add("@Longitude", SqlDbType.Float).Value = inputProduct.Longitude;
                command.Parameters.Add("@Latitude", SqlDbType.Float).Value = inputProduct.Latitude;
                command.Parameters.Add("@ContactPhone", SqlDbType.Int).Value = inputProduct.ContactPhone;
                command.Parameters.Add("@ContactEmail", SqlDbType.NVarChar).Value = inputProduct.ContactEmail;
                command.Parameters.Add("@ContactFax", SqlDbType.Int).Value = inputProduct.ContactFax;
                command.Parameters.Add("@Price", SqlDbType.Float).Value = inputProduct.Price;
                command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = inputProduct.Description;
                command.Parameters.Add("@ExtraDescription", SqlDbType.NVarChar).Value = inputProduct.ExtraDescription;
                command.Parameters.Add("@Availability", SqlDbType.Bit).Value = inputProduct.Availability;
                command.Parameters.Add("@Website", SqlDbType.NVarChar).Value = inputProduct.Website;
                command.Parameters.Add("@CanonicalUrl", SqlDbType.NVarChar).Value = inputProduct.CanonicalUrl;
                command.Parameters.Add("@FK_CityID", SqlDbType.Int).Value = inputProduct.Cities.ID;

                command.ExecuteNonQuery();

                connection = DBConnectionLogic.DisconnectFromDB(connection);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
}
