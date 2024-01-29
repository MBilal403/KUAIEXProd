using DataAccessLayer.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KuaiexDashboard.Repository.Impl
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly SqlConnectionHandler connectionHandler;

        public GenericRepository()
        {
            connectionHandler = new SqlConnectionHandler();
        }

        public IEnumerable<T> GetAll(Func<T, bool> condition = null)
        {
            using (var connection = connectionHandler.OpenConnection())
            {
                try
                {
                    var tableName = typeof(T).Name;
                    var query = $"SELECT * FROM {tableName}";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        var entities = MapDataTableToEntities(dataTable);

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
                    var startRow = (page - 1) * pageSize + 1;
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
            }
        }
        public T GetById(object id)
        {
            using (var connection = connectionHandler.OpenConnection())
            {
                try
                {
                    var tableName = typeof(T).Name;
                    var properties = typeof(T).GetProperties();
                    var primaryKey = properties.FirstOrDefault(p => p.Name.EndsWith("Id", StringComparison.OrdinalIgnoreCase));

                    var query = $"SELECT * FROM {tableName} WHERE {primaryKey.Name} = @Id";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

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
            }
        }
        public void Insert(T entity)
        {
            using (var connection = connectionHandler.OpenConnection())
            {
                try
                {
                    var tableName = typeof(T).Name;

                    var properties = typeof(T).GetProperties();
                    var columns = string.Join(", ", properties.Select(p => p.Name));
                    var parameters = string.Join(", ", properties.Select(p => $"@{p.Name}"));

                    var query = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters})";

                    using (var command = new SqlCommand(query, connection))
                    {
                        foreach (var property in properties)
                        {
                            // Add parameters dynamically based on entity properties
                            command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(entity) ?? DBNull.Value);
                        }

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception (log, rethrow, etc.)
                    throw;
                }
            }
        }
        public void Update(T entity)
        {
            using (var connection = connectionHandler.OpenConnection())
            {
                try
                {
                    connection.Open();
                    // Implement the logic to update an entity in the database
                    // Example query: UPDATE TableName SET Column1 = @Value1, Column2 = @Value2 WHERE Id = @Id
                }
                catch (Exception ex)
                {
                    // Handle the exception (log, rethrow, etc.)
                    throw;
                }
            }
        }
        public void Delete(object id)
        {
            using (var connection = connectionHandler.OpenConnection())
            {
                try
                {
                    connection.Open();
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


    }
}