using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;

namespace StayBazar.Areas.Admin.Controllers
{
    [Common.RoleRequired(Administrator=true,Staff=true)]
    public class RateController : Controller
    {

        #region Methods
        private bool IsOwnProperty(long propertyId){
            long uid = 0;
            long.TryParse(User.Identity.GetUserId(), out uid);
            return (!BLayer.Property.IsOwnerProperty(propertyId, uid));
            
        }
       
        

        public string  SaveAccRate(Models.RateValueModel data)
        {
            CLayer.Rates rat = new CLayer.Rates();
            rat.RateId = data.RRateId;
            rat.AccommodationId = data.AccommodationId;
            long uid = 0;
            long.TryParse(User.Identity.GetUserId(), out uid);
            //Get commission
            CLayer.RateCommission rc = BLayer.Property.GetCommission(BLayer.Accommodation.GetPropertyId(data.AccommodationId));
            if (rc == null) return "false";
            rat.UpdatedBy = uid;
            //find dates
            int days = DateTime.DaysInMonth(2014, data.StartMonth);
            if (days < data.StartDay) data.StartDay = days;
            rat.StartDate = new DateTime(DateTime.Today.Year, data.StartMonth, data.StartDay);
            days = DateTime.DaysInMonth(2014, data.EndMonth);
            if (days < data.EndDay) data.EndDay = days;
            rat.EndDate = new DateTime(DateTime.Today.Year, data.EndMonth, data.EndDay);

            //Save Regular rate
            rat.RateFor = (int)CLayer.Role.Roles.Customer;
            rat.RateId = data.RRateId;
            rat.DailyRate = data.RCDaily;
            rat.WeeklyRate = data.RCWeekly;
            rat.MonthlyRate = data.RCMonthly;
            rat.LongTermRate = data.RCLong;
            rat.GuestRate = data.RCGuest;
           
            BLayer.Rate.Save(rat);

            // Corporate
            rat.RateFor = (int)CLayer.Role.Roles.Corporate;
            rat.RateId = data.CRateId;
            rat.DailyRate = data.CCDaily;
            rat.WeeklyRate = data.CCWeekly;
            rat.MonthlyRate = data.CCMonthly;
            rat.LongTermRate = data.CCLong;
            rat.GuestRate = data.CCGuest;

            BLayer.Rate.Save(rat);

            ////Supplier
            //rat.RateFor = (int)CLayer.Role.Roles.Supplier;
            //rat.RateId = data.SRateId;
            //rat.DailyRate = data.SCDaily;
            //rat.WeeklyRate = data.SCWeekly;
            //rat.MonthlyRate = data.SCMonthly;
            //rat.GuestRate = data.SCGuest;

            //BLayer.Rate.Save(rat, rc.Supplier);

            ////Travel Agent
            //rat.RateFor = (int)CLayer.Role.Roles.Agent;
            //rat.RateId = data.TRateId;
            //rat.DailyRate = data.TCDaily;
            //rat.WeeklyRate = data.TCWeekly;
            //rat.MonthlyRate = data.TCMonthly;
            //rat.GuestRate = data.TCGuest;
           
            //BLayer.Rate.Save(rat, rc.TravelA);
            return "true";
        }

        #endregion
        //
        // GET: /Admin/Rate/
        public ActionResult Index(long? id)
        {
            try{
            Models.RateModel data = new Models.RateModel();
            if (!id.HasValue)
                id = 0;
            else
            {
                data.Rates = BLayer.Rate.GetAll(id.Value);
                data.PropertyId = BLayer.Accommodation.GetPropertyId(id.Value);
                CLayer.RateCommission rc = BLayer.Property.GetCommission(data.PropertyId);
                data.Commission.ComPropertyId = data.PropertyId;
                data.Commission.B2CLongTerm = rc.B2CLongTerm;
                data.Commission.B2CShortTerm = rc.B2CShortTerm;
                data.Commission.B2BLongTerm = rc.B2BLongTerm;
                data.Commission.B2BShortTerm = rc.B2BShortTerm;
            }
            data.RateAccommodationId = id.Value;
           
            return View(data);
            }catch(Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return RedirectToAction("Index", "ErrorPage");
            }
        }

        public ActionResult GetList(long accommodationId)
        {
            Models.RateModel data = new Models.RateModel();
            try
            {
               
                data.Rates = BLayer.Rate.GetAll(accommodationId);
                data.PropertyId = BLayer.Accommodation.GetPropertyId(accommodationId);

                data.RateAccommodationId = accommodationId;
               
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("_List", data);
        }

        //[HttpPost]
        //public string SaveCommission(Models.RateCommissionModel data)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid) return "true";
        //        if (!IsOwnProperty(data.ComPropertyId))
        //        {
        //            RedirectToAction("Index", "ErrorPage");
        //            return "";
        //        }
        //        if(data.ComPropertyId != 0 && data.CommissionReg != 0)
        //        {
        //            CLayer.RateCommission rc = new CLayer.RateCommission();
        //            rc.PropertyId = data.ComPropertyId;
        //            rc.Corporate = data.CommissionCorp;
        //            rc.BAffiliate = 0;
        //            rc.TravelA = data.CommissionTA;
        //            rc.Regular = data.CommissionReg;
        //            rc.Supplier = data.CommissionSup;
        //            BLayer.Property.SetCommission(rc);

        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        Common.LogHandler.HandleError(ex);
        //        return "false";
        //    }
        //    return "true";
        //}
        [HttpPost]
        public string SaveRate(Models.RateValueModel data)
        {
            string result;
            try
            {
                result = SaveAccRate(data); 
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return "false";
            }
            return result;
        }

        public ActionResult GetRates(DateTime StartDate,DateTime EndDate,long AccommodationId)
        {
            Models.RateValueModel rvm = new Models.RateValueModel();
            try
            {
                List<CLayer.Rates> result = BLayer.Rate.GetAll(StartDate, EndDate, AccommodationId);
                foreach (CLayer.Rates rate in result)
                {
                    switch(rate.RateFor)
                    {
                        case (int) CLayer.Role.Roles.Customer:
                            rvm.RRateId = rate.RateId;
                            rvm.RCDaily = rate.DailyRate;
                            rvm.RCWeekly = rate.WeeklyRate;
                            rvm.RCMonthly = rate.MonthlyRate;
                            rvm.RCGuest = rate.GuestRate;
                            rvm.StartDay = rate.StartDate.Day;
                            rvm.RCLong = rate.LongTermRate;
                            rvm.StartMonth = rate.StartDate.Month;
                            rvm.EndDay = rate.EndDate.Day;
                            rvm.EndMonth = rate.EndDate.Month;
                            break;
                        case (int)CLayer.Role.Roles.Corporate:
                            rvm.CRateId = rate.RateId;
                            rvm.CCDaily = rate.DailyRate;
                            rvm.CCWeekly = rate.WeeklyRate;
                            rvm.CCMonthly = rate.MonthlyRate;
                            rvm.CCLong = rate.LongTermRate;
                            rvm.CCGuest = rate.GuestRate;
                            break;
                        //case (int)CLayer.Role.Roles.Supplier:
                        //    rvm.SRateId = rate.RateId;
                        //    rvm.SCDaily = rate.DailyRate;
                        //    rvm.SCWeekly = rate.WeeklyRate;
                        //    rvm.SCMonthly = rate.MonthlyRate;
                        //    rvm.SCGuest = rate.GuestRate;
                        //    break;
                        //case (int)CLayer.Role.Roles.Agent:
                        //    rvm.TRateId = rate.RateId;
                        //    rvm.TCDaily = rate.DailyRate;
                        //    rvm.TCWeekly = rate.WeeklyRate;
                        //    rvm.TCMonthly = rate.MonthlyRate;
                        //    rvm.TCGuest = rate.GuestRate;
                        //    break;
                    }
                }
            }
            catch (Exception ex)
            { Common.LogHandler.HandleError(ex);
                rvm = new Models.RateValueModel();
            }
            return View("_Edit",rvm);
        }

        [HttpPost]
        public ActionResult Delete(DateTime StartDate,DateTime EndDate, long AccommodationId)
        {
            Models.RateModel data = new Models.RateModel();
            try
            {
                BLayer.Rate.Delete(StartDate, EndDate, AccommodationId);
                data.Rates = BLayer.Rate.GetAll(AccommodationId);
                data.PropertyId = BLayer.Accommodation.GetPropertyId(AccommodationId);

                data.RateAccommodationId = AccommodationId;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("_List", data);
        }
	}
}