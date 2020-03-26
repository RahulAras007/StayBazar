using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Controllers
{
    public class ErrorPageController : Controller
    {
        //
        // GET: /ErrorPage/
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BookingApprovalAlert(CLayer.B2BApprovers ApproverDetails)
        {
            try
            {
                string ApprovalRequest = "User " + ApproverDetails.username + " Requesting approval for travel to ________(city name) for ____ ";
                ApprovalRequest += " Days and accommodation  for  nights";
                ApprovalRequest += " @ Rs. per night.Total cost of stay – Rs..";

            }
            catch(Exception ex)
            {
                Common.LogHandler.AddLog(ex.Message);
            }
          
            return View();
          
        }
        public ActionResult BookingApprovalMessage(CLayer.B2BApprovers ApproverDetails)
        {
            string ApprovalRequest = "This booking required approval from " + ApproverDetails.username + ".\r\nThanks for your patience in the mean time.";
            return View();

        }
        public ActionResult BookingConfirm(CLayer.B2BApprovers ApproverDetails)
        {
            string ApprovalRequest = "User " + ApproverDetails.username + " Requesting approval for travel to ________(city name) for ____ ";
            ApprovalRequest += " Days and accommodation  for  nights";
            ApprovalRequest += " @ Rs. per night.Total cost of stay – Rs..";
            return View();

        }

    }
}