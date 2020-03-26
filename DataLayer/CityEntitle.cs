using DataLayer.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer 
{
    public class CityEntitle :  DataLink
    {
        public List<CLayer.CityEntitle> GetById(int EmployeeGradesId)
        {
            List<CLayer.CityEntitle> citye = new List<CLayer.CityEntitle>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>(); 
            param.Add(Connection.GetParameter("pGradeID", DataPlug.DataType._BigInt, EmployeeGradesId));
            DataTable dt = Connection.GetTable("EntitleDetailes_Get", param);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    citye.Add(new CLayer.CityEntitle()
                    {
                        GradeId= Connection.ToInteger(dr["GradeID"]),
                        GradeCode = Connection.ToString(dr["gradeCode"]),
                        Gradedescription = Connection.ToString(dr["GradeDescription"]),
                        CityCLassID = Connection.ToLong(dr["CityCLassID"]),
                        DefaultAmount = Connection.ToLong(dr["DefaultAmount"]),
                        CityCLassAmt = Connection.ToLong(dr["MaximumDailyEnitilement"]),
                        CityCLassName = Connection.ToString(dr["CityclassName"])
                     
                    });
                }
            }
            return citye;
        }
        public CLayer.CityEntitleSvaeRslt Savedetails(string[] ClsID, string[] Amount, CLayer.CityEntitle data)
        {
            CLayer.CityEntitleSvaeRslt rslt = new CLayer.CityEntitleSvaeRslt();
            List<DataPlug.Parameter> param1 = new List<DataPlug.Parameter>();
            param1.Add(Connection.GetParameter("pDefaultAmt", DataPlug.DataType._Decimal, data.DefaultAmount));
            param1.Add(Connection.GetParameter("pGradeID", DataPlug.DataType._BigInt, data.GradeId));
            Connection.ExecuteQueryScalar("entitlements_Update", param1);

            for (int i=0;i<= ClsID.Length-1;i++)
            {
                List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
                param.Add(Connection.GetParameter("pClsID", DataPlug.DataType._BigInt, ClsID[i]));
                param.Add(Connection.GetParameter("pClsAmt", DataPlug.DataType._Decimal, Amount[i]));
                param.Add(Connection.GetParameter("pGradeID", DataPlug.DataType._BigInt, data.GradeId));
                rslt.Result = Connection.ExecuteQueryScalar("entitlements_Save", param).ToString();
            }

            return rslt;
        }
    }
}
