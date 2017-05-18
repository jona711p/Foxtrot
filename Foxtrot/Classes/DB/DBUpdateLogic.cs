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

                //command.Parameters.Add("@ID", SqlDbType.Int).Value = inputActor.ID;
                command.Parameters.Add("@FK_UserID", SqlDbType.Int).Value = inputActor.UserID;
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
        public static void UpdateAdmin(Administrator inputAdmin)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("spUpdateAdmin", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@FK_UserID", SqlDbType.Int).Value = inputAdmin.UserID;
                command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = inputAdmin.FirstName;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = inputAdmin.LastName;
                command.Parameters.Add("@WorkPhone", SqlDbType.Int).Value = inputAdmin.WorkPhone;
                command.Parameters.Add("@WorkEmail", SqlDbType.NVarChar).Value = inputAdmin.WorkEmail;
                command.Parameters.Add("@WorkFax", SqlDbType.Int).Value = inputAdmin.WorkFax;

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
                command.Parameters.Add("@ContactPhone", SqlDbType.Int).Value = inputProduct.ContactPhone[0];
                command.Parameters.Add("@ContactEmail", SqlDbType.NVarChar).Value = inputProduct.ContactEmail[0];
                command.Parameters.Add("@ContactFax", SqlDbType.Int).Value = inputProduct.ContactFax[0];
                command.Parameters.Add("@Price", SqlDbType.Float).Value = inputProduct.Price;
                command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = inputProduct.Description;
                command.Parameters.Add("@ExtraDescription", SqlDbType.NVarChar).Value = inputProduct.ExtraDescription[0].Description;
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
        public static void UpdateEvent(Event tempevent)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            //try
            {
                SqlCommand command = new SqlCommand("spUpdateEvent", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@ID", SqlDbType.Int).Value = tempevent.ID;
                command.Parameters.Add("Name", SqlDbType.NVarChar).Value = tempevent.Name;
                command.Parameters.Add("Address", SqlDbType.NVarChar).Value = tempevent.Address;
                command.Parameters.Add("Longtitude", SqlDbType.Float).Value = tempevent.Longitude;
                command.Parameters.Add("Latitude", SqlDbType.Float).Value = tempevent.Latitude;
                command.Parameters.Add("Description", SqlDbType.NVarChar).Value = tempevent.Description;
                command.Parameters.Add("ExtraDescription", SqlDbType.NVarChar).Value = tempevent.ExtraDescription[0].Description;
                command.Parameters.Add("CanonicalUrl", SqlDbType.NVarChar).Value = tempevent.CanonicalUrl;
                command.Parameters.Add("Website", SqlDbType.NVarChar).Value = tempevent.Website;
                command.Parameters.Add("Availability", SqlDbType.Bit).Value = tempevent.Availability;
                command.Parameters.Add("FK_CityID", SqlDbType.Bit).Value = tempevent.Cities.ID;
                command.Parameters.Add("FK_ActorID", SqlDbType.Bit).Value = tempevent.ActorID;

                command.ExecuteNonQuery();

                connection = DBConnectionLogic.DisconnectFromDB(connection);

            }
            //catch (Exception)
            //{

            //    throw;
            //}
        }
    }
}
