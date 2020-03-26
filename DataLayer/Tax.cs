using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
    public class Tax : DataLink
    {

        public List<CLayer.BookingItemTax> GetAllByBookingItem(long bookingItemId)
        {
            string sql = "SELECT tt.Title,bt.Rate,bt.Amount,bt.OnGrandTotal FROM bookingitem_tax bt INNER JOIN tax t ON bt.TaxId = t.TaxId ";
            sql = sql + " INNER JOIN taxtitle tt ON t.TaxTitleId = tt.TaxTitleId Where bt.BookingItemId=" + bookingItemId.ToString();
            DataTable dt = Connection.GetSQLTable(sql);
            List<CLayer.BookingItemTax> result = new List<CLayer.BookingItemTax>();
            CLayer.BookingItemTax temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.BookingItemTax();
                temp.Title = Connection.ToString(dr["Title"]);
                temp.OnGrandTotal = Connection.ToBoolean(dr["OnGrandTotal"]);
                temp.Amount = Connection.ToDouble(dr["Amount"]);
                temp.Rate = Connection.ToDouble(dr["Rate"]);
                result.Add(temp);
            }
            return result;
        }
        public void AddTaxForBookingItem(CLayer.BookingItemTax data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingItemId", DataPlug.DataType._BigInt, data.BookingItemId));
            param.Add(Connection.GetParameter("pTaxId", DataPlug.DataType._BigInt, data.TaxId));
            param.Add(Connection.GetParameter("pOnGrandTotal", DataPlug.DataType._Bool, data.OnGrandTotal));
            param.Add(Connection.GetParameter("pRate", DataPlug.DataType._Decimal, data.Rate));
            param.Add(Connection.GetParameter("pAmount", DataPlug.DataType._Decimal, data.Amount));

            Connection.ExecuteQuery("BookingItemTax_AddTax", param);
        }
        public List<CLayer.Tax> GetAllForProperty(long propertyId, DateTime checkIn, DateTime bookingDate)
        {
            List<CLayer.Tax> result = new List<CLayer.Tax>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyId));
            param.Add(Connection.GetParameter("pCheckIn", DataPlug.DataType._Date, checkIn));
            param.Add(Connection.GetParameter("pBooking", DataPlug.DataType._Date, bookingDate));
            DataTable dt = Connection.GetTable("tax_GetAllByProperty", param);
            foreach (DataRow dr in dt.Rows)
            {

                result.Add(new CLayer.Tax()
                {
                    TaxId = Connection.ToInteger(dr["TaxId"]),
                    TaxTitleId = Connection.ToInteger(dr["TaxTitleId"]),
                    Rate = Connection.ToDecimal(dr["Rate"]),
                    CountryId = Connection.ToInteger(dr["CountryId"]),
                    //  Country = Connection.ToString(dr["CountryName"]),
                    StateId = Connection.ToInteger(dr["StateId"]),
                    //  State = Connection.ToString(dr["StateName"]),
                    CityId = Connection.ToInteger(dr["CityId"]),
                    //  City = Connection.ToString(dr["CityName"]),
                    Notes = Connection.ToString(dr["Notes"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    StartDate = Connection.ToDate(dr["StartDate"]),
                    EndDate = Connection.ToDate(dr["EndDate"]),
                    OnDate = Connection.ToInteger(dr["OnDate"]),
                    OnTotalAmount = Connection.ToBoolean(dr["OnTotalAmount"]),
                    PriceUpto = Connection.ToDecimal(dr["PriceUpto"]),
                    Unlimited = Connection.ToBoolean(dr["UnlimitedPrice"])
                });
            }
            return result;

        }

        public List<CLayer.Tax> GetAllTypeTaxRate(long BookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            List<CLayer.Tax> result = new List<CLayer.Tax>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, BookingId));
            DataTable dt = Connection.GetTable("ratesbooking_GetAllTypeTaxRate", param);
            CLayer.Tax rt1;
            foreach (DataRow dr in dt.Rows)
            {
                rt1 = new CLayer.Tax();
                rt1.TaxTitleId = Connection.ToInteger(dr["TaxTitleId"]);
                rt1.Title = Connection.ToString(dr["TaxTitleName"]);
                rt1.BookingId = BookingId;
                rt1.Rate = Connection.ToDecimal(dr["Rate"]);
                rt1.TotalTaxAmount = Connection.ToDecimal(dr["Amount"]);
                result.Add(rt1);
            }
            return result;
        }

        public void Delete(int taxId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pTaxId", DataPlug.DataType._Int, taxId));
            Connection.ExecuteQueryScalar("tax_Delete", param);
            return;
        }

        public void DeletePropertyTax(long Id, long Tid)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._Int, Id));
            param.Add(Connection.GetParameter("pTaxTitleId", DataPlug.DataType._Int, Tid));
            Connection.ExecuteQueryScalar("propertytax_Delete", param);
            return;
        }
        public CLayer.Tax Get(int taxId)
        {
            CLayer.Tax result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pTaxId", DataPlug.DataType._Int, taxId));
            DataTable dt = Connection.GetTable("tax_Get", param);
            if (dt.Rows.Count > 0)
            {
                result = new CLayer.Tax()
                {
                    TaxId = Connection.ToInteger(dt.Rows[0]["TaxId"]),
                    Title = Connection.ToString(dt.Rows[0]["Title"]),
                    TaxTitleId = Connection.ToInteger(dt.Rows[0]["TaxTitleId"]),
                    Rate = Connection.ToDecimal(dt.Rows[0]["Rate"]),

                    CountryId = Connection.ToInteger(dt.Rows[0]["CountryId"]),
                    Country = Connection.ToString(dt.Rows[0]["CountryName"]),

                    StateId = Connection.ToInteger(dt.Rows[0]["StateId"]),
                    State = Connection.ToString(dt.Rows[0]["StateName"]),
                    CityId = Connection.ToInteger(dt.Rows[0]["CityId"]),
                    City = Connection.ToString(dt.Rows[0]["CityName"]),

                    Notes = Connection.ToString(dt.Rows[0]["Notes"]),
                    Status = Connection.ToInteger(dt.Rows[0]["Status"]),
                    StartDate = Connection.ToDate(dt.Rows[0]["StartDate"]),
                    EndDate = Connection.ToDate(dt.Rows[0]["EndDate"]),
                    OnDate = Connection.ToInteger(dt.Rows[0]["OnDate"]),
                    OnTotalAmount = Connection.ToBoolean(dt.Rows[0]["OnTotalAmount"]),
                    PriceUpto = Connection.ToDecimal(dt.Rows[0]["PriceUpto"]),
                    UpdatedBy = Connection.ToLong(dt.Rows[0]["UpdatedBy"]),
                    UpdatedDate = Connection.ToDate(dt.Rows[0]["UpdatedDate"]),
                    Unlimited = Connection.ToBoolean(dt.Rows[0]["UnlimitedPrice"])
                };
            }
            return result;
        }
        public List<CLayer.Tax> GetAll()
        {
            List<CLayer.Tax> result = new List<CLayer.Tax>();
            DataTable dt = Connection.GetTable("tax_GetAll", null);
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.Tax()
                {
                    TaxId = Connection.ToInteger(dr["TaxId"]),
                    Title = Connection.ToString(dr["Title"]),
                    TaxTitleId = Connection.ToInteger(dr["TaxTitleId"]),
                    Rate = Connection.ToDecimal(dr["Rate"]),
                    CountryId = Connection.ToInteger(dr["CountryId"]),
                    Country = Connection.ToString(dr["CountryName"]),
                    StateId = Connection.ToInteger(dr["StateId"]),
                    State = Connection.ToString(dr["StateName"]),
                    CityId = Connection.ToInteger(dr["CityId"]),
                    City = Connection.ToString(dr["CityName"]),
                    Notes = Connection.ToString(dr["Notes"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    StartDate = Connection.ToDate(dr["StartDate"]),
                    EndDate = Connection.ToDate(dr["EndDate"]),
                    OnDate = Connection.ToInteger(dr["OnDate"]),
                    OnTotalAmount = Connection.ToBoolean(dr["OnTotalAmount"]),
                    PriceUpto = Connection.ToDecimal(dr["PriceUpto"]),
                    UpdatedBy = Connection.ToLong(dr["UpdatedBy"]),
                    UpdatedDate = Connection.ToDate(dr["UpdatedDate"]),
                    Unlimited = Connection.ToBoolean(dr["UnlimitedPrice"])
                });
            }
            return result;
        }
        public List<CLayer.Tax> GetAllByTaxtTitleId(int taxtitleId)
        {
            List<CLayer.Tax> result = new List<CLayer.Tax>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pTaxTitleId", DataPlug.DataType._Int, taxtitleId));
            DataTable dt = Connection.GetTable("tax_GetAllByTaxTitleId", param);
            foreach (DataRow dr in dt.Rows)
            {

                result.Add(new CLayer.Tax()
                  {
                      TaxId = Connection.ToInteger(dr["TaxId"]),
                      Title = Connection.ToString(dr["Title"]),
                      TaxTitleId = Connection.ToInteger(dr["TaxTitleId"]),
                      Rate = Connection.ToDecimal(dr["Rate"]),
                      CountryId = Connection.ToInteger(dr["CountryId"]),
                      Country = Connection.ToString(dr["CountryName"]),
                      StateId = Connection.ToInteger(dr["StateId"]),
                      State = Connection.ToString(dr["StateName"]),
                      CityId = Connection.ToInteger(dr["CityId"]),
                      City = Connection.ToString(dr["CityName"]),
                      Notes = Connection.ToString(dr["Notes"]),
                      Status = Connection.ToInteger(dr["Status"]),
                      StartDate = Connection.ToDate(dr["StartDate"]),
                      EndDate = Connection.ToDate(dr["EndDate"]),
                      OnDate = Connection.ToInteger(dr["OnDate"]),
                      OnTotalAmount = Connection.ToBoolean(dr["OnTotalAmount"]),
                      PriceUpto = Connection.ToDecimal(dr["PriceUpto"]),
                      UpdatedBy = Connection.ToLong(dr["UpdatedBy"]),
                      UpdatedDate = Connection.ToDate(dr["UpdatedDate"]),
                      Unlimited = Connection.ToBoolean(dr["UnlimitedPrice"])
                  });
            }
            return result;
        }
        public List<CLayer.Tax> GetNonStandardOnly()
        {
            List<CLayer.Tax> result = new List<CLayer.Tax>();
            DataTable dt = Connection.GetTable("tax_GetNonStandardOnly", null);
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.Tax()
                {
                    TaxId = Connection.ToInteger(dt.Rows[0]["TaxId"]),
                    Title = Connection.ToString(dt.Rows[0]["Title"]),
                    Rate = Connection.ToDecimal(dt.Rows[0]["Title"]),
                    CountryId = Connection.ToInteger(dt.Rows[0]["CountryId"]),
                    StateId = Connection.ToInteger(dt.Rows[0]["StateId"]),
                    Description = Connection.ToString(dt.Rows[0]["Description"]),
                    Notes = Connection.ToString(dt.Rows[0]["Notes"]),
                    IsStandard = Connection.ToBoolean(dt.Rows[0]["IsStandard"]),
                    Status = Connection.ToInteger(dt.Rows[0]["Status"])
                });
            }
            return result;
        }
        public List<CLayer.Tax> GetOnStatus(CLayer.ObjectStatus.StatusType status)
        {
            List<CLayer.Tax> result = new List<CLayer.Tax>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, (int)status));
            DataTable dt = Connection.GetTable("tax_GetOnStatus", null);
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.Tax()
                {
                    TaxId = Connection.ToInteger(dt.Rows[0]["TaxId"]),
                    Title = Connection.ToString(dt.Rows[0]["Title"]),
                    Rate = Connection.ToDecimal(dt.Rows[0]["Title"]),
                    CountryId = Connection.ToInteger(dt.Rows[0]["CountryId"]),
                    StateId = Connection.ToInteger(dt.Rows[0]["StateId"]),
                    Description = Connection.ToString(dt.Rows[0]["Description"]),
                    Notes = Connection.ToString(dt.Rows[0]["Notes"]),
                    IsStandard = Connection.ToBoolean(dt.Rows[0]["IsStandard"]),
                    Status = Connection.ToInteger(dt.Rows[0]["Status"])
                });
            }
            return result;
        }
        public List<CLayer.Tax> GetStandardOnly()
        {
            List<CLayer.Tax> result = new List<CLayer.Tax>();
            DataTable dt = Connection.GetTable("tax_GetStandardOnly", null);
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.Tax()
                {
                    TaxId = Connection.ToInteger(dt.Rows[0]["TaxId"]),
                    Title = Connection.ToString(dt.Rows[0]["Title"]),
                    Rate = Connection.ToDecimal(dt.Rows[0]["Title"]),
                    CountryId = Connection.ToInteger(dt.Rows[0]["CountryId"]),
                    StateId = Connection.ToInteger(dt.Rows[0]["StateId"]),
                    Description = Connection.ToString(dt.Rows[0]["Description"]),
                    Notes = Connection.ToString(dt.Rows[0]["Notes"]),
                    IsStandard = Connection.ToBoolean(dt.Rows[0]["IsStandard"]),
                    Status = Connection.ToInteger(dt.Rows[0]["Status"])
                });
            }
            return result;
        }
        public List<CLayer.Tax> GetPropertyTax()
        {
            List<CLayer.Tax> PropertyTax = new List<CLayer.Tax>();
            DataSet ds = Connection.GetDataSet("propertyTax_AllList");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                PropertyTax.Add(new CLayer.Tax()
                {
                    Title = Connection.ToString(dr["TaxName"]),
                    PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                    Rate = Connection.ToDecimal(dr["TaxValue"]),
                    UpdatedBy = Connection.ToInteger(dr["UpdatedBy"]),
                    UpdatedDate = Connection.ToDate(dr["UpdatedDate"]),
                    PropertyId = Connection.ToInteger(dr["PropertyId"]),

                });
            }
            return PropertyTax;
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
        public int Save(CLayer.Tax data)
        {

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pTaxId", DataPlug.DataType._Int, data.TaxId));
            param.Add(Connection.GetParameter("pTaxTitleId", DataPlug.DataType._Int, data.TaxTitleId));
            param.Add(Connection.GetParameter("pRate", DataPlug.DataType._Decimal, data.Rate));

            param.Add(Connection.GetParameter("pCountryId", DataPlug.DataType._Int, data.CountryId));
            param.Add(Connection.GetParameter("pStateId", DataPlug.DataType._Int, data.StateId));
            param.Add(Connection.GetParameter("pCityId", DataPlug.DataType._Int, data.CityId));

            param.Add(Connection.GetParameter("pNotes", DataPlug.DataType._Varchar, data.Notes));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, data.Status));
            param.Add(Connection.GetParameter("pStartDate", DataPlug.DataType._DateTime, data.StartDate));
            param.Add(Connection.GetParameter("pEndDate", DataPlug.DataType._DateTime, data.EndDate));
            param.Add(Connection.GetParameter("pOnDate", DataPlug.DataType._Int, data.OnDate));
            param.Add(Connection.GetParameter("pOnTotalAmount", DataPlug.DataType._Bool, data.OnTotalAmount));
            param.Add(Connection.GetParameter("pPriceUpto", DataPlug.DataType._Decimal, data.PriceUpto));
            param.Add(Connection.GetParameter("pUpdatedBy", DataPlug.DataType._BigInt, data.UpdatedBy));
            param.Add(Connection.GetParameter("pUpdatedDate", DataPlug.DataType._DateTime, data.UpdatedDate));
            param.Add(Connection.GetParameter("pUnlimitedPrice", DataPlug.DataType._Bool, data.Unlimited));
            object result = Connection.ExecuteQueryScalar("tax_Save", param);
            return Connection.ToInteger(result);

        }

        public int SaveTax(CLayer.Tax data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pTaxTitleId", DataPlug.DataType._Int, data.TaxTitleId));
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._Decimal, data.PropertyId));
            param.Add(Connection.GetParameter("pRate", DataPlug.DataType._Decimal, data.Rate));
            param.Add(Connection.GetParameter("pUpdatedBy", DataPlug.DataType._BigInt, data.UpdatedBy));
            param.Add(Connection.GetParameter("pUpdatedDate", DataPlug.DataType._DateTime, data.UpdatedDate));
            object result = Connection.ExecuteQueryScalar("taxproperty_Save", param);
            return Connection.ToInteger(result);

        }
        public void SetStatus(int taxId, CLayer.ObjectStatus.StatusType status)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pTaxId", DataPlug.DataType._Int, taxId));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, (int)status));
            Connection.ExecuteQueryScalar("tax_SetStatus", param);
            return;
        }
        public void AddAmadeusTaxRates(CLayer.Tax data)
        {
            try
            {
                List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

                param.Add(Connection.GetParameter("pTaxId", DataPlug.DataType._BigInt, data.TaxId));
                param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, data.PropertyId));
                param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, data.AccommodationId));
                param.Add(Connection.GetParameter("pBookingID", DataPlug.DataType._BigInt, data.BookingId));
                param.Add(Connection.GetParameter("pTaxRate", DataPlug.DataType._Double, data.TaxRate));
                param.Add(Connection.GetParameter("pTaxAmount", DataPlug.DataType._BigInt, data.PropertyId));
                param.Add(Connection.GetParameter("pTotalCGSTTaxAmount", DataPlug.DataType._Decimal, data.TotalCGSTTaxAmount));
                param.Add(Connection.GetParameter("pTotalSGSTTaxAmount", DataPlug.DataType._Decimal, data.TotalSGSTTaxAmount));
                param.Add(Connection.GetParameter("pTotalIGSTTaxAmount", DataPlug.DataType._Decimal, data.TotalIGSTTaxAmount));
                param.Add(Connection.GetParameter("pCGSTTitle", DataPlug.DataType._Varchar, data.CGSTTitle));
                param.Add(Connection.GetParameter("pSGSTTitle", DataPlug.DataType._Varchar, data.SGSTTitle));
                param.Add(Connection.GetParameter("pIGSTTitle", DataPlug.DataType._Varchar, data.IGSTTitle));
                param.Add(Connection.GetParameter("pBookingCode", DataPlug.DataType._Varchar, data.BookingCode));
                param.Add(Connection.GetParameter("pCGSTTaxRate", DataPlug.DataType._Double, data.CGSTTaxRate));
                param.Add(Connection.GetParameter("pSGSTTaxRate", DataPlug.DataType._Double, data.SGSTTaxRate));
                param.Add(Connection.GetParameter("pIGSTTaxRate", DataPlug.DataType._Double, data.IGSTTaxRate));
                param.Add(Connection.GetParameter("pBookingType", DataPlug.DataType._Double, data.BookingType));

                Connection.ExecuteQuery("amadeustaxrates_save", param);
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }
        public  long GetAmadeusTaxRates(string BookingCode, long PropertyID,long BookingID=0)
        {
            try
            {
                string sql = "Select TaxId From amadeustaxrates Where BookingID="+ BookingID + " AND BookingCode='"+ BookingCode + "' and PropertyID="+ PropertyID + "";
                object obj = Connection.ExecuteSQLQueryScalar(sql);
                return Connection.ToLong(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public  string GetValue(string key)
        {
            return (new DataLayer.Settings()).GetValue(key).Trim();
        }
        public List<CLayer.Tax> GetAmadeusTaxRates(long PropertyID)
        {
            List<CLayer.Tax> result = new List<CLayer.Tax>();
            try
            {
                string sql = "Select * From amadeustaxrates Where  PropertyID=" + PropertyID + "";
                
                DataTable dt = Connection.GetSQLTable(sql);
               
                CLayer.Tax  temp;
                foreach (DataRow dr in dt.Rows)
                {
                    temp = new CLayer.Tax();
                    temp.TaxId = Connection.ToInteger(dr["TaxId"]);
                    temp.PropertyId = Connection.ToLong(dr["PropertyID"]);
                    temp.BookingCode = Connection.ToString(dr["BookingCode"]);
                    temp.BookingId = Connection.ToLong(dr["BookingId"]);
                    temp.CGSTTitle = Connection.ToString(dr["CGSTTitle"]);
                    temp.SGSTTitle = Connection.ToString(dr["SGSTTitle"]);
                    temp.IGSTTitle = Connection.ToString(dr["IGSTTitle"]);
                    temp.TotalCGSTTaxAmount = Connection.ToDecimal(dr["TotalCGSTTaxAmount"]);
                    temp.TotalSGSTTaxAmount = Connection.ToDecimal(dr["TotalSGSTTaxAmount"]);
                    temp.TotalIGSTTaxAmount = Connection.ToDecimal(dr["TotalIGSTTaxAmount"]);
                    temp.AccommodationId = Connection.ToLong(dr["AccommodationId"]);
                    temp.TaxRate= Connection.ToDouble(dr["TaxRate"]);
                    temp.CGSTTaxRate = Connection.ToDouble(dr["CGSTTaxRate"]);
                    temp.SGSTTaxRate = Connection.ToDouble(dr["SGSTTaxRate"]);
                    temp.IGSTTaxRate = Connection.ToDouble(dr["IGSTTaxRate"]);
                    temp.CGSTTaxRate = (temp.CGSTTaxRate <= 0) ? Convert.ToDouble(GetValue(CLayer.Settings.CGST_TAXRATE)) : temp.CGSTTaxRate;
                    temp.SGSTTaxRate = (temp.SGSTTaxRate <= 0) ? Convert.ToDouble(GetValue(CLayer.Settings.SGST_TAXRATE)) : temp.SGSTTaxRate;
                    temp.IGSTTaxRate = (temp.IGSTTaxRate <= 0) ? Convert.ToDouble(GetValue(CLayer.Settings.IGST_TAXRATE)) : temp.IGSTTaxRate;


                    result.Add(temp);
                }
            }
            catch (Exception ex)
            {
                result = null;
                throw ex;
            }
            return result;
        }
    }
}
