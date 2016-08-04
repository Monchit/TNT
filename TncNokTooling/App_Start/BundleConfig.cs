using System.Web;
using System.Web.Optimization;

namespace TncNokTooling
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            // jQueryUI
            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/eggplant/jquery.ui.core.css",
                        //"~/Content/themes/eggplant/jquery.ui.resizable.css",
                        "~/Content/themes/eggplant/jquery.ui.selectable.css",
                        "~/Content/themes/eggplant/jquery.ui.accordion.css",
                        "~/Content/themes/eggplant/jquery.ui.autocomplete.css",
                        "~/Content/themes/eggplant/jquery.ui.button.css",
                        "~/Content/themes/eggplant/jquery.ui.dialog.css",
                        //"~/Content/themes/eggplant/jquery.ui.slider.css",
                        "~/Content/themes/eggplant/jquery.ui.tabs.css",
                        "~/Content/themes/eggplant/jquery.ui.datepicker.css",
                        //"~/Content/themes/eggplant/jquery.ui.progressbar.css",
                        "~/Content/themes/eggplant/jquery.ui.theme.css"));

            // Bootstrap's
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                        "~/Content/bootstrap-flatly.css",
                        "~/Content/bootstrap-responsive.css"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js"));

            // jTable
            bundles.Add(new StyleBundle("~/Content/jTable").Include(
                        "~/Scripts/jtable/themes/metro/lightgray/jtable.css"));
            bundles.Add(new ScriptBundle("~/bundles/jTable").Include(
                        "~/Scripts/jtable/jquery.jtable.js"));

            // form-validator
            bundles.Add(new StyleBundle("~/Content/formvalidator").Include(
                        "~/Scripts/form-validator/theme-default.css"));
            bundles.Add(new ScriptBundle("~/bundles/formvalidator").Include(
                        "~/Scripts/form-validator/jquery.form-validator.js"));

            // Select2
            bundles.Add(new StyleBundle("~/Content/select2").Include(
                        "~/Content/css/select2.css"));
            bundles.Add(new ScriptBundle("~/bundles/select2").Include(
                        "~/Scripts/select2.js"));

            // Noty notifications
            bundles.Add(new ScriptBundle("~/bundles/noty").Include(
                        "~/Scripts/noty/jquery.noty.js",
                        "~/Scripts/noty/themes/default.js",
                        "~/Scripts/noty/layouts/center.js",
                        "~/Scripts/noty/layouts/centerRight.js",
                        "~/Scripts/noty/layouts/topCenter.js",
                        "~/Scripts/noty/layouts/topRight.js"));
        }
    }
}