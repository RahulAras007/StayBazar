using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StayBazar.Areas.Admin.Controllers
{
  [Common.AdminRolePermission(AllowAllRoles=true)]
    public class TaxController : Controller
    {

         static int TitleId;
        private List<CLayer.Tax> InitData(int id)
        {
            List<CLayer.Tax> pts = BLayer.Tax.GetAllByTaxtTitleId(id);
            ViewBag.Tax = new Models.TaxModel() {   
                TaxId = 0,
                TaxTitleId = id,
                Title = "", 
                Rate = 0,                
                IsStandard = false,
                Description = "",
                Notes = "" ,      
                Country ="",
                State ="",     
                City="",
                Status =0,
                UpdatedBy=0,
                OnTotalAmount=false,
                PriceUpto=0           
            };
            return pts;
        }
        public ActionResult Index(int id)
        {
            try
            {
                TitleId = id;
                return View(InitData(id));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }

       
        public ActionResult Delete(int id)
        {
            try
            {
                BLayer.Tax.Delete(id);
                return RedirectToAction("Index", new { id = TitleId });
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Models.TaxModel data)
        {
            try
            {
                
                    string  Userid = User.Identity.GetUserId();
                    long id = 0;
                    long.TryParse(Userid, out id);

               DateTime StartDate=DateTime.Today;
                   DateTime.TryParse(data.StartDate,out StartDate);
                DateTime EndDate=DateTime.Today;
                DateTime.TryParse(data.EndDate, out EndDate);
                if (data.Unlimited==true)
                {
                    data.PriceUpto = 0;
                }
                if(data.CountryId==-1)
                {
                    data.CountryId =null;
                }
                if (data.StateId == -1 )
                {
                    data.StateId = null;
                }
                if(data.CityId==-1)
                {
                    data.CityId = null;
                }
                    CLayer.Tax pt = new CLayer.Tax()
                    {
                        TaxId = data.TaxId,
                        TaxTitleId = TitleId,                      
                        Rate = data.Rate,
                        CountryId = data.CountryId,
                        StateId=data.StateId,
                        CityId=data.CityId,
                        Notes = data.Notes,
                        Status=data.Status,
                        StartDate =StartDate,           
                        EndDate = EndDate,
                        OnDate=data.OnDate,
                        OnTotalAmount=data.OnTotalAmount,
                        PriceUpto=data.PriceUpto,
                        UpdatedBy=id,
                        UpdatedDate=DateTime.Now,
                        Unlimited=data.Unlimited
                    };
                    if (data.StateId > 0)
                     pt.StateId = data.StateId;
                    if (data.StateId == 0)
                        pt.StateId = null;
                    BLayer.Tax.Save(pt);
                    ViewBag.Saved = true;                           
                    return RedirectToAction("Index", new { id = TitleId });
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }   
        public ActionResult Edit(int id)
        {
            try
            {
                ViewBag.Saved = false;
                Models.TaxModel mbt = new Models.TaxModel() { TaxId = 0, TaxTitleId=0 };

                CLayer.Tax pt = BLayer.Tax.Get(id);

                if (pt != null)
                    mbt = new Models.TaxModel(pt.CountryId.Value, ((pt.StateId.Value > 0) ? pt.StateId.Value : -1), ((pt.CityId.Value > 0) ? pt.CityId.Value : -1)) // ,, 
                    {
                        TaxId = pt.TaxId,                       
                        Title = pt.Title,
                        TaxTitleId=pt.TaxTitleId,
                        Rate = pt.Rate,
                        CountryId = pt.CountryId,                       
                        StateId = pt.StateId,
                        CityId=pt.CityId,
                        Notes = pt.Notes,
                        Status=pt.Status,
                        StartDate = pt.StartDate.ToShortDateString(),
                        EndDate = pt.EndDate.ToShortDateString(),
                        OnDate=pt.OnDate,
                        OnTotalAmount=pt.OnTotalAmount,
                        PriceUpto=pt.PriceUpto,
                        UpdatedDate=pt.UpdatedDate,
                       Unlimited=pt.Unlimited
                    };            
                return PartialView("~/Areas/Admin/Views/Tax/_Edit.cshtml", mbt);
                
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        public string GetState(int id)// id == CountryId
        
        {
            List<CLayer.State> states = BLayer.State.GetOnCountry(id);
            
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
           sb.Append("<option value='-1'>Select</option>");
            foreach (CLayer.State st in states)
            {
                sb.Append("<option value='" + st.StateId + "'>" + st.Name + "</option>");
            }
            sb.Append("<option value='-1'>None</option>");
            return sb.ToString();
        }
        [AllowAnonymous]
        public string GetCity(int id)// id == StateId
        {
            List<CLayer.City> cities = BLayer.City.GetOnState(id);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
           sb.Append("<option value='-1'>Select</option>");
            foreach (CLayer.City ct in cities)
            {
                sb.Append("<option value='" + ct.CityId + "'>" + ct.Name + "</option>");
            }
            sb.Append("<option value='-1'>None</option>");
            return sb.ToString();
        }

    }
}