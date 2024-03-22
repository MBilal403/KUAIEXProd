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
   public class DashboardDAL
   {
        private string connectionString = ConfigurationManager.ConnectionStrings["KUAIEXEntities"].ConnectionString;
        public List<GetCurrencyRate_Result> GetCurrencyRates()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetCurrencyRate", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "Currency");

                        List<GetCurrencyRate_Result> currencyRates = new List<GetCurrencyRate_Result>();

                        if (ds.Tables["Currency"].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables["Currency"].Rows)
                            {
                                GetCurrencyRate_Result currencyRate = new GetCurrencyRate_Result
                                {
                                    Id = row.Field<int>("Id"),
                                    Name = row.Field<string>("Name"),
                                    Code = row.Field<string>("Code"),
                                    DD_Rate = row.Field<decimal?>("DD_Rate")
                                };

                                currencyRates.Add(currencyRate);
                            }
                        }

                        return currencyRates;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int GetTotalCustomerCount()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetTotalCustomerList", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        int totalCount = (int)command.ExecuteScalar();
                        return totalCount;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int GetTodayCustomerCount()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetTodayCustomerList", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        return (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int GetTotalCustomerReviewedCount()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetTotalCustomerReviwed", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        return (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int GetTodayCustomerReviewedCount()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetTodayCustomerReviwed", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        return (int)command.ExecuteScalar();
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
