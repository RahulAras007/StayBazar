using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class CityEntitle
    {
        //Savedetails
        public static List<CLayer.CityEntitle> Get(int EmployeeGradesId)
        {
            DataLayer.CityEntitle getbyid = new DataLayer.CityEntitle();
            return getbyid.GetById(EmployeeGradesId);
        }
        public static CLayer.CityEntitleSvaeRslt Savedetails(string[] ClsID, string[] Amount,CLayer.CityEntitle data)
        {
            DataLayer.CityEntitle Save = new DataLayer.CityEntitle();
            return Save.Savedetails(ClsID,  Amount,  data);
        }
    }
}
