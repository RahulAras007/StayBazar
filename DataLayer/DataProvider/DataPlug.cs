using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;

namespace DataLayer.DataProvider
{
    public abstract class DataPlug
    {

        #region "Attributes"

        protected const int COMMAND_TIMEOUT = 10800;
        public enum DataType
        {
            _Varchar = 1,
            _Text = 2,
            _BigInt = 3,
            _Int = 4,
            _Date = 5,
            _DateTime = 6,
            _Bool = 7,
            _Char = 8,
            _Double = 9,
            _Decimal = 10,
            _TimeStamp = 11,
            _LongText=12
        }


        #endregion

        #region "Connections"

        public string ReadOnlyConnectionString { get; set; }
        public string ConnectionString { get; set; }

        #endregion

        #region "Methods To Implement"

        // public abstract Object GetParamType(DataType paramType);
        

        //protected abstract void FillCommandWithParameter(object command, List<Parameter> parameters);
        //protected abstract void ReAssignParameterValues(object command, List<Parameter> parameters);

        #endregion
        public void MakeConnection(string fullAConnectionString,string readOConnectionString)
         {
            ConnectionString = fullAConnectionString;
            ReadOnlyConnectionString = readOConnectionString;

        }
        public void CreateDatabaseInstance()
        {
            // _localInstance = DbProviderFactories.GetFactory(DatabaseProvider);
        }

        public abstract bool TestConn(bool readOnlyConnection);
        
        
        #region "Parameter Creation Methods"

        public Parameter GetParameter(string paramName, DataType paramType)
        {
            return GetParameter(paramName, paramType, null, 0, ParameterDirection.Input);
        }
        public Parameter GetParameter(string paramName, DataType paramType, object value)
        {
            return GetParameter(paramName, paramType, value, 0, ParameterDirection.Input);
        }

        public Parameter GetParameter(string paramName, DataType paramType, object value, int size)
        {
            return GetParameter(paramName, paramType, value, size, ParameterDirection.Input);
        }

        public Parameter GetParameter(string paramName, DataType paramType, object value, int size, ParameterDirection paramDirection)
        {
            Parameter param = new Parameter();
            param.ParameterName = paramName;

            if (size != 0)
            {
                param.Size = size;
            }

            param.Value = value;

            param.ParamType = paramType;
            param.Direction = paramDirection;

            return param;
        }

        #endregion

        #region "Query Execution Methods"


        public object ExecuteSQLQueryScalar(string sqlQuery, bool readOnlyConnection = false)
        {
            return ExecuteSQLQueryScalar(sqlQuery,null,readOnlyConnection);
        }
        public abstract object ExecuteSQLQueryScalar(string sqlQuery, Dictionary<string, object> param, bool readOnlyConnection = false);


        public object ExecuteQueryScalar(string procName, bool readOnlyConnection = false)
        {
            return ExecuteQueryScalar(procName, null, readOnlyConnection);
        }
        public abstract object ExecuteQueryScalar(string procName, List<Parameter> param, bool readOnlyConnection = false);

        public DataTable GetTable(string procName, bool readOnlyConnection = false)
        {
            return GetTable(procName, null, readOnlyConnection);
        }
        public abstract DataTable GetTable(string procName, List<Parameter> param, bool readOnlyConnection = false);

        public abstract DataTable GetSQLTable(string sql, bool readOnlyConnection = false);


        public DataSet GetDataSet(string procName, bool readOnlyConnection = false)
        {
            return GetDataSet(procName, null, readOnlyConnection);
        }
        public abstract DataSet GetDataSet(string procName, List<Parameter> param, bool readOnlyConnection = false);

        public int ExecuteQuery(string procName, bool readOnlyConnection = false)
        {
            return ExecuteQuery(procName, null, readOnlyConnection);
        }

        public abstract int ExecuteQuery(string procName, List<Parameter> param, bool readOnlyConnection = false);

        public int ExecuteSqlQuery(string sql, bool readOnlyConnection = false)
        {
            return ExecuteSqlQuery(sql, null, readOnlyConnection);
        }
        public abstract int ExecuteSqlQuery(string sql, Dictionary<string, object> param, bool readOnlyConnection = false);
      

        #endregion

        /*
        public DataPlug()
        {
            //make the connection string using settings loaded from app.config

            //  System.Configuration()

            CustomSettings sett = CustomSettings.GetSettings();

            //  ConfigurationManager.OpenExeConfiguration(string exePath)

            string username = sett.User;
            string server = sett.Server;
            string port = sett.Port;
            string password = sett.Password;
            // Dim sqlsecurity As String = My.Settings.IntegratedSecurity
            string data = sett.Database;
            // _connectionString = "Host=192.168.2.25:3306;user=dev;password=d;database=sms"
            _connectionString = "server=" + server + ";port=" + port + ";uid=" + username + ";pwd=" + password + ";database=" + data + ";Allow Zero Datetime=false;Convert Zero Datetime=true";
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-IN");

        }

        #region "Methods"

     

        #region "DataReader"
        //Public Function GetSQLReader(ByVal sql As String) As SqlDataReader
        //    Dim dar As SqlDataReader = Nothing
        //    Try
        //        If readerConnection IsNot Nothing Then
        //            If readerConnection.State <> ConnectionState.Open Then
        //                readerConnection = New MySqlConnection(_connectionString)
        //                readerConnection.Open()
        //            End If
        //        Else
        //            readerConnection = New MySqlConnection(_connectionString)
        //            readerConnection.Open()
        //        End If
        //        Dim cmd As New MySqlCommand()
        //        cmd.Connection = readerConnection
        //        cmd.CommandTimeout = COMMAND_TIMEOUT
        //        cmd.CommandType = CommandType.Text
        //        cmd.CommandText = sql

        //        dar = cmd.ExecuteReader(CommandBehavior.SequentialAccess)
        //        listOfDataReader.Add(dar)
        //    Catch ex As Exception
        //        CloseReader(Nothing)
        //        Throw ex
        //        '  Finally

        //    End Try
        //    Return dar
        //End Function

        //Public Sub CloseReader(ByVal reader As SqlDataReader)
        //    Try
        //        If reader IsNot Nothing Then
        //            listOfDataReader.Remove(reader)
        //            reader.Close()
        //        End If
        //    Finally
        //        If listOfDataReader.Count = 0 Then
        //            If readerConnection IsNot Nothing Then
        //                If readerConnection.State = ConnectionState.Open Then readerConnection.Close()
        //            End If
        //        End If
        //    End Try
        //End Sub
        #endregion
       
       */

        #region "Value Conversion"

        public string ToString(object databaseObject)
        {
            string s = string.Empty;
            if(databaseObject != null && databaseObject != System.DBNull.Value)
            {
                s = databaseObject.ToString();
            }
            return s;
        }

        public double ToDouble(object databaseObject)
        {
            double d = 0;
            if (databaseObject != null && databaseObject != System.DBNull.Value)
            {
                double.TryParse(databaseObject.ToString(), out d);
            }

            return d;
        }
        public decimal ToDecimal(object databaseObject)
        {
            decimal d = 0;

            if (databaseObject != null && databaseObject != System.DBNull.Value)
            {
                decimal.TryParse(databaseObject.ToString(), out d);
            }
            return d;
        }

        public int ToInteger(object databaseObject)
        {
            int i = 0;
            if (databaseObject != null && databaseObject != System.DBNull.Value)
            {
                int.TryParse(databaseObject.ToString(), out i);
            }
            return i;
        }

        public short ToShort(object databaseObject)
        {
            short i = 0;
            if (databaseObject != null && databaseObject != System.DBNull.Value)
            {
                if (!short.TryParse(databaseObject.ToString(), out i))
                {
                    i = 0;
                }
            }
            return i;
        }

        public ulong ToULong(object databaseObject)
        {
            ulong i = 0;

            if (databaseObject != null && databaseObject != System.DBNull.Value)
            {
                i = ulong.Parse(databaseObject.ToString());
            }
            return i;
        }

        public long ToLong(object databaseObject)
        {
            long i = 0;

            if (databaseObject != null && databaseObject != System.DBNull.Value)
            {
                i = long.Parse(databaseObject.ToString());
            }
            return i;
        }

        public bool ToBoolean(object databaseObject)
        {
            bool b = false;
            if (databaseObject != null && databaseObject != System.DBNull.Value)
            {
                b = Convert.ToBoolean(databaseObject);
            }
            return b;
        }

        public System.DateTime ToDate(object databaseObject)
        {
            System.DateTime d = new System.DateTime();
            if (databaseObject != null && databaseObject != System.DBNull.Value)
            {
                System.DateTime.TryParse(databaseObject.ToString(), out d);
            }
            return d;
        }

        public Nullable<DateTime> ToNDate(object databaseObject)
        {
            System.DateTime d = new System.DateTime();
            if (databaseObject != null && databaseObject != System.DBNull.Value)
            {
                System.DateTime.TryParse(databaseObject.ToString(), out d);
            }
            return d;
        }

        public double MoneyToDouble(object databaseObject)
        {
            double d = 0;
            if (databaseObject != null && databaseObject != System.DBNull.Value)
            {
                if (!double.TryParse(databaseObject.ToString(), out d))
                {
                    d = 0;
                }
            }
            return d;
        }

        #endregion


        public class Parameter
        {
            public string ParameterName { get; set; }
            public object Value { get; set; }
            public DataPlug.DataType ParamType { get; set; }
            public ParameterDirection Direction { get; set; }
            public int Size { get; set; }
            public bool IsNullable { get; set; }
        }
    }
}
