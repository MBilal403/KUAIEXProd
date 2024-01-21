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
   public class BranchesDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["KUAIEXProdEntities"].ConnectionString;
        public List<GetCityList_Result> GetCityList()
        {
            List<GetCityList_Result> cities = new List<GetCityList_Result>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetCityList", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataSet dataSet = new DataSet();
                            adapter.Fill(dataSet, "City");

                            foreach (DataRow row in dataSet.Tables["City"].Rows)
                            {
                                GetCityList_Result city = new GetCityList_Result
                                {
                                    UID = (Guid)row["UID"],
                                    Name = row["Name"].ToString(),
                                    Status = row["Status"] == DBNull.Value ? 0 : Convert.ToInt32(row["Status"]),
                                    Country = row["Country"].ToString()
                                };

                                cities.Add(city);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return cities;
        }
        public List<GetBranchesListbyCountryBank_Result> GetBranchesListbyCountryBank(int country, int bank)
        {
            List<GetBranchesListbyCountryBank_Result> branches = new List<GetBranchesListbyCountryBank_Result>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetBranchesListbyCountryBank", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Country", SqlDbType.Int).Value = country;
                        command.Parameters.Add("@Bank", SqlDbType.Int).Value = bank;

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GetBranchesListbyCountryBank_Result branch = new GetBranchesListbyCountryBank_Result
                                {
                                    English_Name = reader["English_Name"].ToString(),
                                    Address_Line1 = reader["Address_Line1"].ToString(),
                                    Address_Line2 = reader["Address_Line2"].ToString(),
                                    EMail_Address1 = reader["EMail_Address1"].ToString(),
                                    Address_Line3 = reader["Address_Line3"].ToString(),
                                    Country_Name = reader["Country_Name"].ToString(),
                                    City_Name = reader["City_Name"].ToString(),
                                    Bank_Name = reader["Bank_Name"].ToString(),
                                    Currency = reader["Currency"].ToString()
                                };

                                branches.Add(branch);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return branches;
        }
        public List<GetBranchesList_Result> GetBranchesList()
        {
            List<GetBranchesList_Result> branches = new List<GetBranchesList_Result>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetBranchesList", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "Bank_Branch_Mst");

                        foreach (DataRow row in ds.Tables["Bank_Branch_Mst"].Rows)
                        {
                            GetBranchesList_Result branch = new GetBranchesList_Result
                            {
                                English_Name = row["English_Name"].ToString(),
                                Address_Line1 = row["Address_Line1"].ToString(),
                                Address_Line2 = row["Address_Line2"].ToString(),
                                EMail_Address1 = row["EMail_Address1"].ToString(),
                                Address_Line3 = row["Address_Line3"].ToString(),
                                Country_Name = row["Country_Name"].ToString(),
                                City_Name = row["City_Name"].ToString(),
                                Bank_Name = row["Bank_Name"].ToString(),
                                Currency = row["Currency"].ToString()
                            };

                            branches.Add(branch);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return branches;
        }
        public bool AddBank(Bank_Mst objBank)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("AddBank", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@English_Name", SqlDbType.NVarChar).Value = objBank.English_Name;
                        command.Parameters.Add("@Arabic_Name", SqlDbType.NVarChar).Value = objBank.Arabic_Name;
                        command.Parameters.Add("@Short_English_Name", SqlDbType.NVarChar).Value = objBank.Short_English_Name;
                        command.Parameters.Add("@Short_Arabic_Name", SqlDbType.NVarChar).Value = objBank.Short_Arabic_Name;
                        command.Parameters.Add("@Country_Id", SqlDbType.Int).Value = objBank.Country_Id;
                        command.Parameters.Add("@Currency_Id", SqlDbType.Int).Value = objBank.Currency_Id;
                        command.Parameters.Add("@Upper_Limit", SqlDbType.Decimal).Value = objBank.Upper_Limit;
                        command.Parameters.Add("@Address_Line1", SqlDbType.NVarChar).Value = objBank.Address_Line1;
                        command.Parameters.Add("@Address_Line2", SqlDbType.NVarChar).Value = objBank.Address_Line2;
                        command.Parameters.Add("@Address_Line3", SqlDbType.NVarChar).Value = objBank.Address_Line3;
                        command.Parameters.Add("@Tel_Number1", SqlDbType.NVarChar).Value = objBank.Tel_Number1;
                        command.Parameters.Add("@Fax_Number1", SqlDbType.NVarChar).Value = objBank.Fax_Number1;
                        command.Parameters.Add("@EMail_Address1", SqlDbType.NVarChar).Value = objBank.EMail_Address1;
                        command.Parameters.Add("@Prefix_Draft", SqlDbType.NVarChar).Value = objBank.Prefix_Draft;
                        command.Parameters.Add("@Sufix_Draft", SqlDbType.NVarChar).Value = objBank.Sufix_Draft;
                        command.Parameters.Add("@Reimbursement_Text_Status", SqlDbType.NVarChar).Value = objBank.Reimbursement_Text_Status;
                        command.Parameters.Add("@Contact_Person1", SqlDbType.NVarChar).Value = objBank.Contact_Person1;
                        command.Parameters.Add("@Contact_Title1", SqlDbType.NVarChar).Value = objBank.Contact_Title1;
                        command.Parameters.Add("@Mob_Number1", SqlDbType.NVarChar).Value = objBank.Mob_Number1;
                        command.Parameters.Add("@Contact_Person2", SqlDbType.NVarChar).Value = objBank.Contact_Person2;
                        command.Parameters.Add("@Contact_Title2", SqlDbType.NVarChar).Value = objBank.Contact_Title2;
                        command.Parameters.Add("@Mob_Number2", SqlDbType.NVarChar).Value = objBank.Mob_Number2;
                        command.Parameters.Add("@Remarks", SqlDbType.NVarChar).Value = objBank.Remarks;
                        command.Parameters.Add("@Record_Status", SqlDbType.NVarChar).Value = objBank.Record_Status;
                        command.Parameters.Add("@Option_Status", SqlDbType.NVarChar).Value = objBank.Option_Status;
                        command.Parameters.Add("@Updated_User", SqlDbType.Int).Value = objBank.Updated_User;
                        command.Parameters.Add("@Updated_Date", SqlDbType.DateTime).Value = objBank.Updated_Date;
                        command.Parameters.Add("@HO_Branch1_Id", SqlDbType.Int).Value = objBank.HO_Branch1_Id;
                        command.Parameters.Add("@Dispatch_Status", SqlDbType.NVarChar).Value = objBank.Dispatch_Status;
                        command.Parameters.Add("@Dispatch_Header", SqlDbType.NVarChar).Value = objBank.Dispatch_Header;
                        command.Parameters.Add("@Dispatch_Type", SqlDbType.NVarChar).Value = objBank.Dispatch_Type;
                        command.Parameters.Add("@Column_Separator", SqlDbType.NVarChar).Value = objBank.Column_Separator;
                        command.Parameters.Add("@Bank_Code", SqlDbType.NVarChar).Value = objBank.Bank_Code;
                        command.Parameters.Add("@Remittance_Sequence_Prefix", SqlDbType.NVarChar).Value = objBank.Remittance_Sequence_Prefix;
                        command.Parameters.Add("@Common_Branch_Id", SqlDbType.Int).Value = objBank.Common_Branch_Id;
                        command.Parameters.Add("@Column_Demarkation_Char", SqlDbType.NVarChar).Value = objBank.Column_Demarkation_Char;
                        command.Parameters.Add("@IsDefaultBank", SqlDbType.Int).Value = objBank.IsDefaultBank;
                        command.Parameters.Add("@UID", SqlDbType.UniqueIdentifier).Value = objBank.UID;

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Bank_Mst GetBankByName(string bankName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetBankByName", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@English_Name", SqlDbType.NVarChar).Value = bankName;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Bank_Mst bank = new Bank_Mst
                                {
                                    Bank_Id = reader.GetInt32(reader.GetOrdinal("Bank_Id")),
                                    English_Name = reader.GetString(reader.GetOrdinal("English_Name")),
                                    Arabic_Name = reader.GetString(reader.GetOrdinal("Arabic_Name")),
                                    Short_English_Name = reader.GetString(reader.GetOrdinal("Short_English_Name")),
                                    Short_Arabic_Name = reader.GetString(reader.GetOrdinal("Short_Arabic_Name")),
                                    Country_Id = reader.IsDBNull(reader.GetOrdinal("Country_Id")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("Country_Id")),
                                    Currency_Id = reader.IsDBNull(reader.GetOrdinal("Currency_Id")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("Currency_Id")),
                                    Upper_Limit = reader.IsDBNull(reader.GetOrdinal("Upper_Limit")) ? null : (decimal?)reader.GetDecimal(reader.GetOrdinal("Upper_Limit")),
                                    Address_Line1 = reader.IsDBNull(reader.GetOrdinal("Address_Line1")) ? null : reader.GetString(reader.GetOrdinal("Address_Line1")),
                                    Address_Line2 = reader.IsDBNull(reader.GetOrdinal("Address_Line2")) ? null : reader.GetString(reader.GetOrdinal("Address_Line2")),
                                    Address_Line3 = reader.IsDBNull(reader.GetOrdinal("Address_Line3")) ? null : reader.GetString(reader.GetOrdinal("Address_Line3")),
                                    Tel_Number1 = reader.IsDBNull(reader.GetOrdinal("Tel_Number1")) ? null : reader.GetString(reader.GetOrdinal("Tel_Number1")),
                                    Fax_Number1 = reader.IsDBNull(reader.GetOrdinal("Fax_Number1")) ? null : reader.GetString(reader.GetOrdinal("Fax_Number1")),
                                    EMail_Address1 = reader.IsDBNull(reader.GetOrdinal("EMail_Address1")) ? null : reader.GetString(reader.GetOrdinal("EMail_Address1")),
                                    Prefix_Draft = reader.IsDBNull(reader.GetOrdinal("Prefix_Draft")) ? null : reader.GetString(reader.GetOrdinal("Prefix_Draft")),
                                    Sufix_Draft = reader.IsDBNull(reader.GetOrdinal("Sufix_Draft")) ? null : reader.GetString(reader.GetOrdinal("Sufix_Draft")),
                                    Reimbursement_Text_Status = reader.IsDBNull(reader.GetOrdinal("Reimbursement_Text_Status")) ? null : reader.GetString(reader.GetOrdinal("Reimbursement_Text_Status")),
                                    Contact_Person1 = reader.IsDBNull(reader.GetOrdinal("Contact_Person1")) ? null : reader.GetString(reader.GetOrdinal("Contact_Person1")),
                                    Contact_Title1 = reader.IsDBNull(reader.GetOrdinal("Contact_Title1")) ? null : reader.GetString(reader.GetOrdinal("Contact_Title1")),
                                    Mob_Number1 = reader.IsDBNull(reader.GetOrdinal("Mob_Number1")) ? null : reader.GetString(reader.GetOrdinal("Mob_Number1")),
                                    Contact_Person2 = reader.IsDBNull(reader.GetOrdinal("Contact_Person2")) ? null : reader.GetString(reader.GetOrdinal("Contact_Person2")),
                                    Contact_Title2 = reader.IsDBNull(reader.GetOrdinal("Contact_Title2")) ? null : reader.GetString(reader.GetOrdinal("Contact_Title2")),
                                    Mob_Number2 = reader.IsDBNull(reader.GetOrdinal("Mob_Number2")) ? null : reader.GetString(reader.GetOrdinal("Mob_Number2")),
                                    Remarks = reader.IsDBNull(reader.GetOrdinal("Remarks")) ? null : reader.GetString(reader.GetOrdinal("Remarks")),
                                    Record_Status = reader.IsDBNull(reader.GetOrdinal("Record_Status")) ? null : reader.GetString(reader.GetOrdinal("Record_Status")),
                                    Option_Status = reader.IsDBNull(reader.GetOrdinal("Option_Status")) ? null : reader.GetString(reader.GetOrdinal("Option_Status")),
                                    Updated_User = reader.IsDBNull(reader.GetOrdinal("Updated_User")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("Updated_User")),
                                    Updated_Date = reader.IsDBNull(reader.GetOrdinal("Updated_Date")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("Updated_Date")),
                                    HO_Branch1_Id = reader.IsDBNull(reader.GetOrdinal("HO_Branch1_Id")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("HO_Branch1_Id")),
                                    Dispatch_Status = reader.IsDBNull(reader.GetOrdinal("Dispatch_Status")) ? null : reader.GetString(reader.GetOrdinal("Dispatch_Status")),
                                    Dispatch_Header = reader.IsDBNull(reader.GetOrdinal("Dispatch_Header")) ? null : reader.GetString(reader.GetOrdinal("Dispatch_Header")),
                                    Dispatch_Type = reader.IsDBNull(reader.GetOrdinal("Dispatch_Type")) ? null : reader.GetString(reader.GetOrdinal("Dispatch_Type")),
                                    Column_Separator = reader.IsDBNull(reader.GetOrdinal("Column_Separator")) ? null : reader.GetString(reader.GetOrdinal("Column_Separator")),
                                    Bank_Code = reader.IsDBNull(reader.GetOrdinal("Bank_Code")) ? null : reader.GetString(reader.GetOrdinal("Bank_Code")),
                                    Remittance_Sequence_Prefix = reader.IsDBNull(reader.GetOrdinal("Remittance_Sequence_Prefix")) ? null : reader.GetString(reader.GetOrdinal("Remittance_Sequence_Prefix")),
                                    Common_Branch_Id = reader.IsDBNull(reader.GetOrdinal("Common_Branch_Id")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("Common_Branch_Id")),
                                    Column_Demarkation_Char = reader.IsDBNull(reader.GetOrdinal("Column_Demarkation_Char")) ? null : reader.GetString(reader.GetOrdinal("Column_Demarkation_Char")),
                                    IsDefaultBank = reader.IsDBNull(reader.GetOrdinal("IsDefaultBank")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("IsDefaultBank")),
                                    UID = reader.IsDBNull(reader.GetOrdinal("UID")) ? null : (Guid?)reader.GetGuid(reader.GetOrdinal("UID")),
                                };

                                return bank;
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
        public Bank_Mst GetBankByUID(Guid? UID)
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
