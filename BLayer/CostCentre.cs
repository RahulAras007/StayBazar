using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class CostCentre
    {
        public static List<CLayer.CostCentre> GetAll()
        {
            DataLayer.CostCentre taxtitGet = new DataLayer.CostCentre();
            return taxtitGet.GetAll();
        }
        public static CLayer.CostCentre Get(int CostCentreCode)
        {
            DataLayer.CostCentre getbyid = new DataLayer.CostCentre();
            return getbyid.GetById(CostCentreCode);
        }
        public static int Save(CLayer.CostCentre t)
        {
            DataLayer.CostCentre taxtitsave = new DataLayer.CostCentre();
            //     t.Validate();
            return taxtitsave.Save(t);
        }
    }
}
