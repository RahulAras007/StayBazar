using DataLayer.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class EmployeeGrades : DataLink
    {
        public List<CLayer.EmployeeGrades> GetAll()
        {
            List<CLayer.EmployeeGrades> EmployeeGradeslist = new List<CLayer.EmployeeGrades>();

            DataTable dt = Connection.GetSQLTable("SELECT * FROM employee_Grades");     
            foreach (DataRow dr in dt.Rows)
            {
            EmployeeGradeslist.Add(new CLayer.EmployeeGrades()
            {
              GradeID = Connection.ToLong(dr["GradeID"]),
              GradeCode = Connection.ToString(dr["GradeCode"]),
              GradeDescription = Connection.ToString(dr["GradeDescription"]),
              B2B_ID = Connection.ToLong(dr["B2B_ID"])
                    });
            }
            return EmployeeGradeslist;
        }

        public int Save(CLayer.EmployeeGrades taxTi)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pGradeId", DataPlug.DataType._BigInt, taxTi.GradeID));
            param.Add(Connection.GetParameter("pGradeCode", DataPlug.DataType._Varchar, taxTi.GradeCode));
            param.Add(Connection.GetParameter("pGradeDescription", DataPlug.DataType._Varchar , taxTi.GradeDescription));
            param.Add(Connection.GetParameter("pB2BID", DataPlug.DataType._BigInt, taxTi.B2B_ID));
            int result = Connection.ExecuteQuery("EmployeeGrade_Save", param);
            return result;
        }

        public void Delete(int EmployeeGradesId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pGradeId", DataPlug.DataType._BigInt, EmployeeGradesId));
            Connection.ExecuteQuery("EmployeeGrades_Delete", param);
            return;
        }

        public CLayer.EmployeeGrades GetById(int EmployeeGradesId)
        {
            CLayer.EmployeeGrades taxTi = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pGradeID", DataPlug.DataType._BigInt, EmployeeGradesId));
            DataTable dt = Connection.GetTable("EmployeeGradeById_Get", param);
            if (dt.Rows.Count > 0)
            {
                taxTi = new CLayer.EmployeeGrades();
                taxTi.GradeID = Connection.ToInteger(dt.Rows[0]["GradeID"]);
                taxTi.GradeCode = Connection.ToString(dt.Rows[0]["GradeCode"]);
                taxTi.GradeDescription = Connection.ToString(dt.Rows[0]["GradeDescription"]);
            }
            return taxTi;
        }
    }
}
