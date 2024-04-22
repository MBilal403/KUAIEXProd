using DataAccessLayer.Entities;
using DataAccessLayer.ProcedureResults;
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
        private string connectionString = ConfigurationManager.ConnectionStrings["KUAIEXEntities"].ConnectionString;

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
    }
}
