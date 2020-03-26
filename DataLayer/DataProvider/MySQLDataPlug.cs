using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DataLayer.DataProvider
{
    public class MySQLDataPlug : DataPlug
    {


        public MySqlDbType GetParamType(DataType paramType)
        {
            switch (paramType)
            {
                case DataType._Varchar:
                case DataType._Char:
                    return MySqlDbType.VarChar;
                case DataType._BigInt:
                    return MySqlDbType.Int64;
                case DataType._Date:
                    return MySqlDbType.Date;
                case DataType._DateTime:
                    return MySqlDbType.DateTime;
                case DataType._Text:
                    return MySqlDbType.Text;
                case DataType._Int:
                    return MySqlDbType.Int32;
                case DataType._Bool:
                    return MySqlDbType.Bit;
                case DataType._Double:
                    return MySqlDbType.Double;
                case DataType._Decimal:
                    return MySqlDbType.Decimal;
                case DataType._TimeStamp:
                    return MySqlDbType.Timestamp;
                case DataType._LongText:
                    return MySqlDbType.LongText;
            }
            return MySqlDbType.VarChar;
        }


        public void FillCommandWithParameter(object command, List<Parameter> parameters)
        {
            if (parameters == null) return;
            MySqlCommand cmd = (MySqlCommand)command;
            MySqlParameter param;

            foreach (DataPlug.Parameter dataParam in parameters)
            {
                param = new MySqlParameter();
                param.ParameterName = dataParam.ParameterName;

                if (dataParam.Size != 0) param.Size = dataParam.Size;

                if (dataParam.Value != null)
                {
                    param.Value = dataParam.Value;
                }
                else
                {
                    if (dataParam.Direction != ParameterDirection.ReturnValue & dataParam.Direction != ParameterDirection.Output)
                    {
                        param.IsNullable = true;
                        param.Value = DBNull.Value;
                    }
                }
                param.MySqlDbType = GetParamType(dataParam.ParamType);
                param.Direction = dataParam.Direction;
                cmd.Parameters.Add(param);
            }

        }


        private void ReAssignParameterValues(object command, List<Parameter> parameters)
        {
            MySqlCommand cmd = (MySqlCommand)command;
            int cnt = cmd.Parameters.Count;
            if (cnt == 0) return;
            int i = 0;
            for (i = 0; i < cnt; i++)
            {
                if (cmd.Parameters[i].Direction == ParameterDirection.Output || cmd.Parameters[i].Direction == ParameterDirection.InputOutput || cmd.Parameters[i].Direction == ParameterDirection.ReturnValue)
                    parameters[i].Value = cmd.Parameters[i].Value;
            }
        }

        public override bool TestConn(bool readOnlyConnection)
        {
            bool result = true;
            try
            {
                MySqlConnection conn = new MySqlConnection();

                if (readOnlyConnection)
                    conn.ConnectionString = ReadOnlyConnectionString;
                else
                    conn.ConnectionString = ConnectionString;
                conn.Open();
                conn.Close();
            }
            catch
            {
                result = false;
            }
            return result;
        }

        private void AddParameter(MySqlCommand command, Dictionary<string, object> parameters)
        {
            if (parameters == null) return;
            foreach (var param in parameters)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = param.Key;
                parameter.Value = param.Value ?? DBNull.Value;
                command.Parameters.Add(parameter);
            }
        }

        #region "Query Execution Methods"


        public override object ExecuteSQLQueryScalar(string sqlQuery, Dictionary<string, object> param, bool readOnlyConnection = false)
        {
            object ar = null;
            MySqlConnection conn = new MySqlConnection();

            if (readOnlyConnection)
                conn.ConnectionString = ReadOnlyConnectionString;
            else
                conn.ConnectionString = ConnectionString;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandTimeout = DataPlug.COMMAND_TIMEOUT;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlQuery;

                AddParameter(cmd, param);

                conn.Open();

                ar = cmd.ExecuteScalar();
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return ar;
        }


        public override object ExecuteQueryScalar(string procName, List<DataPlug.Parameter> param = null, bool readOnlyConnection = false)
        {
            object obj = null;
            MySqlConnection conn = new MySqlConnection();

            if (readOnlyConnection)
                conn.ConnectionString = ReadOnlyConnectionString;
            else
                conn.ConnectionString = ConnectionString;
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandTimeout = DataPlug.COMMAND_TIMEOUT;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procName;

                FillCommandWithParameter(cmd, param);
                conn.Open();

                obj = cmd.ExecuteScalar();
                ReAssignParameterValues(cmd, param);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return obj;
        }

        public override DataTable GetTable(string procName, List<DataPlug.Parameter> param = null, bool readOnlyConnection = false)
        {
            DataTable dt = new DataTable();
            MySqlConnection conn = new MySqlConnection();

            if (readOnlyConnection)
                conn.ConnectionString = ReadOnlyConnectionString;
            else
                conn.ConnectionString = ConnectionString;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandTimeout = DataPlug.COMMAND_TIMEOUT;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procName;

                FillCommandWithParameter(cmd, param);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                 conn.Open();

                da.Fill(dt);

                ReAssignParameterValues(cmd, param);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return dt;
        }

        public override DataTable GetSQLTable(string sql, bool readOnlyConnection = false)
        {
            DataTable dt = new DataTable();
            MySqlConnection conn = new MySqlConnection();

            if (readOnlyConnection)
                conn.ConnectionString = ReadOnlyConnectionString;
            else
                conn.ConnectionString = ConnectionString;
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandTimeout = DataPlug.COMMAND_TIMEOUT;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                conn.Open();

                da.Fill(dt);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();

                }
            }
            return dt;
        }


        public override DataSet GetDataSet(string procName, List<DataPlug.Parameter> param = null, bool readOnlyConnection = false)
        {
            DataSet dt = new DataSet();
            MySqlConnection conn = new MySqlConnection();

            if (readOnlyConnection)
                conn.ConnectionString = ReadOnlyConnectionString;
            else
                conn.ConnectionString = ConnectionString;
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandTimeout = DataPlug.COMMAND_TIMEOUT;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procName;

                FillCommandWithParameter(cmd, param);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;

                da.Fill(dt);
                ReAssignParameterValues(cmd, param);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();

                }
            }
            return dt;
        }


        public override int ExecuteQuery(string procName, List<DataPlug.Parameter> param, bool readOnlyConnection = false)
        {
            int ar = 0;
            MySqlConnection conn = new MySqlConnection();

            if (readOnlyConnection)
                conn.ConnectionString = ReadOnlyConnectionString;
            else
                conn.ConnectionString = ConnectionString;
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandTimeout = DataPlug.COMMAND_TIMEOUT;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procName;


                FillCommandWithParameter(cmd, param);
                conn.Open();

                ar = cmd.ExecuteNonQuery();

                ReAssignParameterValues(cmd, param);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return ar;
        }

        public override int ExecuteSqlQuery(string sql, Dictionary<string, object> param, bool readOnlyConnection = false)
        {
            int ar = 0;
            MySqlConnection conn = new MySqlConnection();

            if (readOnlyConnection)
                conn.ConnectionString = ReadOnlyConnectionString;
            else
                conn.ConnectionString = ConnectionString;
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandTimeout = DataPlug.COMMAND_TIMEOUT;
                cmd.CommandText = sql;

                AddParameter(cmd, param);

                conn.Open();

                ar = cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return ar;
        }


        #endregion

    }
}
