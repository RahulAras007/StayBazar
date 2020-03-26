using DataLayer.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class CostCentre : DataLink
    {
        public List<CLayer.CostCentre> GetAll()
        {
            List<CLayer.CostCentre> CostCentrelist = new List<CLayer.CostCentre>();

            DataTable dt = Connection.GetSQLTable("SELECT * FROM ggn_cost_centre");
            foreach (DataRow dr in dt.Rows)
            {
                CostCentrelist.Add(new CLayer.CostCentre()
                {
                    CostCentreCode = Connection.ToLong(dr["CostCentreCode"]),
                    CostcentreName = Connection.ToString(dr["CostCentreName"]),
                    B2B_ID = Connection.ToLong(dr["B2B_ID"])
                });
            }
            return CostCentrelist;
        }
        public CLayer.CostCentre GetById(int CostCentreCode)
        {
            CLayer.CostCentre taxTi = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCostCentreCode", DataPlug.DataType._BigInt, CostCentreCode));
            DataTable dt = Connection.GetTable("SP_CostcentreById_Get", param);
            if (dt.Rows.Count > 0)
            {
                taxTi = new CLayer.CostCentre();
                taxTi.CostCentreCode = Connection.ToLong(dt.Rows[0]["CostCentreCode"]);
                taxTi.CostcentreName = Connection.ToString(dt.Rows[0]["CostCentreName"]);
                taxTi.B2B_ID = Connection.ToLong(dt.Rows[0]["B2B_ID"]);
            }
            return taxTi;
        }
        public int Save(CLayer.CostCentre taxTi)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCostCentreCode", DataPlug.DataType._Varchar, taxTi.CostCentreCode));
            param.Add(Connection.GetParameter("pCostCentreName", DataPlug.DataType._Varchar, taxTi.CostcentreName));
            param.Add(Connection.GetParameter("pB2BID", DataPlug.DataType._BigInt, taxTi.B2B_ID));
            int result = Connection.ExecuteQuery("SP_CostCentre_Save", param);
            return result;
        }
    }
}
