using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;

namespace KuaiexDashboard.DAL
{
    public class UsersDAL
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["KUAIEXProdEntities"].ConnectionString;

        public List<GetUsersList_Result> GetUsersList()
        {
            List<GetUsersList_Result> userList = new List<GetUsersList_Result>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetUsersList", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            foreach (DataRow row in dataTable.Rows)
                            {
                                GetUsersList_Result user = new GetUsersList_Result
                                {
                                    UID = row.IsNull("UID") ? (Guid?)null : (Guid)row["UID"],
                                    UserName = row["UserName"].ToString(),
                                    Name = row["Name"].ToString(),
                                    Email = row["Email"].ToString(),
                                    ContactNo = row["ContactNo"].ToString(),
                                    Status = row.IsNull("Status") ? 0 : Convert.ToInt32(row["Status"]),
                                    UserType = row["UserType"].ToString()
                                };

                                userList.Add(user);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetUsersList DAL: " + ex.Message);
            }

            return userList;
        }


        public Users GetUserByUserName(string userName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetUserByUserName", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserName", userName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapUserFromReader(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                
                return null;
            }

            return null;
        }

        public bool RegisterUser(Users user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("RegisterUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@UserName", user.UserName);
                        command.Parameters.AddWithValue("@Password", user.Password); 
                        command.Parameters.AddWithValue("@Name", user.Name);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@ContactNo", user.ContactNo);
                        command.Parameters.AddWithValue("@Status", user.Status ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@UserTypeId", user.UserTypeId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@UID", user.UID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", user.CreatedBy ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedOn", user.CreatedOn ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@UpdatedBy", user.UpdatedBy ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@UpdatedOn", user.UpdatedOn ?? (object)DBNull.Value);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Users AuthenticateUser(Users objUser)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("AuthenticateUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = objUser.UserName;
                        command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = objUser.Password;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Users user = new Users
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    UserName = reader.GetString(reader.GetOrdinal("UserName")),
                                    Password = reader.GetString(reader.GetOrdinal("Password")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    ContactNo = reader.GetString(reader.GetOrdinal("ContactNo")),
                                    Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("Status")),
                                    UserTypeId = reader.IsDBNull(reader.GetOrdinal("UserTypeId")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                    UID = reader.IsDBNull(reader.GetOrdinal("UID")) ? null : (Guid?)reader.GetGuid(reader.GetOrdinal("UID")),
                                    CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                                    CreatedOn = reader.IsDBNull(reader.GetOrdinal("CreatedOn")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("CreatedOn")),
                                    UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("UpdatedBy")),
                                    UpdatedOn = reader.IsDBNull(reader.GetOrdinal("UpdatedOn")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("UpdatedOn"))
                                };

                                return user;
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


        public List<UserType_Lookup> GetUserTypes()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetUserTypes", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<UserType_Lookup> userTypes = new List<UserType_Lookup>();

                            while (reader.Read())
                            {
                                UserType_Lookup userType = new UserType_Lookup
                                {
                                    UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                    Name = reader.GetString(reader.GetOrdinal("Name"))
                                };

                                userTypes.Add(userType);
                            }

                            return userTypes;
                        }
                    }
                }
            }
            catch (Exception)
            {
               return null;
            }
        }
        

        public Users GetUserByUID(Guid? uid)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetUserByUID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UID", SqlDbType.UniqueIdentifier).Value = (object)uid ?? DBNull.Value;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapUserFromReader(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
               
                return null;
            }

            return null;
        }
        

        public bool UpdateUser(Users user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("UpdateUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Id", user.Id);
                        command.Parameters.AddWithValue("@UserName", user.UserName);
                        command.Parameters.AddWithValue("@Password", user.Password);
                        command.Parameters.AddWithValue("@Name", user.Name);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@ContactNo", user.ContactNo);
                        command.Parameters.AddWithValue("@Status", user.Status ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@UserTypeId", user.UserTypeId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@UID", user.UID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", user.CreatedBy ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedOn", user.CreatedOn ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@UpdatedBy", user.UpdatedBy ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@UpdatedOn", user.UpdatedOn ?? (object)DBNull.Value);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private Users MapUserFromDataRow(DataRow row)
        {
            Users user = new Users
            {
                Id = Convert.ToInt32(row["Id"]),
                UserName = row["UserName"].ToString(),
                Password = row["Password"].ToString(),
                Name = row["Name"].ToString(),
                Email = row["Email"].ToString(),
                ContactNo = row["ContactNo"].ToString(),
                Status = row.IsNull("Status") ? (int?)null : Convert.ToInt32(row["Status"]),
                UserTypeId = row.IsNull("UserTypeId") ? (int?)null : Convert.ToInt32(row["UserTypeId"]),
                UID = row.IsNull("UID") ? (Guid?)null : (Guid)row["UID"],
                CreatedBy = row.IsNull("CreatedBy") ? (int?)null : Convert.ToInt32(row["CreatedBy"]),
                CreatedOn = row.IsNull("CreatedOn") ? (DateTime?)null : Convert.ToDateTime(row["CreatedOn"]),
                UpdatedBy = row.IsNull("UpdatedBy") ? (int?)null : Convert.ToInt32(row["UpdatedBy"]),
                UpdatedOn = row.IsNull("UpdatedOn") ? (DateTime?)null : Convert.ToDateTime(row["UpdatedOn"])
            };

            return user;
        }

        private Users MapUserFromReader(SqlDataReader reader)
        {
            Users user = new Users
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                UserName = reader.GetString(reader.GetOrdinal("UserName")),
                Password = reader.GetString(reader.GetOrdinal("Password")), 
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                ContactNo = reader.GetString(reader.GetOrdinal("ContactNo")),
                Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("Status")),
                UserTypeId = reader.IsDBNull(reader.GetOrdinal("UserTypeId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                UID = reader.IsDBNull(reader.GetOrdinal("UID")) ? (Guid?)null : reader.GetGuid(reader.GetOrdinal("UID")),
                CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                CreatedOn = reader.IsDBNull(reader.GetOrdinal("CreatedOn")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("CreatedOn")),
                UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("UpdatedBy")),
                UpdatedOn = reader.IsDBNull(reader.GetOrdinal("UpdatedOn")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("UpdatedOn"))
            };

            return user;
        }
    }
}
