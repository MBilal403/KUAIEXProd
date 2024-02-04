using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KuaiexDashboard.Repository
{
    public class SqlConnectionHandler
    {
        private SqlConnection connection;
          
        public SqlConnection OpenConnection()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["KUAIEXProdEntities"].ConnectionString);
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            return connection;
        }

        public void Dispose()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}