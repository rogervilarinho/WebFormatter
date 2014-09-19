using System.Web;
using System.Web.Optimization;

namespace WebAffinitiesMVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.readable.css", "~/Content/bootswatch.min.css"));
            bundles.Add(new ScriptBundle("~/Scripts/js").Include("~/Scripts/jquery-1.10.2.min.js", "~/Scripts/bootstrap.min.js", "~/Scripts/bootswatch.js"));
        }
    }
}
