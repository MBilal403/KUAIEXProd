using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using BusinessLogicLayer.DomainEntities;
using System.Data;
using System.Data.SqlClient;
using KuaiexDashboard;
using DataAccessLayer.Entities;

namespace DataAccessLayer
{
    public class GeneralSettingsDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["KUAIEXProdEntities"].ConnectionString;
        public List<GetTermsConditions_Result> GetTermsConditions()
        {
            List<GetTermsConditions_Result> termsList = new List<GetTermsConditions_Result>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetTermsConditions", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GetTermsConditions_Result term = new GetTermsConditions_Result
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Title = reader.GetString(reader.GetOrdinal("Title")),
                                    Description = reader.GetString(reader.GetOrdinal("Description"))
                                };

                                termsList.Add(term);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return termsList;
        }
        public Terms_and_Privacy GetTermsAndPrivacyById(int id)
        {
            Terms_and_Privacy termsAndPrivacy = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetTermsAndPrivacyById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                termsAndPrivacy = new Terms_and_Privacy
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Description = reader.GetString(reader.GetOrdinal("Description")),
                                    Content_Type = reader.IsDBNull(reader.GetOrdinal("Content_Type")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("Content_Type")),
                                    Title = reader.GetString(reader.GetOrdinal("Title"))
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
            return termsAndPrivacy;
        }
        public bool UpdateTermsAndPrivacy(Terms_and_Privacy updatedTermsAndPrivacy)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("UpdateTermsAndPrivacy", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", updatedTermsAndPrivacy.Id);
                        command.Parameters.AddWithValue("@Description", updatedTermsAndPrivacy.Description);
                        command.Parameters.AddWithValue("@Content_Type", updatedTermsAndPrivacy.Content_Type ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Title", updatedTermsAndPrivacy.Title);

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
        public List<GetPrivacyPolicy_Result> GetPrivacyPolicy()
        {
            List<GetPrivacyPolicy_Result> result = new List<GetPrivacyPolicy_Result>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetPrivacyPolicy", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "PrivacyPolicy");

                        if (ds.Tables["PrivacyPolicy"].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables["PrivacyPolicy"].Rows)
                            {
                                GetPrivacyPolicy_Result privacyPolicy = new GetPrivacyPolicy_Result
                                {
                                    Id = Convert.ToInt32(row["Id"]),
                                    Title = Convert.ToString(row["Title"]),
                                    Description = Convert.ToString(row["Description"])
                                };

                                result.Add(privacyPolicy);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        public bool UpdatePrivacyPolicy(Terms_and_Privacy obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("UpdatePrivacyPolicy", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", obj.Id);
                        command.Parameters.AddWithValue("@Title", obj.Title);
                        command.Parameters.AddWithValue("@Content_Type", 1);
                        command.Parameters.AddWithValue("@Description", obj.Description);

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
        public List<GetContactUs_Result> GetContactUs()
        {
            List<GetContactUs_Result> contactList = new List<GetContactUs_Result>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetContactUs", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GetContactUs_Result contact = new GetContactUs_Result
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    ContactNo = reader.GetString(reader.GetOrdinal("ContactNo")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    Address = reader.GetString(reader.GetOrdinal("Address")),
                                    CustomerService = reader.GetString(reader.GetOrdinal("CustomerService"))
                                };

                                contactList.Add(contact);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return contactList;
        }
        public ContactUs GetContactUsById(int contactUsId)
        {
            ContactUs contact = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetContactUsById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ContactUsId", contactUsId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                contact = new ContactUs
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    ContactNo = reader.GetString(reader.GetOrdinal("ContactNo")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    Address = reader.GetString(reader.GetOrdinal("Address")),
                                    CustomerService = reader.GetString(reader.GetOrdinal("CustomerService"))
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

            return contact;
        }
        public bool UpdateContactUs(ContactUs objContactUs)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("UpdateContactUs", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ContactUsId", objContactUs.Id);
                        command.Parameters.AddWithValue("@ContactNo", objContactUs.ContactNo);
                        command.Parameters.AddWithValue("@Email", objContactUs.Email);
                        command.Parameters.AddWithValue("@Address", objContactUs.Address);
                        command.Parameters.AddWithValue("@CustomerService", objContactUs.CustomerService);

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
        public FAQs GetFAQsByQuestion(string question)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetFAQsByQuestion", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Question", question);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                FAQs faqs = new FAQs
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Question = reader.GetString(reader.GetOrdinal("Question")),
                                    Answer = reader.GetString(reader.GetOrdinal("Answer")),
                                    Status = reader.GetString(reader.GetOrdinal("Status"))
                                };

                                return faqs;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }
        public bool AddFAQs(FAQs objFaq)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("AddFAQs", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Question", objFaq.Question);
                        command.Parameters.AddWithValue("@Answer", objFaq.Answer);
                        command.Parameters.AddWithValue("@Status", objFaq.Status);

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
        public List<GetFAQSList_Result> GetFAQSList()
        {
            List<GetFAQSList_Result> faqsList = new List<GetFAQSList_Result>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetFAQSList", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "FAQs");

                        if (ds.Tables["FAQs"].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables["FAQs"].Rows)
                            {
                                GetFAQSList_Result faq = new GetFAQSList_Result
                                {
                                    Id = Convert.ToInt32(row["Id"]),
                                    Question = Convert.ToString(row["Question"]),
                                    Answer = Convert.ToString(row["Answer"]),
                                    Status = Convert.ToString(row["Status"])
                                };

                                faqsList.Add(faq);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return faqsList;
        }
        public FAQs GetFAQById(int id)
        {
            FAQs faq = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetFAQById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            faq = new FAQs
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Question = reader.GetString(reader.GetOrdinal("Question")),
                                Answer = reader.GetString(reader.GetOrdinal("Answer")),
                                Status = reader.GetString(reader.GetOrdinal("Status"))
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return faq;
        }
        public bool UpdateFAQ(FAQs objFAQs)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("UpdateFAQ", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", objFAQs.Id);
                        command.Parameters.AddWithValue("@Question", objFAQs.Question);
                        command.Parameters.AddWithValue("@Answer", objFAQs.Answer);
                        command.Parameters.AddWithValue("@Status", objFAQs.Status);

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
        public List<GetCustomerQueries_Result> GetCustomerQueries()
        {
            List<GetCustomerQueries_Result> result = new List<GetCustomerQueries_Result>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetCustomerQueries", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "CustomerQueries");

                        if (ds.Tables["CustomerQueries"].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables["CustomerQueries"].Rows)
                            {
                                GetCustomerQueries_Result query = new GetCustomerQueries_Result
                                {
                                    id = Convert.ToInt32(row["id"]),
                                    Name = Convert.ToString(row["Name"]),
                                    Email = Convert.ToString(row["Email"]),
                                    Message = Convert.ToString(row["Message"]),
                                    PhoneNo = Convert.ToString(row["PhoneNo"])
                                };

                                result.Add(query);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

    }
}
