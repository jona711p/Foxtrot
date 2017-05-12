using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Classes
{
    /// <summary>
    /// Jonas Lykke, Mikael Paaske & Thomas Nielsen
    /// </summary>
    public class XMLDBWriteLogic
    {
        public static void WriteAdministrators(Administrator inputAdministrator)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);
            try
            {
                SqlCommand command = new SqlCommand("spWriteAdministrators", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Permission", SqlDbType.Int).Value = inputAdministrator.Permission;
                command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = inputAdministrator.FirstName;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = inputAdministrator.LastName;
                command.Parameters.Add("@WorkPhone", SqlDbType.Int).Value = inputAdministrator.WorkPhone;
                command.Parameters.Add("@WorkEmail", SqlDbType.NVarChar).Value = inputAdministrator.WorkEmail;
                command.Parameters.Add("@WorkFax", SqlDbType.Int).Value = inputAdministrator.WorkFax;

                command.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);
        }


        public static int WriteActors(Actor inputActor)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("spWriteActors", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter outputID = new SqlParameter("@ID", SqlDbType.Int);

                command.Parameters.Add("@Permission", SqlDbType.Int).Value = inputActor.Permission;
                command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = inputActor.FirstName;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = inputActor.LastName;
                command.Parameters.Add("@WorkPhone", SqlDbType.Int).Value = inputActor.WorkPhone;
                command.Parameters.Add("@WorkEmail", SqlDbType.NVarChar).Value = inputActor.WorkEmail;
                command.Parameters.Add("@WorkFax", SqlDbType.Int).Value = inputActor.WorkFax;
                command.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = inputActor.CompanyName;

                command.Parameters.Add(outputID);
                outputID.Direction = ParameterDirection.Output;

                command.ExecuteNonQuery();

                connection = DBConnectionLogic.DisconnectFromDB(connection);

                return int.Parse(outputID.Value.ToString());
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void WriteCategories(List<Category> inputCategories)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            foreach (Category category in inputCategories)
            {
                try
                {
                    SqlCommand command = new SqlCommand("spWriteCategories", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@XMLID", SqlDbType.Int).Value = category.XMLID;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = category.Name;

                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);
        }

        public static void WriteCities(List<City> inputCities)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            foreach (City city in inputCities)
            {
                try
                {
                    SqlCommand command = new SqlCommand("spWriteCities", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@ID", SqlDbType.Int).Value = city.ID;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = city.Name;
                    command.Parameters.Add("@PostalCode", SqlDbType.Int).Value = city.PostalCode;

                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);
        }

        public static void WriteFiles(List<File> inputFiles)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            foreach (File file in inputFiles)
            {
                try
                {
                    SqlCommand command = new SqlCommand("spWriteFiles", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@XMLID", SqlDbType.Int).Value = file.XMLID;
                    command.Parameters.Add("@Uri", SqlDbType.NVarChar).Value = file.URI;

                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);
        }

        public static void WriteMainCategories(List<MainCategory> inputMainCategories)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            foreach (MainCategory mainCategory in inputMainCategories)
            {
                try
                {
                    SqlCommand command = new SqlCommand("spWriteMainCategories", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@XMLID", SqlDbType.Int).Value = mainCategory.XMLID;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = mainCategory.Name;

                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);
        }

        public static void WriteOpeningHours(List<OpeningHour> inputOpeningHours)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            foreach (OpeningHour openingHour in inputOpeningHours)
            {
                try
                {
                    SqlCommand command = new SqlCommand("spWriteOpeningHours", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@XMLID", SqlDbType.Int).Value = openingHour.XMLID;
                    command.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = openingHour.StartDate;
                    command.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = openingHour.EndDate;
                    command.Parameters.Add("@StartTime", SqlDbType.DateTime).Value = openingHour.StartTime;
                    command.Parameters.Add("@Endtime", SqlDbType.DateTime).Value = openingHour.EndTime;
                    command.Parameters.Add("@Monday", SqlDbType.Bit).Value = openingHour.Monday;
                    command.Parameters.Add("@Tuesday", SqlDbType.Bit).Value = openingHour.Tuesday;
                    command.Parameters.Add("@Wednesday", SqlDbType.Bit).Value = openingHour.Wednesday;
                    command.Parameters.Add("@Thursday", SqlDbType.Bit).Value = openingHour.Thursday;
                    command.Parameters.Add("@Friday", SqlDbType.Bit).Value = openingHour.Friday;
                    command.Parameters.Add("@Saturday", SqlDbType.Bit).Value = openingHour.Saturday;
                    command.Parameters.Add("@Sunday", SqlDbType.Bit).Value = openingHour.Sunday;

                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);
        }

        public static void WriteProducts(List<Product> inputProducts)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            foreach (Product product in inputProducts)
            {
                try
                {
                    SqlCommand command = new SqlCommand("spWriteProducts", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter outputID = new SqlParameter("@ID", SqlDbType.Int);

                    command.Parameters.Add("@XMLID", SqlDbType.Int).Value = product.XMLID;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = product.Name;
                    command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = product.Address;
                    command.Parameters.Add("@Latitude", SqlDbType.Float).Value = product.Latitude;
                    command.Parameters.Add("@Longitude", SqlDbType.Float).Value = product.Longitude;
                    command.Parameters.Add("@ContactPhone", SqlDbType.Int).Value =
                        product.ContactPhone == null
                        || product.ContactPhone.Count == 0
                            ? null
                            : (int?)product.ContactPhone[0].Value;
                    command.Parameters.Add("@ContactEmail", SqlDbType.NVarChar).Value = product.ContactEmail == null
                        ? null
                        : product.ContactEmail[0];
                    command.Parameters.Add("@ContactFax", SqlDbType.Int).Value = product.ContactFax == null
                        ? null
                        : (int?)product.ContactFax[0].Value;
                    command.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = product.CreationDate;
                    command.Parameters.Add("@Price", SqlDbType.Float).Value = product.Price;
                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = product.Description;
                    command.Parameters.Add("@ExtraDescription", SqlDbType.NVarChar).Value =
                        XMLSortingLogic.TryToConvertNodeValueToStringBuilder(product.ExtraDescription);
                    command.Parameters.Add("@Availability", SqlDbType.Bit).Value = product.Availability;
                    command.Parameters.Add("@Website", SqlDbType.NVarChar).Value = product.Website;
                    command.Parameters.Add("@CanonicalUrl", SqlDbType.NVarChar).Value = product.CanonicalUrl;
                    command.Parameters.Add("@FK_CityID", SqlDbType.Int).Value = product.Cities.ID;
                    command.Parameters.Add("@FK_ActorID", SqlDbType.Int).Value = product.ActorID;

                    command.Parameters.Add(outputID);
                    outputID.Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();

                    product.ID = int.Parse(outputID.Value.ToString());
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);
        }

        public static void WriteRelCategories(List<Product> inputProducts)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);
            {
                foreach (Product product in inputProducts)
                {
                    try
                    {
                        SqlCommand command = new SqlCommand("spWriteRel_Categories", connection);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@FK_ProductID", SqlDbType.Int).Value = product.ID;
                        command.Parameters.Add("@XMLID", SqlDbType.Int).Value = product.Categories.XMLID;

                        command.ExecuteNonQuery();
                    }

                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);
        }

        public static void WriteRelCombiProducts(List<Product> inputProducts)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            foreach (Product product in inputProducts)
            {
                if (product.CombiProducts != null)
                {
                    foreach (CombiProduct combiProduct in product.CombiProducts)
                    {
                        try
                        {
                            SqlCommand command = new SqlCommand("spWriteRel_CombiProducts", connection);
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("@FK_ProductID", SqlDbType.Int).Value = product.ID;
                            command.Parameters.Add("@FK_CombiProductID", SqlDbType.Int).Value = combiProduct.ID;

                            command.ExecuteNonQuery();
                        }

                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }

                else break;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);
        }

        public static void WriteRelEventsProducts(List<Product> inputProducts)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            foreach (Product product in inputProducts)
            {
                if (product.Events != null)
                {
                    foreach (Event events in product.Events)
                    {
                        try
                        {
                            SqlCommand command = new SqlCommand("spWriteRel_EventsProducts", connection);
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("@FK_ProductID", SqlDbType.Int).Value = product.ID;
                            command.Parameters.Add("@FK_EventID", SqlDbType.Int).Value = events.ID;

                            command.ExecuteNonQuery();
                        }

                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }

                else break;
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);
        }

        public static void WriteRelFiles(List<Product> inputProducts)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            foreach (Product product in inputProducts)
            {
                foreach (File file in product.Files)
                {
                    try
                    {
                        SqlCommand command = new SqlCommand("spWriteRel_Files", connection);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@FK_ProductID", SqlDbType.Int).Value = product.ID;
                        command.Parameters.Add("@XMLID", SqlDbType.Int).Value = file.XMLID;

                        command.ExecuteNonQuery();
                    }

                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);
        }

        public static void WriteRelMainCategories(List<Product> inputProducts)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);
            {
                foreach (Product product in inputProducts)
                {
                    try
                    {
                        SqlCommand command = new SqlCommand("spWriteRel_MainCategories", connection);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@FK_ProductID", SqlDbType.Int).Value = product.ID;
                        command.Parameters.Add("@XMLID", SqlDbType.Int).Value = product.MainCategories.XMLID;

                        command.ExecuteNonQuery();
                    }

                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);
        }

        public static void WriteRelOpeningHours(List<Product> inputProducts)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            foreach (Product product in inputProducts)
            {
                foreach (OpeningHour openingHour in product.OpeningHours)
                {
                    try
                    {
                        SqlCommand command = new SqlCommand("spWriteRel_OpeningHours", connection);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@FK_ProductID", SqlDbType.Int).Value = product.ID;
                        command.Parameters.Add("@FK_EventID", SqlDbType.Int).Value = product.Events;
                        command.Parameters.Add("@XMLID", SqlDbType.Int).Value = openingHour.XMLID;

                        command.ExecuteNonQuery();
                    }

                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            connection = DBConnectionLogic.DisconnectFromDB(connection);
        }
    }
}