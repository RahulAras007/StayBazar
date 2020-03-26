using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StayBazar.Areas.Admin.Models;
using System.Threading.Tasks;

namespace StayBazar.Areas.Admin.Controllers
{
    
    public class CurrencyController : Controller
    {
        private List<CLayer.Currency> InitData()
        {
            List<CLayer.Currency> pts = BLayer.Currency.GetAll();
            ViewBag.Currency = new CurrencyModel() { CurrencyId = 0, Title = "", Symbol = "", ConversionRate = 0, ConversionPercentage = 0, IsDefault = false, Status = (int)CurrencyModel.StatusTypes.Enabled };
            return pts;
        }
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            try
            {
                return View(InitData());
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(CurrencyModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CLayer.Currency pt = new CLayer.Currency()
                    {
                        CurrencyId = data.CurrencyId,
                        Title = data.Title,
                        Symbol = data.Symbol,
                        ConversionRate = data.ConversionRate,
                        ConversionPercentage = data.ConversionPercentage,
                        IsDefault = data.IsDefault,
                        Status = data.Status,
                        Exchangecode=data.Exchangecode
                    };
                    BLayer.Currency.Save(pt);
                    ViewBag.Saved = true;
                }
                else
                    ViewBag.Saved = false;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission]
        [HttpPost]
        public ActionResult GET(int id)
        {
            try
            {
                ViewBag.Saved = false;
                CurrencyModel mbt = new CurrencyModel() { CurrencyId = 0 };

                CLayer.Currency pt = BLayer.Currency.Get(id);

                if (pt != null)
                    mbt = new CurrencyModel()
                    {
                        CurrencyId = pt.CurrencyId,
                        Title = pt.Title,
                        Symbol = pt.Symbol,
                        ConversionRate = pt.ConversionRate,
                        ConversionPercentage = pt.ConversionPercentage,
                        IsDefault = pt.IsDefault,
                        Exchangecode=pt.Exchangecode,
                        Status = pt.Status
                    };

                return PartialView("_Edit", mbt);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }



        [AllowAnonymous]
        public async Task<string> UpdateExchgRate()
        {
            try
            {
                string url = System.Configuration.ConfigurationManager.AppSettings.Get("CurrencyExchgLink") + "&source=USD&format=1&currencies=";
                List<CLayer.Currency> currs = BLayer.Currency.GetAll();
                System.Net.Http.HttpClient wb = new System.Net.Http.HttpClient();
                string result = "";
                string INRresult = "";
                int i, j;
                double rt = 0;
                double inrrt = 0;
                double othrt = 0;
                double inrrate = 0;
                double usdothrate = 0;
                double inrothrte = 0;


                // Calculate INR rate
                INRresult = await wb.GetStringAsync(url + "INR");
                if (!INRresult.ToLower().Contains("invalid"))
                {
                    i = INRresult.LastIndexOf(":");
                    if (i != -1)
                    {
                        j = INRresult.IndexOf("}");
                        if (j != -1)
                        {
                            INRresult = INRresult.Substring(i + 1, (j - i - 1)).Trim();
                            // Common.LogHandler.Logger.Info(result);
                            inrrt = 0;
                            if (double.TryParse(INRresult, out inrrt))
                            {
                                inrrate = inrrt;

                            }
                        }
                    }
                }

                foreach (CLayer.Currency cur in currs)
                {
                    string[] CURRCY = cur.Exchangecode.Split('-');
                    cur.Exchangecode = CURRCY[1];
                    if (cur.Exchangecode != "" && cur.Exchangecode.Length == 3)
                    {

                        //  other current rate based on USD
                        result = await wb.GetStringAsync(url + cur.Exchangecode);
                        if (!result.ToLower().Contains("invalid"))
                        {
                            i = result.LastIndexOf(":");
                            if (i != -1)
                            {
                                j = result.IndexOf("}");
                                if (j != -1)
                                {
                                    result = result.Substring(i + 1, (j - i - 1)).Trim();
                                    // Common.LogHandler.Logger.Info(result);
                                    othrt = 0;
                                    if (double.TryParse(result, out othrt))
                                    {
                                        usdothrate = othrt;

                                    }
                                }
                            }
                        }


                        // rate based on INR,  convert USD - OTHER to INR - OTHER
                        inrothrte = usdothrate / inrrate;

                        // save rate
                        BLayer.Currency.SetRate((int)cur.CurrencyId, inrothrte);

                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return ex.Message;
            }
            return "true"; //RedirectToAction("Index", "Currency", new { Area = "Admin" });
        }




        [Common.AdminRolePermission(AllowAllRoles=true)]
        public async Task<ActionResult> UpdateCurrencyRate()
        {
            try
            {
                string url = System.Configuration.ConfigurationManager.AppSettings.Get("CurrencyExchgLink") + "&source=USD&format=1&currencies=";

                List<CLayer.Currency> currs = BLayer.Currency.GetAll();
                System.Net.Http.HttpClient wb = new System.Net.Http.HttpClient();
                string result = "";
                string INRresult = "";
                int i, j;
                double rt = 0;
                double inrrt = 0;
                double othrt = 0;
                double inrrate = 0;
                double usdothrate = 0;
                double inrothrte = 0;


                // Calculate INR rate
                INRresult = await wb.GetStringAsync(url + "INR");
                if (!INRresult.ToLower().Contains("invalid"))
                {
                    i = INRresult.LastIndexOf(":");
                    if (i != -1)
                    {
                        j = INRresult.IndexOf("}");
                        if (j != -1)
                        {
                            INRresult = INRresult.Substring(i + 1, (j - i - 1)).Trim();
                            // Common.LogHandler.Logger.Info(result);
                            inrrt = 0;
                            if (double.TryParse(INRresult, out inrrt))
                            {
                                inrrate = inrrt;

                            }
                        }
                    }
                }

                foreach (CLayer.Currency cur in currs)
                {
                    string[] CURRCY = cur.Exchangecode.Split('-');
                    cur.Exchangecode = CURRCY[1];
                    if (cur.Exchangecode != "" && cur.Exchangecode.Length == 3)
                    {

                        //  other current rate based on USD
                        result = await wb.GetStringAsync(url + cur.Exchangecode);
                        if (!result.ToLower().Contains("invalid"))
                        {
                            i = result.LastIndexOf(":");
                            if (i != -1)
                            {
                                j = result.IndexOf("}");
                                if (j != -1)
                                {
                                    result = result.Substring(i + 1, (j - i - 1)).Trim();
                                    // Common.LogHandler.Logger.Info(result);
                                    othrt = 0;
                                    if (double.TryParse(result, out othrt))
                                    {
                                        usdothrate = othrt;

                                    }
                                }
                            }
                        }


                        // rate based on INR convert USD - OTHER to INR - OTHER
                        inrothrte = usdothrate / inrrate;
                        BLayer.Currency.SetRate((int)cur.CurrencyId, inrothrte);

                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);

            }
            return RedirectToAction("Index", "Currency", new { Area = "Admin" });
        }



    }
}