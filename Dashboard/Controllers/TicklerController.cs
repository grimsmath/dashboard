using Dashboard.Models;
using Dashboard.Models.Tickler;
using Dashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dashboard.Controllers
{
    public class TicklerController : DefaultController
    {
		DashboardContext _db = new DashboardContext();

        //
        // GET: /Tickler/
        public ActionResult Index()
        {
            ViewBag.Years = new SelectList(Dates.GetTenYearDropdown()).Items;

			return View();
		}

        //
        // GET: /Tickler/Create
		public ActionResult Create(string status = "", string message = "")
        {
			ViewBag.Years = new SelectList(Dates.GetTenYearDropdown()).Items;
			ViewBag.Months = new SelectList(Dates.GetMonthDropdown()).Items;
			ViewBag.Activities = new SelectList(_db.TicklerActivitiesDropdown()).Items;
			ViewBag.SubLibraries1 = new SelectList(_db.TicklerSubLibrariesDropdown()).Items;
			ViewBag.SubLibraries2 = new SelectList(_db.TicklerSubLibrariesDropdown()).Items;

			return View();
		}

        //
        // GET: /Tickler/List
        public ActionResult List()
        {
            return View();  
        }

        //
        // POST: /Tickler/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, TicklerViewModel viewModel)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public JsonResult StatsByUserId(int? id, int? start, int? end)
        {
            var model = from r in _db.TicklerStatsByUser
                        select r;

            if (start.HasValue)
            {
                int count = (int) start - 1;
                model = model.Skip(count);
            }

            if (end.HasValue)
            {
                model = model.Take((int)end - (int)start);
            }
            else
            {
                model = model.Take(1000);
            }

            return Json(new
            {
                Error = false,
                model
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CatalogingTitlesByUserForYear(int? id /* id=year */)
        {
            var model = from r in this._db.TicklerCatalogingByTitle1
                        where r.Year == id && r.TitleCount > 0
                        group r by r.Cataloger into g
                        select new
                        {
                            Cataloger = g.Key,
                            TitleCount = g.Sum(p => p.TitleCount)
                        };
                        
            return Json(new
            {
                Error = false,
                model
            }, JsonRequestBehavior.AllowGet);            
        }

        [HttpGet]
        public JsonResult CatalogingVolumesByUserForYear(int? id /* id=year */)
        {
            var model = from r in this._db.TicklerCatalogingByVolume1
                        where r.Year == id && r.VolumeCount > 0
                        group r by r.Cataloger into g
                        select new
                        {
                            Cataloger = g.Key,
                            VolumeCount = g.Sum(p => p.VolumeCount)
                        };
            
            return Json(new
            {
                Error = false,
                model
            }, JsonRequestBehavior.AllowGet);              
        }

        [HttpGet]
        public JsonResult CatalogingCollectionsForYear(int? id /* id=year */)
        {
            var model = from r in this._db.TicklerCatalogingByCollection
                        where r.Year == id
                        select r;

            return Json(new
            {
                Error = false,
                model
            }, JsonRequestBehavior.AllowGet);              
        }

		[HttpGet]
		public JsonResult UserByUniversityID(string id)
		{
			var model = from r in _db.TicklerUsers
						where r.Username == id
						select r;

			return Json(new
			{
				Error = false,
				model
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult UserByUserID(string url)
		{
			RouteInfo info = new RouteInfo(new Uri(Request.Url.AbsoluteUri), HttpContext.Request.ApplicationPath);

			int id = int.Parse(info.RouteData.Values["id"].ToString());

			var model = from r in _db.TicklerUsers
						where r.id == id
						select r;

			return Json(new
			{
				Error = false,
				model
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult GetTicklerActivity(string id)
		{
			int activityId = int.Parse(id);

			var model = from r in _db.TicklerActivities
						where r.id == activityId
						select r;

			return Json(new
			{
				Error = false,
				model
			}, JsonRequestBehavior.AllowGet);
		}

        /**
         * *************************************************************************************
         *     This CREATE method is for the stand-alone program that is used with XulRunner
         * *************************************************************************************
         */

        [HttpPost]
		public JsonResult Create(FormCollection form)
		{
			// Create a new statistic object & populate it
			TicklerStat myStat = new TicklerStat();

			List<String> keys = form.AllKeys.ToList();

			// Transfer the form values to the object
			myStat.UserID = int.Parse(form["UserID"]);
			myStat.Month = int.Parse(form["Months"]);
			myStat.Year = int.Parse(form["Years"]);
			myStat.ActivityTypeID = int.Parse(form["Activities"]);
			myStat.SubLibrary1 = int.Parse(form["SubLibraries1"]);
			myStat.SubLibrary2 = int.Parse(form["SubLibraries2"]);
			myStat.TitleCount = (form["TitleCount"].ToString() == "")
				? 0
				: int.Parse(form["TitleCount"]);

			myStat.VolumeCount = (form["VolumeCount"].ToString() == "")
				? 0
				: int.Parse(form["VolumeCount"]);

            myStat.TitleYear = (form["TitleYear"].ToString() == "")
                ? 0
                : int.Parse(form["TitleYear"]);

			myStat.Created = DateTime.Now.Date;

			// Add the object to the collection
			_db.TicklerStats.Add(myStat);

			// Save the changes to the database
			if (_db.SaveChanges() > 0)
				return Json(new { success = true, message = "Successfully submitted tickler." });
			else
				return Json(new { success = false, message = "Failed to submit tickler" });
		}
    }
}
