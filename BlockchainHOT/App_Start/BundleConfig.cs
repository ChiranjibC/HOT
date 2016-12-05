using System.Web;
using System.Web.Optimization;

namespace BlockchainHOT
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

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      //"~/Content/themes/jquery-ui-custom.css",
                      "~/Content/themes/base/all.css",
                      "~/Content/jquery.jqGrid/ui.jqgrid.css"
                      ));


            bundles.Add(new ScriptBundle("~/bundles/jqgrid").Include(
                        "~/Scripts/grid.locale-en.js",
                        "~/Scripts/jquery.jqGrid*"));

            bundles.Add(new ScriptBundle("~/bundles/sitejs").Include(                        
                        "~/Scripts/sitejs/*.js"));

            bundles.Add(new ScriptBundle("~/bundles/partialjs").Include(
                        "~/Scripts/sitejs/partial/*.js"));
        }
    }
}
