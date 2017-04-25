using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dashboard.Contexts;
using System.Collections;
using System.Data.Common;

namespace Dashboard.Controllers
{
    public class LabController : Controller
    {
		LabUsageContext _labs = new LabUsageContext();
		
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

		#region Lab Usage

		#region Overall Data

		[HttpGet]
		public JsonResult LabUsageSnapshot()
		{
			DateTime today = DateTime.Today;

			var byYear = LoginsForCurrentYear();
			var byMonth = LoginsForCurrentMonth();
            var byWeek = TotalLoginsForCurrentWeek();
			var byDay = LoginsForCurrentDay();

			return Json(new
			{
				Error = false,
				byYear,
				byMonth,
				byWeek,
				byDay
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult LabUsageToday()
		{
			DateTime today = DateTime.Today;

			var tempModel = from r in _labs.Usage
							where r.LDate >= today
							select r;

			var model = from r in tempModel.ToList()
						select new
						{
							ID = r.ID,
							Prefix = r.Prefix,
							Decal = r.Decal,
							Model = r.Model,
							LabID = r.Lab,
							UserID = r.Userid,
							LoginDate = r.LDate.ToString("MM/dd/yyyy HH:mm:ss"),
							LogoutDate = r.LDate2.ToString("MM/dd/yyyy HH:mm:ss")
						};

			return Json(new
			{
				Error = false,
				model
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult LabUsageByMonth(string url)
		{
			string[] parms = url.Split('/');
			int year = int.Parse(parms[0]);
			int month = int.Parse(parms[1]);

			var data = from r in _labs.Usage
					   where r.LDate.Year == year
					   where r.LDate.Month == month
					   group r by r.LDate.Month into g
					   select new
					   {
						   LabYear = year,
						   LabMonth = g.Key,
						   LoginCount = g.Count()
					   };

			return Json(new
			{
				Error = false,
				data
			}, JsonRequestBehavior.AllowGet);
		}

		#endregion

		#region Lab Logins

		[HttpGet]
		public JsonResult LoginsForCurrentYear()
		{
			var data = from r in _labs.Usage
					   where r.LDate.Year == DateTime.Today.Year
					   group r by r.LDate.Year into g
					   select new
					   {
						   LabYear = g.Key,
						   LoginCount = g.Count()
					   };

			return Json(new
			{
				data
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult LoginsForCurrentMonth()
		{
			var data = from r in _labs.Usage
					   where r.LDate.Year == DateTime.Today.Year
					   where r.LDate.Month == DateTime.Today.Month
					   group r by new { r.LDate.Year, r.LDate.Month } into g
					   select new
					   {
						   LabMonth = g.Key,
						   LoginCount = g.Count()
					   };

			return Json(new
			{
				data
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult TotalLoginsForCurrentWeek()
		{
            DateTime start = Dates.GetMondayOfCurrentWeek();
			DateTime end = Dates.Tonight(DateTime.Today);

			var data = (from r in _labs.Usage
					   where r.LDate >= start
					   where r.LDate <= end
					   orderby r.LDate.Day descending
					   group r by new { r.LDate.Day } into g
					   select new
					   {
						   LabDay = g.Key,
						   LoginCount = g.Count()
					   }).Sum(p => p.LoginCount);

			return Json(new
			{
				data
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult LoginsForCurrentWeek()
		{
            DateTime start = Dates.GetMondayOfCurrentWeek();
			DateTime end = Dates.Tonight(DateTime.Today);

			var data = from r in _labs.Usage
					   where r.LDate >= start
					   where r.LDate <= end
					   orderby r.LDate.Day descending
					   group r by new { r.LDate.Day } into g
					   select new
					   {
						   LabDay = g.Key,
						   LoginCount = g.Count()
					   };

			return Json(new
			{
				data
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult LoginsForCurrentDay()
		{
			var data = from r in _labs.Usage
					   where r.LDate.Year == DateTime.Today.Year
					   where r.LDate.Month == DateTime.Today.Month
					   where r.LDate.Day == DateTime.Today.Day
					   group r by new { r.LDate.Year, r.LDate.Month, r.LDate.Day } into g
					   select new
					   {
						   LabDay = g.Key,
						   LoginCount = g.Count()
					   };

			return Json(new
			{
				data
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult LoginsByYear()
		{
			var data = from r in _labs.Usage
                       where r.LDate.Year <= DateTime.Today.Year
					   group r by r.LDate.Year into g
					   select new
					   {
						   LabYear = g.Key,
						   LoginCount = g.Count()
					   };

			return Json(new
			{
				data
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult LoginsByMonth(string url)
		{
			string[] parms = url.Split('/');

			int year = int.Parse(parms[0]);
			int month = int.Parse(parms[1]);

			var data = from r in _labs.Usage
					   where r.LDate.Year == year
					   where r.LDate.Month == month
					   group r by r.LDate.Month into g
					   select new
					   {
						   LabYear = year,
						   LabMonth = g.Key,
						   LoginCount = g.Count()
					   };

			return Json(new
			{
				Error = false,
				data
			}, JsonRequestBehavior.AllowGet);
		}

        [HttpGet]
        public JsonResult LoginCountForWeek()
        {
            string startDate = Dates.GetMondayOfCurrentWeek().ToString(Dates.DATE_FORMAT);
            string endDate = Dates.Tonight(DateTime.Today).ToString(Dates.DATE_FORMAT);

            var currentWeek = LoginsBetweenDates(startDate + "/" + endDate);

            return Json(new
            {
                Error = false,
                data1 = currentWeek,
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DashboardPatronCount()
        {
            // These variables contain the current week's dates
            string startDate1 = Dates.GetMondayOfCurrentWeek().ToString(Dates.DATE_FORMAT);
            string endDate1 = Dates.Tonight(DateTime.Today).ToString(Dates.DATE_FORMAT);

            // The following variables are the same dates but from last year
            string startDate2 = Dates.GetMondayOfCurrentWeek().AddYears(-1).ToString(Dates.DATE_FORMAT);
            string endDate2 = Dates.Tonight(DateTime.Today).AddYears(-1).ToString(Dates.DATE_FORMAT);

            // These variables will hold the actual json
            var currentWeek1 = LoginsBetweenDates(startDate1 + "/" + endDate1);
            var currentWeek2 = LoginsBetweenDates(startDate2 + "/" + endDate2);

            return Json(new
            {
                Error = false,
                series1 = currentWeek1,
                series2 = currentWeek2
            }, JsonRequestBehavior.AllowGet);
        }

		[HttpGet]
		public JsonResult LoginsBetweenDates(string url)
		{
			string[] parms = url.Split('/');

            DateTime startDate = DateTime.Parse(parms[0]);
			DateTime endDate = Dates.Tonight(DateTime.Parse(parms[1]));

			var model = (from r in _labs.Usage
						 where r.LDate >= startDate
						 where r.LDate <= endDate
						 group r by new { r.LDate.Year, r.LDate.Month, r.LDate.Day } into g
						 select new
						 {
							 ServerYear = g.Key.Year,
							 ServerMonth = g.Key.Month,
							 ServerDay = g.Key.Day,
							 LoginCount = g.Count()
						 }).OrderBy(a => a.ServerMonth);

            model = model.OrderBy(r => r.ServerDay);

			return Json(new
			{
				Error = false,
				model
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult LoginsByHourBetweenDates(string url)
		{
			string[] parms = url.Split('/');
			DateTime date1 = DateTime.Parse(parms[0]);
			DateTime date2 = DateTime.Parse(parms[1]);

			String dateBegin = string.Format("{0}-{1}-{2} 00:00:00.000",
				date1.Month, date1.Day, date1.Year);

			String dateEnd = string.Format("{0}-{1}-{2} 23:59:59.000",
				date2.Month, date2.Day, date2.Year);

			// Define the SQL statement
			String cmd = "SELECT DATEPART(HH, LDate) as Hour, ";
			cmd += "COUNT(*) AS LoginCount FROM NFPROD1.dbo.LibraryLabUsage ";
			cmd += "WHERE LDate BETWEEN '" + dateBegin + "' ";
			cmd += "AND '" + dateEnd + "' ";
			cmd += "GROUP BY DATEPART(HH, LDate)";

			// We will save the data into an arraylist
			ArrayList model = new ArrayList();

			// Grab the results
			DbDataReader reader = Databases.QueryResults("Wildcat", cmd, null);

			// Convert the results into something more useful
			while (reader.Read())
			{
				model.Add(new
				{
					Hour = reader["Hour"],
					LogoutCount = reader["LoginCount"]
				});
			}

			return Json(new
			{
				Error = false,
				model
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult LoginsByDay(string url)
		{
			string[] parms = url.Split('/');
			DateTime date = DateTime.Parse(parms[0]);

			String dateBegin = string.Format("{0}-{1}-{2}",
				date.Month, date.Day, date.Year);

			// Define the SQL statement
			String cmd = "SELECT DATEPART(HH, LDate) as Hour, ";
			cmd += "COUNT(*) AS LogoutCount FROM NFPROD1.dbo.LibraryLabUsage ";
			cmd += "WHERE LDate = '" + dateBegin + "' ";
			cmd += "GROUP BY DATEPART(HH, LDate)";

			// We will save the data into an arraylist
			ArrayList model = new ArrayList();

			// Grab the results
			DbDataReader reader = Databases.QueryResults("Wildcat", cmd, null);

			// Convert the results into something more useful
			while (reader.Read())
			{
				model.Add(new
				{
					Hour = reader["Hour"],
					LogoutCount = reader["LoginCount"]
				});
			}

			return Json(new
			{
				Error = false,
				model
			}, JsonRequestBehavior.AllowGet);
		}

		#endregion

		#region Lab Logouts

		[HttpGet]
		public JsonResult LogoutsByHourBetweenDates(string url)
		{
			string[] parms = url.Split('/');
			DateTime date1 = DateTime.Parse(parms[0]);
			DateTime date2 = DateTime.Parse(parms[1]);

			String dateBegin = string.Format("{0}-{1}-{2} 00:00:00.000",
				date1.Month, date1.Day, date1.Year);

			String dateEnd = string.Format("{0}-{1}-{2} 23:59:59.000",
				date2.Month, date2.Day, date2.Year);

			// Define the SQL statement
			String cmd = "SELECT DATEPART(HH, LDate2) as Hour, ";
			cmd += "COUNT(*) AS LogoutCount FROM NFPROD1.dbo.LibraryLabUsage ";
			cmd += "WHERE LDate2 BETWEEN '" + dateBegin + "' ";
			cmd += "AND '" + dateEnd + "' ";
			cmd += "GROUP BY DATEPART(HH, LDate2)";

			// We will save the data into an arraylist
			ArrayList model = new ArrayList();

			// Grab the results
			DbDataReader reader = Databases.QueryResults("Wildcat", cmd, null);

			// Convert the results into something more useful
			while (reader.Read())
			{
				model.Add(new
				{
					Hour = reader["Hour"],
					LogoutCount = reader["LogoutCount"]
				});
			}

			return Json(new
			{
				Error = false,
				model
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult LogoutsByDay(string url)
		{
			string[] parms = url.Split('/');
			DateTime date = DateTime.Parse(parms[0]);

			String dateBegin = string.Format("{0}-{1}-{2}",
				date.Month, date.Day, date.Year);

			// Define the SQL statement
			String cmd = "SELECT DATEPART(HH, LDate2) as Hour, ";
			cmd += "COUNT(*) AS LogoutCount FROM NFPROD1.dbo.LibraryLabUsage ";
			cmd += "WHERE LDate2 = '" + dateBegin + "' ";
			cmd += "GROUP BY DATEPART(HH, LDate2)";

			// We will save the data into an arraylist
			ArrayList model = new ArrayList();

			// Grab the results
			DbDataReader reader = Databases.QueryResults("Wildcat", cmd, null);

			// Convert the results into something more useful
			while (reader.Read())
			{
				model.Add(new
				{
					Hour = reader["Hour"],
					LogoutCount = reader["LogoutCount"]
				});
			}

			return Json(new
			{
				Error = false,
				model
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult LogoutsByMonth(string url)
		{
			string[] parms = url.Split('/');
			int year = int.Parse(parms[0]);
			int month = int.Parse(parms[1]);

			var data = from r in _labs.Usage
					   where r.LDate2.Year == year
					   where r.LDate2.Month == month
					   group r by r.LDate2.Month into g
					   select new
					   {
						   LabYear = year,
						   LabMonth = g.Key,
						   LogoutCount = g.Count()
					   };

			return Json(new
			{
				Error = false,
				data
			}, JsonRequestBehavior.AllowGet);
		}

		#endregion

		#endregion

    }
}
