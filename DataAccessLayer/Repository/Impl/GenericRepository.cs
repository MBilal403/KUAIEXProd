using DataAccessLayer.Helpers;
using DataAccessLayer.Repository;
using DataAccessLayer.Repository.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace KuaiexDashboard.Repository.Impl
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly SqlConnectionHandler connectionHandler;
        private readonly ConditionToWhereClauseConverter<T> Converter;


        public GenericRepository()
        {
            connectionHandler = new SqlConnectionHandler();
            Converter = new ConditionToWhereClauseConverter<T>();
        }

        public List<TResult> GetAllWithJoins<TResult>(List<JoinInfo> joins, Func<TResult, bool> condition = null, string columns = null) where TResult : class
        {
            using (var connection = connectionHandler.OpenConnection())
            {
                try
                {
                    string tableName = typeof(T).Name;

                    var columnList = columns != null ? columns : "*";

                    string query = $"SELECT {columnList} FROM {tableName}";

                    // Add joins if join conditions are provided
                    if (joins != null && joins.Any())
                    {
                        foreach (var join in joins)
                        {
                            query += $" {join.JoinType} JOIN {join.TargetTable} ON {join.JoinCondition}";
                        }
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        var entities = MapDataTableToEntities<TResult>(dataTable);

                        // Apply the condition if provided
                        if (condition != null)
                        {

                            entities = entities.Where(condition);
                        }

                        return entities.ToList();
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Dispose(); // Ensure connection is always disposed
                }
            }
        }
        public List<T> GetAll(Expression<Func<T, bool>> condition = null, params Expression<Func<T, object>>[] columns)
        {
            using (var connection = connectionHandler.OpenConnection())
            {
                try
                {
                    var tableName = typeof(T).Name;

                    var columnList = columns != null && columns.Any() ? string.Join(", ", columns.Select(GetColumnName)) : "*";

                    var query = $"SELECT {columnList} FROM {tableName}";

                    // Add WHERE clause if condition is provided

                    if (condition != null)
                    {
                        var whereClause = Converter.ConvertToWhereClause(condition);
                        query += $" WHERE {whereClause}";
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        return MapDataTableToEntities(dataTable).ToList();
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Dispose(); // Ensure connection is always disposed
                }
            }
        }
        public T GetbyId(int id, params Expression<Func<T, object>>[] columns)
        {
            using (var connection = connectionHandler.OpenConnection())
            {
                try
                {
                    var tableName = typeof(T).Name;

                    var columnList = columns != null && columns.Any() ? string.Join(", ", columns.Select(GetColumnName)) : "*";

                    var query = $"SELECT {columnList} FROM {tableName} where UID = '{id}'";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        return MapDataTableToEntities(dataTable).ToList().FirstOrDefault();
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Dispose(); // Ensure connection is always disposed
                }
            }
        }
        public PagedResult<T> GetPagedData(int page, int pageSize)
        {
            using (var connection = connectionHandler.OpenConnection())
            {
                try
                {
                    var tableName = typeof(T).Name;

                    // Calculate the starting row for the current page
                    var startRow = Math.Max((page - 1) * pageSize + 1, 0);
                    var endRow = startRow + pageSize - 1;

                    // Retrieve total record count
                    var totalRecordsQuery = $"SELECT COUNT(*) FROM {tableName}";
                    using (var countCommand = new SqlCommand(totalRecordsQuery, connection))
                    {
                        var totalRecords = (int)countCommand.ExecuteScalar();

                        // Retrieve paginated data
                        var query = $"SELECT * FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY Id) AS RowNum FROM {tableName}) AS Temp WHERE RowNum BETWEEN {startRow} AND {endRow}";
                        using (var adapter = new SqlDataAdapter(query, connection))
                        {
                            var dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            var entities = MapDataTableToEntities(dataTable);

                            // Create and return PagedResult object
                            var result = new PagedResult<T>
                            {
                                CurrentPage = page,
                                PageSize = pageSize,
                                TotalSize = totalRecords,
                                Data = entities.ToList()
                            };

                            return result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception (log, rethrow, etc.)
                    throw;
                }
                finally
                {
                    connection.Dispose(); // Ensure connection is always disposed
                }
            }
        }
        public T FindBy(Expression<Func<T, bool>> condition = null)
        {
            using (var connection = connectionHandler.OpenConnection())
            {
                try
                {
                    var tableName = typeof(T).Name;
                    var properties = typeof(T).GetProperties();
                    var primaryKey = properties.FirstOrDefault(p => p.Name.EndsWith("Id", StringComparison.OrdinalIgnoreCase));

                    var query = $"SELECT * FROM {tableName} ";

                    var whereClause = Converter.ConvertToWhereClause(condition);
                    query += $" WHERE {whereClause}";

                    using (var command = new SqlCommand(query, connection))
                    {

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                T result = Activator.CreateInstance<T>();
                                foreach (var property in properties)
                                {
                                    if (!object.Equals(reader[property.Name], DBNull.Value))
                                    {
                                        property.SetValue(result, reader[property.Name]);
                                    }
                                }
                                return result;
                            }
                            else
                            {
                                return null; // Entity not found
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception (log, rethrow, etc.)
                    throw;
                }
                finally
                {
                    connection.Dispose(); // Ensure connection is always disposed
                }
            }
        }
        public int Insert(T entity)
        {
            using (var connection = connectionHandler.OpenConnection())
            {
                try
                {
                    var tableName = typeof(T).Name;

                    // Skip Primary Key of Table
                    var properties = typeof(T).GetProperties().Where(p => !Attribute.IsDefined(p, typeof(KeyAttribute)));

                    // Neglect the Null Values
                    var columns = string.Join(", ", properties.Where(p => p.GetValue(entity) != null).Select(p => p.Name));
                    var parameters = string.Join(", ", properties.Where(p => p.GetValue(entity) != null).Select(p => $"@{p.Name}"));

                    var query = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters});  SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        foreach (var property in properties)
                        {
                            var value = property.GetValue(entity);
                            // Add parameters dynamically based on entity properties
                            command.Parameters.AddWithValue($"@{property.Name}", value ?? DBNull.Value);
                        }

                        int insertedId = Convert.ToInt32(command.ExecuteScalar());

                        return insertedId;
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception (log, rethrow, etc.)
                    throw;
                }
                finally
                {
                    connection.Dispose(); // Ensure connection is always disposed
                }
            }
        }
        public void Update(T entity,string whereClause)
        {
            using (var connection = connectionHandler.OpenConnection())
            {
                try
                {
                    var tableName = typeof(T).Name;
                    var properties = typeof(T).GetProperties().Where(p => p.GetValue(entity) != null && !Attribute.IsDefined(p, typeof(KeyAttribute)));

                    // Create SET clause for update
                    var updateColumns = string.Join(", ", properties.Where(p => p.GetValue(entity) != null).Select(p => $"{p.Name} = @{p.Name}"));

                    var query = $"UPDATE {tableName} SET {updateColumns} ";

                    query += $" WHERE {whereClause}";

                    using (var command = new SqlCommand(query, connection))
                    {
                        foreach (var property in properties)
                        {
                            var value = property.GetValue(entity);
                            command.Parameters.AddWithValue($"@{property.Name}", value ?? DBNull.Value);
                        }

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception (log, rethrow, etc.)
                    throw;
                }
                finally
                {
                    connection.Dispose(); // Ensure connection is always disposed
                }
            }
        }

        public void Delete(object id)
        {
            using (var connection = connectionHandler.OpenConnection())
            {
                try
                {
                    var tableName = typeof(T).Name;
                    var query = $"DELETE FROM {tableName} WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception (log, rethrow, etc.)
                    throw;
                }
                finally
                {
                    connection.Dispose(); // Ensure connection is always disposed
                }
            }
        }
        public PagedResult<T> GetPagedDataFromSP<T>(string storedProcedureName, int page = 1, int pageSize = 10, string searchString = null) where T : class
        {
            using (var connection = connectionHandler.OpenConnection())
            {
                try
                {
                    var tableName = typeof(T).Name;

                    var startRow = page;
                    var endRow = startRow + pageSize - 1;

           
                    using (var command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                     
                        command.Parameters.AddWithValue("@StartRow", startRow);
                        command.Parameters.AddWithValue("@EndRow", endRow);
                        command.Parameters.AddWithValue("@searchString", "%" + searchString + "%");

                        // Add output parameter for total record count
                        var totalRecordsParameter = new SqlParameter
                        {
                            ParameterName = "@TotalCount",
                            SqlDbType = SqlDbType.Int,
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(totalRecordsParameter);

                        // Add output parameter for total record count
                        var filterRecordsParameter = new SqlParameter
                        {
                            ParameterName = "@FilteredCount",
                            SqlDbType = SqlDbType.Int,
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(filterRecordsParameter);

                        using (var adapter = new SqlDataAdapter(command))
                        {
                            var dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            var entities = MapDataTableToEntities<T>(dataTable);

                            // Retrieve total record count from output parameter
                            var totalRecords = (int)totalRecordsParameter.Value;
                            var filteredRecords = (int)filterRecordsParameter.Value;

                            var result = new PagedResult<T>
                            {
                                CurrentPage = page,
                                PageSize = pageSize,
                                TotalSize = totalRecords,
                                FilterRecored = filteredRecords,
                                Data = entities.ToList()
                            };

                            return result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception (log, rethrow, etc.)
                    throw;
                }
                finally
                {
                    connection.Dispose(); // Ensure connection is always disposed
                }
            }
        }
        public List<TResult> GetDataFromSP<TResult>(string storedProcedureName) where TResult : class
        {
            using (var connection = connectionHandler.OpenConnection())
            {
                try
                {
                    // Call the stored procedure for paginated data
                    using (var command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (var adapter = new SqlDataAdapter(command))
                        {
                            var dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            var entities = MapDataTableToEntities<TResult>(dataTable);

                            return entities.ToList();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception (log, rethrow, etc.)
                    throw;
                }
                finally
                {
                    connection.Dispose(); // Ensure connection is always disposed
                }
            }
        }
        private IEnumerable<T> MapDataTableToEntities(DataTable dataTable)
        {
            List<T> result = new List<T>();
            foreach (DataRow row in dataTable.Rows)
            {
                T item = Activator.CreateInstance<T>();

                foreach (var property in typeof(T).GetProperties())
                {
                    if (dataTable.Columns.Contains(property.Name) && !object.Equals(row[property.Name], DBNull.Value))
                    {
                        property.SetValue(item, row[property.Name], null);
                    }
                }
                result.Add(item);
            }
            return result;
        }
        private IEnumerable<TResult> MapDataTableToEntities<TResult>(DataTable dataTable) where TResult : class
        {
            List<TResult> result = new List<TResult>();
            foreach (DataRow row in dataTable.Rows)
            {
                TResult item = Activator.CreateInstance<TResult>();

                foreach (var property in typeof(TResult).GetProperties())
                {
                    if (dataTable.Columns.Contains(property.Name) && !object.Equals(row[property.Name], DBNull.Value))
                    {
                        property.SetValue(item, row[property.Name], null);
                    }
                }
                result.Add(item);
            }
            return result;
        }
        private string GetColumnName(Expression<Func<T, object>> columnExpression)
        {
            // Check if the expression is a MemberExpression
            if (columnExpression.Body is MemberExpression memberExpression)
            {
                // Get the name of the property
                return memberExpression.Member.Name;
            }
            // Check if the expression is a UnaryExpression with an operand of MemberExpression
            else if (columnExpression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression unaryMemberExpression)
            {
                // Get the name of the property
                return unaryMemberExpression.Member.Name;
            }
            else
            {
                throw new ArgumentException("Invalid expression. Expression must be a property access expression.");
            }
        }

    }

}
