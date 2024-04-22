using BusinessLogicLayer.DomainEntities;
using DataAccessLayer.Entities;
using DataAccessLayer.ProcedureResults;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace KuaiexDashboard
{
    public class BeneficiaryDAL
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["KUAIEXEntities"].ConnectionString;
        public List<Remittance_Purpose_Lookup> GetRemittancePurposeList()
        {
            List<Remittance_Purpose_Lookup> remittancePurposes = new List<Remittance_Purpose_Lookup>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetRemittancePurposeList", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "Remittance_Purpose_Lookup");
                    foreach (DataRow row in ds.Tables["Remittance_Purpose_Lookup"].Rows)
                    {
                        Remittance_Purpose_Lookup remittancePurpose = new Remittance_Purpose_Lookup
                        {
                            Id = row.Field<int>("Id"),
                            Name = row.Field<string>("Name"),
                            Record_Status = row.Field<string>("Record_Status"),
                            Display_Order = row.Field<int>("Display_Order")
                        };

                        remittancePurposes.Add(remittancePurpose);
                    }
                }
            }

            return remittancePurposes;
        }

        public List<Remittance_Type_Mst> GetRemittanceTypeList()
        {
            List<Remittance_Type_Mst> remittanceTypeList = new List<Remittance_Type_Mst>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetRemittanceTypeList", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "Remittance_Type_Mst");

                    foreach (DataRow row in ds.Tables["Remittance_Type_Mst" +
                        ""].Rows)
                    {
                        Remittance_Type_Mst remittanceType = new Remittance_Type_Mst
                        {
                            Remittance_Type_Id = row.Field<int>("Remittance_Type_Id"),
                            English_Name = row.Field<string>("English_Name"),
                            Arabic_Name = row.Field<string>("Arabic_Name"),
                            Display_Order = row.Field<int?>("Display_Order"),
                            Remittance_Master_Type = row.Field<string>("Remittance_Master_Type"),
                            Voucher_Sub_Type = row.Field<string>("Voucher_Sub_Type"),
                            Record_Status = row.Field<string>("Record_Status"),
                            Option_Status = row.Field<string>("Option_Status"),
                            Updated_User = row.Field<int?>("Updated_User"),
                            Updated_Date = row.Field<DateTime?>("Updated_Date")
                        };

                        remittanceTypeList.Add(remittanceType);
                    }
                }
            }

            return remittanceTypeList;
        }
       
        

    public List<Bank_Account_Type> GetBankAccountTypes()
        {
            List<Bank_Account_Type> bankAccountTypes = new List<Bank_Account_Type>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetBankAccountTypes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "Bank_Account_Type");

                    foreach (DataRow row in ds.Tables["Bank_Account_Type"].Rows)
                    {
                        Bank_Account_Type bankAccountType = new Bank_Account_Type
                        {
                            Id = row.Field<int>("Id"),
                            Name = row.Field<string>("Name"),
                            Status = row.Field<int?>("Status")
                        };

                        bankAccountTypes.Add(bankAccountType);
                    }
                }
            }

            return bankAccountTypes;
        }
        public List<IdentificationTypeLookup> GetIdentificationTypes()
        {
            List<IdentificationTypeLookup> identificationTypes = new List<IdentificationTypeLookup>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetIdentificationTypes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "IdentificationTypeLookup");
                    foreach (DataRow row in ds.Tables["IdentificationTypeLookup"].Rows)
                    {
                        IdentificationTypeLookup identificationType = new IdentificationTypeLookup
                        {
                            Id = row.Field<int>("Id"),
                            Description = row.Field<string>("Description")
                        };

                        identificationTypes.Add(identificationType);
                    }
                }
            }

            return identificationTypes;
        }
        public List<City> GetBranchCities()
        {
            List<City> branchCities = new List<City>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetBranchCities", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "City");
                    foreach (DataRow row in ds.Tables["City"].Rows)
                    {
                        City branchCity = new City
                        {
                            Id = row.Field<int>("Id"),
                            Country_Id = row.Field<int>("Country_Id"),
                            Name = row.Field<string>("Name"),
                            Status = Convert.ToInt32(row.Field<string>("Status")),
                            UID = row.Field<Guid?>("UID"),
                            CreatedBy = row.Field<int?>("CreatedBy"),
                            CreatedOn = row.Field<DateTime?>("CreatedOn"),
                            UpdatedBy = row.Field<int?>("UpdatedBy"),
                            UpdatedOn = row.Field<DateTime?>("UpdatedOn"),
                            Prod_City_Id = row.Field<int?>("Prod_City_Id")
                        };

                        branchCities.Add(branchCity);
                    }
                }
            }

            return branchCities;
        }

        public List<API_GetBank_MstByCountry_Id_Result> GetBanksByCountryId(int countryId)
        {
            List<API_GetBank_MstByCountry_Id_Result> banks = new List<API_GetBank_MstByCountry_Id_Result>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("API_GetBank_MstByCountry_Id", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Country_Id", SqlDbType.Int).Value = countryId;
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "Bank_Mst");
                    foreach (DataRow row in ds.Tables["Bank_Mst"].Rows)
                    {
                        API_GetBank_MstByCountry_Id_Result bank = new API_GetBank_MstByCountry_Id_Result
                        {
                            Bank_Id = row.Field<int>("Bank_Id"),
                            English_Name = row.Field<string>("English_Name")
                        };

                        banks.Add(bank);
                    }
                }
            }

            return banks;
        }
        public List<API_GetRoutingBank_Branch_Result> GetBankBranchesByBankId(int bankId)
        {
            List<API_GetRoutingBank_Branch_Result> bankBranches = new List<API_GetRoutingBank_Branch_Result>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("API_GetBankBranchesByBankId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Bank_Id", SqlDbType.Int).Value = bankId;
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "Bank_Branch_Mst");

                    foreach (DataRow row in ds.Tables["Bank_Branch_Mst"].Rows)
                    {
                        API_GetRoutingBank_Branch_Result bankBranch = new API_GetRoutingBank_Branch_Result
                        {
                            Bank_Branch_Id = row.Field<int>("Bank_Branch_Id"),
                            Bank_Id = row.Field<int?>("Bank_Id"),
                            Branch_Code = row.Field<string>("Branch_Code"),
                            English_Name = row.Field<string>("English_Name"),
                            Arabic_Name = row.Field<string>("Arabic_Name"),
                            City_Id = row.Field<int?>("City_Id"),
                            Country_Id = row.Field<int?>("Country_Id"),
                            Currency_Id = row.Field<int?>("Currency_Id"),
                            Upper_Limit = row.Field<decimal?>("Upper_Limit"),
                            Account_Reference = row.Field<string>("Account_Reference"),
                            Reference_Location = row.Field<string>("Reference_Location"),
                            Address_Line1 = row.Field<string>("Address_Line1"),
                            Address_Line2 = row.Field<string>("Address_Line2"),
                            Address_Line3 = row.Field<string>("Address_Line3"),
                            Tel_Number1 = row.Field<string>("Tel_Number1"),
                            Fax_Number1 = row.Field<string>("Fax_Number1"),
                            EMail_Address1 = row.Field<string>("EMail_Address1"),
                            Contact_Person1 = row.Field<string>("Contact_Person1"),
                            Contact_Title1 = row.Field<string>("Contact_Title1"),
                            Contact_Person2 = row.Field<string>("Contact_Person2"),
                            Contact_Title2 = row.Field<string>("Contact_Title2"),
                            Draft_Status = row.Field<string>("Draft_Status"),
                            Record_Status = row.Field<string>("Record_Status"),
                            Option_Status = row.Field<string>("Option_Status"),
                            Updated_User = row.Field<int?>("Updated_User"),
                            Updated_Date = row.Field<DateTime?>("Updated_Date"),
                            Bank_SubAgent_Id = row.Field<int?>("Bank_SubAgent_Id")
                           
                        };

                        bankBranches.Add(bankBranch);
                    }
                }
            }

            return bankBranches;
        }
        public int CheckBeneficiaryExistence(string fullName)
        {
            int existenceStatus = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("CheckBeneficiaryExistence", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@FullName", fullName);

                        SqlParameter existenceStatusParam = new SqlParameter("@ExistenceStatus", SqlDbType.Int);
                        existenceStatusParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(existenceStatusParam);

                        command.ExecuteNonQuery();

                        existenceStatus = Convert.ToInt32(existenceStatusParam.Value);
                    }
                }
            }
            catch (Exception ex)
            {
               
            }

            return existenceStatus;
        }
    
        
    }
}
