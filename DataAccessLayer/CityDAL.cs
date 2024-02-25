using DataAccessLayer.Entities;
using DataAccessLayer.ProcedureResults;
using KuaiexDashboard;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
  public class CityDAL
  {
        private string connectionString = ConfigurationManager.ConnectionStrings["KUAIEXProdEntities"].ConnectionString;
        public List<GetCountryList_Result> GetActiveCountryList()
        {
            List<GetCountryList_Result> result = new List<GetCountryList_Result>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetActiveCountryList", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        foreach (DataRow row in dataTable.Rows)
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

                            result.Add(country);
                        }
                    }
                }
            }

            return result;
        }

        public City GetCityByName(string cityName)
        {
            City city = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetCityByName", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = cityName;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataSet dataSet = new DataSet();
                            adapter.Fill(dataSet);

                            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                            {
                                DataRow row = dataSet.Tables[0].Rows[0];

                                city = new City
                                {
                                    Id = Convert.ToInt32(row["Id"]),
                                    Country_Id = row.IsNull("Country_Id") ? null : (int?)row["Country_Id"],
                                    Name = row["Name"].ToString(),
                                    Status = row.IsNull("Status") ? null : (int?)row["Status"],
                                    UID = row.IsNull("UID") ? null : (Guid?)row["UID"],
                                    CreatedBy = row.IsNull("CreatedBy") ? null : (int?)row["CreatedBy"],
                                    CreatedOn = row.IsNull("CreatedOn") ? null : (DateTime?)row["CreatedOn"],
                                    UpdatedBy = row.IsNull("UpdatedBy") ? null : (int?)row["UpdatedBy"],
                                    UpdatedOn = row.IsNull("UpdatedOn") ? null : (DateTime?)row["UpdatedOn"],
                                    Prod_City_Id = row.IsNull("Prod_City_Id") ? null : (int?)row["Prod_City_Id"]
                                };

                                return city;
                            }
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddCity(City objCity)
        {
            
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("AddCity", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Country_Id", SqlDbType.Int).Value = objCity.Country_Id ?? (object)DBNull.Value;
                        command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = objCity.Name;
                        command.Parameters.Add("@Status", SqlDbType.Int).Value = objCity.Status ?? (object)DBNull.Value;
                        command.Parameters.Add("@UID", SqlDbType.UniqueIdentifier).Value = objCity.UID ?? (object)DBNull.Value;
                        command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = objCity.CreatedBy ?? (object)DBNull.Value;
                        command.Parameters.Add("@CreatedOn", SqlDbType.DateTime).Value = objCity.CreatedOn ?? (object)DBNull.Value;
                        command.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = objCity.UpdatedBy ?? (object)DBNull.Value;
                        command.Parameters.Add("@UpdatedOn", SqlDbType.DateTime).Value = objCity.UpdatedOn ?? (object)DBNull.Value;
                        command.Parameters.Add("@Prod_City_Id", SqlDbType.Int).Value = objCity.Prod_City_Id ?? (object)DBNull.Value;

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding City: " + ex.Message);
                return false;
            }
        }
        public List<GetCityList_Result> GetActiveCities()
        {
            List<GetCityList_Result> cityList = new List<GetCityList_Result>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetCityList", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();

                        adapter.Fill(dataTable);

                        foreach (DataRow row in dataTable.Rows)
                        {
                            GetCityList_Result item = new GetCityList_Result
                            {
                                Name = row.Field<string>("Name"),
                                Status = Convert.ToInt32(row.Field<int?>("Status"))
                            };

                            cityList.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return cityList;
        }

        public City GetCityByUID(Guid? UID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetCityByUID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@UID", SqlDbType.UniqueIdentifier).Value = UID;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                City city = new City
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Country_Id = reader.IsDBNull(reader.GetOrdinal("Country_Id")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("Country_Id")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("Status")),
                                    UID = reader.IsDBNull(reader.GetOrdinal("UID")) ? null : (Guid?)reader.GetGuid(reader.GetOrdinal("UID")),
                                    CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                                    CreatedOn = reader.IsDBNull(reader.GetOrdinal("CreatedOn")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("CreatedOn")),
                                    UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("UpdatedBy")),
                                    UpdatedOn = reader.IsDBNull(reader.GetOrdinal("UpdatedOn")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("UpdatedOn")),
                                    Prod_City_Id = reader.IsDBNull(reader.GetOrdinal("Prod_City_Id")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("Prod_City_Id"))
                                };

                                return city;
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
        public void EditCity(City updatedCity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("EditCity", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@Id", SqlDbType.Int).Value = updatedCity.Id;
                        command.Parameters.Add("@Country_Id", SqlDbType.Int).Value = (object)updatedCity.Country_Id ?? DBNull.Value;
                        command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = updatedCity.Name;
                        command.Parameters.Add("@Status", SqlDbType.Int).Value = (object)updatedCity.Status ?? DBNull.Value;
                        command.Parameters.Add("@UID", SqlDbType.UniqueIdentifier).Value = (object)updatedCity.UID ?? DBNull.Value;
                        command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = (object)updatedCity.CreatedBy ?? DBNull.Value;
                        command.Parameters.Add("@CreatedOn", SqlDbType.DateTime).Value = (object)updatedCity.CreatedOn ?? DBNull.Value;
                        command.Parameters.Add("@UpdatedBy", SqlDbType.Int).Value = (object)updatedCity.UpdatedBy ?? DBNull.Value;
                        command.Parameters.Add("@UpdatedOn", SqlDbType.DateTime).Value = (object)updatedCity.UpdatedOn ?? DBNull.Value;
                        command.Parameters.Add("@Prod_City_Id", SqlDbType.Int).Value = (object)updatedCity.Prod_City_Id ?? DBNull.Value;

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
        public List<City> GetAllCities()
        {
            List<City> cities = new List<City>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetAllCities", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                City city = new City
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Country_Id = reader.IsDBNull(reader.GetOrdinal("Country_Id")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("Country_Id")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("Status")),
                                    UID = reader.IsDBNull(reader.GetOrdinal("UID")) ? null : (Guid?)reader.GetGuid(reader.GetOrdinal("UID")),
                                    CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                                    CreatedOn = reader.IsDBNull(reader.GetOrdinal("CreatedOn")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("CreatedOn")),
                                    UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("UpdatedBy")),
                                    UpdatedOn = reader.IsDBNull(reader.GetOrdinal("UpdatedOn")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("UpdatedOn")),
                                    Prod_City_Id = reader.IsDBNull(reader.GetOrdinal("Prod_City_Id")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("Prod_City_Id"))
                                };

                                cities.Add(city);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return cities;
        }
        public void UpdateCity_ProdCityId(int? ProdCityId, int CityId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("UpdateProdCityId", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@Id", SqlDbType.Int).Value = CityId;
                        command.Parameters.Add("@Prod_City_Id", SqlDbType.Int).Value = ProdCityId;

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

    }
}
