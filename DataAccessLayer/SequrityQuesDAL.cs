using DataAccessLayer.Entities;
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
    public class SequrityQuesDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["KUAIEXProdEntities"].ConnectionString;
        public SecurityQuestions GetSecurityQuestionByQuestion(string question)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetSecurityQuestionByQuestion", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Question", question);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "SecurityQuestions");

                        if (ds.Tables["SecurityQuestions"].Rows.Count > 0)
                        {
                            DataRow row = ds.Tables["SecurityQuestions"].Rows[0];

                            SecurityQuestions securityQuestion = new SecurityQuestions
                            {
                                Id = Convert.ToInt32(row["Id"]),
                                Question = Convert.ToString(row["Question"]),
                                Status = Convert.ToString(row["Status"]),
                                UID = row["UID"] != DBNull.Value ? (Guid?)row["UID"] : null
                            };

                            return securityQuestion;
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
        public bool AddSecurityQuestion(SecurityQuestions objQuestion)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("AddSecurityQuestion", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Question", objQuestion.Question);
                        command.Parameters.AddWithValue("@Status", objQuestion.Status);
                        command.Parameters.AddWithValue("@UID", objQuestion.UID ?? (object)DBNull.Value);

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
                                SecurityQuestions securityQuestion = new SecurityQuestions
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Question = reader.GetString(reader.GetOrdinal("Question")),
                                    Status = reader.GetString(reader.GetOrdinal("Status")),
                                    UID = reader.IsDBNull(reader.GetOrdinal("UID")) ? (Guid?)null : reader.GetGuid(reader.GetOrdinal("UID"))
                                };

                                securityQuestions.Add(securityQuestion);
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
        public SecurityQuestions GetSecurityQuestionByUID(Guid? UID)
        {
            SecurityQuestions securityQuestion = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetSecurityQuestionByUID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UID", UID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                securityQuestion = new SecurityQuestions
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Question = reader.GetString(reader.GetOrdinal("Question")),
                                    Status = reader.GetString(reader.GetOrdinal("Status")),
                                    UID = reader.IsDBNull(reader.GetOrdinal("UID")) ? (Guid?)null : reader.GetGuid(reader.GetOrdinal("UID"))
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

            return securityQuestion;
        }
        public bool EditSecurityQuestion(SecurityQuestions objQuestion)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("EditSecurityQuestion", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", objQuestion.Id);
                        command.Parameters.AddWithValue("@Question", objQuestion.Question);
                        command.Parameters.AddWithValue("@Status", objQuestion.Status);
                        command.Parameters.AddWithValue("@UID", objQuestion.UID);

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

    }
}
