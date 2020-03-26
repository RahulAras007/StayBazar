using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
    public class PropertyTax : DataLink
    {
        public decimal GetTotalTax(long propertyId)
        {
            string sql = "Select Sum(TaxValue) as val From propertyTax Where  PropertyId=" + propertyId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToDecimal(obj);
        }

        public List<CLayer.Tax> GetPropertyTaxById(long PropertyId)
        {
            List<CLayer.Tax> PropertyTax = new List<CLayer.Tax>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._Int, PropertyId));
            DataSet dt = Connection.GetDataSet("PropertyTaxById_Get", param);
            foreach (DataRow dr in dt.Tables[0].Rows)
            {
                PropertyTax.Add(new CLayer.Tax()
                {
                    Title = Connection.ToString(dr["TaxName"]),
                    PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                    Rate = Connection.ToDecimal(dr["TaxValue"]),
                    UpdatedBy = Connection.ToInteger(dr["UpdatedBy"]),
                    UpdatedDate = Connection.ToDate(dr["UpdatedDate"]),
                    PropertyId = Connection.ToInteger(dr["PropertyId"]),
                    TaxTitleId = Connection.ToInteger(dr["TaxTitle"]),

                });
            }
            return PropertyTax;
        }
    }
}
