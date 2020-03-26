using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace StayBazar.Controllers
{
    public class RateController : Controller
    {
        public ActionResult Index(long accommodationId)
        {
            Models.RatesAccommodationModel data = new Models.RatesAccommodationModel();
            List<CLayer.Rates> obj = BLayer.Rate.GetAllRates(accommodationId);
            string obj1 = BLayer.Accommodation.GetAccommodationTitle(accommodationId);
            data.Type = obj1;
            foreach (CLayer.Rates rate in obj)
            {               
                switch (rate.RateFor)
                {
                    case (int)CLayer.Role.Roles.Customer:
                        data.RRateId = rate.RateId;
                        data.RCDaily = rate.DailyRate;
                        data.RCWeekly = rate.WeeklyRate;
                        data.RCMonthly = rate.MonthlyRate;
                        data.RCGuest = rate.GuestRate;
                        data.RCLong = rate.LongTermRate;
                        break;
                    case (int)CLayer.Role.Roles.Corporate:
                        data.CRateId = rate.RateId;
                        data.CCDaily = rate.DailyRate;
                        data.CCWeekly = rate.WeeklyRate;
                        data.CCMonthly = rate.MonthlyRate;
                        data.CCLong = rate.LongTermRate;
                        data.CCGuest = rate.GuestRate;
                        break;
                }
            }
            return View(data);

        }
    }
}