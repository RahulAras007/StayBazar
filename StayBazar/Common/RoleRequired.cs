using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StayBazar.Common
{
    public class RoleRequired : FilterAttribute, IAuthorizationFilter
    {
        public bool Administrator { get; set; }
        public bool Staff { get; set; }
   //     public bool Guest { get; set; }
        public bool Customer { get; set; }
        public bool Supplier { get; set; }
        public bool Agent { get; set; }
        public bool Corporate { get; set; }
        public bool CorporateUser { get; set; }
        public bool Affiliate { get; set; }
        public bool SalesPerson { get; set; }
        public RoleRequired()
        {
            Administrator = false;
            Staff = false;
     //       Guest = false;
            Customer = false;
            Supplier = false;
            Agent = false;
            Corporate = false;
            CorporateUser = false;
            Affiliate = false;
            SalesPerson = false;
        }
        //public override 
        public void OnAuthorization(AuthorizationContext filterContext)
        {

            try
            {
                string email = filterContext.HttpContext.User.Identity.GetUserName();
                int rle = BLayer.User.GetRole(email);
                bool match = false;

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
    
            }catch(Exception ex)
            {
                LogHandler.LogError(ex);
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}