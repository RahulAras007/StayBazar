using DataLayer.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Offers : DataLink
    {

        public List<CLayer.Offers> GetForPropertyCalc(long propertyId, int activeStatus, DateTime checkIn, DateTime checkOut)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pActiveStatus", DataPlug.DataType._Int, activeStatus));
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyId));
            param.Add(Connection.GetParameter("pCheckIn", DataPlug.DataType._DateTime, checkIn));
            param.Add(Connection.GetParameter("pCheckOut", DataPlug.DataType._DateTime, checkOut));
            DataTable dt = Connection.GetTable("offer_GetPropOfferByDate", param);
            List<CLayer.Offers> result = new List<CLayer.Offers>();
            CLayer.Offers temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Offers();
                temp.OfferId = Connection.ToLong(dr["OfferId"]);
                temp.OfferTitle = Connection.ToString(dr["OfferTitle"]);
                temp.OfferType = Connection.ToInteger(dr["OfferType"]);
                temp.RateType = Connection.ToInteger(dr["RateType"]);
                temp.StartDate = Connection.ToDate(dr["StartDate"]);
                temp.EndDate = Connection.ToDate(dr["EndDate"]);
                temp.FreeDays = Connection.ToInteger(dr["FreeDays"]);
                temp.NoOfDays = Connection.ToInteger(dr["NoOfDays"]);
                temp.Amount = Connection.ToDecimal(dr["Amount"]);
                result.Add(temp);
            }
            return result;
        }

        public List<CLayer.Offers> GetForAccommodationCalc(long accommodationId, int activeStatus, DateTime checkIn, DateTime checkOut)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pActiveStatus", DataPlug.DataType._Int, activeStatus));
            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, accommodationId));
            param.Add(Connection.GetParameter("pCheckIn", DataPlug.DataType._DateTime, checkIn));
            param.Add(Connection.GetParameter("pCheckOut", DataPlug.DataType._DateTime, checkOut));
            DataTable dt = Connection.GetTable("offer_GetAccOfferByDate", param);
            List<CLayer.Offers> result = new List<CLayer.Offers>();
            CLayer.Offers temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Offers();
                temp.OfferId = Connection.ToLong(dr["OfferId"]);
                temp.OfferTitle = Connection.ToString(dr["OfferTitle"]);
                temp.OfferType = Connection.ToInteger(dr["OfferType"]);
                temp.RateType = Connection.ToInteger(dr["RateType"]);
                temp.StartDate = Connection.ToDate(dr["StartDate"]);
                temp.EndDate = Connection.ToDate(dr["EndDate"]);
                temp.FreeDays = Connection.ToInteger(dr["FreeDays"]);
                temp.NoOfDays = Connection.ToInteger(dr["NoOfDays"]);
                temp.Amount = Connection.ToDecimal(dr["Amount"]);
                result.Add(temp);
            }
            return result;
        }

        public List<CLayer.Offers> GetForProperty(long propertyId, int activeStatus)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pActiveStatus", DataPlug.DataType._Int, activeStatus));
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyId));
            DataTable dt = Connection.GetTable("offer_ForProperty", param);
            List<CLayer.Offers> result = new List<CLayer.Offers>();
            CLayer.Offers temp;
            foreach(DataRow dr in dt.Rows)
            {
                temp = new CLayer.Offers();
                temp.OfferId = Connection.ToLong(dr["OfferId"]);
                temp.OfferTitle = Connection.ToString(dr["OfferTitle"]);
                temp.OfferType = Connection.ToInteger(dr["OfferType"]);
                temp.RateType = Connection.ToInteger(dr["RateType"]);
                temp.StartDate = Connection.ToDate(dr["StartDate"]);
                temp.EndDate = Connection.ToDate(dr["EndDate"]);
                temp.FreeDays = Connection.ToInteger(dr["FreeDays"]);
                temp.NoOfDays = Connection.ToInteger(dr["NoOfDays"]);
                temp.Amount = Connection.ToDecimal(dr["Amount"]);
                result.Add(temp);
            }
            return result;
        }
        public List<CLayer.Offers> GetForAccommodation(long accommodationId, int activeStatus)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pActiveStatus", DataPlug.DataType._Int, activeStatus));
            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, accommodationId));
            DataTable dt = Connection.GetTable("offer_ForAccommodation", param);
            List<CLayer.Offers> result = new List<CLayer.Offers>();
            CLayer.Offers temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Offers();
                temp.OfferId = Connection.ToLong(dr["OfferId"]);
                temp.OfferTitle = Connection.ToString(dr["OfferTitle"]);
                temp.OfferType = Connection.ToInteger(dr["OfferType"]);
                temp.RateType = Connection.ToInteger(dr["RateType"]);
                temp.StartDate = Connection.ToDate(dr["StartDate"]);
                temp.EndDate = Connection.ToDate(dr["EndDate"]);
                temp.FreeDays = Connection.ToInteger(dr["FreeDays"]);
                temp.NoOfDays = Connection.ToInteger(dr["NoOfDays"]);
                temp.Amount = Connection.ToDecimal(dr["Amount"]);
                result.Add(temp);
            }
            return result;
        }

        public List<CLayer.Accommodation> PropertyListAccommodationListByOfferId(int Status, long OfferId, int SearchValue, int Start, int Limit)
        {
            if (SearchValue == 1)
            {
                List<CLayer.Accommodation> accommodations = new List<CLayer.Accommodation>();
                List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
                param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, Status));
                param.Add(Connection.GetParameter("pOfferId", DataPlug.DataType._BigInt, OfferId));
                param.Add(Connection.GetParameter("pSearchValue", DataPlug.DataType._Int, SearchValue));
                param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
                param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
                DataSet ds = Connection.GetDataSet("offer_PropertyListAccommodationListByOfferId", param);
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    accommodations.Add(new CLayer.Accommodation()
                    {
                        AccommodationId = Connection.ToLong(dr["AccommodationId"]),
                        PropertyId = Connection.ToLong(dr["PropertyId"]),
                        PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                        Status = Connection.ToInteger(dr["Status"]),
                        StayCategory = Connection.ToString(dr["StayCategory"]),
                        Location = Connection.ToString(dr["Location"]),
                        AccommodationType = Connection.ToString(dr["AccommodationType"]),
                        MaxNoOfPeople = Connection.ToInteger(dr["MaxNoOfPeople"]),
                        MaxNoOfChildren = Connection.ToInteger(dr["MaxNoOfChildren"]),
                        MinNoOfPeople = Connection.ToInteger(dr["MinNoOfPeople"]),
                        BedRooms = Connection.ToInteger(dr["BedRooms"]),
                        Description = Connection.ToString(dr["Description"]),
                        Area = Connection.ToDecimal(dr["Area"]),
                        Address = Connection.ToString(dr["Address"]),
                        Statename = Connection.ToString(dr["Statename"]),
                        City = Connection.ToString(dr["City"]),
                        Ownername = Connection.ToString(dr["Ownername"]),
                        TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                    });
                }
                return accommodations;
            }
            else
            {
                List<CLayer.Accommodation> accommodations = new List<CLayer.Accommodation>();
                List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
                param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, Status));
                param.Add(Connection.GetParameter("pOfferId", DataPlug.DataType._BigInt, OfferId));
                param.Add(Connection.GetParameter("pSearchValue", DataPlug.DataType._Int, SearchValue));
                param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
                param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
                DataSet ds = Connection.GetDataSet("offer_PropertyListAccommodationListByOfferId", param);
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    accommodations.Add(new CLayer.Accommodation()
                    {

                        PropertyId = Connection.ToLong(dr["PropertyId"]),
                        PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                 
                        Location = Connection.ToString(dr["Location"]),
                    
                        Address = Connection.ToString(dr["Address"]),
                        Statename = Connection.ToString(dr["Statename"]),
                        City = Connection.ToString(dr["City"]),
                        Ownername = Connection.ToString(dr["Ownername"]),
                        TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                    });
                }
                return accommodations;

            }
        }

        public List<CLayer.Accommodation> GetProperties(int Status,string SearchString, int SearchValue, int Start, int Limit)
        {
            List<CLayer.Accommodation> accommodations = new List<CLayer.Accommodation>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, Status));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, SearchString));
            param.Add(Connection.GetParameter("pSearchValue", DataPlug.DataType._Int, SearchValue));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            DataSet ds = Connection.GetDataSet("offer_PropertyAllList", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                accommodations.Add(new CLayer.Accommodation()
                {
                   
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    PropertyTitle=Connection.ToString(dr["PropertyTitle"]),
                  //  Status = Connection.ToInteger(dr["Status"]),
                    Location = Connection.ToString(dr["Location"]),
                    Address = Connection.ToString(dr["Address"]),                                   
                    Statename = Connection.ToString(dr["Statename"]),
                    City = Connection.ToString(dr["City"]),
                    Ownername = Connection.ToString(dr["Ownername"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return accommodations;
        }

        public List<CLayer.Accommodation> GetAccommodation(int Status, int StayCategoryId, string SearchString, int SearchValue, int Start, int Limit)
        {
            List<CLayer.Accommodation> accommodations = new List<CLayer.Accommodation>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, Status));
            param.Add(Connection.GetParameter("pStayCategoryId", DataPlug.DataType._Int, StayCategoryId));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, SearchString));
            param.Add(Connection.GetParameter("pSearchValue", DataPlug.DataType._Int, SearchValue));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            DataSet ds = Connection.GetDataSet("offer_AccommodationAllList", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                accommodations.Add(new CLayer.Accommodation()
                {
                    AccommodationId = Connection.ToLong(dr["AccommodationId"]),
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    PropertyTitle=Connection.ToString(dr["PropertyTitle"]),
                    Status = Connection.ToInteger(dr["Status"]),                              
                    StayCategory = Connection.ToString(dr["StayCategory"]),                  
                    Location = Connection.ToString(dr["Location"]),
                    AccommodationType = Connection.ToString(dr["AccommodationType"]),
                    MaxNoOfPeople = Connection.ToInteger(dr["MaxNoOfPeople"]),
                    MaxNoOfChildren = Connection.ToInteger(dr["MaxNoOfChildren"]),
                    MinNoOfPeople = Connection.ToInteger(dr["MinNoOfPeople"]),
                    BedRooms = Connection.ToInteger(dr["BedRooms"]),
                    Description = Connection.ToString(dr["Description"]),
                    Area=Connection.ToDecimal(dr["Area"]),
                    Statename = Connection.ToString(dr["Statename"]),
                    City = Connection.ToString(dr["City"]),
                    Ownername = Connection.ToString(dr["Ownername"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return accommodations;
        }

        public long Save(CLayer.Offers data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfferId", DataPlug.DataType._BigInt, data.OfferId));
            param.Add(Connection.GetParameter("pNoOfDays", DataPlug.DataType._Int, data.NoOfDays));
            param.Add(Connection.GetParameter("pStartDate", DataPlug.DataType._DateTime, data.StartDate));
            param.Add(Connection.GetParameter("pEndDate", DataPlug.DataType._DateTime, data.EndDate));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, data.Status));
            param.Add(Connection.GetParameter("pAmount", DataPlug.DataType._Double, data.Amount));
            param.Add(Connection.GetParameter("pOfferFor", DataPlug.DataType._Int, data.OfferFor));
            param.Add(Connection.GetParameter("pRateType", DataPlug.DataType._Int, data.RateType));
            param.Add(Connection.GetParameter("pSBCommission", DataPlug.DataType._Decimal, data.SBCommission));
            param.Add(Connection.GetParameter("pOfferTitle", DataPlug.DataType._Varchar, data.OfferTitle));
            param.Add(Connection.GetParameter("pFreeDays", DataPlug.DataType._Int, data.FreeDays));
            param.Add(Connection.GetParameter("pOfferType", DataPlug.DataType._Int, data.OfferType));
            param.Add(Connection.GetParameter("pStayCategoryId", DataPlug.DataType._Int, data.StayCategoryId));
            object result = Connection.ExecuteQueryScalar("Offer_Save", param);
            return Connection.ToLong(result);
        }
        public long SaveAccommodationProperty(CLayer.Offers data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfferId", DataPlug.DataType._BigInt, data.OfferId));
            param.Add(Connection.GetParameter("pOfferType", DataPlug.DataType._Int, data.OfferType));
            param.Add(Connection.GetParameter("pIds", DataPlug.DataType._Varchar, data.Ids));
            object result = Connection.ExecuteQueryScalar("offer_AccommodationPropertySave", param);
            return Connection.ToLong(result);
        }
        public CLayer.Offers   GetByOfferId(long OfferId, int Status)
        {
            CLayer.Offers Offerob = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfferId", DataPlug.DataType._BigInt, OfferId));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._BigInt, Status));    
            DataTable dt = Connection.GetTable("Offer_GetById", param);

            if (dt.Rows.Count > 0)
            {
                    Offerob = new CLayer.Offers();
                    Offerob.OfferId = OfferId;
                    Offerob.OfferTitle = Connection.ToString(dt.Rows[0]["OfferTitle"]);
                    Offerob.OfferId = Connection.ToLong(dt.Rows[0]["OfferId"]);
                    Offerob.NoOfDays = Connection.ToInteger(dt.Rows[0]["NoOfDays"]);
                    Offerob.StartDate = Connection.ToDate(dt.Rows[0]["StartDate"]);
                    Offerob.EndDate = Connection.ToDate(dt.Rows[0]["EndDate"]);
                    Offerob.Status = Connection.ToInteger(dt.Rows[0]["Status"]);
                    //AccommodationId = Connection.ToLong(dt.Rows[0]["AccommodationId"]);
                    Offerob.Amount = Connection.ToDecimal(dt.Rows[0]["Amount"]);
                    Offerob.OfferFor = Connection.ToInteger(dt.Rows[0]["OfferFor"]);
                    Offerob.RateType = Connection.ToInteger(dt.Rows[0]["RateType"]);
                    Offerob.SBCommission = Connection.ToDecimal(dt.Rows[0]["SBCommission"]);
                    Offerob.OfferTitle = Connection.ToString(dt.Rows[0]["OfferTitle"]);
                    Offerob. FreeDays = Connection.ToInteger(dt.Rows[0]["FreeDays"]);
                    //PropertyId = Connection.ToLong(dt.Rows[0]["PropertyId"]);
                    Offerob. OfferType = Connection.ToInteger(dt.Rows[0]["OfferType"]);
                    Offerob.StayCategoryId = Connection.ToInteger(dt.Rows[0]["StayCategoryId"]);
                    //Display
                    Offerob.Title = Connection.ToString(dt.Rows[0]["Title"]);
                    Offerob.Location = Connection.ToString(dt.Rows[0]["Location"]);
                    Offerob.Address = Connection.ToString(dt.Rows[0]["Address"]);
               
            }
            return Offerob;
        }      
        //OfferList for Admin
        public List<CLayer.Offers> GetAllBySatus(CLayer.ObjectStatus.StatusType Status,int Start,int Limit)
        {

            List<CLayer.Offers> offer = new List<CLayer.Offers>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._BigInt, Status));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            DataSet ds = Connection.GetDataSet("offer_GetByStatusAll", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                offer.Add(new CLayer.Offers()
                {
                    OfferId = Connection.ToLong(dr["OfferId"]),
                    NoOfDays = Connection.ToInteger(dr["NoOfDays"]),
                    StartDate = Connection.ToDate(dr["StartDate"]),
                    EndDate = Connection.ToDate(dr["EndDate"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    AccommodationId = Connection.ToLong(dr["AccommodationId"]),
                    Amount = Connection.ToDecimal(dr["Amount"]),
                    OfferFor = Connection.ToInteger(dr["OfferFor"]),
                    RateType = Connection.ToInteger(dr["RateType"]),
                    SBCommission = Connection.ToDecimal(dr["SBCommission"]),
                    OfferTitle = Connection.ToString(dr["OfferTitle"]),
                    FreeDays = Connection.ToInteger(dr["FreeDays"]),
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    OfferType = Connection.ToInteger(dr["OfferType"]),
                    StayCategoryId = Connection.ToInteger(dr["StayCategoryId"]),
                    //Display
                    Title = Connection.ToString(dr["Title"]),
                    Location = Connection.ToString(dr["Location"]),
                    Address = Connection.ToString(dr["Address"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return offer;

        }
        //OfferList Display Expired Date  and Active Date with Tab value
        public List<CLayer.Offers> GetAllExpiredandActive(int Status,string SearchString,int Tab,int Start,int Limit)
        {
            //`Offer_GetExpiredAndActive`(IN pStatus INT ,IN pType INT, pStart INT, pLimit INT)
                List<CLayer.Offers> offer = new List<CLayer.Offers>();
                List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();              
                param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._BigInt, Status));
                param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, SearchString));
                param.Add(Connection.GetParameter("pType", DataPlug.DataType._BigInt, Tab));             
                param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
                param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
                DataSet ds = Connection.GetDataSet("Offer_GetExpiredAndActive", param);
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    offer.Add(new CLayer.Offers()
                    {
                        OfferId = Connection.ToLong(dr["OfferId"]),
                        NoOfDays = Connection.ToInteger(dr["NoOfDays"]),
                        StartDate = Connection.ToDate(dr["StartDate"]),
                        EndDate = Connection.ToDate(dr["EndDate"]),
                        Status = Connection.ToInteger(dr["Status"]),
                        //AccommodationId = Connection.ToLong(dr["AccommodationId"]),
                        Amount = Connection.ToDecimal(dr["Amount"]),
                        OfferFor = Connection.ToInteger(dr["OfferFor"]),
                        RateType = Connection.ToInteger(dr["RateType"]),
                        SBCommission = Connection.ToDecimal(dr["SBCommission"]),
                        OfferTitle = Connection.ToString(dr["OfferTitle"]),
                        FreeDays = Connection.ToInteger(dr["FreeDays"]),
                       // PropertyId = Connection.ToLong(dr["PropertyId"]),
                        OfferType = Connection.ToInteger(dr["OfferType"]),
                        StayCategoryId = Connection.ToInteger(dr["StayCategoryId"]),
                        //Display
                        //Title = Connection.ToString(dr["Title"]),
                        //Location = Connection.ToString(dr["Location"]),
                       // Address = Connection.ToString(dr["Address"]),
                        TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                    });
                }
                return offer;
            
        }     
        //offerList Delete 
        public void DeleteOffer(long offerId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfferId", DataPlug.DataType._BigInt, offerId));
            Connection.ExecuteQueryScalar("offers_Delete", param);
            return;
        }
        //offerList Update Status 
        public int EditStatusChange(CLayer.Offers data)
        {

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfferId", DataPlug.DataType._Int, data.OfferId));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, data.Status));
            param.Add(Connection.GetParameter("pStatusActive", DataPlug.DataType._Int, (int)CLayer.ObjectStatus.StatusType.Active));
            param.Add(Connection.GetParameter("pStatusInactive", DataPlug.DataType._Int, (int)CLayer.ObjectStatus.StatusType.Disabled));
            param.Add(Connection.GetParameter("pUpdatedBy", DataPlug.DataType._Int, data.UpdatedBy));
            param.Add(Connection.GetParameter("pUpdatedDate", DataPlug.DataType._DateTime, data.UpdatedDate));
            object result = Connection.ExecuteQueryScalar("offer_StatusEdit", param);
            return Connection.ToInteger(result);          
        }
        //special Offer Home page Display 
        public  List<CLayer.Offers> specialOfferGet(long PropertyId)
        {
            
            List<CLayer.Offers> offer = new List<CLayer.Offers>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, PropertyId));
            DataTable dt = Connection.GetTable("Offer_specialByPropertyId", param);
            //`OfferspecialOffer_PropertyId`(pOfferStatus INT ,pPropertyStatus INT )
            foreach (DataRow dr in dt.Rows)
            {
                offer.Add(new CLayer.Offers()
                {
                    OfferId = Connection.ToLong(dr["OfferId"]),
                    NoOfDays = Connection.ToInteger(dr["NoOfDays"]),
                    StartDate = Connection.ToDate(dr["StartDate"]),
                    EndDate = Connection.ToDate(dr["EndDate"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    //AccommodationId = Connection.ToLong(dr["AccommodationId"]),
                    Amount = Connection.ToDecimal(dr["Amount"]),
                    OfferFor = Connection.ToInteger(dr["OfferFor"]),
                    RateType = Connection.ToInteger(dr["RateType"]),
                    SBCommission = Connection.ToDecimal(dr["SBCommission"]),
                    OfferTitle = Connection.ToString(dr["OfferTitle"]),
                    FreeDays = Connection.ToInteger(dr["FreeDays"]),
                    //PropertyId = Connection.ToLong(dr["PropertyId"]),
                    OfferType = Connection.ToInteger(dr["OfferType"]),
                    StayCategoryId = Connection.ToInteger(dr["StayCategoryId"]),
                    //Display
                    Title = Connection.ToString(dr["Title"]),
                    Location = Connection.ToString(dr["Location"]),
                    Address = Connection.ToString(dr["Address"])
                });
            }
            return offer;
        }

        public long DeleteProperty(long PropertyId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, PropertyId));
            object result = Connection.ExecuteQueryScalar("offers_DeleteProperty", param);
            return Connection.ToLong(result);
        }
        public long DeleteAccommodation(long AccommodationId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, AccommodationId));
           object result= Connection.ExecuteQueryScalar("offers_DeleteAccommodation", param);
           return Connection.ToLong(result);
            //return();
        }
    }
}
