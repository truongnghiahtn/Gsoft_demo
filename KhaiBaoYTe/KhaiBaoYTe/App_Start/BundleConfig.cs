using System.Web;
using System.Web.Optimization;

namespace KhaiBaoYTe
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.min.js",
                      "~/Scripts/respond.js",
                      "~/Content/js/bootstrap4-toggle.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css", 
                      "~/adminlte/plugins/fontawesome-free/css/all.min.css",
                      "~/adminlte/css/adminlte.css",
                      "~/adminlte/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css",
                      "~/adminlte/plugins/datatables-responsive/css/responsive.bootstrap4.min.css",
                      "~/Content/main_layout.css",
                      "~/Content/css/bootstrap4-toggle.css"
                      ));
            bundles.Add(new ScriptBundle("~/adminlte/js").Include(
                      "~/adminlte/plugins/datatables/jquery.dataTables.js",
                      "~/adminlte/plugins/datatables-bs4/js/dataTables.bootstrap4.min.js",
                      "~/adminlte/plugins/datatables-responsive/js/dataTables.responsive.min.js",
                      "~/adminlte/plugins/datatables-responsive/js/responsive.bootstrap4.min.js",
                      "~/adminlte/js/adminlte.min.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/chart").Include(
                      "~/Scripts/chart.js"
                ));
        }
    }
}
