using DataLayer.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class APIPriceMarkup : DataLink
    {
        
        public List<CLayer.APIPriceMarkup> GetAPIPriceMarkup(string SearchString, int start, int limit)
        {
            List<CLayer.APIPriceMarkup> API = new List<CLayer.APIPriceMarkup>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, SearchString));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            DataSet ds = Connection.GetDataSet("Sp_APIPriceMarkup_Get", param);
            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    API.Add(new CLayer.APIPriceMarkup()
            //    {
            //        TotalRows = Connection.ToInteger(dr["NumberOfRows"])
            //    });
            //}
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                API.Add(new CLayer.APIPriceMarkup()
                {
                    //APIPriceMarkupCode = Connection.ToInteger(dt.Rows[0]["api_pricemarkup_code"]),
                    //APIDescription = Connection.ToString(dt.Rows[0]["api_description"]),
                    //CustomerId = Connection.ToInteger(dt.Rows[0]["customer_id"]),
                    //SellMarkup = Connection.ToDecimal(dt.Rows[0]["sell_markup_inPercentage"])

                    APIPriceMarkupCode = Connection.ToInteger(dr["api_pricemarkup_code"]),
                    DescriptionId = Connection.ToInteger(dr["api_code"]),
                    CustomerId = Connection.ToInteger(dr["customer_id"]),
                    SellMarkup = Connection.ToString(dr["sell_markup_inPercentage"]),
                    CustomerName = Connection.ToString(dr["Name"]),
                    TotalRows = Connection.ToInteger(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return API;
        }

        //Added by rahul for Listing New API Markup price for new Screen on 30-01-2020
        public List<CLayer.APIPriceMarkup> GetNewAPIPriceMarkUp()
        {
            List<CLayer.APIPriceMarkup> API = new List<CLayer.APIPriceMarkup>();
            DataSet ds = Connection.GetDataSet("Sp_NewAPIPriceMarkup_Get");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                API.Add(new CLayer.APIPriceMarkup()
                {
                    APIPriceMarkupCode = Connection.ToInteger(dr["pricemarkup_code"]),
                    DescriptionId = Connection.ToInteger(dr["description"]),
                    SellMarkup = Connection.ToString(dr["markup_per"])
                });
            }
            return API;
        }
        //-----END------
        public CLayer.APIPriceMarkup Get(long id)
        {
            CLayer.APIPriceMarkup user = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pID", DataPlug.DataType._BigInt, id));
            DataTable dt = Connection.GetTable("sp_api_pricemarkup_get", param);
            if (dt.Rows.Count > 0)
            {
                user = new CLayer.APIPriceMarkup();
                user.APIPriceMarkupCode = Connection.ToLong(dt.Rows[0]["api_pricemarkup_code"]);
                user.DescriptionId = Connection.ToInteger(dt.Rows[0]["api_code"]);
                user.CustomerId = Connection.ToLong(dt.Rows[0]["customer_id"]);
                user.SellMarkup = Connection.ToString(dt.Rows[0]["sell_markup_inPercentage"]);
                user.CustomerName = Connection.ToString(dt.Rows[0]["Name"]);
            }
            return user;
        }
        //Added by rahul for Listing New API Markup price in edit for new Screen on 30-01-2020
        public CLayer.APIPriceMarkup GetNewAPIDetails(long id)
        {
            CLayer.APIPriceMarkup user = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pID", DataPlug.DataType._BigInt, id));
            DataTable dt = Connection.GetTable("sp_new_api_pricemarkup_get", param);
            if (dt.Rows.Count > 0)
            {
                user = new CLayer.APIPriceMarkup();
                user.APIPriceMarkupCode = Connection.ToLong(dt.Rows[0]["pricemarkup_code"]);
                user.DescriptionId = Connection.ToInteger(dt.Rows[0]["description"]);
                user.SellMarkup = Connection.ToString(dt.Rows[0]["markup_per"]);
            }
            return user;
        }
        //----END-----
        public int Save(CLayer.APIPriceMarkup API)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCode", DataPlug.DataType._Varchar, API.APIPriceMarkupCode));
            param.Add(Connection.GetParameter("pDescription", DataPlug.DataType._Int, API.DescriptionId));
            param.Add(Connection.GetParameter("pCustomerID", DataPlug.DataType._Varchar, API.CustomerId));
            param.Add(Connection.GetParameter("pSellMarkup", DataPlug.DataType._Varchar, API.SellMarkup));
            int result = Connection.ExecuteQuery("sp_api_pricemarkup_save", param);
            return result;
        }
        //Added by rahul for Saving New API Markup price in edit for new Screen on 30-01-2020
        public int SaveNewAPIMarkup(CLayer.APIPriceMarkup API)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCode", DataPlug.DataType._Varchar, API.APIPriceMarkupCode));
            param.Add(Connection.GetParameter("pSellMarkup", DataPlug.DataType._Varchar, API.SellMarkup));
            int result = Connection.ExecuteQuery("sp_new_api_pricemarkup_save", param);
            return result;
        }
        //---END--
        public void Delete(long Id)
        {
            Connection.ExecuteSqlQuery("Delete from `users_api_pricemarkup` WHERE api_pricemarkup_code =" + Id.ToString());
            return;
        }
        public List<CLayer.APIOfflineCustomer> GetAlCustomerList()
        {
            List<CLayer.APIOfflineCustomer> bookings = new List<CLayer.APIOfflineCustomer>();
            DataSet ds = Connection.GetDataSet("sp_offlinecustomerlist_for_APIPriceMarkup");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                bookings.Add(new CLayer.APIOfflineCustomer()
                {
                    CustomerId = Connection.ToLong(dr["CustomerId"]),
                    CustomerName = Connection.ToString(dr["CustomerName"])
                });
            }
            return bookings;
        }

        public decimal GSTRate(string HSNCode, int price)
        {
            string sql = "select gst_igst_rate from mac_gst_slabs where gst_sac_code ='" + HSNCode + "' and (gst_slab_from <=" + price + " and gst_slab_to >=" + price+")";
            return Connection.ToDecimal(Connection.ExecuteSQLQueryScalar(sql));
        }
    }
}
