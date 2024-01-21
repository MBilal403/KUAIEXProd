using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace KuaiexDashboard.DAL
{
    public class BankMstDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["KUAIEXProdEntities"].ConnectionString;

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
                                UID = row["UID"] as Guid?,
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
        public List<GetBanksList_Result> GetBanksList()
        {
              try
              {
                    List<GetBanksList_Result> banks = new List<GetBanksList_Result>();

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand("GetBanksList", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataSet dataSet = new DataSet();

                            adapter.Fill(dataSet, "Bank_Mst");

                            foreach (DataRow row in dataSet.Tables["Bank_Mst"].Rows)
                            {
                                GetBanksList_Result banklist = new GetBanksList_Result
                                {
                                    UID = row["UID"] as Guid?,
                                    English_Name = row["English_Name"].ToString(),
                                    Address_Line1 = row["Address_Line1"].ToString(),
                                    Address_Line2 = row["Address_Line2"].ToString(),
                                    Address_Line3 = row["Address_Line3"].ToString(),
                                    Country_Name = row["Country_Name"].ToString(),
                                    Currency = row["Currency"].ToString(),
                                };

                                banks.Add(banklist);
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
        public Bank_Mst GetBankByUID(Guid UID)
        {
            try
            {
                Bank_Mst bank = new Bank_Mst();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetBankByUID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UID", UID);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            bank.UID = (Guid)reader["UID"];
                            bank.English_Name = reader["English_Name"].ToString();
                            bank.Arabic_Name = reader["Arabic_Name"].ToString();
                            bank.Short_English_Name = reader["Short_English_Name"].ToString();
                            bank.Short_Arabic_Name = reader["Short_Arabic_Name"].ToString();
                            bank.Country_Id = (int)reader["Country_Id"];
                            bank.Currency_Id = (int)reader["Currency_Id"];
                            bank.Upper_Limit = reader["Upper_Limit"] != DBNull.Value ? (decimal?)reader["Upper_Limit"] : null;
                            bank.Address_Line1 = reader["Address_Line1"].ToString();
                            bank.Address_Line2 = reader["Address_Line2"].ToString();
                            bank.Address_Line3 = reader["Address_Line3"].ToString();
                            bank.Tel_Number1 = reader["Tel_Number1"].ToString();
                            bank.Fax_Number1 = reader["Fax_Number1"].ToString();
                            bank.EMail_Address1 = reader["EMail_Address1"].ToString();
                            bank.Prefix_Draft = reader["Prefix_Draft"].ToString();
                            bank.Sufix_Draft = reader["Sufix_Draft"].ToString();
                            bank.Reimbursement_Text_Status = reader["Reimbursement_Text_Status"].ToString();
                            bank.Contact_Person1 = reader["Contact_Person1"].ToString();
                            bank.Contact_Title1 = reader["Contact_Title1"].ToString();
                            bank.Mob_Number1 = reader["Mob_Number1"].ToString();
                            bank.Contact_Person2 = reader["Contact_Person2"].ToString();
                            bank.Contact_Title2 = reader["Contact_Title2"].ToString();
                            bank.Mob_Number2 = reader["Mob_Number2"].ToString();
                            bank.Remarks = reader["Remarks"].ToString();
                            bank.Record_Status = reader["Record_Status"].ToString();
                            bank.Option_Status = reader["Option_Status"].ToString();
                            bank.Updated_User = reader["Updated_User"] != DBNull.Value ? (int?)reader["Updated_User"] : null;
                            bank.Updated_Date = reader["Updated_Date"] != DBNull.Value ? (DateTime?)reader["Updated_Date"] : null;
                            bank.HO_Branch1_Id = reader["HO_Branch1_Id"] != DBNull.Value ? (int?)reader["HO_Branch1_Id"] : null;
                            bank.Dispatch_Status = reader["Dispatch_Status"].ToString();
                            bank.Dispatch_Header = reader["Dispatch_Header"].ToString();
                            bank.Dispatch_Type = reader["Dispatch_Type"].ToString();
                            bank.Column_Separator = reader["Column_Separator"].ToString();
                            bank.Bank_Code = reader["Bank_Code"].ToString();
                            bank.Remittance_Sequence_Prefix = reader["Remittance_Sequence_Prefix"].ToString();
                            bank.Common_Branch_Id = reader["Common_Branch_Id"] != DBNull.Value ? (int?)reader["Common_Branch_Id"] : null;
                            bank.Column_Demarkation_Char = reader["Column_Demarkation_Char"].ToString();
                            bank.IsDefaultBank = reader["IsDefaultBank"] != DBNull.Value ? (int?)reader["IsDefaultBank"] : null;
                        }
                    }
                }

                return bank;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string UpdateBank(Bank_Mst objBank)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("UpdateBank", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UID", objBank.UID);
                        command.Parameters.AddWithValue("@English_Name", objBank.English_Name);
                        command.Parameters.AddWithValue("@Arabic_Name", objBank.Arabic_Name);
                        command.Parameters.AddWithValue("@Short_English_Name", objBank.Short_English_Name);
                        command.Parameters.AddWithValue("@Short_Arabic_Name", objBank.Short_Arabic_Name);
                        command.Parameters.AddWithValue("@Country_Id", objBank.Country_Id);
                        command.Parameters.AddWithValue("@Currency_Id", objBank.Currency_Id);
                        command.Parameters.AddWithValue("@Upper_Limit", objBank.Upper_Limit);
                        command.Parameters.AddWithValue("@Address_Line1", objBank.Address_Line1);
                        command.Parameters.AddWithValue("@Address_Line2", objBank.Address_Line2);
                        command.Parameters.AddWithValue("@Address_Line3", objBank.Address_Line3);
                        command.Parameters.AddWithValue("@Record_Status", objBank.Record_Status ?? "In_Active");

                        command.ExecuteNonQuery();
                    }
                }

                return "success";
            }
            catch (Exception ex)
            {
                
                throw new Exception("Failed to update bank", ex);
            }
        }
    }
}
