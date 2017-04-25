using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Dashboard.Contexts;
using Dashboard.Models;

namespace Dashboard.Controllers
{
    public class AppController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PatronCount()
        {
            return View("Patrons");
        }

        public ActionResult EDS()
        {
            return View("EDS");
        }

        public ActionResult LabUsage()
        {
            return View("LabUsage");
        }

        public ActionResult LibAnswers()
        {
            return View("LibAnswers");
        }
    }
}
