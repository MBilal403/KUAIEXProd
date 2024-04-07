using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace KuaiexDashboard.DAL
{
    public class BankMstDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["KUAIEXEntities"].ConnectionString;

        public List<Currency> GetCurrency()
        {
            try
            {
                List<Currency> currencies = new List<Currency>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetCurrencyRate", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataSet dataSet = new DataSet();

                        adapter.Fill(dataSet, "Currency");

                        foreach (DataRow row in dataSet.Tables["Currency"].Rows)
                        {
                            Currency currency = new Currency
                            {
                                Id = Convert.ToInt32(row["Id"]),
                                Name = row["Name"].ToString(),
                                Code = row["Code"].ToString(),
                                DD_Rate = Convert.ToDecimal(row["DD_Rate"]),

                            };

                            currencies.Add(currency);
                        }
                    }
                }

                return currencies;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<GetBanksListByCountry_Result> GetBanksListByCountry(int countryId)
        {
            try
            {
                List<GetBanksListByCountry_Result> banks = new List<GetBanksListByCountry_Result>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetBanksListByCountry", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Country", countryId);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataSet dataSet = new DataSet();

                        adapter.Fill(dataSet, "Bank_Mst");

                        foreach (DataRow row in dataSet.Tables["Bank_Mst"].Rows)
                        {
                            GetBanksListByCountry_Result bank = new GetBanksListByCountry_Result
                            {
                                Bank_Id = Convert.ToInt32(row["Bank_Id"].ToString()),
                                English_Name = row["English_Name"].ToString(),
                                Address_Line1 = row["Address_Line1"].ToString(),
                                Address_Line2 = row["Address_Line2"].ToString(),
                                Address_Line3 = row["Address_Line3"].ToString(),
                                Country_Name = row["Country_Name"].ToString(),
                                Currency = row["Currency"].ToString(),

                            };

                            banks.Add(bank);
                        }
                    }
                }

                return banks;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
