using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;


namespace DataLayer.DataProvider
{
    public class SqlDataPlug : DataPlug
    {
        //"System.Data.SqlClient"

        

        public SqlDbType GetParamType(DataType paramType)
        {
            switch (paramType)
            {
                case DataType._Varchar:
                    return SqlDbType.VarChar;
                case DataType._Char:
                    return SqlDbType.Char;
                case DataType._BigInt:
                    return SqlDbType.BigInt;
                case DataType._Date:
                    return SqlDbType.Date;
                case DataType._DateTime:
                    return SqlDbType.DateTime;
                case DataType._Text:
                    return SqlDbType.Text;
                case DataType._Int:
                    return SqlDbType.Int;
                case DataType._Bool:
                    return SqlDbType.Bit;
                case DataType._Double:
                    return SqlDbType.Float;
                case DataType._Decimal:
                    return SqlDbType.Decimal;
                case DataType._TimeStamp:
                    return SqlDbType.Timestamp;
                case DataType._LongText:
                    return SqlDbType.Text;
            }
            return SqlDbType.VarChar;
        }

        protected void FillCommandWithParameter(object command, List<Parameter> parameters)
        {
            SqlCommand cmd = (SqlCommand)command;
            if (parameters == null) return;
            SqlParameter param;

            foreach (DataPlug.Parameter dataParam in parameters)
            {
                param = new SqlParameter();
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
                param.SqlDbType = GetParamType(dataParam.ParamType);
                param.Direction = dataParam.Direction;
                cmd.Parameters.Add(param);
            }

        }

        

        protected void ReAssignParameterValues(object command, List<Parameter> parameters)
        {
            SqlCommand cmd = (SqlCommand)command;
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
                SqlConnection conn = new SqlConnection();

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

        private void AddParameter(SqlCommand command, Dictionary<string, object> parameters)
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
            SqlConnection conn = new SqlConnection();

            if (readOnlyConnection)
                conn.ConnectionString = ReadOnlyConnectionString;
            else
                conn.ConnectionString = ConnectionString;

            try
            {
                SqlCommand cmd = new SqlCommand();
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
            SqlConnection conn = new SqlConnection();

            if (readOnlyConnection)
                conn.ConnectionString = ReadOnlyConnectionString;
            else
                conn.ConnectionString = ConnectionString;
            try
            {
                SqlCommand cmd = new SqlCommand();
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
            SqlConnection conn = new SqlConnection();

            if (readOnlyConnection)
                conn.ConnectionString = ReadOnlyConnectionString;
            else
                conn.ConnectionString = ConnectionString;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandTimeout = DataPlug.COMMAND_TIMEOUT;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procName;

                FillCommandWithParameter(cmd, param);

                SqlDataAdapter da = new SqlDataAdapter();
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
            SqlConnection conn = new SqlConnection();

            if (readOnlyConnection)
                conn.ConnectionString = ReadOnlyConnectionString;
            else
                conn.ConnectionString = ConnectionString;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandTimeout = DataPlug.COMMAND_TIMEOUT;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;

                SqlDataAdapter da = new SqlDataAdapter();
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
            SqlConnection conn = new SqlConnection();

            if (readOnlyConnection)
                conn.ConnectionString = ReadOnlyConnectionString;
            else
                conn.ConnectionString = ConnectionString;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandTimeout = DataPlug.COMMAND_TIMEOUT;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procName;

                FillCommandWithParameter(cmd, param);

                SqlDataAdapter da = new SqlDataAdapter();
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
            SqlConnection conn = new SqlConnection();

            if (readOnlyConnection)
                conn.ConnectionString = ReadOnlyConnectionString;
            else
                conn.ConnectionString = ConnectionString;
            try
            {
                SqlCommand cmd = new SqlCommand();
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
            SqlConnection conn = new SqlConnection();

            if (readOnlyConnection)
                conn.ConnectionString = ReadOnlyConnectionString;
            else
                conn.ConnectionString = ConnectionString;
            try
            {
                SqlCommand cmd = new SqlCommand();
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
