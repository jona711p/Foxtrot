using System;
using System.Data;
using System.Data.SqlClient;
using Foxtrot.Classes.XMLImport;

namespace Foxtrot.Classes.DB
{
    /// <summary>
    /// Jonas Lykke, Mikael Paaske & Thomas Nielsen
    /// </summary>
    public class DBWriteLogic
    {
        public static void WriteNewProduct(Product inputProducts)
        {
            SqlConnection connection = null;
            connection = DBConnectionLogic.ConnectToDB(connection);

            try
            {
                SqlCommand command = new SqlCommand("spWriteNewProducts", connection);
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
                command.Parameters.Add("@FK_ActorID", SqlDbType.Int).Value = inputProducts.ActorID;

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