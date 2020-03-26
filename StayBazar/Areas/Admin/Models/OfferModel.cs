using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace StayBazar.Areas.Admin.Models
{
    public class OfferModel
    {
        public long OfferId { get; set; }
        public int NoOfDays { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [Display(Name = "Title")]
        public string OfferTitle { get; set; }
        public CLayer.ObjectStatus.StatusType Status { get; set; }       
        public long AccommodationId { get; set; }
        public decimal Amount { get; set; }
        public int OfferFor { get; set; }
        [Required(ErrorMessage = " is required")]
        public int RateType { get; set; }
        public int Rate { get; set; }
        public decimal SBCommission { get; set; }
        public int FreeDays { get; set; }
        public long PropertyId { get; set; }
        [Required(ErrorMessage = " is required")]
        public int OfferType { get; set; }
        public int OfferTypes { get; set; }
        public int StayCategoryId { get; set; }
        public string Ids { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public SelectList OfferForList { get; set; }
        public enum OfferForEn { All = 2, Regular = 4, Supplier = 8, Corporate = 16, TravelAgent = 32, BusinessAffliate = 64 }      
        public enum OfferCategory { Active = 1, Expired = 2, All=3}
        public enum OfferOn { Accommodation = 1, Property = 2 }
        public enum serchacc { Property = 1, City = 2, State = 3, Supplier = 4 }      
        public long TotalRows { get; set; }
        public int currentPage { get; set; }
        public int Limit { get; set; }
        public string SearchString { get; set; }
        public int SearchValue { get; set; }
        public string SearchStringForAccommodation { get; set; }
        public int SearchValueForAccommodation { get; set; }
        public int SearchValueForProperty { get; set; }
        public List<CLayer.Offers> offerlist { get; set; }
        public SelectList StatusOfferTypes { get; set; }
        public SelectList OfferTypeLi { get; set; }
        public AccommodationModel Accommodation { get; set; }//Model        
        public List<CLayer.Accommodation> Accommodations { get; set; }

        // for save
        public long SOfferId { get; set; }
        public string SOfferTitle { get; set; }
        public DateTime SStartDate { get; set; }
        public DateTime SEndDate { get; set; }
        public int SRateType { get; set; }
        public int SNoOfDays { get; set; }
        public decimal SAmount { get; set; }
        public int SOfferFor { get; set; }
        public long SAccommodationId { get; set; }
        public long SPropertyId { get; set; }
        public int SOfferType { get; set; }
        public decimal SSBCommission { get; set; }
        public int SFreeDays { get; set; }
        public string SIds { get; set; }
        public int SStayCategoryId { get; set; }
        public int SRate { get; set; }
        //Properrty List
        public string Address { get; set; }
        public string Statename { get; set; }
        public string City { get; set; }
        public string Ownername { get; set; }

        public OfferModel()
        {
            List<KeyValuePair<int, string>> objStatustypes = new List<KeyValuePair<int, string>>();
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.Active, CLayer.ObjectStatus.StatusType.Active.ToString()));
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.Blocked, CLayer.ObjectStatus.StatusType.Blocked.ToString()));
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.Deleted, CLayer.ObjectStatus.StatusType.Deleted.ToString()));
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.NotVerified, "Recent"));
            StatusOfferTypes = new SelectList(objStatustypes, "Key", "Value");
            List<KeyValuePair<int, string>> objOffertypes = new List<KeyValuePair<int, string>>();
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.OfferRateType.OfferPercentageRate, CLayer.ObjectStatus.OfferRateType.OfferPercentageRate.ToString()));
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.OfferRateType.OfferFlatRate, CLayer.ObjectStatus.OfferRateType.OfferFlatRate.ToString()));
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.OfferRateType.OfferFreeRate, CLayer.ObjectStatus.OfferRateType.OfferFreeRate.ToString()));
            OfferTypeLi = new SelectList(objOffertypes,"Key", "Value");
            offerlist = new List<CLayer.Offers>();
            Array OfferListarry = Enum.GetValues(typeof(OfferForEn));
            List<SelectListItem>Items= new List<SelectListItem>(OfferListarry.Length);
            foreach (var i in OfferListarry)
            {
                Items.Add(new SelectListItem { Text = Enum.GetName(typeof(OfferForEn), i), Value = ((int)i).ToString() });
            }
           OfferForList = new SelectList(Items, "Value", "Text");       
           Accommodations = new List<CLayer.Accommodation>();
           Accommodation = new AccommodationModel();//Model
           StartDate = DateTime.Now.Date;
           EndDate = DateTime.Now.AddDays(1); //DateTime.Now.Date;
           RateType = (int)CLayer.ObjectStatus.OfferRateType.OfferPercentageRate;
           OfferType = (int)StayBazar.Areas.Admin.Models.OfferModel.OfferOn.Accommodation;
        }
    }  

    public class offersearch
    {
        public long TotalRows { get; set; }
        public int currentPage { get; set; }
        public int Limit { get; set; }
        public string SearchString { get; set; }
        public int Id { get; set; }
        public int SearchValue { get; set; }
        public CLayer.ObjectStatus.StatusType Status { get; set; }
        public enum OfferCategory { Active = 1, Expired = 2, All = 3 }
        public List<CLayer.Offers> offerlist { get; set; }
        public offersearch()
        {
            offerlist = new List<CLayer.Offers>();
        }
    }
 public class OffersaveModel
 {
     public long SOfferId { get; set; }
     public long OfferId { get; set; }
     public string SOfferTitle { get; set; }
     public DateTime SStartDate { get; set; }
     public DateTime SEndDate { get; set; }
     public int SRateType { get; set; }
     public int SNoOfDays { get; set; }
     public decimal SAmount { get; set; }
     public int OfferFor { get; set; }
     public long SAccommodationId { get; set; }
     public long SPropertyId { get; set; }
     public int SOfferType { get; set; }   
     public decimal SBCommission { get; set; }   
     public int SFreeDays { get; set; }    
     public string SIds { get; set; }
     public int StayCategoryId { get; set; } 
     public CLayer.ObjectStatus.StatusType SStatus { get; set; }
     public AccommodationModel Accommodation { get; set; }//Model  
     public List<CLayer.Accommodation> Accommodations { get; set; }
     public OffersaveModel()
     {
         Accommodations = new List<CLayer.Accommodation>();
         Accommodation = new AccommodationModel();//Model
     }
 }
 }
    



