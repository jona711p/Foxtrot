using System;
using System.Data;
using System.Data.SqlClient;

namespace Foxtrot.Classes.DB
{
    class DBUpdateLogic
    {
        public static void UpdateAdmin(Administrator inputAdmin)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("spUpdateAdmin", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@UserID", SqlDbType.Int).Value = inputAdmin.UserID;
                command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = inputAdmin.FirstName;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = inputAdmin.LastName;
                command.Parameters.Add("@WorkPhone", SqlDbType.Int).Value = inputAdmin.WorkPhone;
                command.Parameters.Add("@WorkEmail", SqlDbType.NVarChar).Value = inputAdmin.WorkEmail;
                command.Parameters.Add("@WorkFax", SqlDbType.Int).Value = inputAdmin.WorkFax;

                command.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);
        }

        public static void UpdateActor(Actor inputActor)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("spUpdateActor", connection);
                command.CommandType = CommandType.StoredProcedure;
                
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = inputActor.UserID;
                command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = inputActor.FirstName;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = inputActor.LastName;
                command.Parameters.Add("@WorkPhone", SqlDbType.Int).Value = inputActor.WorkPhone;
                command.Parameters.Add("@WorkEmail", SqlDbType.NVarChar).Value = inputActor.WorkEmail;
                command.Parameters.Add("@WorkFax", SqlDbType.Int).Value = inputActor.WorkFax;
                command.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = inputActor.CompanyName;

                command.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);
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
                command.Parameters.Add("@FK_MainCategoryID", SqlDbType.Int).Value = inputProduct.MainCategories.ID;
                command.Parameters.Add("@FK_CategoryID", SqlDbType.Int).Value = inputProduct.Categories.ID;
                command.Parameters.Add("@FK_OpeningHourID", SqlDbType.Int).Value = inputProduct.OpeningHours.ID;

                command.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);
        }
        public static void UpdateOpeningHours(OpeningHour inputTimes)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("spUpdateOpeningHours", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@ID", SqlDbType.Int).Value = inputTimes.ID;
                command.Parameters.Add("@StartDate", SqlDbType.Date).Value = inputTimes.StartDate;
                command.Parameters.Add("@EndDate", SqlDbType.Date).Value = inputTimes.EndDate;
                command.Parameters.Add("@StartTime", SqlDbType.Time).Value = inputTimes.StartTime.Value.TimeOfDay;
                command.Parameters.Add("@EndTime", SqlDbType.Time).Value = inputTimes.EndTime.Value.TimeOfDay;
                command.Parameters.Add("@Monday", SqlDbType.Bit).Value = inputTimes.Monday;
                command.Parameters.Add("@Tuesday", SqlDbType.Bit).Value = inputTimes.Tuesday;
                command.Parameters.Add("@Wednesday", SqlDbType.Bit).Value = inputTimes.Wednesday;
                command.Parameters.Add("@Thursday", SqlDbType.Bit).Value = inputTimes.Thursday;
                command.Parameters.Add("@Friday", SqlDbType.Bit).Value = inputTimes.Friday;
                command.Parameters.Add("@Saturday", SqlDbType.Bit).Value = inputTimes.Saturday;
                command.Parameters.Add("@Sunday", SqlDbType.Bit).Value = inputTimes.Sunday;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            connection = DBConnectionLogic.DisconnectFromDB(connection);
        }
        public static void UpdateFiles(Product inputProduct)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                for (int i = 0; i < inputProduct.Files.Count; i++)
                {
                    if (inputProduct.Files[i].ID == null)

                    {
                        //spcreatefile
                    }
                    else if(inputProduct.Files[i].ID != null && string.IsNullOrEmpty(inputProduct.Files[i].URI))
                    {
                        //slet fil med id
                    }
                    else
                    {
                        SqlCommand command = new SqlCommand("spUpdateFiles", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@FileID", SqlDbType.Int).Value = inputProduct.Files[i].ID;
                        command.Parameters.Add("@Uri", SqlDbType.Date).Value = inputProduct.Files[i].URI;
                        command.ExecuteNonQuery();
                    }

                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            connection = DBConnectionLogic.DisconnectFromDB(connection);
        }
    }
}
