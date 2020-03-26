using DataLayer.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Salutation : DataLink
    {
        public List<CLayer.Salutation> GetAll()
        {
            List<CLayer.Salutation> salutationlist = new List<CLayer.Salutation>();

            DataTable dt = Connection.GetSQLTable("SELECT *,(CASE WHEN (SELECT COUNT(*) FROM user p WHERE p.SalutationId=c.SalutationId)>0 THEN 0 ELSE 1 END ) " +
                     " AS CanDelete FROM salutation c  ORDER BY Title ASC");
            foreach (DataRow dr in dt.Rows)
            {
                salutationlist.Add(new CLayer.Salutation()
                {
                    SalutationId = Connection.ToLong(dr["SalutationId"]),
                    Title = Connection.ToString(dr["Title"]),
                    CanDelete = Connection.ToBoolean(dr["CanDelete"])
                });
            }

            return salutationlist;
        }

        public int Save(CLayer.Salutation salutation)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pSalutationId", DataPlug.DataType._Int, salutation.SalutationId));
            param.Add(Connection.GetParameter("pTitle", DataPlug.DataType._Varchar, salutation.Title));
            int result = Connection.ExecuteQuery("salutation_Save", param);
            return result;
        }

        public void Delete(int SalutationId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pSalutationId", DataPlug.DataType._Int, SalutationId));
            Connection.ExecuteQuery("salutation_Delete", param);
            return;
        }

        public CLayer.Salutation Get(int SalutationId)
        {
            CLayer.Salutation salutation = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pSalutationId", DataPlug.DataType._BigInt, SalutationId));
            DataTable dt = Connection.GetTable("salutation_Get", param);
            if (dt.Rows.Count > 0)
            {
                salutation = new CLayer.Salutation();
                salutation.SalutationId = Connection.ToLong(dt.Rows[0]["SalutationId"]);
                salutation.Title = Connection.ToString(dt.Rows[0]["Title"]);
            }
            return salutation;
        }
    }
}
