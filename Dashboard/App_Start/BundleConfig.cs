using System.Web;
using System.Web.Optimization;

namespace Dashboard
{
	public class BundleConfig
	{
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
			/* 
			 * ***********************************
			 *			 CSS BUNDLES
			 * ***********************************
			 */
			bundles.Add(new StyleBundle("~/Content/css").Include(
				"~/Content/css/*.css"));

			/* 
			 * ***********************************
			 *		  JAVASCRIPT BUNDLES
			 * ***********************************
			 */

			// jQuery
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
				"~/Scripts/jquery-{version}.js"));

			// jQuery UI
			bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
				"~/Scripts/jquery-ui-{version}.js"));

			// jQuery Plugins
			bundles.Add(new ScriptBundle("~/bundles/jquery-plugins").Include(
				"~/Scripts/jquery.unobtrusive-ajax.js",
				"~/Scripts/jquery.cookie.js",
				"~/Scripts/jquery.validate*"));

			// Bootstrap
			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
				"~/Scripts/bootstrap.js",
				"~/Scripts/bootstrap-responsive.js",
                "~/Scripts/bootstrap-datetimepicker.js"));

			// HighCharts
			bundles.Add(new ScriptBundle("~/bundles/highcharts").Include(
				"~/Scripts/highcharts.src.js"));

			// FullCalendar
			bundles.Add(new ScriptBundle("~/bundles/fullcalendar").Include(
				"~/Scripts/fullcalendar.js"));

			// Data Tables
			bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
				"~/Scripts/jquery.dataTables.js",
				"~/Scripts/DT_bootstrap.js",
				"~/Scripts/DT_bootstrap_pagination.js"));

			// Library Scripts
			bundles.Add(new ScriptBundle("~/bundles/library").Include(
				"~/Scripts/library-*"));
		}
	}
}