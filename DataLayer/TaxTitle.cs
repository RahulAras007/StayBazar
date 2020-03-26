using DataLayer.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class TaxTitle : DataLink
    {
        public List<CLayer.TaxTitle> GetAll()
        {
            List<CLayer.TaxTitle> taxtitlelist = new List<CLayer.TaxTitle>();

            DataTable dt = Connection.GetSQLTable("SELECT *,(CASE WHEN (SELECT COUNT(*) FROM tax t WHERE t.TaxId=c.TaxTitleId)>0 THEN 0 ELSE 1 END ) " +
                     " AS CanDelete FROM taxtitle c  ORDER BY Title ASC");     
            foreach (DataRow dr in dt.Rows)
            {
                taxtitlelist.Add(new CLayer.TaxTitle()
                {
                    TaxTitleId = Connection.ToInteger(dr["TaxTitleId"]),
                    Title = Connection.ToString(dr["Title"]),                    
                });
            }
            return taxtitlelist;
        }

        public int Save(CLayer.TaxTitle taxTi)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pTaxTitleId", DataPlug.DataType._Int, taxTi.TaxTitleId));
            param.Add(Connection.GetParameter("pTitle", DataPlug.DataType._Varchar, taxTi.Title));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, taxTi.Status));
            int result = Connection.ExecuteQuery("TaxTitle_Save", param);
            return result;
        }

        public void Delete(int TaxTitleId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pTaxTitleId", DataPlug.DataType._BigInt, TaxTitleId));
            Connection.ExecuteQuery("TaxTitle_Delete", param);
            return;
        }

        public CLayer.TaxTitle GetById(int TaxTitleId)
        {
            CLayer.TaxTitle taxTi = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pTaxTitleId", DataPlug.DataType._BigInt, TaxTitleId));
            DataTable dt = Connection.GetTable("TaxTitleById_Get", param);
            if (dt.Rows.Count > 0)
            {
                taxTi = new CLayer.TaxTitle();
                taxTi.TaxTitleId = Connection.ToInteger(dt.Rows[0]["TaxTitleId"]);
                taxTi.Title = Connection.ToString(dt.Rows[0]["Title"]);
                taxTi.Status = Connection.ToInteger(dt.Rows[0]["Status"]);
            }
            return taxTi;
        }
    }
}
