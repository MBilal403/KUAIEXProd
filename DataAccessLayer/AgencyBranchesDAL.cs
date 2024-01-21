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
   public class AgencyBranchesDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["KUAIEXProdEntities"].ConnectionString;
        public BranchesInfo GetBranchesInfoByName(string name)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("GetBranchesInfoByName", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "BranchesInfo");

                        if (ds.Tables["BranchesInfo"].Rows.Count > 0)
                        {
                            DataRow row = ds.Tables["BranchesInfo"].Rows[0];
                            BranchesInfo branchesInfo = new BranchesInfo
                            {
                                Id = row.Field<int>("Id"),
                                Name = row.Field<string>("Name"),
                                ContactNo = row.Field<string>("ContactNo"),
                                Address = row.Field<string>("Address"),
                                Longitude = row.Field<string>("Longitude"),
                                Latitude = row.Field<string>("Latitude"),
                                Status = row.Field<int?>("Status"),
                            };

                            return branchesInfo;
                        }
                    }
                }

                return null; 
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool AddBranchesInfo(BranchesInfo objBranchesInfo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("AddBranchesInfo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = objBranchesInfo.Name;
                        command.Parameters.Add("@ContactNo", SqlDbType.NVarChar).Value = objBranchesInfo.ContactNo;
                        command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = objBranchesInfo.Address;
                        command.Parameters.Add("@Longitude", SqlDbType.NVarChar).Value = objBranchesInfo.Longitude;
                        command.Parameters.Add("@Latitude", SqlDbType.NVarChar).Value = objBranchesInfo.Latitude;
                        command.Parameters.Add("@Status", SqlDbType.Int).Value = objBranchesInfo.Status;

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
        public List<BranchesInfo> GetBrancheskInfoList()
        {
            List<BranchesInfo> branchesInfoList = new List<BranchesInfo>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetBrancheskInfoList", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "BranchesInfo");

                        foreach (DataRow row in ds.Tables["BranchesInfo"].Rows)
                        {
                            BranchesInfo branchesInfo = new BranchesInfo
                            {
                                Id = row.Field<int>("Id"),
                                Name = row.Field<string>("Name"),
                                ContactNo = row.Field<string>("ContactNo"),
                                Address = row.Field<string>("Address"),
                                Longitude = row.Field<string>("Longitude"),
                                Latitude = row.Field<string>("Latitude"),
                                Status = row.Field<int?>("Status"),
                            };

                            branchesInfoList.Add(branchesInfo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return branchesInfoList;
        }
        public BranchesInfo GetBranchesInfoById(int branchId)
        {
            BranchesInfo branchesInfo = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetBranchesInfoById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = branchId;

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "BranchesInfo");

                        if (ds.Tables["BranchesInfo"].Rows.Count > 0)
                        {
                            DataRow row = ds.Tables["BranchesInfo"].Rows[0];
                            branchesInfo = new BranchesInfo
                            {
                                Id = Convert.ToInt32(row["Id"]),
                                Name = row["Name"].ToString(),
                                ContactNo = row["ContactNo"].ToString(),
                                Address = row["Address"].ToString(),
                                Longitude = row["Longitude"].ToString(),
                                Latitude = row["Latitude"].ToString(),
                                Status = Convert.ToInt32(row["Status"])
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return branchesInfo;
        }
        public bool EditBranchesInfo(BranchesInfo objBranchesInfo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("EditBranchesInfo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = objBranchesInfo.Id;
                        command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = objBranchesInfo.Name;
                        command.Parameters.Add("@ContactNo", SqlDbType.NVarChar).Value = objBranchesInfo.ContactNo;
                        command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = objBranchesInfo.Address;
                        command.Parameters.Add("@Longitude", SqlDbType.NVarChar).Value = objBranchesInfo.Longitude;
                        command.Parameters.Add("@Latitude", SqlDbType.NVarChar).Value = objBranchesInfo.Latitude;
                        command.Parameters.Add("@Status", SqlDbType.Int).Value = objBranchesInfo.Status;

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
