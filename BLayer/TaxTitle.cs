using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class TaxTitle
    {
        public static List<CLayer.TaxTitle> GetAll()
        {
            DataLayer.TaxTitle taxtitGet = new DataLayer.TaxTitle();
            return taxtitGet.GetAll();
        }

        public static int Save(CLayer.TaxTitle t)
        {
            DataLayer.TaxTitle taxtitsave = new DataLayer.TaxTitle();
            t.Validate();
            return taxtitsave.Save(t);
        }

        public static void Delete(int TaxTitleId)
        {
            DataLayer.TaxTitle taxtitDe = new DataLayer.TaxTitle();
            taxtitDe.Delete(TaxTitleId);
            return;
        }

        public static CLayer.TaxTitle Get(int TaxTitleId)
        {
            DataLayer.TaxTitle getbyid = new DataLayer.TaxTitle();
            return getbyid.GetById(TaxTitleId);
        }

    }
}
