﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Foxtrot.Classes;

namespace Classes
{
    public class DBLogic
    {
        static SqlConnection ConnectToDB(SqlConnection connection)
        {
            try
            {
                if (connection == null)
                    connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Foxtrot"].ConnectionString);
                connection.Open();
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return connection;
        }

        static SqlConnection DisconnectFromDB(SqlConnection connection)
        {
            try
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return connection;
        }

        public static void WriteCitiesToDB(List<City> cities)
        {
            SqlConnection connection = null;

            connection = ConnectToDB(connection);

            foreach (City city in cities)
            {
                try
                {
                    SqlCommand command = new SqlCommand("spWhiteCitiesToDB", connection);
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

            connection = DisconnectFromDB(connection);
        }

        public static void WriteCategoriesToDB(List<Category> categories)
        {
            SqlConnection connection = null;

            connection = ConnectToDB(connection);

            foreach (Category category in categories)
            {
                try
                {
                    SqlCommand command = new SqlCommand("spWhiteCategoriesToDB", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@ID", SqlDbType.Int).Value = category.ID;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = category.Name;

                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

            connection = DisconnectFromDB(connection);
        }

        public static void WriteMainCategoriesToDB(List<MainCategory> mainCategories)
        {
            SqlConnection connection = null;

            connection = ConnectToDB(connection);

            foreach (MainCategory maincategory in mainCategories)
            {
                try
                {
                    SqlCommand command = new SqlCommand("spWhiteMainCategoriesToDB", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@ID", SqlDbType.Int).Value = maincategory.ID;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = maincategory.Name;

                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

            connection = DisconnectFromDB(connection);
        }

        public static void WriteProductsToDB(List<Product> products)
        {
            SqlConnection connection = null;

            connection = ConnectToDB(connection);

            foreach (Product product in products)
            {
                try
                {
                    SqlCommand command = new SqlCommand("spWhiteProductsToDB", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@ID", SqlDbType.Int).Value = product.ID;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = product.Name;
                    command.Parameters.Add("@Actor", SqlDbType.NVarChar).Value = product.Actor;
                    command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = product.Address;
                    command.Parameters.Add("@Latitude", SqlDbType.Float).Value = product.Latitude;
                    command.Parameters.Add("@Longitude", SqlDbType.Float).Value = product.Longitude;

                    //command.Parameters.Add("@Phone", SqlDbType.Int).Value = product.Phone;
                    //command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = product.Email;
                    //command.Parameters.Add("@Fax", SqlDbType.Int).Value = product.Fax;

                    command.Parameters.Add("@CanonicalUrl", SqlDbType.NVarChar).Value = product.CanonicalUrl;
                    command.Parameters.Add("@Url", SqlDbType.NVarChar).Value = product.Url;
                    command.Parameters.Add("@Created", SqlDbType.DateTime).Value = product.Created;
                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = product.Description;
                    command.Parameters.Add("@City", SqlDbType.Int).Value = product.Cities.ID;
                    command.Parameters.Add("@Category", SqlDbType.Int).Value = product.Categories.ID;
                    command.Parameters.Add("@MainCategory", SqlDbType.Int).Value = product.MainCategories.ID;

                    //command.Parameters.Add("@Files", SqlDbType.NVarChar).Value = product.Files;

                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

            connection = DisconnectFromDB(connection);
        }
    }
}