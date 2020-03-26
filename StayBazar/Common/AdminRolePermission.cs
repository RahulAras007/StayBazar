using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StayBazar.Common
{
    public class AdminRolePermission : FilterAttribute, IAuthorizationFilter
    {
        public bool AllowAllRoles { get; set; }
        public int ModuleId { get; set; }
        public AdminRolePermission()
        {
            ModuleId = 0;
            AllowAllRoles = false;
        }
        //public override 
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                string email = filterContext.HttpContext.User.Identity.GetUserName();
                int rle = BLayer.User.GetRole(email);
                bool match = false;

                if(AllowAllRoles)
                {
                    if ((int)CLayer.Role.Roles.Affiliate == rle )
                        match = true;
                    else if ((int)CLayer.Role.Roles.Corporate == rle)
                        match = true;
                    else if ((int)CLayer.Role.Roles.CorporateUser == rle)
                        match = true;
                    else if ((int)CLayer.Role.Roles.Customer == rle)
                        match = true;
                    else if ((int)CLayer.Role.Roles.Supplier == rle)
                        match = true;
                    else if ((int)CLayer.Role.Roles.Agent == rle)
                        match = true; 
                        
                   if(match)
                   {
                       filterContext.Result = new HttpUnauthorizedResult();
                   }
                   return;
                }

                List<CLayer.RolePermission> permissions = BLayer.RolePermissions.GetBlockedPermissions(rle);

                if(ModuleId>0){
                    if(permissions.Exists(m=>m.ModuleId == ModuleId))
                    { filterContext.Result = new HttpUnauthorizedResult(); }
                    
                    return;
                }
               
                var rd = filterContext.HttpContext.Request.RequestContext.RouteData;
                string currentAction = rd.GetRequiredString("action");
                string currentController = rd.GetRequiredString("controller");
                if (currentAction == null) currentAction = "";
                if (currentController == null)
                {

                    currentController = "";

                }
                if(currentController =="")
                { return;  }
         //       string currentArea = rd.Values["area"] as string;

          //      if (currentArea.ToLower() != "admin") return;
                string conts= "";
                currentController = currentController.ToLower();
                foreach(CLayer.RolePermission rp in permissions)
                {
                    conts = rp.Controllers;
                    if (conts != null && conts != "")
                    {
                        conts = conts.ToLower().Replace("  ", " ").Replace(" ", ",");
                        if (conts.Contains(currentController))
                        {
                            filterContext.Result = new HttpUnauthorizedResult();
                            return;
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex);
                filterContext.Result = new HttpUnauthorizedResult();
            }

        }



        #region "Modules"

        public const int CORPORATE_REGISTRATION	= 1;
        public const int SUPPLIER_OWNER_REGISTRATION=2;
        public const int TRAVEL_AGENT_REGISTRATION=	3;
        public const int AFFILIATE_REGISTRATION=	4;
        public const int CORPORATES=	5;
        public const int CUSTOMERS	=6;
        public const int SUPPLIERS=	7;
        public const int TRAVEL_AGENTS=	8;
        public const int AFFILIATES=	9;
        public const int MANAGE_PROPERTY=	10;
        //for explicit permissions

        public const int INVOICE_APPROVAL = 65;
        public const int OFFLINEBOOKING_DELETE = 66;
        public const int OFFLINEBOOKING_EDIT = 67;
        public const int LOWERMARGINBOOKING_APPROVAL_REJECT = 74;
        //mostly unused
        public const int QUERY=	11;
        public const int COMPLAINTS=	12;
        public const int FEEDBACK=	13;
        public const int BOOKINGS=	14;
        public const int BOOKING_REQUESTS=	15;
        public const int BOOKING_REQUESTS_OFFLINE_PROPERTY=	16;
        public const int BOOKINGS_WITH_PARTIAL_PAYMENT=	17;
        public const int FINAL_PAYMENT_ATTEMPTS_FAILURES=	18;
        public const int CANCELLED_PARTIAL_PAYMENT_BOOKINGS	=19;
        public const int BOOKING_REVIEW_ATTEMPTS=20;
        public const int CORPORATE_CREDIT_BOOKINGS	=21;
        public const int OFFLINE_BOOKING_FOR_CUSTOMER=	22;
        public const int SUPPLIER_INVOICE=	23;
        public const int MANAGE_OFFLINE_BOOKING=	24;
        public const int EXTERNAL_INVENTORY_BOOKING_REQUEST=	25;
        public const int SUPPLIER_PAYMENT=	26;
        public const int ACCOMMODATION_FEATURE	=27;
        public const int ACCOMMODATION_TYPE=	28;
        public const int COUNTRY=	29;
        public const int CURRENCY=	30;
        public const int DATA_IMPORT	=31;
        public const int PROPERTY_FEATURE=	32;
        public const int OFFERS=	33;
        public const int RECOMMENDED	=34;
        public const int REVIEWS=	35;
        public const int SALUTATION	=36;
        public const int STAFF=	37;
        public const int STAY_CATEGORY=	38;
        public const int SUBSCRIPTIONS	=39;
        public const int TAX	=40;
        public const int TESTIMONIAL	=41;
        public const int SETTINGS	=42;
        public const int SEO_STATIC_PAGES=	43;
        public const int CUSTOM_PROPERTY=	44;
        public const int FAILED_TRANSACTIONS=	45;
        public const int COLLECTION=	46;
        public const int DAILY_BOOKING=	47;
        public const int DAILY_INVENTORY_AND_BOOKING=	48;
        public const int GROSS_MARGIN=	49;
        public const int ROOM_INVENTORY_AVAILABILITY=	50;
        public const int SUPPLIERS_REGISTRATION=	51;
        public const int SUPPLIER_PAYMENT_2=	52;
        public const int PROPERTY_DETAILS	=53;
        public const int CITY_WISE_STATISTICS=	54;
        public const int OFFLINE_PAYMENTS_REPORT	=55;
        public const int CORPORATE_CREDIT_BOOKINGS_REPORT=	56;
        public const int CORPORATE_DISCOUNTS_REPORT=	57;
        public const int SUPPLIER_INVOICE_2	=58;
        public const int SUPPLIER_INVOICE_PENDING	=59;
        public const int PENDING_CUSTOMER_INVOICE	=60;
        public const int MARGIN_TRACKING	=61;
        public const int BOOKING_STATUS_REPORT	=62;
        public const int DOWNLOAD_PROPERTY_REPORT=	63;
        public const int ROLES=	64;
        // end
        #endregion
    }
}