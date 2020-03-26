using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class EmployeeGrades
    {
        public static List<CLayer.EmployeeGrades> GetAll()
        {
            DataLayer.EmployeeGrades taxtitGet = new DataLayer.EmployeeGrades();
            return taxtitGet.GetAll();
        }

        public static int Save(CLayer.EmployeeGrades t)
        {
            DataLayer.EmployeeGrades taxtitsave = new DataLayer.EmployeeGrades();
       //     t.Validate();
            return taxtitsave.Save(t);
        }

        public static void Delete(int EmployeeGradesId)
        {
            DataLayer.EmployeeGrades taxtitDe = new DataLayer.EmployeeGrades();
            taxtitDe.Delete(EmployeeGradesId);
            return;
        }

        public static CLayer.EmployeeGrades Get(int EmployeeGradesId)
        {
            DataLayer.EmployeeGrades getbyid = new DataLayer.EmployeeGrades();
            return getbyid.GetById(EmployeeGradesId);
        }

    }
}
