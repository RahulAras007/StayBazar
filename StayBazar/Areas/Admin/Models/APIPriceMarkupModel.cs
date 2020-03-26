using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using StayBazar.Lib.Security;

namespace StayBazar.Areas.Admin.Models
{
    public class APIPriceMarkupModel
    {
        
        public string CustomerName { get; set; }
        public string SearchString { get; set; }
        public int start { get; set; }
        public int Limit { get; set; }
        public int currentPage { get; set; }
        public long TotalRows { get; set; }
        public APIUserModel CorpUser { get; set; }
        public List<CLayer.APIPriceMarkup> API { get; set; }
        //List<CLayer.CostCentre> ECostCentre = BLayer.CostCentre.GetAll();
        //ECustomerList = new SelectList(ECostCentre, "CostCentreCode", "CostcentreName");

        public APIPriceMarkupModel()
        {
            CorpUser = new APIUserModel();
        }
    }
    public class APIUserModel
    {
        public long APIPriceMarkupCode { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public SelectList ECustomerList { get; set; }
        public int DescriptionId { get; set; }
        public SelectList Description { get; set; }
        public string APIDescription { get; set; }
        public string SellPrice { get; set; }
        public APIUserModel()
        {
            List<KeyValuePair<int, string>> statustypes = new List<KeyValuePair<int, string>>();
            statustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.APIPriceMarkup.TamarindAPI, CLayer.ObjectStatus.APIPriceMarkup.TamarindAPI.ToString()));
            statustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.APIPriceMarkup.TBOAPI, CLayer.ObjectStatus.APIPriceMarkup.TBOAPI.ToString()));
            Description = new SelectList(statustypes, "Key", "Value");

            List<CLayer.APIOfflineCustomer> ECustomer = BLayer.APIPriceMarkup.GetAlCustomerList();
            ECustomerList = new SelectList(ECustomer, "CustomerId", "CustomerName");
        }
    }




}
