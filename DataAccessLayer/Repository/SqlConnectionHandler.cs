using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace KuaiexDashboard.Repository
{
    public class SqlConnectionHandler
    {
        private SqlConnection connection;

        public SqlConnectionHandler(string DbName)
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings[DbName].ConnectionString);
        }

        public SqlConnection OpenConnection()
        {
           
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