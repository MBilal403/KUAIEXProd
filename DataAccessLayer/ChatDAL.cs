using DataAccessLayer.DomainEntities;
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
    public class ChatDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["KUAIEXProdEntities"].ConnectionString;
        public List<CustomerChatMain_Result> GetCustomerChatMain()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("CustomerChatMain", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();

                        adapter.Fill(dataTable);

                        List<CustomerChatMain_Result> customerChatMainList = new List<CustomerChatMain_Result>();

                        foreach (DataRow row in dataTable.Rows)
                        {
                            CustomerChatMain_Result customerChatMain = new CustomerChatMain_Result
                            {
                                Name = row.Field<string>("Name"),
                                UID = row.Field<Guid?>("UID"),
                                ChatOn = row.Field<string>("ChatOn"),
                                NickName = row.Field<string>("NickName")
                            };

                            customerChatMainList.Add(customerChatMain);
                        }

                        return customerChatMainList;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CustomerChatDetail_Result> GetCustomerChatDetail(Guid UID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("CustomerChatDetail", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@UID", SqlDbType.UniqueIdentifier).Value = UID;

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();

                        adapter.Fill(dataTable);

                        List<CustomerChatDetail_Result> customerChatDetailList = new List<CustomerChatDetail_Result>();

                        foreach (DataRow row in dataTable.Rows)
                        {
                            CustomerChatDetail_Result customerChatDetail = new CustomerChatDetail_Result
                            {
                                Message = row.Field<string>("Message"),
                                Message_Type = row.Field<int?>("Message_Type"),
                                ChatOn = row.Field<string>("ChatOn")
                            };

                            customerChatDetailList.Add(customerChatDetail);
                        }

                        return customerChatDetailList;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CustomerChatTitle_Result> GetCustomerChatTitle(Guid UID)
        {
            List<CustomerChatTitle_Result> result = new List<CustomerChatTitle_Result>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("CustomerChatTitle", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@UID", SqlDbType.UniqueIdentifier).Value = UID;

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "CustomerChatTitle");

                        if (ds.Tables["CustomerChatTitle"].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables["CustomerChatTitle"].Rows)
                            {
                                CustomerChatTitle_Result chatTitle = new CustomerChatTitle_Result
                                {
                                    Name = row.Field<string>("Name"),
                                    UID = row.Field<Guid?>("UID"),
                                    NickName = row.Field<string>("NickName")
                                };

                                result.Add(chatTitle);
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
        public bool SaveCustomerChat(Guid? UID, string message)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SaveCustomerChat", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UID", UID);
                        command.Parameters.AddWithValue("@Message", message);
                        command.Parameters.AddWithValue("@Message_Type", 1);
                        command.Parameters.AddWithValue("@Created_On", DateTime.Now);

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
        public List<Customer_Chat> GetAllChats()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetAllChats", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "CustomerChat");

                        List<Customer_Chat> chats = new List<Customer_Chat>();

                        if (ds.Tables["CustomerChat"].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables["CustomerChat"].Rows)
                            {
                                Customer_Chat chat = new Customer_Chat
                                {
                                    Id = row.Field<int>("Id"),
                                    Message = row.Field<string>("Message"),
                                    UID = row.Field<Guid?>("UID"),
                                    Message_Type = row.Field<int?>("Message_Type"),
                                    Created_On = row.Field<DateTime?>("Created_On"),
                                    App_Type = row.Field<int?>("App_Type"),
                                    Agency_Id = row.Field<int?>("Agency_Id"),
                                    Agency_Branch_Id = row.Field<int?>("Agency_Branch_Id")
                                };

                                chats.Add(chat);
                            }
                        }

                        return chats;
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
