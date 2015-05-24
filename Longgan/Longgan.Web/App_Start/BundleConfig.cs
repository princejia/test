using System.Web;
using System.Web.Optimization;

namespace Longgan.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/jquery.transit.min.js",
                      "~/Scripts/retina-1.1.0.js",
                      "~/Scripts/jquery.unveilEffects.min.js",
                      "~/Scripts/jetmenu.js",
                      "~/Scripts/jquery.isotope.min.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/application.js",
                      "~/Scripts/jasny-bootstrap.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/rs-plugin").Include(
                      "~/Scripts/jquery.nivo.slider.js",
                      "~/Scripts/zoombox.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/style.css"));

            bundles.Add(new StyleBundle("~/Content/Admincss").Include(
                      "~/Content/AdminCss/bootstrap.css",
                      "~/Content/AdminCss/Site.css",
                      "~/Content/AdminCss/jasny-bootstrap.css"                      
                      ));

            //bundles.Add(new StyleBundle("~/Content/rs-plugin").Include(
            //    ));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            //BundleTable.EnableOptimizations = true;
        }
    }
}
