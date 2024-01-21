using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class Kuaiex_Prod
    {
        public int GetCountryIdByCountryName(string Country_Name)
        {
            int Customer_Id = 0;
            try
            {
                string Qry = @"Select * From Country_Mst where English_Name = '" + Country_Name.Replace("'","''") + "'";
                DataSet ds = GeneralDAL.GetRecordWithExtendedTimeOut_Query(Qry);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Customer_Id = int.Parse(ds.Tables[0].Rows[0]["Country_Id"].ToString());
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            return Customer_Id;
        }

        public int GetCityIdByCityName(string City_Name)
        {
            int Customer_Id = 0;
            try
            {
                string Qry = @"Select * From City_Mst where English_Name = '" + City_Name + "'";
                DataSet ds = GeneralDAL.GetRecordWithExtendedTimeOut_Query(Qry);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Customer_Id = int.Parse(ds.Tables[0].Rows[0]["City_Id"].ToString());
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            return Customer_Id;
        }

        public int GetRemittanceIdByIdentificationNumber(string Identification_No)
        {
            int Customer_Id = 0;
            try
            {
                string Qry = @"Select * From Customer_Mst where Identification_Number = '" + Identification_No + "'";
                DataSet ds = GeneralDAL.GetRecordWithExtendedTimeOut_Query(Qry);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Customer_Id = int.Parse(ds.Tables[0].Rows[0]["Customer_Id"].ToString());
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            return Customer_Id;
        }

        public int GetBeneficiaryIdByIdentificationNumber(string Identification_No)
        {
            int Customer_Id = 0;
            try
            {
                string Qry = @"Select * From Drawee_Mst where Identification_Number = '" + Identification_No + "'";
                DataSet ds = GeneralDAL.GetRecordWithExtendedTimeOut_Query(Qry);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Customer_Id = int.Parse(ds.Tables[0].Rows[0]["Drawee_Id"].ToString());
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            return Customer_Id;
        }
    }

    public class GeneralDAL
    {
        public static string ConStr = ConnectionManager.ConStr;

        // function that creates a list of an object from the given data table
        public static List<T> CreateListFromTable<T>(DataTable tbl) where T : new()
        {
            // define return list
            List<T> lst = new List<T>();

            // go through each row
            foreach (DataRow r in tbl.Rows)
            {
                // add to the list
                lst.Add(CreateItemFromRow<T>(r));
            }

            // return the list
            return lst;
        }

        // function that creates an object from the given data row
        public static T CreateItemFromRow<T>(DataRow row) where T : new()
        {
            // create a new object
            T item = new T();

            // set the item
            SetItemFromRow(item, row);

            // return 
            return item;
        }

        public static void SetItemFromRow<T>(T item, DataRow row) where T : new()
        {
            // go through each column
            foreach (DataColumn c in row.Table.Columns)
            {
                // find the property for the column
                PropertyInfo p = item.GetType().GetProperty(c.ColumnName);

                // if exists, set the value
                if (p != null && row[c] != DBNull.Value)
                {
                    p.SetValue(item, row[c], null);
                }
            }
        }

        //call stored procedure to get data.
        public static DataSet GetRecordWithExtendedTimeOut(string SPName, params SqlParameter[] SqlPrms)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlConnection con = new SqlConnection(ConStr);

            try
            {
                cmd = new SqlCommand(SPName, con);
                cmd.Parameters.AddRange(SqlPrms);
                cmd.CommandTimeout = 240;
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (SqlException)
            {
                throw;
            }

            return ds;
        }

        //call stored procedure to get data.
        public static DataSet GetRecordWithExtendedTimeOut_Query(string Qry)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlConnection con = new SqlConnection(ConStr);

            try
            {
                cmd = new SqlCommand(Qry, con);
                cmd.CommandTimeout = 240;
                cmd.CommandType = CommandType.Text;
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (SqlException)
            {
                throw;
            }

            return ds;
        }

        //call stored procedure to get data.
        public static string GetRecordWithExtendedTimeOut_WithOutParams(string SPName, out string Param, params SqlParameter[] SqlPrms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(ConStr);
            int rowsEffected = 0;
            Param = string.Empty;
            try
            {
                con.Open();
                cmd = new SqlCommand(SPName, con);
                cmd.Parameters.AddRange(SqlPrms);
                cmd.CommandTimeout = 240;
                cmd.CommandType = CommandType.StoredProcedure;
                rowsEffected = cmd.ExecuteNonQuery();

                if (SPName == "API_CustomerBlockStatus")
                {
                    Param = cmd.Parameters["@BlockStatus"].Value.ToString();
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return Param;
        }

        //call stored procedure to insert data.
        public static int InsertRecordWithExtendedTimeOut(string SPName, params SqlParameter[] SqlPrms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(ConStr);
            int rowsEffected = 0;
            try
            {
                con.Open();
                cmd = new SqlCommand(SPName, con);
                cmd.Parameters.AddRange(SqlPrms);
                cmd.CommandTimeout = 240;
                cmd.CommandType = CommandType.StoredProcedure;
                rowsEffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }

            return rowsEffected;
        }

        //call stored procedure to insert data.
        public static int InsertRecordWithExtendedTimeOut_WithOutParams(string SPName, out string[] ArTokens, params SqlParameter[] SqlPrms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(ConStr);
            int rowsEffected = 0;
            ArTokens = new string[2];
            try
            {
                con.Open();
                cmd = new SqlCommand(SPName, con);
                cmd.Parameters.AddRange(SqlPrms);
                cmd.CommandTimeout = 240;
                cmd.CommandType = CommandType.StoredProcedure;
                rowsEffected = cmd.ExecuteNonQuery();

                ArTokens[0] = cmd.Parameters["@UID"].Value.ToString();
                ArTokens[1] = cmd.Parameters["@UID_Token"].Value.ToString();
            }
            catch (SqlException)
            {
                throw;
            }

            return rowsEffected;
        }

        //call stored procedure to update data.
        public static int UpdateRecordWithExtendedTimeOut(string SPName, params SqlParameter[] SqlPrms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(ConStr);
            int rowsEffected = 0;

            try
            {
                con.Open();
                cmd = new SqlCommand(SPName, con);
                cmd.Parameters.AddRange(SqlPrms);
                cmd.CommandTimeout = 240;
                cmd.CommandType = CommandType.StoredProcedure;
                rowsEffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }

            return rowsEffected;
        }

        //call stored procedure to update data.
        public static int DeleteRecordWithExtendedTimeOut(string SPName, params SqlParameter[] SqlPrms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(ConStr);
            int rowsEffected = 0;

            try
            {
                con.Open();
                cmd = new SqlCommand(SPName, con);
                cmd.Parameters.AddRange(SqlPrms);
                cmd.CommandTimeout = 240;
                cmd.CommandType = CommandType.StoredProcedure;
                rowsEffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }

            return rowsEffected;
        }

        public static List<SqlParameter> GenerateSqlParamList<T>(T item, string Type) where T : new()
        {
            List<SqlParameter> objList = new List<SqlParameter>();

            PropertyInfo[] propertyInfos;
            propertyInfos = typeof(T).GetProperties();

            foreach (PropertyInfo obj in propertyInfos)
            {
                if (obj.CanRead)
                {
                    if (Type.ToUpper() == "INSERT")
                    {
                        if (obj.Name.Contains("Updated_By") || obj.Name.Contains("Updated_On") || obj.Name.Contains("Updated_Ip") || obj.Name == "Id")
                        {

                        }
                        else
                        {
                            objList.Add(new SqlParameter("@" + obj.Name, item.GetType().GetProperty(obj.Name).GetValue(item, null)));
                        }
                    }
                    else if (Type.ToUpper() == "UPDATE")
                    {
                        if (obj.Name.Contains("Created_By") || obj.Name.Contains("Created_On") || obj.Name.Contains("Created_Ip"))
                        {

                        }
                        else
                        {
                            objList.Add(new SqlParameter("@" + obj.Name, item.GetType().GetProperty(obj.Name).GetValue(item, null)));
                        }
                    }

                }
            }
            return objList;
        }


        public static List<SqlParameter> GenerateSqlParamList<T>(dynamic Id, string Type) where T : new()
        {
            List<SqlParameter> objList = new List<SqlParameter>();

            PropertyInfo[] propertyInfos;
            propertyInfos = typeof(T).GetProperties();

            foreach (PropertyInfo obj in propertyInfos)
            {
                if (obj.CanRead)
                {
                    if (Type.ToUpper() == "SELECT")
                    {
                        if (obj.Name == "Id")
                        {
                            objList.Add(new SqlParameter("@" + obj.Name, Id));
                        }
                    }
                    else if (Type.ToUpper() == "DELETE")
                    {
                        if (obj.Name == "Id")
                        {
                            objList.Add(new SqlParameter("@" + obj.Name, Id));
                        }
                    }

                }
            }
            return objList;
        }

        public static List<SqlParameter> GenerateSqlParamList<T>(dynamic UserName, dynamic Password, dynamic Device_Key, string Type) where T : new()
        {
            List<SqlParameter> objList = new List<SqlParameter>();

            PropertyInfo[] propertyInfos;
            propertyInfos = typeof(T).GetProperties();

            foreach (PropertyInfo obj in propertyInfos)
            {
                if (obj.CanRead)
                {
                    if (Type.ToUpper() == "SELECT")
                    {
                        if (obj.Name == "UserName")
                        {
                            objList.Add(new SqlParameter("@" + obj.Name, UserName));
                        }
                        if (obj.Name == "Password")
                        {
                            objList.Add(new SqlParameter("@" + obj.Name, Password));
                        }
                        if (obj.Name == "Device_Key")
                        {
                            objList.Add(new SqlParameter("@" + obj.Name, Device_Key));
                        }
                    }
                }
            }
            return objList;
        }

        public static List<SqlParameter> GenerateSqlParamListForSelect<T>(T item, string Type) where T : new()
        {
            List<SqlParameter> objList = new List<SqlParameter>();

            PropertyInfo[] propertyInfos;
            propertyInfos = typeof(T).GetProperties();

            foreach (PropertyInfo obj in propertyInfos)
            {
                if (obj.CanRead)
                {
                    if (Type.ToUpper() == "SELECT")
                    {
                        if (obj.Name.Contains("Updated_By") || obj.Name.Contains("Updated_On") || obj.Name.Contains("Updated_Ip"))
                        {

                        }
                        else
                        {
                            objList.Add(new SqlParameter("@" + obj.Name, item.GetType().GetProperty(obj.Name).GetValue(item, null)));
                        }
                    }
                }
            }
            return objList;
        }
    }

    class ConnectionManager
    {
        static string string1 = ConfigurationManager.AppSettings["string1"].ToString();
        static string string2 = ConfigurationManager.AppSettings["string2"].ToString();
        static string string3 = ConfigurationManager.AppSettings["string3"].ToString();
        static string string4 = ConfigurationManager.AppSettings["string4"].ToString();

        public static string ConStr = string.Empty;
        static ConnectionManager()
        {
            ConStr = "Data Source=" + string1 + ";" +
                                       "Initial Catalog=" + string2 + ";" +
                                       "User id=" + string3 + ";" +
                                       "Password=" + string4 + ";";
        }
    }
}
