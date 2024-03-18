using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using KuaiexDashboard;
using System.Configuration;
using BusinessLogicLayer.DomainEntities;
using DataAccessLayer.Entities;
using DataAccessLayer.ProcedureResults;

namespace DataAccessLayer
{
    public class RemitterDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["KUAIEXProdEntities"].ConnectionString;

        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetAllCustomers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer
                            {
                                Customer_Id = Convert.ToInt32(reader["Customer_Id"]),
                                Agency_Id = reader["Agency_Id"] as int?,
                                Agency_Branch_Id = reader["Agency_Branch_Id"] as int?,
                                Name = reader["Name"] as string,
                                Gender = reader["Gender"] as int?,
                                Identification_Type = reader["Identification_Type"] as int?,
                                Identification_Number = reader["Identification_Number"] as string,
                                Identification_Expiry_Date = reader["Identification_Expiry_Date"] as DateTime?,
                                // Map other properties accordingly
                                Date_Of_Birth = reader["Date_Of_Birth"] as DateTime?,
                                Occupation = reader["Occupation"] as string,
                                Nationality = reader["Nationality"] as string,
                                Mobile_No = reader["Mobile_No"] as string,
                                Email_Address = reader["Email_Address"] as string,
                                Area = reader["Area"] as string,
                                Block = reader["Block"] as string,
                                Street = reader["Street"] as string,
                                Building = reader["Building"] as string,
                                Floor = reader["Floor"] as string,
                                Flat = reader["Flat"] as string,
                                Login_Id = reader["Login_Id"] as string,
                                Password = reader["Password"] as string,
                                Security_Question_Id_1 = reader["Security_Question_Id_1"] as int?,
                                Security_Answer_1 = reader["Security_Answer_1"] as string,
                                Security_Question_Id_2 = reader["Security_Question_Id_2"] as int?,
                                Security_Answer_2 = reader["Security_Answer_2"] as string,
                                Security_Question_Id_3 = reader["Security_Question_Id_3"] as int?,
                                Security_Answer_3 = reader["Security_Answer_3"] as string,
                                Device_Key = reader["Device_Key"] as string,
                                UID = reader["UID"] as Guid?,
                                UID_Token = reader["UID_Token"] as Guid?,
                                CreatedBy = reader["CreatedBy"] as int?,
                                CreatedOn = reader["CreatedOn"] as DateTime?,
                                CreatedIp = reader["CreatedIp"] as string,
                                UpdatedBy = reader["UpdatedBy"] as int?,
                                UpdatedOn = reader["UpdatedOn"] as DateTime?,
                                UpdatedIp = reader["UpdatedIp"] as string,
                                IsBlocked = reader["IsBlocked"] as int?,
                                Block_Count = reader["Block_Count"] as int?,
                                InvalidTryCount = reader["InvalidTryCount"] as int?,
                                Civil_Id_Front = reader["Civil_Id_Front"] as string,
                                Civil_Id_Back = reader["Civil_Id_Back"] as string,
                                Pep_Status = reader["Pep_Status"] as bool?,
                                Pep_Description = reader["Pep_Description"] as string,
                                Identification_Additional_Detail = reader["Identification_Additional_Detail"] as string,
                                Residence_Type = reader["Residence_Type"] as int?,
                                Telephone_No = reader["Telephone_No"] as string,
                                Birth_Place = reader["Birth_Place"] as string,
                                Birth_Country = reader["Birth_Country"] as int?,
                                Monthly_Income = reader["Monthly_Income"] as decimal?,
                                Expected_Monthly_Trans_Count = reader["Expected_Monthly_Trans_Count"] as int?,
                                Other_Income = reader["Other_Income"] as decimal?,
                                Other_Income_Detail = reader["Other_Income_Detail"] as string,
                                Monthly_Trans_Limit = reader["Monthly_Trans_Limit"] as decimal?,
                                Yearly_Trans_Limit = reader["Yearly_Trans_Limit"] as decimal?,
                                Compliance_Limit = reader["Compliance_Limit"] as decimal?,
                                Compliance_Trans_Count = reader["Compliance_Trans_Count"] as int?,
                                Compliance_Limit_Expiry = reader["Compliance_Limit_Expiry"] as DateTime?,
                                Compliance_Comments = reader["Compliance_Comments"] as string,
                                IsVerified = reader["IsVerified"] as int?,
                                IsReviwed = reader["IsReviwed"] as bool?,
                                Prod_Remitter_Id = reader["Prod_Remitter_Id"] as int?,
                                Employer = reader["Employer"] as string,
                                Is_Profile_Completed = reader["Is_Profile_Completed"] as int?
                            };

                            customers.Add(customer);
                        }
                    }
                }
            }

            return customers;
        }
        public Customer GetCustomerByUID(Guid? uid)
        {
            Customer customer = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetCustomerByUID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@UID", SqlDbType.UniqueIdentifier).Value = (object)uid ?? DBNull.Value;

                        connection.Open();

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            if (dataTable.Rows.Count > 0)
                            {
                                DataRow row = dataTable.Rows[0];
                                customer = new Customer
                                {
                                    Customer_Id = Convert.ToInt32(row["Customer_Id"]),
                                    Agency_Id = row.Field<int?>("Agency_Id"),
                                    Agency_Branch_Id = row.Field<int?>("Agency_Branch_Id"),
                                    Name = row.Field<string>("Name"),
                                    Gender = row.Field<int?>("Gender"),
                                    Identification_Type = row.Field<int?>("Identification_Type"),
                                    Identification_Number = row.Field<string>("Identification_Number"),
                                    Identification_Expiry_Date = row.Field<DateTime?>("Identification_Expiry_Date"),
                                    Date_Of_Birth = row.Field<DateTime?>("Date_Of_Birth"),
                                    Occupation = row.Field<string>("Occupation"),
                                    Nationality = row.Field<string>("Nationality"),
                                    Mobile_No = row.Field<string>("Mobile_No"),
                                    Email_Address = row.Field<string>("Email_Address"),
                                    Area = row.Field<string>("Area"),
                                    Block = row.Field<string>("Block"),
                                    Street = row.Field<string>("Street"),
                                    Building = row.Field<string>("Building"),
                                    Floor = row.Field<string>("Floor"),
                                    Flat = row.Field<string>("Flat"),
                                    Login_Id = row.Field<string>("Login_Id"),
                                    Password = row.Field<string>("Password"),
                                    Security_Question_Id_1 = row.Field<int?>("Security_Question_Id_1"),
                                    Security_Answer_1 = row.Field<string>("Security_Answer_1"),
                                    Security_Question_Id_2 = row.Field<int?>("Security_Question_Id_2"),
                                    Security_Answer_2 = row.Field<string>("Security_Answer_2"),
                                    Security_Question_Id_3 = row.Field<int?>("Security_Question_Id_3"),
                                    Security_Answer_3 = row.Field<string>("Security_Answer_3"),
                                    Device_Key = row.Field<string>("Device_Key"),
                                    UID = row.Field<Guid?>("UID"),
                                    UID_Token = row.Field<Guid?>("UID_Token"),
                                    CreatedBy = row.Field<int?>("CreatedBy"),
                                    CreatedOn = row.Field<DateTime?>("CreatedOn"),
                                    CreatedIp = row.Field<string>("CreatedIp"),
                                    UpdatedBy = row.Field<int?>("UpdatedBy"),
                                    UpdatedOn = row.Field<DateTime?>("UpdatedOn"),
                                    UpdatedIp = row.Field<string>("UpdatedIp"),
                                    IsBlocked = row.Field<int?>("IsBlocked"),
                                    Block_Count = row.Field<int?>("Block_Count"),
                                    InvalidTryCount = row.Field<int?>("InvalidTryCount"),
                                    Civil_Id_Front = row.Field<string>("Civil_Id_Front"),
                                    Civil_Id_Back = row.Field<string>("Civil_Id_Back"),
                                    Pep_Status = row.Field<bool?>("Pep_Status"),
                                    Pep_Description = row.Field<string>("Pep_Description"),
                                    Identification_Additional_Detail = row.Field<string>("Identification_Additional_Detail"),
                                    Residence_Type = row.Field<int?>("Residence_Type"),
                                    Telephone_No = row.Field<string>("Telephone_No"),
                                    Birth_Place = row.Field<string>("Birth_Place"),
                                    Birth_Country = row.Field<int?>("Birth_Country"),
                                    Monthly_Income = row.Field<decimal?>("Monthly_Income"),
                                    Expected_Monthly_Trans_Count = row.Field<int?>("Expected_Monthly_Trans_Count"),
                                    Other_Income = row.Field<decimal?>("Other_Income"),
                                    Other_Income_Detail = row.Field<string>("Other_Income_Detail"),
                                    Monthly_Trans_Limit = row.Field<decimal?>("Monthly_Trans_Limit"),
                                    Yearly_Trans_Limit = row.Field<decimal?>("Yearly_Trans_Limit"),
                                    Compliance_Limit = row.Field<decimal?>("Compliance_Limit"),
                                    Compliance_Trans_Count = row.Field<int?>("Compliance_Trans_Count"),
                                    Compliance_Limit_Expiry = row.Field<DateTime?>("Compliance_Limit_Expiry"),
                                    Compliance_Comments = row.Field<string>("Compliance_Comments"),
                                    IsVerified = row.Field<int?>("IsVerified"),
                                    IsReviwed = row.Field<bool?>("IsReviwed"),
                                    Prod_Remitter_Id = row.Field<int?>("Prod_Remitter_Id"),
                                    Employer = row.Field<string>("Employer"),
                                    Is_Profile_Completed = row.Field<int?>("Is_Profile_Completed")
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

            return customer;
        }
        public void UnblockCustomer(Guid? UID)
        {
            try
            {
                if (UID.HasValue)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand("API_UnblockCustomer", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@UID", UID.Value);

                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    throw new ArgumentNullException(nameof(UID), "UID cannot be null");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateRemitter(Customer updatedCustomer)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("UpdateRemitter", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Identification_Type", updatedCustomer.Identification_Type);
                        command.Parameters.AddWithValue("@Identification_Number", updatedCustomer.Identification_Number);
                        command.Parameters.AddWithValue("@Name", updatedCustomer.Name);
                        command.Parameters.AddWithValue("@Nationality", updatedCustomer.Nationality);
                        command.Parameters.AddWithValue("@Date_Of_Birth", updatedCustomer.Date_Of_Birth);
                        command.Parameters.AddWithValue("@Identification_Expiry_Date", updatedCustomer.Identification_Expiry_Date);
                        command.Parameters.AddWithValue("@Occupation", updatedCustomer.Occupation);
                        command.Parameters.AddWithValue("@Mobile_No", updatedCustomer.Mobile_No);
                        command.Parameters.AddWithValue("@Area", updatedCustomer.Area ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Block", updatedCustomer.Block ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Street", updatedCustomer.Street ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Building", updatedCustomer.Building ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Floor", updatedCustomer.Floor ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Flat", updatedCustomer.Flat ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Civil_Id_Front", updatedCustomer.Civil_Id_Front);
                        command.Parameters.AddWithValue("@Civil_Id_Back", updatedCustomer.Civil_Id_Back);
                        command.Parameters.AddWithValue("@Identification_Additional_Detail",
                            updatedCustomer.Identification_Additional_Detail ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Residence_Type", updatedCustomer.Residence_Type ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Telephone_No", updatedCustomer.Telephone_No ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Birth_Place", updatedCustomer.Birth_Place ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Birth_Country", updatedCustomer.Birth_Country ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Monthly_Income", updatedCustomer.Monthly_Income ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Expected_Monthly_Trans_Count",
                            updatedCustomer.Expected_Monthly_Trans_Count ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Other_Income", updatedCustomer.Other_Income ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Other_Income_Detail",
                            updatedCustomer.Other_Income_Detail ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Security_Question_Id_1", updatedCustomer.Security_Question_Id_1);
                        command.Parameters.AddWithValue("@Security_Question_Id_2", updatedCustomer.Security_Question_Id_2);
                        command.Parameters.AddWithValue("@Security_Question_Id_3", updatedCustomer.Security_Question_Id_3);
                        command.Parameters.AddWithValue("@Security_Answer_1", updatedCustomer.Security_Answer_1);
                        command.Parameters.AddWithValue("@Security_Answer_2", updatedCustomer.Security_Answer_2);
                        command.Parameters.AddWithValue("@Security_Answer_3", updatedCustomer.Security_Answer_3);
                        command.Parameters.AddWithValue("@UID", updatedCustomer.UID);
                        command.Parameters.AddWithValue("@Employer", updatedCustomer.Employer);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Customer GetCustomerByIdentificationAndName(string identificationNumber, string name)
        {
            Customer customer = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetCustomerByIdentificationAndName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Identification_Number", identificationNumber);
                    command.Parameters.AddWithValue("@Name", name);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customer = new Customer
                                {
                                    Customer_Id = Convert.ToInt32(reader["Customer_Id"]),
                                    Agency_Id = reader["Agency_Id"] as int?,
                                    Agency_Branch_Id = reader["Agency_Branch_Id"] as int?,
                                    Name = reader["Name"] as string,
                                    Gender = reader["Gender"] as int?,
                                    Identification_Type = reader["Identification_Type"] as int?,
                                    Identification_Number = reader["Identification_Number"] as string,
                                    Identification_Expiry_Date = reader["Identification_Expiry_Date"] as DateTime?,
                                    Date_Of_Birth = reader["Date_Of_Birth"] as DateTime?,
                                    Occupation = reader["Occupation"] as string,
                                    Nationality = reader["Nationality"] as string,
                                    Mobile_No = reader["Mobile_No"] as string,
                                    Email_Address = reader["Email_Address"] as string,
                                    Area = reader["Area"] as string,
                                    Block = reader["Block"] as string,
                                    Street = reader["Street"] as string,
                                    Building = reader["Building"] as string,
                                    Floor = reader["Floor"] as string,
                                    Flat = reader["Flat"] as string,
                                    Login_Id = reader["Login_Id"] as string,
                                    Password = reader["Password"] as string,
                                    Security_Question_Id_1 = reader["Security_Question_Id_1"] as int?,
                                    Security_Answer_1 = reader["Security_Answer_1"] as string,
                                    Security_Question_Id_2 = reader["Security_Question_Id_2"] as int?,
                                    Security_Answer_2 = reader["Security_Answer_2"] as string,
                                    Security_Question_Id_3 = reader["Security_Question_Id_3"] as int?,
                                    Security_Answer_3 = reader["Security_Answer_3"] as string,
                                    Device_Key = reader["Device_Key"] as string,
                                    UID = reader["UID"] as Guid?,
                                    UID_Token = reader["UID_Token"] as Guid?,
                                    CreatedBy = reader["CreatedBy"] as int?,
                                    CreatedOn = reader["CreatedOn"] as DateTime?,
                                    CreatedIp = reader["CreatedIp"] as string,
                                    UpdatedBy = reader["UpdatedBy"] as int?,
                                    UpdatedOn = reader["UpdatedOn"] as DateTime?,
                                    UpdatedIp = reader["UpdatedIp"] as string,
                                    IsBlocked = reader["IsBlocked"] as int?,
                                    Block_Count = reader["Block_Count"] as int?,
                                    InvalidTryCount = reader["InvalidTryCount"] as int?,
                                    Civil_Id_Front = reader["Civil_Id_Front"] as string,
                                    Civil_Id_Back = reader["Civil_Id_Back"] as string,
                                    Pep_Status = reader["Pep_Status"] as bool?,
                                    Pep_Description = reader["Pep_Description"] as string,
                                    Identification_Additional_Detail = reader["Identification_Additional_Detail"] as string,
                                    Residence_Type = reader["Residence_Type"] as int?,
                                    Telephone_No = reader["Telephone_No"] as string,
                                    Birth_Place = reader["Birth_Place"] as string,
                                    Birth_Country = reader["Birth_Country"] as int?,
                                    Monthly_Income = reader["Monthly_Income"] as decimal?,
                                    Expected_Monthly_Trans_Count = reader["Expected_Monthly_Trans_Count"] as int?,
                                    Other_Income = reader["Other_Income"] as decimal?,
                                    Other_Income_Detail = reader["Other_Income_Detail"] as string,
                                    Monthly_Trans_Limit = reader["Monthly_Trans_Limit"] as decimal?,
                                    Yearly_Trans_Limit = reader["Yearly_Trans_Limit"] as decimal?,
                                    Compliance_Limit = reader["Compliance_Limit"] as decimal?,
                                    Compliance_Trans_Count = reader["Compliance_Trans_Count"] as int?,
                                    Compliance_Limit_Expiry = reader["Compliance_Limit_Expiry"] as DateTime?,
                                    Compliance_Comments = reader["Compliance_Comments"] as string,
                                    IsVerified = reader["IsVerified"] as int?,
                                    IsReviwed = reader["IsReviwed"] as bool?,
                                    Prod_Remitter_Id = reader["Prod_Remitter_Id"] as int?,
                                    Employer = reader["Employer"] as string,
                                    Is_Profile_Completed = reader["Is_Profile_Completed"] as int?
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return customer;
        }
        public List<SecurityQuestions> GetAllSecurityQuestions()
        {
            List<SecurityQuestions> securityQuestions = new List<SecurityQuestions>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetAllSecurityQuestions", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SecurityQuestions question = new SecurityQuestions
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Question = reader.GetString(reader.GetOrdinal("Question")),
                                    Status = reader.GetString(reader.GetOrdinal("Status")),
                                    UID = reader.IsDBNull(reader.GetOrdinal("UID")) ? null : (Guid?)reader.GetGuid(reader.GetOrdinal("UID"))
                                };

                                securityQuestions.Add(question);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return securityQuestions;
        }

    }
}