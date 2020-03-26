using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class Salutation
    {
        public static List<CLayer.Salutation> GetAll()
        {
            DataLayer.Salutation salutationGA = new DataLayer.Salutation();
            return salutationGA.GetAll();
        }

        public static int Save(CLayer.Salutation salutation)
        {
            DataLayer.Salutation salutationS = new DataLayer.Salutation();
            salutation.Validate();
            return salutationS.Save(salutation);
        }

        public static void Delete(int SalutationId)
        {
            DataLayer.Salutation salutation = new DataLayer.Salutation();
            salutation.Delete(SalutationId);
            return;
        }

        public static CLayer.Salutation Get(int SalutationId)
        {
            DataLayer.Salutation salutation = new DataLayer.Salutation();
            return salutation.Get(SalutationId);
        }
    }
}
