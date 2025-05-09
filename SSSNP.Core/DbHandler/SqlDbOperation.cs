using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace SSSNP_SNSOP.Core.DbHandler
{
    public class SqlDbOperation : IDbOperation
    {
        //private readonly string connectionStr = "Server=167.86.68.15; Database=PBW_SNSOP; User Id=snsop;Password=Win@2019!@#$;Connect Timeout=12000;"; // get from config
        private string connectionStr = "";

        public SqlDbOperation(string connStr)
        {
            connectionStr = connStr;
        }
        public bool InsertData(string tableName, Dictionary<string, object> data)
        {
            var sqlConnection = new SqlConnection(connectionStr);
            string columns = "";
            string param = "";
            List<object> values = new List<object>();
            try
            {
                int i = 1;
                foreach (KeyValuePair<string, object> value in data)
                {
                    columns += String.Format(" {0},", value.Key);
                    param += String.Format(" {0},", "@param" + i);
                    values.Add(value.Value);
                    i++;
                }
                columns = columns.Substring(0, columns.Length - 1);
                param = param.Substring(0, param.Length - 1);
                string sql = String.Format("INSERT INTO {0}({1}) VALUES({2});", tableName, columns, param);


                SqlCommand commands = new SqlCommand(sql, sqlConnection);
                commands.CommandText = sql;
                commands.CommandType = CommandType.Text;

                int j = 1;
                foreach (object value in values)
                {
                    string paramName = "@param" + j++;
                    commands.Parameters.Add(new SqlParameter(paramName, value));
                }

                sqlConnection.Open();
                if (commands.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                sqlConnection.Close();
                return false;
            }
            catch (Exception x)
            {
                throw x;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }

        public bool GenericInsertDataList<T>(List<T> dataList, string tableName)
        {
            using (var sqlConnection = new SqlConnection(connectionStr))
            {
                sqlConnection.Open();
                var transaction = sqlConnection.BeginTransaction();

                try
                {
                    string columns = "";
                    List<string> valueList = new List<string>();
                    List<SqlParameter> parameters = new List<SqlParameter>();

                    var properties = typeof(T).GetProperties();
                    int paramIndex = 0;

                    // Build the column names
                    columns = string.Join(",", properties.Select(p => p.Name));

                    foreach (var data in dataList)
                    {
                        List<string> currentValues = new List<string>();
                        foreach (var property in properties)
                        {
                            paramIndex++;
                            var value = property.GetValue(data) ?? DBNull.Value;

                            string paramName = $"@param{paramIndex}";
                            currentValues.Add(paramName);
                            parameters.Add(new SqlParameter(paramName, value));
                        }

                        valueList.Add($"({string.Join(",", currentValues)})");
                    }

                    // Combine all values into a single SQL statement
                    string sql = $"INSERT INTO {tableName} ({columns}) VALUES {string.Join(",", valueList)};";

                    using (var command = new SqlCommand(sql, sqlConnection, transaction))
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return true; // All inserts succeeded
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Consider logging the exception
                    throw;
                }
            }
        }

        public bool GenericInsertData<T>(T data, string tableName)
        {
            var sqlConnection = new SqlConnection(connectionStr);
            string columns = "";
            string param = "";
            List<object> values = new List<object>();

            try
            {
                var properties = typeof(T).GetProperties();
                int i = 1;

                foreach (var property in properties)
                {
                    var value = property.GetValue(data);
                    columns += $"{property.Name},";
                    param += $"@param{i},";
                    values.Add(value);
                    i++;
                }

                columns = columns.TrimEnd(',');
                param = param.TrimEnd(',');

                string sql = $"INSERT INTO {tableName}({columns}) VALUES({param});";

                using (SqlCommand commands = new SqlCommand(sql, sqlConnection))
                {
                    int j = 1;
                    foreach (var value in values)
                    {
                        string paramName = "@param" + j++;
                        commands.Parameters.Add(new SqlParameter(paramName, value ?? DBNull.Value));
                    }

                    sqlConnection.Open();
                    return commands.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }

        public bool UpdateData(string tableName, Dictionary<string, object> data, string wherePart)
        {
            var sqlConnection = new SqlConnection(connectionStr);
            string fields = "";
            List<object> updatedValue = new List<object>();
            if (data != null && data.Count > 0)
            {
                int i = 1;
                foreach (KeyValuePair<string, object> value in data)
                {
                    fields += String.Format(" {0} = {1},", value.Key.ToString(), "@param" + i);
                    updatedValue.Add(value.Value);
                    i++;
                }
                fields = fields.Substring(0, fields.Length - 1);
            }
            try
            {
                string sql = String.Format("UPDATE {0} SET {1} WHERE {2};", tableName, fields, wherePart);

                SqlCommand commands = new SqlCommand(sql, sqlConnection);
                commands.CommandText = sql;
                commands.CommandType = CommandType.Text;

                int j = 1;
                foreach (object obj in updatedValue)
                {
                    string param = "@param" + (j++);
                    commands.Parameters.Add(new SqlParameter(param, obj));
                }

                sqlConnection.Open();
                if (commands.ExecuteNonQuery() > 0)
                {
                    commands.Dispose();
                    return true;
                }
                sqlConnection.Close();
            }
            catch (Exception x)
            {
                throw x;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return false;
        }
        public DataTable GetDataTable(string sql)
        {
            var sqlDbConnection = new SqlConnection(connectionStr);
            DataTable dataTable = new DataTable();
            try
            {
                SqlCommand commands = new SqlCommand(sql, sqlDbConnection);
                commands.CommandText = sql;
                sqlDbConnection.Open();
                SqlDataReader reader = commands.ExecuteReader();
                dataTable.Load(reader);
                reader.Close();
                sqlDbConnection.Close();
            }
            catch (Exception x)
            {
                throw x;
            }
            finally
            {
                if (sqlDbConnection.State == ConnectionState.Open)
                {
                    sqlDbConnection.Close();
                }
            }
            return dataTable;
        }
        public DataSet ExecuteCmdForSp(List<CmdParamModel> cmdParams, string spName)
        {
            var sqlDbConnection = new SqlConnection(connectionStr);
            try
            {
                var sqlCommand = new SqlCommand(spName, sqlDbConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlDbConnection.Open();
                SqlCommandBuilder.DeriveParameters(sqlCommand);
                sqlDbConnection.Close();

                foreach (var cmdParam in cmdParams)
                {
                    if (sqlCommand.Parameters.Contains(cmdParam.ParamName))
                    {
                        if (sqlCommand.Parameters[cmdParam.ParamName].SqlDbType == SqlDbType.Decimal)
                        {
                            sqlCommand.Parameters[cmdParam.ParamName].Value = string.IsNullOrWhiteSpace(cmdParam.ParamValue?.ToString())
                                ? DBNull.Value
                                : cmdParam.ParamValue;
                        }
                        else
                        {
                            sqlCommand.Parameters[cmdParam.ParamName].Value = cmdParam.ParamValue;
                        }
                    }
                    else
                    {
                        throw new Exception("SP: " + spName + " is not having parameter: " + cmdParam.ParamName);
                    }
                }

                var dataSet = new DataSet();
                var oracleAdapter = new SqlDataAdapter(sqlCommand);

                oracleAdapter.Fill(dataSet);

                var dataTable = new DataTable("out");
                dataTable.Columns.Add("key");
                dataTable.Columns.Add("value");

                foreach (SqlParameter oracleParam in sqlCommand.Parameters)
                {
                    if (oracleParam.Direction != ParameterDirection.InputOutput &&
                        oracleParam.Direction != ParameterDirection.Output)
                    {
                        continue;
                    }

                    var dataRow = dataTable.NewRow();
                    dataRow["key"] = oracleParam.ParameterName;
                    dataRow["value"] = oracleParam.Value;
                    dataTable.Rows.Add(dataRow);
                }
                dataSet.Tables.Add(dataTable);
                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlDbConnection.State == ConnectionState.Open)
                {
                    sqlDbConnection.Close();
                }
            }
        }

        public DataTable ExecuteDataTable(string query, CommandType commandType)
        {
            var sqlConnection = new SqlConnection(connectionStr);
            var dataTable = new DataTable();
            try
            {
                using (var sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.CommandType = commandType;
                    using (var sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        sqlDataAdapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return dataTable;
        }

        public DataTable ExecuteDataTable(string query, CommandType commandType, SqlParameter param)
        {
            var sqlConnection = new SqlConnection(connectionStr);
            var dataTable = new DataTable();
            try
            {
                using (var sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.CommandType = commandType;
                    sqlCommand.Parameters.Add(param);
                    using (var sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        sqlDataAdapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return dataTable;
        }

        public DataTable ExecuteDataTable(string query, CommandType commandType, List<SqlParameter> parameters)
        {
            var sqlConnection = new SqlConnection(connectionStr);
            var dataTable = new DataTable();
            try
            {
                using (var sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.CommandType = commandType;

                    // Add the list of parameters to the command if any
                    if (parameters != null && parameters.Count > 0)
                    {
                        sqlCommand.Parameters.AddRange(parameters.ToArray());
                    }

                    // Open the connection
                    sqlConnection.Open();

                    // Execute the query and fill the DataTable
                    using (var sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        sqlDataAdapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle or log exceptions
                throw new Exception($"Error executing SQL query: {ex.Message}", ex);
            }
            finally
            {
                // Ensure the connection is closed
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
            return dataTable;
        }

        public DataTable ExecuteDataTable(string query, CommandType commandType, List<SqlParameter> parameters, SqlConnection connection, SqlTransaction transaction)
        {
            var dataTable = new DataTable();

            try
            {
                using (var sqlCommand = new SqlCommand(query, connection, transaction))
                {
                    sqlCommand.CommandType = commandType;

                    if (parameters != null && parameters.Count > 0)
                    {
                        sqlCommand.Parameters.AddRange(parameters.ToArray());
                    }

                    using (var sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        sqlDataAdapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing SQL query in transaction: {ex.Message}", ex);
            }

            return dataTable;
        }


    }
}