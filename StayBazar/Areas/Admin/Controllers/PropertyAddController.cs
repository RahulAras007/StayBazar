using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace StayBazar.Areas.Admin.Controllers
{
    public class PropertyAddController : Controller
    {
        //
        // GET: /Admin/PropertyAdd/

        public async Task<ActionResult> Index(long PageId)
        {
            Models.StaticHtmlPageModel data = new Models.StaticHtmlPageModel();
            data.PageId = PageId;
            CLayer.StaticPage dt = BLayer.StaticPage.GetPage(PageId);
            data.PageTitle = dt.PageTitle;
            data.Location = dt.Location;
            data.City = dt.City;
            data.PageTitle = dt.PageTitle;
            data.Destination = dt.City;
            List<CLayer.SearchResult> dtlist1 = BLayer.Property.GetAllPrptyStatic();
            SearchResults result = await SearchFilter(data);
            data.PropertyList = result.Results;
            List<CLayer.SearchResult> dtlist2 = BLayer.Property.GetStaticPagePrpty(PageId);
            data.MaxCount = result.TotalRows;
            data.PropertyAdd = dtlist2;
            data.MaxCount = result.TotalRows;
            data.CurrentPage = 1;
            return View(data);

        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        [HttpPost]
        public async Task<ActionResult> AddProperty(IEnumerable<int> Propertyselected, Models.StaticHtmlPageModel model)
        {
            long PageId = model.PageId;

            if (Propertyselected != null)
            {
                foreach (var PropertyId in Propertyselected)
                {
                    CLayer.StaticPage dt = new CLayer.StaticPage();
                    dt.PropertyId = PropertyId;
                    dt.PageId = PageId;
                    BLayer.StaticPage.SaveProperty(dt);
                }
            }

            return await Search(model);
            //return RedirectToAction("Index", new { PageId = model.PageId });
        }
        public async Task<ActionResult> Remove(long PropertyId, long PageId,string Destination, Models.StaticHtmlPageModel model)
        {
            try
            {
                CLayer.StaticPage dt = BLayer.StaticPage.GetPage(PageId);
                BLayer.StaticPage.RemoveStaticProperty(PropertyId);
                model.PageTitle = dt.PageTitle;
                model.Destination = Destination;
                model.City = Destination;
                return await Search(model);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }

        // search property
        #region Custom Methods

        private class SearchResults
        {
            public List<CLayer.SearchResult> Results;
            public int TotalRows;
        }
        private async Task<List<CLayer.SearchResult>> FillLocations(List<CLayer.SearchResult> data)
        {
            if (data == null || data.Count == 0) return data;
            string location;
            foreach (CLayer.SearchResult sr in data)
            {
                if (sr.Lattitude == "0" && sr.Longitude == "0")
                {
                    location = sr.City + ", " + sr.State;
                    if (sr.Pincode != "") location = location + " " + sr.Pincode;
                    location = location + ", " + sr.Country;
                    Common.Utils.Location pos;

                    pos = await Common.Utils.GetLocation(location);
                    sr.Lattitude = pos.Lattitude.ToString();
                    sr.Longitude = pos.Longitude.ToString();
                }
            }

            return data;
        }
        private async Task<SearchResults> SearchFilter(StayBazar.Areas.Admin.Models.StaticHtmlPageModel data)
        {
            SearchResults searchr = new SearchResults();

            CLayer.SearchCriteria cr = new CLayer.SearchCriteria();
            //int temp;

            cr.Adults = 0;

            cr.Children = 0;

            cr.StayType = 0;

            cr.Bedrooms = 0;
            int totalCount = 0;

            cr.Destination = data.Destination;
            if (cr.Destination != null && cr.Destination != "")
            {
                cr.Destination = cr.Destination.Replace("'", "").Replace(";", "").Replace("\"", "");
            }
            cr.CheckOut = DateTime.Today.AddDays(2);
            cr.CheckIn = DateTime.Today.AddDays(1);
            cr.Lattitude = 0;
            cr.Longitude = 0;
            cr.Bedrooms = 0;
            cr.RangeBudgetMax = 0;
            cr.RangeBudgetMin = 0;
            cr.DistanceInKm = 0;
            cr.Features = "";
            cr.StarRatingRange = 0;
            string loc = "";
            if (data.Location != null && data.Location != "")
            {
                loc = data.Location;

                if (cr.Destination != null && cr.Destination != "")
                {
                    loc = BLayer.City.GetLocation(loc);
                    if (loc == "")
                    {
                        loc = BLayer.City.GetLocation(data.Destination);
                        //if (loc == "") ...
                        //loc = data.Location + ", " + loc;
                    }
                }
            }
            else
            {
                loc = BLayer.City.GetLocation(data.Destination);
            }
            if (loc == "")
            {
                cr.Lattitude = 0;
                cr.Longitude = 0;
            }
            else
            {
                Common.Utils.Location pos;
                pos = await Common.Utils.GetLocation(loc);
                cr.Lattitude = pos.Lattitude;
                cr.Longitude = pos.Longitude;
            }
            int rip = 0;
            int.TryParse(System.Configuration.ConfigurationManager.AppSettings.Get(Common.Utils.SERARCH_MAX_ROWS), out rip);
            cr.NoOfRows = rip;


            cr.Features = "";

            int sr = data.CurrentPage - 1;

            if (sr < 0) sr = 0;
            sr = sr * cr.NoOfRows;
            cr.StaringRow = sr;
            cr.SortOrder = (CLayer.SearchCriteria.SortBy.PriceAsc);
            cr.Location = data.Location;

            if (cr.Location != null && cr.Location != "")
            {
                cr.Location = cr.Location.Replace("'", "").Replace(";", "").Replace("\"", "");
            }
            cr.UserType = CLayer.Role.Roles.Customer;
            cr.LoggedInUser = 0;
            //data.PropertyList = await FillLocations(BLayer.Property.SearchWithFilter(out totalCount, cr));
            //data.TotalRows = totalCount;
            searchr.Results = await FillLocations(BLayer.Property.SearchWithFilter(out totalCount, cr));
            searchr.TotalRows = totalCount;
            return searchr;
        }
        #endregion
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> ApplyFilter(StayBazar.Areas.Admin.Models.StaticHtmlPageModel data)
        {
            //sumbit from the home page or from side box
            StayBazar.Models.SearchResultModel md = new StayBazar.Models.SearchResultModel();
            if(data.CurrentPage <1)
            {
                data.CurrentPage = 1;
            }
            SearchResults result = await SearchFilter(data);
            List<CLayer.SearchResult> dtlist2 = BLayer.Property.GetStaticPagePrpty(data.PageId);
            data.PropertyAdd = dtlist2;
            
            data.PropertyList = result.Results;
            data.MaxCount = result.TotalRows;
            data.IsSearched = true;
            md.Results = result.Results;
            md.CurrentPageIndex = data.CurrentPage;
            md.MaxCount = result.TotalRows;
            md.IsSearched = true;
            md.Destination = data.Destination;
            return View("~/Areas/Admin/Views/PropertyAdd/SearchList.cshtml", data);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> SearchPager(StayBazar.Areas.Admin.Models.StaticHtmlPageModel data)
        {

            StayBazar.Areas.Admin.Models.StaticHtmlPageModel data1 = new StayBazar.Areas.Admin.Models.StaticHtmlPageModel();
            SearchResults result = await SearchFilter(data);
            List<CLayer.SearchResult> dtlist2 = BLayer.Property.GetStaticPagePrpty(data.PageId);
            data1.PropertyAdd = dtlist2;
            data1.PropertyList = result.Results;
            data1.IsSearched = true;
            data1.SearchString = data.SearchString;
            data1.MaxCount = result.TotalRows;
            data1.CurrentPage = data.CurrentPage;
            return View("~/Areas/Admin/Views/PropertyAdd/SearchList.cshtml", data1);
        }
        public async Task<ActionResult> Search(StayBazar.Areas.Admin.Models.StaticHtmlPageModel data)
        {
            //sumbit from the home page or from side box
            StayBazar.Models.SearchResultModel md = new StayBazar.Models.SearchResultModel();
            if (data.CurrentPage < 1)
            {
                data.CurrentPage = 1;
            }
            SearchResults result = await SearchFilter(data);
            List<CLayer.SearchResult> dtlist2 = BLayer.Property.GetStaticPagePrpty(data.PageId);
            data.PropertyAdd = dtlist2;

            data.PropertyList = result.Results;
            data.MaxCount = result.TotalRows;
            data.IsSearched = true;
            md.Results = result.Results;
            md.CurrentPageIndex = data.CurrentPage;
            md.MaxCount = result.TotalRows;
            md.IsSearched = true;
            md.Destination = data.Destination;
            return View("~/Areas/Admin/Views/PropertyAdd/Index.cshtml", data);
        }
    }

}