using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataLayer.DataProvider;


namespace DataLayer
{
    public class Discount : DataProvider.DataLink
    {

        public CLayer.Discount GetDiscount(long b2bId, long propertyId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pB2bId", DataPlug.DataType._BigInt, b2bId));
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyId));
            DataTable dt = Connection.GetTable("b2b_GetDiscount", param);

            CLayer.Discount result = new CLayer.Discount();
            result.ShortTermRate = 0;
            result.LongTermRate = 0;
            
            if(dt.Rows.Count>0)
            {
                result.ShortTermRate = Connection.ToDouble(dt.Rows[0]["DiscountShort"]);
                result.LongTermRate = Connection.ToDouble(dt.Rows[0]["DiscountLong"]);
                result.BaseLongTerm = Connection.ToDouble(dt.Rows[0]["BaseLong"]);
                result.BaseShortTerm = Connection.ToDouble(dt.Rows[0]["BaseShort"]);
            }
            return result;
        }

        public CLayer.Discount GetStdDiscount(long propertyId)
        {
            string sql = "Select B2BStdShortTermDis,B2BStdLongTermDis From property Where PropertyId =" + propertyId.ToString();
            DataTable dt = Connection.GetSQLTable(sql);
            CLayer.Discount dis = null;
            if(dt.Rows.Count > 0 )
            {
                dis = new CLayer.Discount();
                dis.PropertyId = propertyId;
                dis.LongTermRate = Connection.ToDouble(dt.Rows[0]["B2BStdLongTermDis"]);
                dis.ShortTermRate = Connection.ToDouble(dt.Rows[0]["B2BStdShortTermDis"]);
            }
            return dis;
        }

        public void SaveStdDiscount(CLayer.Discount data)
        {
            StringBuilder sql= new StringBuilder();
            sql.Append("Update property Set B2BStdShortTermDis=");
            sql.Append(data.ShortTermRate);
            sql.Append(",B2BStdLongTermDis=");
            sql.Append(data.LongTermRate);
            sql.Append(" Where PropertyId =");
            sql.Append(data.PropertyId);
            Connection.ExecuteSqlQuery(sql.ToString());
        }

        public void Save(CLayer.Discount data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pB2BId", DataPlug.DataType._BigInt, data.B2BId));
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, data.PropertyId));
            param.Add(Connection.GetParameter("pShortTerm", DataPlug.DataType._Decimal, data.ShortTermRate));
            param.Add(Connection.GetParameter("pLongTerm", DataPlug.DataType._Decimal, data.LongTermRate));
            Connection.ExecuteQuery("discount_Save", param);
        }
        public List<CLayer.Discount> GetAll(long propertyId)
        {
            string sql = "Select b.B2BId,b.Name,d.ShortTerm,d.LongTerm From discount d INNER JOIN  b2b b ON d.B2Bid = b.B2BId Where d.PropertyId=" + propertyId.ToString();
            DataTable dt = Connection.GetSQLTable(sql);
            List<CLayer.Discount> result = new List<CLayer.Discount>();
            CLayer.Discount temp;

            foreach(DataRow dr in dt.Rows)
            {
                temp = new CLayer.Discount();
                temp.B2BId = Connection.ToLong(dr["B2BId"]);
                temp.B2BName = Connection.ToString(dr["Name"]);
                temp.ShortTermRate = Connection.ToDouble(dr["ShortTerm"]);
                temp.LongTermRate = Connection.ToDouble(dr["LongTerm"]);
                temp.PropertyId = propertyId;
                result.Add(temp);
            }

            return result;
        }

        public CLayer.Discount Get(long b2bId,long propertyId)
        {
            string sql = "Select b.B2BId,b.Name,d.ShortTerm,d.LongTerm From discount d INNER JOIN  b2b b ON d.B2Bid = b.B2BId Where d.PropertyId=" + propertyId.ToString() + " And d.B2BId=" + b2bId.ToString();
            DataTable dt = Connection.GetSQLTable(sql);
            CLayer.Discount  result = null;

            if(dt.Rows.Count > 0)
            {
                result = new CLayer.Discount();
                result.B2BId = Connection.ToLong(dt.Rows[0]["B2BId"]);
                result.B2BName = Connection.ToString(dt.Rows[0]["Name"]);
                result.ShortTermRate = Connection.ToDouble(dt.Rows[0]["ShortTerm"]);
                result.LongTermRate = Connection.ToDouble(dt.Rows[0]["LongTerm"]);
                result.PropertyId = propertyId;
            }
            return result;
        }

        public void Delete(long b2bId,long propertyId)
        {
            string sql = "Delete From discount Where B2BId=" + b2bId.ToString() + " And PropertyId=" + propertyId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }
    }
}
