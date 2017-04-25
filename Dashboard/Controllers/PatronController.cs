using Dashboard.Contexts;
using Dashboard.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dashboard.Controllers
{
    public class PatronController : Controller
    {
		PatronCounterContext _patron = new PatronCounterContext();

        //
        // GET: /Patron/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Patron/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Patron/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Patron/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Patron/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Patron/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Patron/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Patron/Delete/5

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

        public ActionResult Query()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Query(PatronCounterQueryViewModel viewModel)
        {
            var model = "";



            return Json(model);
        }

        

		#region Patron Counter

		[HttpGet]
		public JsonResult PatronCountSnapshot(string url)
		{
			var byYear = PatronCountByYear(url);
			var byMonth = PatronCountByMonth(DateTime.Today.Month.ToString());

			DateTime today = DateTime.Today;
			DateTime weekAgo = today.Subtract(new TimeSpan(6, 0, 0, 0));

			var byWeek = PatronCountBetweenDates(weekAgo.ToString(Dates.DATE_FORMAT) + "/" + today.ToString(Dates.DATE_FORMAT));
			var byDay = PatronCountToday("");
            var byYesterday = PatronCountOnDay(DateTime.Today.AddDays(-1).ToString(Dates.DATE_FORMAT));
            var lastYear = PatronCountOnDay(DateTime.Today.AddYears(-1).ToString(Dates.DATE_FORMAT));

			return Json(new
			{
				Error = false,
				byYear,
				byMonth,
				byWeek,
				byDay,
                byYesterday,
                lastYear
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult PatronCountByDay(string url)
		{
			DateTime today;
			DateTime.TryParse(url, out today);

			var model = from r in _patron.Patrons
						where r.ServerDate.Year == today.Year &&
							  r.ServerDate.Month == today.Month &&
							  r.ServerDate.Day == today.Day
						group r by new { r.ServerDate.Year, r.ServerDate.Month, r.ServerDate.Day } into g
						select new
						{
							ServerDate = g.Key,
							PatronCount = Math.Round(g.Sum(p => p.ValueA) / 2)
						};

			return Json(new
			{
				Error = false,
				model
			}, JsonRequestBehavior.AllowGet);
		}

        [HttpGet]
        public JsonResult PatronCountOnDay(string url)
        {
            DateTime myDate = DateTime.Parse(url);

            var model = from r in _patron.Patrons
                        where r.ServerDate.Year == myDate.Year &&
                              r.ServerDate.Month == myDate.Month &&
                              r.ServerDate.Day == myDate.Day
                        group r by new { r.ServerDate.Year, r.ServerDate.Month, r.ServerDate.Day } into g
                        select new
                        {
                            ServerDate = g.Key,
                            PatronCount = Math.Round(g.Sum(p => p.ValueA) / 2)
                        };

            return Json(new
            {
                Error = false,
                model
            }, JsonRequestBehavior.AllowGet);
        }

		[HttpGet]
		public JsonResult PatronCountToday(string url)
		{
			DateTime today = DateTime.Today;

			var model = from r in _patron.Patrons
						where r.ServerDate.Year == today.Year &&
							  r.ServerDate.Month == today.Month &&
							  r.ServerDate.Day == today.Day
						group r by new { r.ServerDate.Year, r.ServerDate.Month, r.ServerDate.Day } into g
						select new
						{
							ServerDate = g.Key,
							PatronCount = Math.Round(g.Sum(p => p.ValueA) / 2)
						};

			return Json(new
			{
				Error = false,
				model
			}, JsonRequestBehavior.AllowGet);
		}

        [HttpGet]
        public JsonResult PatronCountCurrentWeek(string url)
        {
            DateTime startDate, endDate;

            startDate = Dates.GetMondayOfCurrentWeek();
            endDate = Dates.Tonight(DateTime.Today);

            var model = from r in _patron.Patrons
                        where r.ServerDate >= startDate && r.ServerDate <= endDate
                        orderby r.ServerDate.Day ascending
                        group r by new { r.ServerDate.Year, r.ServerDate.Month, r.ServerDate.Day } into g
                        select new
                        {
                            ServerDay = g.Key,
                            PatronCount = Math.Round(g.Sum(p => p.ValueA) / 2)
                        };

            return Json(new
            {
                Error = false,
                model
            }, JsonRequestBehavior.AllowGet);
        }

		[HttpGet]
		public JsonResult PatronCountLast7(string url)
		{
			DateTime startDate, endDate;

			endDate = DateTime.Today;
			startDate = endDate.Subtract(new TimeSpan(7, 0, 0, 0));

			var model = from r in _patron.Patrons
						where r.ServerDate >= startDate && r.ServerDate <= endDate
						orderby r.ServerDate.Day ascending
						group r by new { r.ServerDate.Year, r.ServerDate.Month, r.ServerDate.Day } into g
						select new
						{
							ServerDay = g.Key,
							PatronCount = Math.Round(g.Sum(p => p.ValueA) / 2)
						};

			return Json(new
			{
				Error = false,
				model
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult PatronCountByWeek(string url)
		{
			DateTime startDate, endDate;

			if (url != null && url.Length > 0)
			{
				// Use start date - 7 days
				endDate = DateTime.Parse(url);
				startDate = endDate.Subtract(new TimeSpan(7, 0, 0, 0));
			}
			else
			{
				// Use current date - 7 days
				endDate = DateTime.Today;
				startDate = endDate.Subtract(new TimeSpan(7, 0, 0, 0));
			}

			var model = from r in _patron.Patrons
						where r.ServerDate >= startDate
						&& r.ServerDate <= endDate
						group r by new { r.ServerDate.Year, r.ServerDate.Month, r.ServerDate.Day } into g
						select new
						{
							ServerDate = g.Key,
							PatronCount = Math.Round(g.Sum(p => p.ValueA) / 2)
						};

			return Json(new
			{
				Error = false,
				model
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult PatronCountByMonth(string url)
		{
			int month = 0;
			int.TryParse(url, out month);

			var model = from r in _patron.Patrons
						where r.ServerDate.Month == month
						group r by r.ServerDate.Month into g
						select new
						{
							ServerMonth = g.Key,
							PatronCount = Math.Round(g.Sum(p => p.ValueA) / 2)
						};

			return Json(new
			{
				Error = false,
				model
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult PatronCountSameWeek(string url)
		{
			return null;
		}

		[HttpGet]
		public JsonResult PatronCountMonthOverMonth(string url)
		{
			// URL should indicate the year we want monthly data for
			int year = int.Parse(url);

			// Define the SQL statement
			String cmd = "SELECT * FROM fn_UNFPatronCountByMonth(@Year)";

			// We will save the data into an arraylist
			ArrayList model = new ArrayList();

			// Grab the results
			DbDataReader reader = Databases.QueryResults("Patrons", cmd,
				new Dictionary<string, object>()
                {
                    { "@Year", 2012 }   
                });

			// Convert the results into something more useful
			while (reader.Read())
			{
				model.Add(new
				{
					ServerYear = reader["ServerYear"],
					ServerMonth = reader["ServerMonth"],
					PatronCount = reader["PatronCount"]
				});
			}

			return Json(new
			{
				Error = false,
				model
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult PatronCountBetweenDates(string url)
		{
			string[] parms = url.Split('/');
			if (parms.Length == 2)
			{
				DateTime startDate = DateTime.Parse(parms[0]);
				DateTime endDate = Dates.Tonight(DateTime.Parse(parms[1]));

                var model = from sensordata in
                                (
                                    from sensordata in _patron.Patrons
                                    where sensordata.ServerDate >= startDate &&
                                    sensordata.ServerDate <= endDate
                                    select new
                                    {
                                        sensordata.ValueA,
                                        Dummy = "x"
                                    }
                                )
                            group sensordata by new { sensordata.Dummy } into g
                            select new
                            {
                                PatronCount = Math.Round(g.Sum(p => p.ValueA) / 2)
                            };

				return Json(new
				{
					Error = false,
					model
				}, JsonRequestBehavior.AllowGet);
			}
			else
			{
				return Json(new
				{
					Error = true
				}, JsonRequestBehavior.AllowGet);
			}
		}

		[HttpGet]
		public JsonResult PatronCountByYear(string url)
		{
			int Year = DateTime.Today.Year;

			if (url != null && url.Length == 1)
			{
				// Get the date value
				int.TryParse(url, out Year);
			}

			var model = from r in _patron.Patrons
						//where r.ServerDate.Year == Year
						group r by r.ServerDate.Year into g
						select new
						{
							ServerYear = g.Key,
							PatronCount = Math.Round((g.Sum(p => p.ValueA) / 2))
						};

			return Json(new
			{
				Error = false,
				model
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult PatronCountYearOverYear()
		{
			var model = from r in _patron.Patrons
						group r by r.ServerDate.Year into g
						select new
						{
							ServerYear = g.Key,
							PatronCount = Math.Round(g.Sum(p => p.ValueA))
						};

			return Json(new
			{
				Error = false,
				model
			}, JsonRequestBehavior.AllowGet);
		}

		#endregion
    }
}
