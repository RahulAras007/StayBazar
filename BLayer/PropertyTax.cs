using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class PropertyTax
    {
         public static decimal GetTotalTax(long propertyId){
             DataLayer.PropertyTax pt = new DataLayer.PropertyTax();
             return pt.GetTotalTax(propertyId);
         }

         public static List<CLayer.Tax> GetPropertyTaxById(long propertyId)
         {
             DataLayer.PropertyTax pt = new DataLayer.PropertyTax();
             return pt.GetPropertyTaxById(propertyId);
         }

    }
}
