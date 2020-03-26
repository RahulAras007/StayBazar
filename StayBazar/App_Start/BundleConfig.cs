using System.Web;
using System.Web.Optimization;

namespace StayBazar
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //16-11-2019 - ramesh
            bundles.Add(new ScriptBundle("~/bundles/jqueryold").Include(
            "~/Scripts/jquery-1.10.1.min.js",
            "~/Scripts/jquery.cookie.js",
            "~/Scripts/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/content/js/jquery-1.11.1.min.js",
                        "~/content/js/jquery.cookie.js",
                        "~/content/js/main.js"));

            //16-11-2019 ramesh
            bundles.Add(new ScriptBundle("~/bundles/jqueryuiold").Include(
                        "~/Scripts/jquery-ui.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/content/js/jquery-ui.1.10.4.min.js"));

            //16-11-2019 ramesh
            bundles.Add(new ScriptBundle("~/bundles/jqueryvalold").Include(
                    "~/Scripts/jquery.validate*"));

            // not working
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/content/js/jquery.validate.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                     "~/Scripts/owl.carousel.js",                            // old slider js
                    // "~/Content/owl-carrosel/owl.carousel.js",                    // New slider js
                   //"~/Content/owl-carrosel/jquery.mousewheel.js",                  // New slider js
                      "~/Scripts/bootstrap-slider.js",
                      "~/Scripts/jquery.jtruncate.js",
                      "~/Scripts/jquery.twbsPagination.js",
                      "~/Scripts/richmarker-compiled.js"
                //     "~/Scripts/bootstrap-formhelpers.js")); 
              //        "~/Scripts/jquery.dpNumberPicker-1.0.1.js"
              ));
            //    "~/Scripts/bootstrap-datepicker.js",
            bundles.Add(new StyleBundle("~/Content/css").Include(
                   //     "~/Content/bootstrap.min.css",
                  //     "~/Content/bootstrap-theme.min.css",
                      "~/Content/owl.carousel.css",                       // old slider css
                     //  "~/Content/owl-carrosel/assets/owl.carousel.css",     // New slider css
                        "~/Content/owl.theme.css",
                        "~/Content/owl.transitions.css",
                        "~/Content/jquery-ui.min.css",
                        "~/Content/bootstrap-formhelpers.css",
                        "~/Content/font-awesome.min.css",
                        "~/Content/bootstrap-slider.css",
                        "~/Content/jquery-ui.structure.min.css",
                        "~/Content/jquery-ui.theme.min.css",
                        "~/Content/site.css"
                       ));
             bundles.Add(new StyleBundle("~/Content/bootcss").Include(
                      "~/Content/bootstrap.min.css",
                     "~/Content/bootstrap-theme.min.css"));
            // "~/Content/style.css",
            // "~/Content/jquery.dpNumberPicker-holoLight-1.0.1.css"
            //"~/Content/datepicker.css",

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/bootstrap.css",
                        "~/Content/themes/base/bootstrap.min.css",
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}
