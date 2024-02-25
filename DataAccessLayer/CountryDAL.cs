﻿using KuaiexDashboard;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.ProcedureResults;

namespace DataAccessLayer
{
    public class CountryDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["KUAIEXProdEntities"].ConnectionString;
        public Country GetCountryByName(string countryName)
        {
            Country country = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetCountryByName", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = countryName;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                country = new Country
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    Nationality = reader.GetString(reader.GetOrdinal("Nationality")),
                                    Status = reader.GetString(reader.GetOrdinal("Status")),
                                    Alpha_2_Code = reader.GetString(reader.GetOrdinal("Alpha_2_Code")),
                                    Alpha_3_Code = reader.GetString(reader.GetOrdinal("Alpha_3_Code")),
                                    Remittance_Status = reader.GetString(reader.GetOrdinal("Remittance_Status")),
                                    City_Id = reader.IsDBNull(reader.GetOrdinal("City_Id")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("City_Id")),
                                    High_Risk_Status = reader.GetString(reader.GetOrdinal("High_Risk_Status")),
                                    Country_Dialing_Code = reader.GetString(reader.GetOrdinal("Country_Dialing_Code")),
                                    CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                                    CreatedOn = reader.IsDBNull(reader.GetOrdinal("CreatedOn")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("CreatedOn")),
                                    CreatedIp = reader.GetString(reader.GetOrdinal("CreatedIp")),
                                    UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("UpdatedBy")),
                                    UpdatedOn = reader.IsDBNull(reader.GetOrdinal("UpdatedOn")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("UpdatedOn")),
                                    UpdatedIp = reader.GetString(reader.GetOrdinal("UpdatedIp")),
                                    UID = reader.IsDBNull(reader.GetOrdinal("UID")) ? null : (Guid?)reader.GetGuid(reader.GetOrdinal("UID")),
                                    Comission = reader.IsDBNull(reader.GetOrdinal("Comission")) ? null : (decimal?)reader.GetDecimal(reader.GetOrdinal("Comission")),
                                    Prod_Country_Id = reader.IsDBNull(reader.GetOrdinal("Prod_Country_Id")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("Prod_Country_Id"))
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return country;
        }
        public bool AddCountry(Country country)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("AddCountry", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Name", country.Name);
                        command.Parameters.AddWithValue("@Nationality", country.Nationality);
                        command.Parameters.AddWithValue("@Status", country.Status);
                        command.Parameters.AddWithValue("@Alpha_2_Code", country.Alpha_2_Code);
                        command.Parameters.AddWithValue("@Alpha_3_Code", country.Alpha_3_Code);
                        command.Parameters.AddWithValue("@Remittance_Status", country.Remittance_Status);
                        command.Parameters.AddWithValue("@City_Id", country.City_Id ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@High_Risk_Status", country.High_Risk_Status);
                        command.Parameters.AddWithValue("@Country_Dialing_Code", country.Country_Dialing_Code);
                        command.Parameters.AddWithValue("@CreatedBy", country.CreatedBy ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedOn", country.CreatedOn ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedIp", country.CreatedIp);
                        command.Parameters.AddWithValue("@UpdatedBy", country.UpdatedBy ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@UpdatedOn", country.UpdatedOn ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@UpdatedIp", country.UpdatedIp);
                        command.Parameters.AddWithValue("@UID", country.UID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Comission", country.Comission ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Prod_Country_Id", country.Prod_Country_Id ?? (object)DBNull.Value);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding country: " + ex.Message);
                return false;
            }
        }
        public List<City> GetActiveCities()
        {
            List<City> cityList = new List<City>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetCityList", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Get the ordinal positions of the columns for better performance
                            int idOrdinal = reader.GetOrdinal("Id");
                            int nameOrdinal = reader.GetOrdinal("Name");
                            int statusOrdinal = reader.GetOrdinal("Status");

                            while (reader.Read())
                            {
                                City item = new City
                                {
                                    Id = reader.GetInt32(idOrdinal),
                                    Name = reader.GetString(nameOrdinal),
                                    // Check for DBNull using IsDBNull and use conditional operator for nullable int
                                    Status = reader.IsDBNull(statusOrdinal) ? (int?)null : reader.GetInt32(statusOrdinal)
                                };

                                cityList.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return cityList;
        }
        public List<GetCountryList_Result> GetCountryList()
        {
            List<GetCountryList_Result> countries = new List<GetCountryList_Result>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetCountryList", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "Country");
                    foreach (DataRow row in ds.Tables["Country"].Rows)
                    {
                        GetCountryList_Result country = new GetCountryList_Result
                        {
                            UID = row.Field<Guid?>("UID"),
                            Name = row.Field<string>("Name"),
                            Nationality = row.Field<string>("Nationality"),
                            Alpha_2_Code = row.Field<string>("Alpha_2_Code"),
                            Alpha_3_Code = row.Field<string>("Alpha_3_Code"),
                            Status = row.Field<string>("Status"),
                            City = row.Field<string>("City")
                        };

                        countries.Add(country);
                    }
                }
            }

            return countries;
        }
        public Country GetCountryByUID(Guid? UID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetCountryByUID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@UID", SqlDbType.UniqueIdentifier).Value = UID;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataSet dataSet = new DataSet();
                            adapter.Fill(dataSet);

                            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                            {
                                DataRow row = dataSet.Tables[0].Rows[0];

                                return new Country
                                {
                                    Id = Convert.ToInt32(row["Id"]),
                                    Name = row["Name"].ToString(),
                                    Nationality = row["Nationality"].ToString(),
                                    Status = row["Status"].ToString(),
                                    Alpha_2_Code = row["Alpha_2_Code"].ToString(),
                                    Alpha_3_Code = row["Alpha_3_Code"].ToString(),
                                    Remittance_Status = row["Remittance_Status"].ToString(),
                                    City_Id = row.IsNull("City_Id") ? null : (int?)row["City_Id"],
                                    High_Risk_Status = row["High_Risk_Status"].ToString(),
                                    Country_Dialing_Code = row["Country_Dialing_Code"].ToString(),
                                    CreatedBy = row.IsNull("CreatedBy") ? null : (int?)row["CreatedBy"],
                                    CreatedOn = row.IsNull("CreatedOn") ? null : (DateTime?)row["CreatedOn"],
                                    CreatedIp = row["CreatedIp"].ToString(),
                                    UpdatedBy = row.IsNull("UpdatedBy") ? null : (int?)row["UpdatedBy"],
                                    UpdatedOn = row.IsNull("UpdatedOn") ? null : (DateTime?)row["UpdatedOn"],
                                    UpdatedIp = row["UpdatedIp"].ToString(),
                                    UID = row.IsNull("UID") ? null : (Guid?)row["UID"],
                                    Comission = row.IsNull("Comission") ? null : (decimal?)row["Comission"],
                                    Prod_Country_Id = row.IsNull("Prod_Country_Id") ? null : (int?)row["Prod_Country_Id"]
                                };
                            }
                        }
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public Country GetCountryByUID(Guid? UID)
        //{
        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();

        //            using (SqlCommand command = new SqlCommand("GetCountryByUID", connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;

        //                command.Parameters.Add("@UID", SqlDbType.UniqueIdentifier).Value = UID;

        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    if (reader.Read())
        //                    {
        //                        Country country = new Country
        //                        {
        //                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
        //                            Name = reader.GetString(reader.GetOrdinal("Name")),
        //                            Nationality = reader.GetString(reader.GetOrdinal("Nationality")),
        //                            Status = reader.GetString(reader.GetOrdinal("Status")),
        //                            Alpha_2_Code = reader.GetString(reader.GetOrdinal("Alpha_2_Code")),
        //                            Alpha_3_Code = reader.GetString(reader.GetOrdinal("Alpha_3_Code")),
        //                            Remittance_Status = reader.GetString(reader.GetOrdinal("Remittance_Status")),
        //                            City_Id = reader.IsDBNull(reader.GetOrdinal("City_Id")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("City_Id")),
        //                            High_Risk_Status = reader.GetString(reader.GetOrdinal("High_Risk_Status")),
        //                            Country_Dialing_Code = reader.GetString(reader.GetOrdinal("Country_Dialing_Code")),
        //                            CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("CreatedBy")),
        //                            CreatedOn = reader.IsDBNull(reader.GetOrdinal("CreatedOn")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("CreatedOn")),
        //                            CreatedIp = reader.GetString(reader.GetOrdinal("CreatedIp")),
        //                            UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("UpdatedBy")),
        //                            UpdatedOn = reader.IsDBNull(reader.GetOrdinal("UpdatedOn")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("UpdatedOn")),
        //                            UpdatedIp = reader.GetString(reader.GetOrdinal("UpdatedIp")),
        //                            UID = reader.IsDBNull(reader.GetOrdinal("UID")) ? null : (Guid?)reader.GetGuid(reader.GetOrdinal("UID")),
        //                            Comission = reader.IsDBNull(reader.GetOrdinal("Comission")) ? null : (decimal?)reader.GetDecimal(reader.GetOrdinal("Comission")),
        //                            Prod_Country_Id = reader.IsDBNull(reader.GetOrdinal("Prod_Country_Id")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("Prod_Country_Id"))
        //                        };

        //                        return country;
        //                    }
        //                }
        //            }

        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public void UpdateCountry(Country obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("UpdateCountry", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@Id", SqlDbType.Int).Value = obj.Id;
                        command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = obj.Name;
                        command.Parameters.Add("@Nationality", SqlDbType.NVarChar).Value = obj.Nationality;
                        command.Parameters.Add("@Status", SqlDbType.NVarChar).Value = obj.Status;
                        command.Parameters.Add("@Alpha_2_Code", SqlDbType.NVarChar).Value = obj.Alpha_2_Code;
                        command.Parameters.Add("@Alpha_3_Code", SqlDbType.NVarChar).Value = obj.Alpha_3_Code;
                        command.Parameters.Add("@Remittance_Status", SqlDbType.NVarChar).Value = obj.Remittance_Status;
                        command.Parameters.Add("@City_Id", SqlDbType.Int).Value = (object)obj.City_Id ?? DBNull.Value;
                        command.Parameters.Add("@High_Risk_Status", SqlDbType.NVarChar).Value = obj.High_Risk_Status;
                        command.Parameters.Add("@Country_Dialing_Code", SqlDbType.NVarChar).Value = obj.Country_Dialing_Code;
                        command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = (object)obj.CreatedBy ?? DBNull.Value;
                        command.Parameters.Add("@CreatedOn", SqlDbType.DateTime).Value = (object)obj.CreatedOn ?? DBNull.Value;
                        command.Parameters.Add("@CreatedIp", SqlDbType.NVarChar).Value = obj.CreatedIp;
                        command.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = (object)obj.UpdatedBy ?? DBNull.Value;
                        command.Parameters.Add("@UpdatedOn", SqlDbType.DateTime).Value = (object)obj.UpdatedOn ?? DBNull.Value;
                        command.Parameters.Add("@UpdatedIp", SqlDbType.NVarChar).Value = obj.UpdatedIp;
                        command.Parameters.Add("@UID", SqlDbType.UniqueIdentifier).Value = (object)obj.UID ?? DBNull.Value;
                        command.Parameters.Add("@Comission", SqlDbType.Decimal).Value = (object)obj.Comission ?? DBNull.Value;
                        command.Parameters.Add("@Prod_Country_Id", SqlDbType.Int).Value = (object)obj.Prod_Country_Id ?? DBNull.Value;

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateCountry_ProdCountryId(int? ProdCountryId, int CountryId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("UpdateProdCountryId", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@Id", SqlDbType.Int).Value = CountryId;                        
                        command.Parameters.Add("@Prod_Country_Id", SqlDbType.Int).Value = ProdCountryId;

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Country> GetAllCountries()
        {
            List<Country> countries = new List<Country>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetAllCountries", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataSet dataSet = new DataSet();
                            adapter.Fill(dataSet);
                            
                            foreach (DataRow row in dataSet.Tables[0].Rows)
                            {
                                Country country = new Country
                                {
                                    Id = Convert.ToInt32(row["Id"]),
                                    Name = row["Name"].ToString(),
                                    Nationality = row["Nationality"].ToString(),
                                    Status = row["Status"].ToString(),
                                    Alpha_2_Code = row["Alpha_2_Code"].ToString(),
                                    Alpha_3_Code = row["Alpha_3_Code"].ToString(),
                                    Remittance_Status = row["Remittance_Status"].ToString(),
                                    City_Id = row.IsNull("City_Id") ? null : (int?)row["City_Id"],
                                    High_Risk_Status = row["High_Risk_Status"].ToString(),
                                    Country_Dialing_Code = row["Country_Dialing_Code"].ToString(),
                                    CreatedBy = row.IsNull("CreatedBy") ? null : (int?)row["CreatedBy"],
                                    CreatedOn = row.IsNull("CreatedOn") ? null : (DateTime?)row["CreatedOn"],
                                    CreatedIp = row["CreatedIp"].ToString(),
                                    UpdatedBy = row.IsNull("UpdatedBy") ? null : (int?)row["UpdatedBy"],
                                    UpdatedOn = row.IsNull("UpdatedOn") ? null : (DateTime?)row["UpdatedOn"],
                                    UpdatedIp = row["UpdatedIp"].ToString(),
                                    UID = row.IsNull("UID") ? null : (Guid?)row["UID"],
                                    Comission = row.IsNull("Comission") ? null : (decimal?)row["Comission"],
                                    Prod_Country_Id = row.IsNull("Prod_Country_Id") ? null : (int?)row["Prod_Country_Id"]
                                };

                                countries.Add(country);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return countries;
        }

        //public List<Country> GetAllCountries()
        //{
        //    List<Country> countries = new List<Country>();

        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            using (SqlCommand command = new SqlCommand("GetAllCountries", connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;

        //                connection.Open();

        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        Country country = new Country
        //                        {
        //                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
        //                            Name = reader.GetString(reader.GetOrdinal("Name")),
        //                            Nationality = reader.GetString(reader.GetOrdinal("Nationality")),
        //                            Status = reader.GetString(reader.GetOrdinal("Status")),
        //                            Alpha_2_Code = reader.GetString(reader.GetOrdinal("Alpha_2_Code")),
        //                            Alpha_3_Code = reader.GetString(reader.GetOrdinal("Alpha_3_Code")),
        //                            Remittance_Status = reader.GetString(reader.GetOrdinal("Remittance_Status")),
        //                            City_Id = reader.IsDBNull(reader.GetOrdinal("City_Id")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("City_Id")),
        //                            High_Risk_Status = reader.GetString(reader.GetOrdinal("High_Risk_Status")),
        //                            Country_Dialing_Code = reader.GetString(reader.GetOrdinal("Country_Dialing_Code")),
        //                            CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("CreatedBy")),
        //                            CreatedOn = reader.IsDBNull(reader.GetOrdinal("CreatedOn")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("CreatedOn")),
        //                            CreatedIp = reader.GetString(reader.GetOrdinal("CreatedIp")),
        //                            UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("UpdatedBy")),
        //                            UpdatedOn = reader.IsDBNull(reader.GetOrdinal("UpdatedOn")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("UpdatedOn")),
        //                            UpdatedIp = reader.GetString(reader.GetOrdinal("UpdatedIp")),
        //                            UID = reader.IsDBNull(reader.GetOrdinal("UID")) ? null : (Guid?)reader.GetGuid(reader.GetOrdinal("UID")),
        //                            Comission = reader.IsDBNull(reader.GetOrdinal("Comission")) ? null : (decimal?)reader.GetDecimal(reader.GetOrdinal("Comission")),
        //                            Prod_Country_Id = reader.IsDBNull(reader.GetOrdinal("Prod_Country_Id")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("Prod_Country_Id"))
        //                        };

        //                        countries.Add(country);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return countries;
        //}
    }
}
