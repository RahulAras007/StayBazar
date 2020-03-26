using System;
using System.Collections.Generic;
using DataLayer.DataProvider;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Settings : DataLink
    {
        public string GetValue(string key)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pKey", DataPlug.DataType._Varchar, key));
            object value = Connection.ExecuteQueryScalar("settings_Get", param);
            return Connection.ToString(value);
        }

        public void SetValue(string key, string value)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pKey", DataPlug.DataType._Varchar, key));
            param.Add(Connection.GetParameter("pValue", DataPlug.DataType._Varchar, value));
            Connection.ExecuteQuery("settings_Set",param);
        }
    }
}
