using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Foxtrot.GUI.XMLImport;

namespace Foxtrot.Classes.DB
{
    /// <summary>
    /// Jonas Lykke, Mikael Paaske & Thomas Nielsen
    /// </summary>
    public class DBWriteLogic
    {
        //public static void WriteNewProduct(Product inputProducts)
        //{
        //    SqlConnection connection = null;
        //    connection = DBConnectionLogic.ConnectToDB(connection);

        //    try
        //    {
        //        SqlCommand command = new SqlCommand("spWriteNewProducts", connection);
        //        command.CommandType = CommandType.StoredProcedure;

        //        command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = inputProducts.Name;
        //        command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = inputProducts.Address;
        //        command.Parameters.Add("@Latitude", SqlDbType.Float).Value = inputProducts.Latitude;
        //        command.Parameters.Add("@Longitude", SqlDbType.Float).Value = inputProducts.Longitude;
        //        command.Parameters.Add("@ContactPhone", SqlDbType.Int).Value =
        //            inputProducts.ContactPhone == null
        //            || inputProducts.ContactPhone.Count == 0
        //                ? null
        //                : (int?)inputProducts.ContactPhone[0].Value;

        //        command.Parameters.Add("@ContactEmail", SqlDbType.NVarChar).Value = inputProducts.ContactEmail == null
        //            ? null
        //            : inputProducts.ContactEmail[0];

        //        command.Parameters.Add("@ContactFax", SqlDbType.Int).Value = inputProducts.ContactFax == null
        //            ? null
        //            : (int?)inputProducts.ContactFax[0].Value;

        //        command.Parameters.Add("@Price", SqlDbType.Float).Value = inputProducts.Price;
        //        command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = inputProducts.Description;
        //        command.Parameters.Add("@ExtraDescription", SqlDbType.NVarChar).Value =
        //            XMLSortingLogic.TryToConvertNodeValueToStringBuilder(inputProducts.ExtraDescription);

        //        command.Parameters.Add("@Availability", SqlDbType.Bit).Value = inputProducts.Availability;
        //        command.Parameters.Add("@Website", SqlDbType.NVarChar).Value = inputProducts.Website;
        //        command.Parameters.Add("@CanonicalUrl", SqlDbType.NVarChar).Value = inputProducts.CanonicalUrl;
        //        command.Parameters.Add("@FK_CityID", SqlDbType.Int).Value = inputProducts.Cities.ID;
        //        command.Parameters.Add("@FK_UserID", SqlDbType.Int).Value = inputProducts.UserID;

        //        // MANGLER FLERE PARAMETER!

        //        command.ExecuteNonQuery();
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    connection = DBConnectionLogic.DisconnectFromDB(connection);
        //}

        public static int? WriteNewProduct(Product inputProducts)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            //try
            {
                SqlCommand command = new SqlCommand("spWriteNewProduct", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = inputProducts.Name;
                command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = inputProducts.Address;
                command.Parameters.Add("@Latitude", SqlDbType.Float).Value = inputProducts.Latitude;
                command.Parameters.Add("@Longitude", SqlDbType.Float).Value = inputProducts.Longitude;
                command.Parameters.Add("@ContactPhone", SqlDbType.Int).Value =
                    inputProducts.ContactPhone == null
                    || inputProducts.ContactPhone.Count == 0
                        ? null
                        : (int?)inputProducts.ContactPhone[0].Value;

                command.Parameters.Add("@ContactEmail", SqlDbType.NVarChar).Value = inputProducts.ContactEmail == null
                    ? null
                    : inputProducts.ContactEmail[0];

                command.Parameters.Add("@ContactFax", SqlDbType.Int).Value = inputProducts.ContactFax == null
                    ? null
                    : (int?)inputProducts.ContactFax[0].Value;

                command.Parameters.Add("@Price", SqlDbType.Float).Value = inputProducts.Price;
                command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = inputProducts.Description;
                command.Parameters.Add("@ExtraDescription", SqlDbType.NVarChar).Value =
                    XMLSortingLogic.TryToConvertNodeValueToStringBuilder(inputProducts.ExtraDescription);

                command.Parameters.Add("@Availability", SqlDbType.Bit).Value = inputProducts.Availability;
                command.Parameters.Add("@Website", SqlDbType.NVarChar).Value = inputProducts.Website;
                command.Parameters.Add("@CanonicalUrl", SqlDbType.NVarChar).Value = inputProducts.CanonicalUrl;
                command.Parameters.Add("@FK_CityID", SqlDbType.Int).Value = inputProducts.Cities.ID;
                command.Parameters.Add("@FK_UserID", SqlDbType.Int).Value = inputProducts.UserID;
                command.Parameters.Add("@FK_MainCategoryID", SqlDbType.Int).Value = inputProducts.MainCategories.ID;
                command.Parameters.Add("@FK_CategoryID", SqlDbType.Int).Value = inputProducts.Categories.ID;

                //todo OPENINGHOURS MANGLER FLERE PARAMETER!
                command.Parameters.Add("@StartDate", SqlDbType.Date).Value = inputProducts.OpeningHours.StartDate;
                command.Parameters.Add("@EndDate", SqlDbType.Date).Value = inputProducts.OpeningHours.EndDate;
                command.Parameters.Add("@StartTime", SqlDbType.Time).Value = DateTime.Now.Subtract(inputProducts.OpeningHours.StartTime.Value);//usikker
                command.Parameters.Add("@EndTime", SqlDbType.Time).Value = DateTime.Now.Subtract(inputProducts.OpeningHours.EndTime.Value); //usikker


                command.Parameters.Add("@Monday", SqlDbType.Bit).Value = inputProducts.OpeningHours.Monday;
                command.Parameters.Add("@Tuesday", SqlDbType.Bit).Value = inputProducts.OpeningHours.Tuesday;
                command.Parameters.Add("@Wednesday", SqlDbType.Bit).Value = inputProducts.OpeningHours.Wednesday;
                command.Parameters.Add("@Thursday", SqlDbType.Bit).Value = inputProducts.OpeningHours.Thursday;
                command.Parameters.Add("@Friday", SqlDbType.Bit).Value = inputProducts.OpeningHours.Friday;
                command.Parameters.Add("@Saturday", SqlDbType.Bit).Value = inputProducts.OpeningHours.Saturday;
                command.Parameters.Add("@Sunday", SqlDbType.Bit).Value = inputProducts.OpeningHours.Sunday;
                inputProducts.ID=(int)command.ExecuteScalar();
            }

            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            connection = DBConnectionLogic.DisconnectFromDB(connection);
            return inputProducts.ID;
        }

        public static List<File> WriteNewFiles(List<File> inputFiles)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);
            try
            {
                for (int i = 0; i < inputFiles.Count; i++)
                {
                    {
                        SqlCommand command = new SqlCommand(string.Format(@"INSERT INTO Files(URI) VALUES('{0}');
                        select CAST(SCOPE_IDENTITY() AS INT )", inputFiles[i].URI), connection);
                      
                        inputFiles[i].ID = ((int)command.ExecuteScalar());  
                    }
                }
            }

            catch (Exception)
            {
                throw;
            }

            finally
            {
                connection = DBConnectionLogic.DisconnectFromDB(connection);
            }

            return inputFiles;
        }

        public static void WriteNewRelFiles(Product inputProduct)
        {
            //List<int> FileIDList = new List<int>();

            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                for (int i = 0; i < inputProduct.Files.Count; i++)
                {
                    {
                        SqlCommand command = new SqlCommand(String.Format(@"INSERT INTO Rel_Files(FK_ProductID, FK_FileID)
                        VALUES({0}, {1})", inputProduct.ID, inputProduct.Files[i].ID), connection);
                        command.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception)
            {
                throw;
            }

            finally
            {
                connection = DBConnectionLogic.DisconnectFromDB(connection);
            }
        }

        public static void WriteCombiProduct(CombiProduct inputCombiProduct)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("spWriteCombiProduct", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter outputID = new SqlParameter("@CombiProductID", SqlDbType.Int);

                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = inputCombiProduct.Name;
                command.Parameters.Add("@PackagePrice", SqlDbType.Float).Value = inputCombiProduct.PackagePrice;
                command.Parameters.Add("@Availability", SqlDbType.Bit).Value = inputCombiProduct.Availability;
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = inputCombiProduct.UserID;

                command.Parameters.Add(outputID);
                outputID.Direction = ParameterDirection.Output;

                command.ExecuteNonQuery();

                inputCombiProduct.ID = int.Parse(outputID.Value.ToString());
            }

            catch (Exception)
            {
                throw;
            }

            finally
            {
                connection = DBConnectionLogic.DisconnectFromDB(connection);
            }
        }

        public static void WritRelCombiProducts(CombiProduct inputCombiProduct)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                foreach (int productID in inputCombiProduct.ProductID)
                {
                    SqlCommand command = new SqlCommand("spWriteRel_CombiProducts", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@FK_CombiProductID", SqlDbType.Int).Value = inputCombiProduct.ID;
                    command.Parameters.Add("@FK_ProductID", SqlDbType.Int).Value = productID;

                    command.ExecuteNonQuery();
                }
            }

            catch (Exception)
            {
                throw;
            }

            finally
            {
                connection = DBConnectionLogic.DisconnectFromDB(connection);
            }
        }
    }
}