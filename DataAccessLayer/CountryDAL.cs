using KuaiexDashboard;
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
