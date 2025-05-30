using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QLQG.DAL
{
    public class DataProvider
    {
        private static DataProvider instance;
        private readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["QLQuanGameConnection"].ConnectionString;

        public static DataProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataProvider();
                }
                return instance;
            }
            private set => instance = value;
        }

        private DataProvider() { }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public DataTable ExecuteQuery(string query, (string, object)[] parameters, CommandType commandType = CommandType.Text)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = commandType;
                        if (parameters != null)
                        {
                            foreach (var (name, value) in parameters)
                            {
                                Console.WriteLine($"ExecuteQuery: Thêm tham số {name} = {value}");
                                command.Parameters.AddWithValue(name, value ?? DBNull.Value);
                            }
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong ExecuteQuery: {ex.Message}\n{ex.StackTrace}");
                throw new Exception($"Lỗi khi thực thi truy vấn: {query}", ex);
            }
        }

        public int ExecuteNonQuery(string query, (string, object)[] parameters, CommandType commandType = CommandType.Text)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = commandType;
                        if (parameters != null)
                        {
                            foreach (var (name, value) in parameters)
                            {
                                Console.WriteLine($"ExecuteNonQuery: Thêm tham số {name} = {value}");
                                command.Parameters.AddWithValue(name, value ?? DBNull.Value);
                            }
                        }
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong ExecuteNonQuery: {ex.Message}\n{ex.StackTrace}");
                throw new Exception($"Lỗi khi thực thi truy vấn: {query}", ex);
            }
        }

        public object ExecuteScalar(string query, params (string, object)[] parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;
                        if (parameters != null)
                        {
                            foreach (var (name, value) in parameters)
                            {
                                Console.WriteLine($"ExecuteScalar: Thêm tham số {name} = {value}");
                                command.Parameters.AddWithValue(name, value ?? DBNull.Value);
                            }
                        }
                        return command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong ExecuteScalar: {ex.Message}\n{ex.StackTrace}");
                throw new Exception($"Lỗi khi thực thi truy vấn: {query}", ex);
            }
        }

        public void ExecuteStoredProcedure(string storedProcedureName, out Dictionary<string, object> outputParams, params (string, object)[] parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        Dictionary<string, object> outputValues = new Dictionary<string, object>();

                        if (parameters != null)
                        {
                            foreach (var (name, value) in parameters)
                            {
                                SqlParameter param = new SqlParameter(name, value ?? DBNull.Value);
                                if (value == null)
                                {
                                    param.Direction = ParameterDirection.Output;
                                    param.SqlDbType = SqlDbType.Int;
                                }
                                Console.WriteLine($"ExecuteStoredProcedure: Thêm tham số {name} = {value}");
                                command.Parameters.Add(param);
                            }
                        }

                        command.ExecuteNonQuery();

                        foreach (SqlParameter param in command.Parameters)
                        {
                            if (param.Direction == ParameterDirection.Output)
                            {
                                outputValues[param.ParameterName] = param.Value;
                            }
                        }

                        outputParams = outputValues;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong ExecuteStoredProcedure: {ex.Message}\n{ex.StackTrace}");
                throw new Exception($"Lỗi khi thực thi stored procedure: {storedProcedureName}", ex);
            }
        }
    }
}