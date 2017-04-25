using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dashboard.Controllers
{
    public class TimeTrackerController : Controller
    {
        //
        // GET: /TimeTracker/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /TimeTracker/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /TimeTracker/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TimeTracker/Create

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
        // GET: /TimeTracker/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /TimeTracker/Edit/5

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
        // GET: /TimeTracker/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /TimeTracker/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
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
    }
}
