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
                                Id = row.Field<int>("Id"),
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
