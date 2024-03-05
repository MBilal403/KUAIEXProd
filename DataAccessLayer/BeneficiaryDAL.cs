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
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["KUAIEXProdEntities"].ConnectionString;
        public List<GetBeneficiaryList_Result> GetBeneficiaryList()
        {
            List<GetBeneficiaryList_Result> beneficiaries = new List<GetBeneficiaryList_Result>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetBeneficiaryList", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "Beneficiary");

                    foreach (DataRow row in ds.Tables["Beneficiary"].Rows)
                    {
                        GetBeneficiaryList_Result beneficiary = new GetBeneficiaryList_Result
                        {
                            UID = row.Field<Guid?>("UID"),
                            FullName = row.Field<string>("FullName"),
                            Birth_Date = row.Field<DateTime?>("Birth_Date"),
                            Address_Line3 = row.Field<string>("Address_Line3"),
                            Branch_Address_Line3 = row.Field<string>("Branch_Address_Line3"),
                            Country_Name = row.Field<string>("Country_Id"),
                            Currency = row.Field<string>("Currency_Id"),
                            Bank_Name = row.Field<string>("Bank_Id"),
                            Customer_Name = row.Field<string>("Customer_Id")
                        };

                        beneficiaries.Add(beneficiary);
                    }
                }
            }

            return beneficiaries;
        }
        public List<Beneficiary> GetAllBeneficiary()
        {
            List<Beneficiary> beneficiaries = new List<Beneficiary>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetAllBeneficiary", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "Beneficiary");

                    foreach (DataRow row in ds.Tables["Beneficiary"].Rows)
                    {
                        Beneficiary beneficiary = new Beneficiary
                        {
                            Beneficiary_Id = Convert.ToInt32(row.Field<int>("Beneficiary_Id")),
                            Customer_Id = Convert.ToInt32(row.Field<int?>("Customer_Id")),
                            FullName = row.Field<string>("FullName"),
                            Address_Line1 = row.Field<string>("Address_Line1"),
                            Address_Line2 = row.Field<string>("Address_Line2"),
                            Address_Line3 = row.Field<string>("Address_Line3"),
                            Country_Id = row.Field<int?>("Country_Id"),
                            Currency_Id = row.Field<int?>("Currency_Id"),
                            Birth_Date = row.Field<DateTime>("Birth_Date"),
                            Remittance_Purpose = row.Field<string>("Remittance_Purpose"),
                            Remittance_Type_Id = row.Field<int?>("Remittance_Type_Id"),
                            Remittance_Instruction = row.Field<string>("Remittance_Instruction"),
                            Phone_No = row.Field<string>("Phone_No"),
                            Mobile_No = row.Field<string>("Mobile_No"),
                            Fax_No = row.Field<string>("Fax_No"),
                            Email_Address1 = row.Field<string>("Email_Address1"),
                            Email_Address2 = row.Field<string>("Email_Address2"),
                            Identification_Type = row.Field<int?>("Identification_Type"),
                            Identification_No = row.Field<string>("Identification_No"),
                            Identification_Remarks = row.Field<string>("Identification_Remarks"),
                            Identification_Issue_Date = row.Field<DateTime>("Identification_Issue_Date"),
                            Identification_Expiry_Date = row.Field<DateTime>("Identification_Expiry_Date"),
                            Bank_Id = row.Field<int?>("Bank_Id"),
                            Branch_Id = row.Field<int?>("Branch_Id"),
                            Bank_Account_No = row.Field<string>("Bank_Account_No"),
                            Bank_Name = row.Field<string>("Bank_Name"),
                            Branch_Name = row.Field<string>("Branch_Name"),
                            Branch_City_Id = row.Field<int?>("Branch_City_Id"),
                            Branch_City_Name = row.Field<string>("Branch_City_Name"),
                            Branch_Address_Line1 = row.Field<string>("Branch_Address_Line1"),
                            Branch_Address_Line2 = row.Field<string>("Branch_Address_Line2"),
                            Branch_Address_Line3 = row.Field<string>("Branch_Address_Line3"),
                            Branch_Number = row.Field<string>("Branch_Number"),
                            Branch_Phone_Number = row.Field<string>("Branch_Phone_Number"),
                            Branch_Fax_Number = row.Field<string>("Branch_Fax_Number"),
                            Destination_Country_Id = row.Field<int?>("Destination_Country_Id"),
                            Destination_City_Id = row.Field<int?>("Destination_City_Id"),
                            DD_Beneficiary_Name = row.Field<string>("DD_Beneficiary_Name"),
                            Bank_Account_type = row.Field<string>("Bank_Account_type"),
                            Gender = row.Field<int?>("Gender"),
                            Remittance_Remarks = row.Field<string>("Remittance_Remarks"),
                            Bank_Code = row.Field<string>("Bank_Code"),
                            Routing_Bank_Id = row.Field<int?>("Routing_Bank_Id"),
                            Routing_Bank_Branch_Id = row.Field<int?>("Routing_Bank_Branch_Id"),
                            Remittance_Subtype_Id = row.Field<int?>("Remittance_Subtype_Id"),
                            Birth_Place = row.Field<string>("Birth_Place"),
                            IsBannedList = row.Field<char>("IsBannedList"),
                            BannedListCreatedOn = row.Field<DateTime>("BannedListCreatedOn"),
                            BannedListClearedBy = row.Field<int?>("BannedListClearedBy"),
                            TransFastInfo = row.Field<string>("TransFastInfo"),
                            UID = row.Field<Guid>("UID"),
                            CreatedBy = row.Field<int?>("CreatedBy"),
                            CreatedOn = row.Field<DateTime>("CreatedOn"),
                            CreatedIp = row.Field<string>("CreatedIp"),
                            UpdatedBy = row.Field<int?>("UpdatedBy"),
                            UpdatedOn = row.Field<DateTime>("UpdatedOn"),
                            UpdatedIp = row.Field<string>("UpdatedIp"),
                            Prod_Beneficiary_Id = row.Field<int?>("Prod_Beneficiary_Id"),
                            Remittance_Purpose_Detail = row.Field<string>("Remittance_Purpose_Detail"),
                            Remitter_Relation = row.Field<string>("Remitter_Relation"),
                            Remitter_Relation_Detail = row.Field<string>("Remitter_Relation_Detail"),
                            Source_Of_Income = row.Field<string>("Source_Of_Income"),
                            Source_Of_Income_Detail = row.Field<string>("Source_Of_Income_Detail"),
                            Nationality_Id = row.Field<int?>("Nationality_Id"),
                            Bank_Account_Title = row.Field<string>("Bank_Account_Title"),
                        };
                        beneficiaries.Add(beneficiary);
                    }
                }
            }

            return beneficiaries;
        }

        public Beneficiary GetBeneficiaryByUID(Guid? uid)
        {
            Beneficiary beneficiary = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetBeneficiaryByUID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@UID", SqlDbType.UniqueIdentifier).Value = (object)uid ?? DBNull.Value;

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            beneficiary = new Beneficiary
                            {
                                Beneficiary_Id = reader.GetInt32(reader.GetOrdinal("Beneficiary_Id")),
                                Customer_Id = reader.GetInt32(reader.GetOrdinal("Customer_Id")),
                                FullName = reader.GetString(reader.GetOrdinal("FullName")),
                                Address_Line1 = reader.GetString(reader.GetOrdinal("Address_Line1")),
                                Address_Line2 = reader.GetString(reader.GetOrdinal("Address_Line2")),
                                Address_Line3 = reader.GetString(reader.GetOrdinal("Address_Line3")),
                                Country_Id = reader.GetInt32(reader.GetOrdinal("Country_Id")),
                                Currency_Id = reader.GetInt32(reader.GetOrdinal("Currency_Id")),
                                Birth_Date = reader.GetDateTime(reader.GetOrdinal("Birth_Date")),
                                Remittance_Purpose = reader.GetString(reader.GetOrdinal("Remittance_Purpose")),
                                Remittance_Type_Id = reader.GetInt32(reader.GetOrdinal("Remittance_Type_Id")),
                                Remittance_Instruction = reader.GetString(reader.GetOrdinal("Remittance_Instruction")),
                                Phone_No = reader.GetString(reader.GetOrdinal("Phone_No")),
                                Mobile_No = reader.GetString(reader.GetOrdinal("Mobile_No")),
                                Fax_No = reader.GetString(reader.GetOrdinal("Fax_No")),
                                Email_Address1 = reader.GetString(reader.GetOrdinal("Email_Address1")),
                                Email_Address2 = reader.GetString(reader.GetOrdinal("Email_Address2")),
                                Identification_Type = reader.GetInt32(reader.GetOrdinal("Identification_Type")),
                                Identification_No = reader.GetString(reader.GetOrdinal("Identification_No")),
                                Identification_Remarks = reader.GetString(reader.GetOrdinal("Identification_Remarks")),
                                Identification_Issue_Date = reader.GetDateTime(reader.GetOrdinal("Identification_Issue_Date")),
                                Identification_Expiry_Date = reader.GetDateTime(reader.GetOrdinal("Identification_Expiry_Date")),
                                Bank_Id = reader.GetInt32(reader.GetOrdinal("Bank_Id")),
                                Branch_Id = reader.GetInt32(reader.GetOrdinal("Branch_Id")),
                                Bank_Account_No = reader.GetString(reader.GetOrdinal("Bank_Account_No")),
                                Bank_Name = reader.GetString(reader.GetOrdinal("Bank_Name")),
                                Branch_Name = reader.GetString(reader.GetOrdinal("Branch_Name")),
                                Branch_City_Id = reader.GetInt32(reader.GetOrdinal("Branch_City_Id")),
                                Branch_City_Name = reader.GetString(reader.GetOrdinal("Branch_City_Name")),
                                Branch_Address_Line1 = reader.GetString(reader.GetOrdinal("Branch_Address_Line1")),
                                Branch_Address_Line2 = reader.GetString(reader.GetOrdinal("Branch_Address_Line2")),
                                Branch_Address_Line3 = reader.GetString(reader.GetOrdinal("Branch_Address_Line3")),
                                Branch_Number = reader.GetString(reader.GetOrdinal("Branch_Number")),
                                Branch_Phone_Number = reader.GetString(reader.GetOrdinal("Branch_Phone_Number")),
                                Branch_Fax_Number = reader.GetString(reader.GetOrdinal("Branch_Fax_Number")),
                                Destination_Country_Id = reader.GetInt32(reader.GetOrdinal("Destination_Country_Id")),
                                Destination_City_Id = reader.GetInt32(reader.GetOrdinal("Destination_City_Id")),
                              //  Remitter_Relation_Id = reader.GetInt32(reader.GetOrdinal("Remitter_Relation_Id")),
                                DD_Beneficiary_Name = reader.GetString(reader.GetOrdinal("DD_Beneficiary_Name")),
                                Bank_Account_type = reader.GetString(reader.GetOrdinal("Bank_Account_type")),
                                Gender = reader.GetInt32(reader.GetOrdinal("Gender")),
                                Remittance_Remarks = reader.GetString(reader.GetOrdinal("Remittance_Remarks")),
                                Bank_Code = reader.GetString(reader.GetOrdinal("Bank_Code")),
                                Routing_Bank_Id = reader.GetInt32(reader.GetOrdinal("Routing_Bank_Id")),
                                Routing_Bank_Branch_Id = reader.GetInt32(reader.GetOrdinal("Routing_Bank_Branch_Id")),
                                Remittance_Subtype_Id = reader.GetInt32(reader.GetOrdinal("Remittance_Subtype_Id")),
                                Birth_Place = reader.GetString(reader.GetOrdinal("Birth_Place")),
                                IsBannedList = (char)reader.GetValue(reader.GetOrdinal("IsBannedList")),
                                BannedListCreatedOn = reader.GetDateTime(reader.GetOrdinal("BannedListCreatedOn")),
                                BannedListClearedBy = reader.GetInt32(reader.GetOrdinal("BannedListClearedBy")),
                                TransFastInfo = reader.GetString(reader.GetOrdinal("TransFastInfo")),
                                UID = (Guid)reader.GetValue(reader.GetOrdinal("UID")),
                                CreatedBy = reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                                CreatedOn = reader.GetDateTime(reader.GetOrdinal("CreatedOn")),
                                CreatedIp = reader.GetString(reader.GetOrdinal("CreatedIp")),
                                UpdatedBy = reader.GetInt32(reader.GetOrdinal("UpdatedBy")),
                                UpdatedOn = reader.GetDateTime(reader.GetOrdinal("UpdatedOn")),
                                UpdatedIp = reader.GetString(reader.GetOrdinal("UpdatedIp")),
                                Prod_Beneficiary_Id = reader.GetInt32(reader.GetOrdinal("Prod_Beneficiary_Id")),
                                Remittance_Purpose_Detail = reader.GetString(reader.GetOrdinal("Remittance_Purpose_Detail")),
                                Remitter_Relation = reader.GetString(reader.GetOrdinal("Remitter_Relation")),
                                Remitter_Relation_Detail = reader.GetString(reader.GetOrdinal("Remitter_Relation_Detail")),
                                Source_Of_Income = reader.GetString(reader.GetOrdinal("Source_Of_Income")),
                                Source_Of_Income_Detail = reader.GetString(reader.GetOrdinal("Source_Of_Income_Detail")),
                                Nationality_Id = reader.GetInt32(reader.GetOrdinal("Nationality_Id")),
                                Bank_Account_Title = reader.GetString(reader.GetOrdinal("Bank_Account_Title")),
                            };
                        }
                    }
                }
            }

            return beneficiary;
        }
        public List<Currency_Result> GetCurrencyByCountry(int countryId)
        {
            List<Currency_Result> currencyList = new List<Currency_Result>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("GetCurrencyByCountry", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CountryId", countryId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();

                connection.Open();
                adapter.Fill(dataSet);

                if (dataSet.Tables.Count > 0)
                {
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        Currency_Result currency = new Currency_Result
                        {
                            CurrencyId = Convert.ToInt32(row["CurrencyId"]),
                            CurrencyName = row["CurrencyName"].ToString(),
                            CurrencyCode = row["CurrencyCode"].ToString()
                        };

                        currencyList.Add(currency);
                    }
                }
            }

            return currencyList;
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
                            Id = row.Field<int>("Id"),
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
        public List<Bank_Branch_Mst> GetBankBranches(int bankId)
        {
            List<Bank_Branch_Mst> bankBranches = new List<Bank_Branch_Mst>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetBankBranches", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@BankId", SqlDbType.Int).Value = bankId;
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "Bank_Branch_Mst");

                    foreach (DataRow row in ds.Tables["Bank_Branch_Mst"].Rows)
                    {
                        Bank_Branch_Mst bankBranch = new Bank_Branch_Mst
                        {
                            Bank_Branch_Id = row.Field<int>("Bank_Branch_Id"),
                            Bank_Id = row.Field<int>("Bank_Id"),
                            Branch_Code = row.Field<string>("Branch_Code"),
                            English_Name = row.Field<string>("English_Name"),
                            Arabic_Name = row.Field<string>("Arabic_Name"),
                            City_Id = row.Field<int>("City_Id"),
                            Country_Id = row.Field<int>("Country_Id"),
                            Currency_Id = row.Field<int>("Currency_Id"),
                            Upper_Limit = row.Field<decimal>("Upper_Limit"),
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
        public int EditBeneficiary(Beneficiary objBeneficiary)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("EditBeneficiary", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@UID", objBeneficiary.UID);
                        command.Parameters.AddWithValue("@FullName", objBeneficiary.FullName);
                        command.Parameters.AddWithValue("@Customer_Id", objBeneficiary.Customer_Id);
                        command.Parameters.AddWithValue("@Country_Id", objBeneficiary.Country_Id);
                        command.Parameters.AddWithValue("@Currency_Id", objBeneficiary.Currency_Id);
                        command.Parameters.AddWithValue("@Address_Line1", objBeneficiary.Address_Line1);
                        command.Parameters.AddWithValue("@Address_Line2", objBeneficiary.Address_Line2);
                        command.Parameters.AddWithValue("@Address_Line3", objBeneficiary.Address_Line3);
                        command.Parameters.AddWithValue("@Remittance_Purpose", objBeneficiary.Remittance_Purpose);
                        command.Parameters.AddWithValue("@Birth_Date", objBeneficiary.Birth_Date);
                        command.Parameters.AddWithValue("@Remittance_Type_Id", objBeneficiary.Remittance_Type_Id);
                        command.Parameters.AddWithValue("@Remittance_Instruction", objBeneficiary.Remittance_Instruction);
                        command.Parameters.AddWithValue("@Phone_No", objBeneficiary.Phone_No);
                        command.Parameters.AddWithValue("@Mobile_No", objBeneficiary.Mobile_No);
                        command.Parameters.AddWithValue("@Prod_Beneficiary_Id", objBeneficiary.Prod_Beneficiary_Id);
                        command.Parameters.AddWithValue("@Fax_No", objBeneficiary.Fax_No);
                        command.Parameters.AddWithValue("@Email_Address1", objBeneficiary.Email_Address1);
                        command.Parameters.AddWithValue("@Email_Address2", objBeneficiary.Email_Address2);
                        command.Parameters.AddWithValue("@Identification_Type", objBeneficiary.Identification_Type);
                        command.Parameters.AddWithValue("@Identification_No", objBeneficiary.Identification_No);
                        command.Parameters.AddWithValue("@Identification_Remarks", objBeneficiary.Identification_Remarks);
                        command.Parameters.AddWithValue("@Identification_Issue_Date", objBeneficiary.Identification_Issue_Date);
                        command.Parameters.AddWithValue("@Identification_Expiry_Date", objBeneficiary.Identification_Expiry_Date);
                        command.Parameters.AddWithValue("@Bank_Id", objBeneficiary.Bank_Id);
                        command.Parameters.AddWithValue("@Branch_Id", objBeneficiary.Branch_Id);
                        command.Parameters.AddWithValue("@Bank_Account_No", objBeneficiary.Bank_Account_No);
                        command.Parameters.AddWithValue("@Bank_Name", objBeneficiary.Bank_Name);
                        command.Parameters.AddWithValue("@Branch_Name", objBeneficiary.Branch_Name);
                        command.Parameters.AddWithValue("@Branch_City_Id", objBeneficiary.Branch_City_Id);
                        command.Parameters.AddWithValue("@Branch_City_Name", objBeneficiary.Branch_City_Name);
                        command.Parameters.AddWithValue("@Branch_Address_Line1", objBeneficiary.Branch_Address_Line1);
                        command.Parameters.AddWithValue("@Branch_Address_Line2", objBeneficiary.Branch_Address_Line2);
                        command.Parameters.AddWithValue("@Branch_Address_Line3", objBeneficiary.Branch_Address_Line3);
                        command.Parameters.AddWithValue("@Branch_Number", objBeneficiary.Branch_Number);
                        command.Parameters.AddWithValue("@Branch_Phone_Number", objBeneficiary.Branch_Phone_Number);
                        command.Parameters.AddWithValue("@Branch_Fax_Number", objBeneficiary.Branch_Fax_Number);
                        command.Parameters.AddWithValue("@Destination_Country_Id", objBeneficiary.Destination_Country_Id);
                      //  command.Parameters.AddWithValue("@Remitter_Relation", objBeneficiary.Remitter_Relation_Id);
                        command.Parameters.AddWithValue("@DD_Beneficiary_Name", objBeneficiary.DD_Beneficiary_Name);
                        command.Parameters.AddWithValue("@Bank_Account_type", objBeneficiary.Bank_Account_type);
                        command.Parameters.AddWithValue("@Remittance_Remarks", objBeneficiary.Remittance_Remarks);
                        command.Parameters.AddWithValue("@Bank_Code", objBeneficiary.Bank_Code);
                        command.Parameters.AddWithValue("@Routing_Bank_Id", objBeneficiary.Routing_Bank_Id);
                        command.Parameters.AddWithValue("@Routing_Bank_Branch_Id", objBeneficiary.Routing_Bank_Branch_Id);
                        command.Parameters.AddWithValue("@Remittance_Subtype_Id", objBeneficiary.Remittance_Subtype_Id);
                        command.Parameters.AddWithValue("@Birth_Place", objBeneficiary.Birth_Place);
                        command.Parameters.AddWithValue("@TransFastInfo", objBeneficiary.TransFastInfo);

                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                
            }

            return rowsAffected;
        }
        
    }
}
