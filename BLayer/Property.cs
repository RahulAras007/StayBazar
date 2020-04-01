using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class Property
    {
        /// <summary>
        /// Checks for active property and it should come under active supplier too..
        /// </summary>
        /// <param name="propertyId">Property ID</param>
        /// <returns>Boolean: Valid Property</returns>
        public static bool IsValid(long propertyId)
        {
            CLayer.Property pr = BLayer.Property.Get(propertyId);
            if (pr == null) return false;
            if (pr.Status != CLayer.ObjectStatus.StatusType.Active) return false;
            if (BLayer.User.GetStatus(pr.OwnerId) != CLayer.ObjectStatus.StatusType.Active) return false;
            return true;
        }
        public static long GetSupplierId(long propertyId)
        {
            DataLayer.Property pr = new DataLayer.Property();
            return pr.GetSupplierId(propertyId);
        }
        public static List<string> GetPropertyEmailAndId(long supplierId, DateTime forDate)
        {
            DataLayer.Property pr = new DataLayer.Property();
            return pr.GetPropertyEmailAndId(supplierId, forDate);
        }
        public static CLayer.Property GetCancellationCharges(long propertyId)
        {
            DataLayer.Property pr = new DataLayer.Property();
            return pr.GetCancellationCharges(propertyId);
        }

        public static int GetPropertybyEmail(string Email)
        {
            DataLayer.Property pr = new DataLayer.Property();
            return pr.GetPropertybyEmail(Email);
        }
        public static String GetGDSHotelImage(long PropertyID)
        {
            DataLayer.Property pr = new DataLayer.Property();
            return pr.GetGDSHotelImage(PropertyID);
        }

        public static CLayer.Property GetPartialPayment(long propertyId)
        {
            DataLayer.Property pr = new DataLayer.Property();
            return pr.GetPartialPayment(propertyId);
        }
        public static void SaveCancellationDetails(long propertyId, double charge, int period, CLayer.ObjectStatus.CancellationType cancType, bool appliesForRefund)
        {
            DataLayer.Property pr = new DataLayer.Property();
            pr.SaveCancellationDetails(propertyId, charge, period, cancType, appliesForRefund);
        }
        public static List<CLayer.Accommodation> GetDetailsForBooking(long propertyId, CLayer.SearchCriteria criteria)
        {
            DataLayer.Property pr = new DataLayer.Property();
            return pr.GetDetailsForBooking(propertyId, criteria);
        }
        public static CLayer.SearchResult GetGDSImageandDesctiption(long PropertyID)
        {
            DataLayer.Property pr = new DataLayer.Property();
            return pr.GetGDSImageandDesctiption(PropertyID);
        }
        public static void SetCommission(CLayer.RateCommission commission)
        {
            DataLayer.Property prperty = new DataLayer.Property();
            if (commission.B2CShortTerm <= 0) commission.B2CShortTerm = commission.B2CShortTerm;
            if (commission.B2CLongTerm <= 0) commission.B2CLongTerm = commission.B2CLongTerm;
            if (commission.B2BShortTerm <= 0) commission.B2BShortTerm = commission.B2BShortTerm;
            if (commission.B2BLongTerm <= 0) commission.B2BLongTerm = commission.B2BLongTerm;
            prperty.SetCommission(commission);
        }

        public static CLayer.RateCommission GetCommission(long propertyId)
        {
            DataLayer.Property prperty = new DataLayer.Property();
            return prperty.GetCommission(propertyId);
        }
        public static List<CLayer.Property> SearchProperty(string searchText, bool bySupplier)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.SearchProperty(searchText, bySupplier);
        }
        public static List<CLayer.Property> GetAllPropertiesForratesave()
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.GetAllPropertiesForratesave();
        }

        public static bool IsOwnerProperty(long propertyId, long ownerId)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.IsOwnerProperty(propertyId, ownerId);
        }
        public static List<CLayer.Property> GetAll(long ownerId)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.GetAll(ownerId);
        }
        public static List<CLayer.Property> propertydisable_GetAll()
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.propertydisable_GetAll();
        }
        public static List<CLayer.Property> GetAllPropertiesForsitemap()
        {
            DataLayer.Property pr = new DataLayer.Property();
            return pr.GetAllPropertiesForsitemap();
        }
        public static CLayer.Property PropertyAvgRate(long PropertyId)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.PropertyAvgRate(PropertyId);
        }
        public static CLayer.Property PropertyForMostPopularGDS(long PropertyId)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.PropertyForMostPopularGDS(PropertyId);
        }
        public static CLayer.Property PropertyForAutoComplete(long PropertyId, DateTime pStart, DateTime pEnd)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.PropertyForAutoComplete(PropertyId, pStart, pEnd);
        }

        public static long Save(CLayer.Property propertydata)
        {
            DataLayer.Property property = new DataLayer.Property();
            propertydata.B2CMarkupShortTerm = Convert.ToDecimal(BLayer.Settings.GetValue((CLayer.Settings.B2C_MARKUP_SHORT_TERM)));
            propertydata.B2CMarkupLongTerm = Convert.ToDecimal(BLayer.Settings.GetValue((CLayer.Settings.B2C_MARKUP_LONG_TERM)));
            propertydata.B2BMarkupShortTerm = Convert.ToDecimal(BLayer.Settings.GetValue((CLayer.Settings.B2B_MARKUP_SHORT_TERM)));
            propertydata.B2BMarkupLongTerm = Convert.ToDecimal(BLayer.Settings.GetValue((CLayer.Settings.B2B_MARKUP_LONG_TERM)));
            propertydata.B2BStdLongTermDis = Convert.ToDecimal(BLayer.Settings.GetValue((CLayer.Settings.B2B_STD_LONG_TERM_DIS)));
            propertydata.B2BStdShortTermDis = Convert.ToDecimal(BLayer.Settings.GetValue((CLayer.Settings.B2B_STD_SHORT_TERM_DIS)));
            propertydata.Validate();
            return property.Save(propertydata);
        }
        public static long SaveAmadeus_Property(CLayer.Property propertydata)
        {
            DataLayer.Property property = new DataLayer.Property();

            return property.SaveAmadeus_Property(propertydata);
        }
        public static int SetPosition(long propertyId, string positionLat, string positionLng)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.SetPosition(propertyId, positionLat, positionLng);
        }

        public static int SetHotelIdAPI(long propertyId, string HotelId, int InventoryAPIType)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.SetHotelIdAPI(propertyId, HotelId, InventoryAPIType);
        }
        public static int SetStatus(long propertyId, CLayer.ObjectStatus.StatusType status)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.SetStatus(propertyId, status);
        }
        public static bool CanDelete(long propertyid)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.CanDelete(propertyid);
        }
        public static string GetHotelId(long propertyid)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.GetHotelId(propertyid);
        }
        public static string GetRateCardDetailId(long propertyid)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.GetRateCardDetailId(propertyid);
        }

        public static string GetTamrindCityID(long propertyId)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.GetTamrindCityID(propertyId);
        }

        public static int GetInventoryAPITypeId(long propertyid)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.GetInventoryAPITypeId(propertyid);
        }
        public static int GetInventoryAPITypeIdd(long propertyid)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.GetInventoryAPITypeIdd(propertyid);
        }
        public static int GetPropertyInventorytype(long PropertyId)
        {
            DataLayer.Property bok = new DataLayer.Property();
            return bok.GetPropertyInventorytype(PropertyId);
        }
        public static void Delete(long propertyid)
        {
            DataLayer.Property property = new DataLayer.Property();
            property.Delete(propertyid);
        }

        public static CLayer.Property Get(long propertyid)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.Get(propertyid);
        }
        public static CLayer.Property GetTamarind(long propertyid)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.GetTamarind(propertyid);
        }
        public static CLayer.Property GetCheckTime(long propertyid)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.GetCheckTime(propertyid);
        }
        public static CLayer.Property GetPosition(long propertyid)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.GetPosition(propertyid);
        }
        //public static List<CLayer.SearchResult> Search(out int totalCount,CLayer.SearchCriteria crit)
        //{
        //    DataLayer.Property property = new DataLayer.Property();
        //    crit.DefaultUserType = CLayer.Role.Roles.Customer;
        //    return property.Search(out totalCount,crit);
        //}
        public static List<CLayer.Property> GetAllGDSProperties()
        {
            DataLayer.Property bok = new DataLayer.Property();
            return bok.GetAllGDSProperties();
        }
        public static List<CLayer.Property> GetAllGDSPropertiesWithOutData()
        {
            DataLayer.Property bok = new DataLayer.Property();
            return bok.GetAllGDSPropertiesWithOutData();
        }
        public static List<CLayer.Property> GetAllTamarindPropertyDescriptionsWithOutData()
        {
            DataLayer.Property bok = new DataLayer.Property();
            return bok.GetAllTamarindPropertyDescriptionsWithOutData();
        }
        public static List<CLayer.Property> GetAllTBOPropertyDescriptionsWithOutData()
        {
            DataLayer.Property bok = new DataLayer.Property();
            return bok.GetAllTBOPropertyDescriptionsWithOutData();
        }
        public static List<CLayer.Property> GetAllGDSPropertyDescriptionsWithOutData()
        {
            DataLayer.Property bok = new DataLayer.Property();
            return bok.GetAllGDSPropertyDescriptionsWithOutData();
        }
        public static List<CLayer.Property> GetAllGDSPropertyFormattedDescriptionsWithOutData()
        {
            DataLayer.Property bok = new DataLayer.Property();
            return bok.GetAllGDSFormattedPropertyDescriptionsWithOutData();
        }
        public static List<CLayer.Property> GetAllGDSPropertyTitlesWithOutData()
        {
            DataLayer.Property bok = new DataLayer.Property();
            return bok.GetAllGDSPropertyTitlesWithOutData();
        }
        public static List<CLayer.Property> GetAllTamarindPropertyTitlesWithOutData()
        {
            DataLayer.Property bok = new DataLayer.Property();
            return bok.GetAllTamarindPropertyTitlesWithOutData();
        }
        public static List<CLayer.Property> GetAllTBOPropertyTitlesWithOutData()
        {
            DataLayer.Property bok = new DataLayer.Property();
            return bok.GetAllTBOPropertyTitlesWithOutData();
        }
        public static List<CLayer.SearchResult> SearchWithFilter(out int totalCount, CLayer.SearchCriteria criteria)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.SearchWithFilter(out totalCount, criteria);
        }
        public static List<CLayer.SearchResult> SearchWithFilter_Amadeus(out int totalCount, CLayer.SearchCriteria criteria)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.SearchWithFilter_Amadeus(out totalCount, criteria);
        }
        public static List<CLayer.SearchResult> GetStaticPagePrpty(long PageId)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.GetStaticPagePrpty(PageId);
        }
        public static List<CLayer.SearchResult> GetAllPrptyStatic()
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.GetAllPrptyStatic();
        }
        public static string GetCity(string City)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.Getcity(City);
        }
        public static int SavePartialPay(CLayer.Property data)
        {
            DataLayer.Property task = new DataLayer.Property();
            return task.SavePartialPay(data);
        }
        public static decimal GetPropertyB2CpartialamountPerc(long Pid)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.GetPropertyB2CpartialamountPerc(Pid);
        }
        public static decimal GetPropertyB2BpartialamountPerc(long Pid)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.GetPropertyB2BpartialamountPerc(Pid);
        }
        public static List<string> GetLocation(string Location)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.GetLocation(Location);
        }

        public static DataTable GetAccommodationDetailsFrompropertyid(long id)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.GetAccommodationDetailsFrompropertyid(id);
        }
        public static long GetPropertyIdFromTamarindId(long id)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.GetPropertyIdFromTamarindId(id);
        }
        public static long GetPropertyIdFromTBOId(long id)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.GetPropertyIdFromTBOId(id);
        }
        public static DataTable GetHotelIDFrompropertyid(long id)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.GetHotelIDFrompropertyid(id);
        }
        public static DataTable GetHotelIDFromTamarindid(long id)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.GetHotelIDFromTamarindid(id);
        }
        public static DataTable GetHotelIDFromTBOid(long id)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.GetHotelIDFromTBOid(id);
        }
        public static int GetPropertyInventoryAPIType(long PropertyId)
        {
            DataLayer.Property bok = new DataLayer.Property();
            return bok.GetInventoryAPITypeId(PropertyId);
        }
        public static void GDSSaveImageurl(long PropertyId, string url)
        {
            DataLayer.Property bok = new DataLayer.Property();
            bok.GDSSaveImageurl(PropertyId, url);
        }
        public static void GDSUpdatePropertyDescription(long PropertyId, string Description)
        {
            DataLayer.Property bok = new DataLayer.Property();
            bok.GDSUpdatePropertyDescription(PropertyId, Description);
        }
        public static void GDSUpdatePropertyDescriptionFormatted(long PropertyId, string Description, int StarRatings, string Response)
        {
            DataLayer.Property bok = new DataLayer.Property();
            bok.GDSUpdatePropertyDescriptionFormatted(PropertyId, Description, StarRatings, Response);
        }
        public static void GDSUpdatePropertyStarRatings(long PropertyId, int StarRatings)
        {
            DataLayer.Property bok = new DataLayer.Property();
            bok.GDSUpdatePropertyStarRatings(PropertyId, StarRatings);
        }
        public static void GDSUpdatePropertyTitle(long PropertyId, string Title)
        {
            DataLayer.Property bok = new DataLayer.Property();
            bok.GDSUpdatePropertyTitle(PropertyId, Title);
        }
        public static void GDSUpdatePropertyContactNumbers(long PropertyId, string Phone, string Mobile, string Email = "")
        {
            DataLayer.Property bok = new DataLayer.Property();
            bok.GDSUpdatePropertyContactNumbers(PropertyId, Phone, Mobile, Email);
        }
        public static long GetGDSPropertyImagesCount(long PropertyID)
        {
            DataLayer.Property bok = new DataLayer.Property();
            return bok.GetGDSPropertyImagesCount(PropertyID);
        }
        public static List<string> GetGDSHotelAllImages(long propertyid, int limit = 0)
        {
            DataLayer.Property bok = new DataLayer.Property();
            return bok.GetGDSHotelAllImages(propertyid, limit);
        }
        public static int DeleteGDSPropertyImages(long PropertyID)
        {
            DataLayer.Property bok = new DataLayer.Property();
            return bok.DeleteGDSPropertyImages(PropertyID);
        }
        public static bool HasValidAccountForCustomProperty(long propertyId)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.HasValidAccountForCustomProperty(propertyId);
        }

        public static bool HasValidAccountForProperty(long propertyId)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.HasValidAccountForProperty(propertyId);
        }

        public static DataTable SearchForGDSProperties(List<string> codes)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.SearchForGDSProperties(codes);
        }
        public static DataTable SearchForGDSPropertiesWithUser(List<string> codes)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.SearchForGDSPropertiesWithUser(codes);
        }
        public static void GDSSavePropertyDescriptions(long pID, long propertyid, CLayer.DetailContents pDetailContents)
        {
            DataLayer.Property property = new DataLayer.Property();
            property.GDSSavePropertyDescriptions(pID, propertyid, pDetailContents);
        }
        public static DataTable GetHotelFormattedDescription(long id)
        {
            DataLayer.Property property = new DataLayer.Property();
            return property.GetHotelFormattedDescription(id);
        }
        public static List<CLayer.Property> GetAllGDSPropertiesRecommended()
        {
            DataLayer.Property bok = new DataLayer.Property();
            return bok.GetAllGDSPropertiesRecommended();
        }
        public static List<CLayer.Property> PropertyGetOnCity(int CityId)
        {
            DataLayer.Property bok = new DataLayer.Property();
            return bok.PropertyGetOnCity(CityId);
        }
        public static List<CLayer.Property> GetDefaultHotels(long pUserID, string pDestination)
        {
            DataLayer.Property bok = new DataLayer.Property();
            return bok.GetDefaultHotels(pUserID, pDestination);
        }
        public static CLayer.Property GetBookingPropertyDetails(long propertyId)
        {
            DataLayer.Property bok = new DataLayer.Property();
            return bok.GetBookingPropertyDetails(propertyId);
        }
        public static string GetPropertyTamarindFlag(long PropertyID, string PropertyName)
        {
            DataLayer.Property bok = new DataLayer.Property();
            return bok.GetPropertyTamarindFlag(PropertyID, PropertyName);
        }
        public static int GetTamarindInventoryAPITypeId(long tamarindhotelid)
        {
            DataLayer.Property bok = new DataLayer.Property();
            return bok.GetTamarindInventoryAPITypeId(tamarindhotelid);
        }
        public static int GetTBOInventoryAPITypeId(long tamarindhotelid)
        {
            DataLayer.Property bok = new DataLayer.Property();
            return bok.GetTBOInventoryAPITypeId(tamarindhotelid);
        }
    }
}
