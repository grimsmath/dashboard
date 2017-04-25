using Dashboard.Contexts;
using Dashboard.Models;
using Dashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.DirectoryServices.ActiveDirectory;

namespace Dashboard.Controllers
{
    public class EResourcesController : DefaultController
    {
		DashboardContext _db = new DashboardContext();

        public ActionResult Index()
        {
			return View();
        }

		#region Databases

    	public ActionResult Databases()
		{
			ViewBag.Vendors = _db.EResVendorsDropdown();
			ViewBag.Databases = _db.EResDatabasesDropdown();
			ViewBag.FundingSourceTypes = _db.EResFundingSourceTypeDropdown();
			ViewBag.DatabaseTypes = _db.EResDatabaseTypeDropdown();
			ViewBag.Years = new SelectList(Dates.GetTenYearDropdown()).Items;
			ViewBag.Months = new SelectList(Dates.GetMonthDropdown()).Items;

			return View();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public JsonResult CreateDatabase(EResDatabaseViewModel viewModel)
        {
			bool bReturn = false;
			string message = "failed";

            // TODO: Add insert logic here
			EResDatabase newDb = new EResDatabase() 
			{
				DatabaseName = viewModel.DatabaseName,
				VendorID = viewModel.VendorID, 
				DatabaseType = viewModel.DatabaseType,
				DatabaseCost = viewModel.DatabaseCost,
				FundingSource = viewModel.FundingSource
			};

			if (_db.EResDatabases.Add(newDb) != null)
			{
				if (_db.SaveChanges() > 0)
				{
					bReturn = true;
					message = "success";
				}
			}

			return Json(new
			{
				Success = bReturn,
				Message = message,
				SelectList = _db.EResDatabasesDropdown()
			}, JsonRequestBehavior.AllowGet);
        }

		[HttpPost, ValidateAntiForgeryToken]
		public JsonResult UpdateDatabase(EResDatabaseViewModel viewModel)
		{
			bool bReturn = false;
			string message = "failed";

			EResDatabase myDb = _db.EResDatabases.Find(viewModel.DatabaseID);
			if (myDb != null)
			{
				myDb.DatabaseName = viewModel.DatabaseName;
				myDb.VendorID = viewModel.VendorID;
				myDb.DatabaseType = viewModel.DatabaseType;
				myDb.DatabaseCost = viewModel.DatabaseCost;
				myDb.FundingSource = viewModel.FundingSource;
				
				try
				{
					_db.SaveChanges();

					bReturn = true;
					message = "success";
				}
				catch (InvalidOperationException ex)
				{
					message = ex.Message;
				}
			}
	
			return Json(new
			{
				Success = bReturn,
				Message = message,
				SelectList = _db.EResDatabasesDropdown()
			}, JsonRequestBehavior.AllowGet);			
		}

		[HttpPost, ValidateAntiForgeryToken]
		public JsonResult DeleteDatabase(int id)
		{
			bool bReturn = false;
			string message = "failed";

			EResDatabase myDb = _db.EResDatabases.Find(id);
			if (myDb != null)
			{
				EResDatabase deletedDb = _db.EResDatabases.Remove(myDb);
				if (deletedDb != null)
				{
					try
					{
						if (_db.SaveChanges() > 0)
						{
							bReturn = true;
							message = "success";
						}
					}
					catch (Exception ex)
					{
						message = ex.Message;
					}
				}
			}

			return Json(new
			{
				Success = bReturn,
				Message = message,
				SelectList = _db.EResDatabasesDropdown()
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpGet, AllowAnonymous]
		public JsonResult GetDatabases(DataTableParam dtParams)
		{
			IEnumerable<EResDatabaseViewModel> aaData2;

			int totalRecords = _db.EResDatabases.Count();
			int filterdCount = totalRecords;

			var aaData = from r in _db.EResDatabases
						 join v in _db.EResVendors on r.VendorID equals v.VendorID
						 select new EResDatabaseViewModel
						 {
							 DatabaseID = r.DatabaseID,
							 DatabaseName = r.DatabaseName,
							 DatabaseType = r.DatabaseType,
							 DatabaseTypeName = (r.DatabaseType == 1) ? "A&I" : "Full Text",
							 FundingSource = r.FundingSource,
							 DatabaseCost = r.DatabaseCost,
							 FundingSourceName = (r.FundingSource == 1) ? "UNF" : (r.FundingSource == 2) ? "FLVC" : "Other",
							 VendorID = r.VendorID,
							 VendorName = v.VendorName
						 };

			if (!string.IsNullOrEmpty(dtParams.sSearch))
			{
				aaData = aaData.Where(d => d.DatabaseName.Contains(dtParams.sSearch));
				filterdCount = aaData.Count();
			}

			/*
			 * Sorting
			 */
			var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
			var sortDirection = Request["sSortDir_0"]; // asc or desc

			Func<EResDatabaseViewModel, string> orderingFunction = (c => sortColumnIndex == 0 ? c.DatabaseID.ToString() :
																			sortColumnIndex == 1 ? c.DatabaseName :
																			sortColumnIndex == 2 ? c.DatabaseTypeName :
																			sortColumnIndex == 3 ? c.DatabaseCost.ToString() :
																			sortColumnIndex == 4 ? c.FundingSource.ToString() :
																			sortColumnIndex == 5 ? c.VendorID.ToString() :
																			c.DatabaseName);

			if (sortDirection == "asc")
				aaData2 = aaData.OrderBy(orderingFunction).ToList();
			else
				aaData2 = aaData.OrderByDescending(orderingFunction).ToList();

			aaData2 = aaData2.Skip(dtParams.iDisplayStart).Take(dtParams.iDisplayLength);

			var list = (from x in aaData2
						select new EResDatabaseViewModel
						{
							DatabaseID = x.DatabaseID,
							DatabaseName = x.DatabaseName,
							DatabaseType = x.DatabaseType,
							DatabaseTypeName = x.DatabaseTypeName,
							DatabaseCost = x.DatabaseCost,
							DatabaseCostString = x.DatabaseCost.ToString("C"),
							FundingSource = x.FundingSource,
							FundingSourceName = x.FundingSourceName,
							VendorID = x.VendorID,
							VendorName = x.VendorName
						}).ToList();

			return Json(new
			{
				sEcho = dtParams.sEcho,
				iTotalRecords = totalRecords,
				iTotalDisplayRecords = filterdCount,
				aaData = list
			}, JsonRequestBehavior.AllowGet);
		}

        [HttpGet]
        public ActionResult Database(int mode = 0)
        {
            return PartialView("_ModalDatabase");    
        }

        [HttpGet]
        public ActionResult Cost(int mode = 0)
        {
            return PartialView("_ModalDatabaseCost");
        }

        [HttpPost]
        public ActionResult Cost(EResDatabaseCostViewModel viewModel)
        {
            if (! ModelState.IsValid)
            {
                return View(viewModel);    
            }

            return RedirectToAction("Cost");
        }

        public JsonResult GetDatabaseCosts(int databaseId, DataTableParam dtParams)
        {
            var data = (from r in _db.EResDatabaseCosts
                        where r.DatabaseId == databaseId
                        join d in _db.EResDatabases on r.DatabaseId equals d.DatabaseID
                        join c in _db.EResVendors on d.VendorID equals c.VendorID
                        select new EResDatabaseCostViewModel
                        {
                            DatabaseId = r.DatabaseId,
                            Database = d.DatabaseName,
                            Year = r.Year,
                            Cost = r.Cost,
                            FundingSource = (d.FundingSource == 1) ? "UNF" : (d.FundingSource == 2) ? "FLVC" : "Other",
                            Vendor = c.VendorName
                        }).ToList();

            int totalRecords = data.Count();
            int filterdCount = totalRecords;
            
            return Json(new
            {
                sEcho = dtParams.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = filterdCount,
                aaData = data
            });
        }


		#endregion

		#region Vendors

		public ActionResult Vendors()
		{
			ViewBag.Vendors = _db.EResVendorsDropdown();
			ViewBag.Databases = _db.EResDatabasesDropdown();
			ViewBag.FundingSourceTypes = _db.EResFundingSourceTypeDropdown();
			ViewBag.DatabaseTypes = _db.EResDatabaseTypeDropdown();
			ViewBag.Years = new SelectList(Dates.GetTenYearDropdown()).Items;
			ViewBag.Months = new SelectList(Dates.GetMonthDropdown()).Items;

			return View();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public JsonResult CreateVendor(EResVendorViewModel viewModel)
		{
			bool bReturn = false;
			string message = "failed";

			EResVendor newVendor = new EResVendor()
			{
				VendorName = viewModel.VendorName
			};

			if (_db.EResVendors.Add(newVendor) != null)
			{
				if (_db.SaveChanges() > 0)
				{
					bReturn = true;
					message = "success";
				}
			}				

			return Json(new
			{
				Success = bReturn,
				Message = message,
				SelectList = _db.EResVendorsDropdown()
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public JsonResult UpdateVendor(EResVendorViewModel viewModel)
		{
			bool bReturn = false;
			string message = "failed";

			EResVendor myVendor = _db.EResVendors.Find(viewModel.VendorID);
			if (myVendor != null)
			{
				myVendor.VendorName = viewModel.VendorName;

				try
				{
					_db.SaveChanges();

					bReturn = true;
					message = "success";
				}
				catch (InvalidOperationException ex)
				{
					message = ex.Message;
				}
			}

			return Json(new
			{
				Success = bReturn,
				Message = message,
				SelectList = _db.EResVendorsDropdown()
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public JsonResult DeleteVendor(int id)
		{
			bool bReturn = false;
			string message = "failed";

			EResVendor myVendor = _db.EResVendors.Find(id);
			if (myVendor != null)
			{
				EResVendor deletedVendor = _db.EResVendors.Remove(myVendor);
				if (deletedVendor != null)
				{
					try
					{
						if (_db.SaveChanges() > 0)
						{
							bReturn = true;
							message = "success";
						}
					}
					catch (Exception ex)
					{
						message = ex.Message;
					}
				}
			}

			return Json(new
			{
				Success = bReturn,
				Message = message,
				SelectList = _db.EResVendorsDropdown()
			}, JsonRequestBehavior.AllowGet);			
		}

		[HttpGet, AllowAnonymous]
		public JsonResult GetVendors(DataTableParam dtParams)
		{
			IEnumerable<EResVendor> aaData;

			int totalRecords = _db.EResVendors.Count();
			int filteredCount = totalRecords;

			if (!string.IsNullOrEmpty(dtParams.sSearch))
			{
				aaData = _db.EResVendors.Where(d => d.VendorName.Contains(dtParams.sSearch));
				filteredCount = aaData.Count();
			}
			else
			{
				aaData = _db.EResVendors;
			}

			aaData = aaData.Skip(dtParams.iDisplayStart).Take(dtParams.iDisplayLength);

			/*
			 * Sorting
			 */
			var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);

			Func<EResVendor, string> orderingFunction = (c => sortColumnIndex == 0 ? c.VendorID.ToString() : c.VendorName);

			var sortDirection = Request["sSortDir_0"]; // asc or desc

			if (sortDirection == "asc")
				aaData = aaData.OrderBy(orderingFunction);
			else
				aaData = aaData.OrderByDescending(orderingFunction);

			return Json(new
			{
				sEcho = int.Parse(dtParams.sEcho),
				iTotalRecords = totalRecords,
				iTotalDisplayRecords = filteredCount,
				aaData
			}, JsonRequestBehavior.AllowGet);
		}

		#endregion

		#region Stats

		public ActionResult Statistics()
		{
			ViewBag.Vendors = _db.EResVendorsDropdown();
			ViewBag.Databases = _db.EResDatabasesDropdown();
			ViewBag.FundingSourceTypes = _db.EResFundingSourceTypeDropdown();
			ViewBag.DatabaseTypes = _db.EResDatabaseTypeDropdown();
			ViewBag.Years = new SelectList(Dates.GetTenYearDropdown()).Items;
			ViewBag.Months = new SelectList(Dates.GetMonthDropdown()).Items;

			return View();
		}

		[HttpGet, AllowAnonymous]
		public JsonResult GetStats(DataTableParam dtParams)
		{
			IEnumerable<EResStatViewModel> aaData2;
			int totalRecords = _db.EResStats.Count();
			int filterdCount = totalRecords;

            System.Nullable<long> totalDownloads = 0;

			try
			{
                totalDownloads = (from p in _db.EResStats
                                  select p.Downloads).Sum();
			}
			catch {}

			var aaData = from r in _db.EResStats
						 join d in _db.EResDatabases on r.DatabaseID equals d.DatabaseID
						 select new EResStatViewModel
						 {
							 StatID = r.StatID,
							 DatabaseID = r.DatabaseID,
							 DatabaseName = d.DatabaseName,
							 Month = r.Month,
							 Year = r.Year,
							 Searches = r.Searches,
							 Downloads = r.Downloads,
							 LinkedTo = r.LinkedTo,
							 LinkedFrom = r.LinkedFrom,
							 CostPerUse = (r.Downloads > 0) ? ((d.DatabaseCost / r.Downloads) / 12): 0,
							 CostPerLinkFrom = (r.LinkedFrom > 0) ? (d.DatabaseCost / r.LinkedFrom) : 0,
                             Percentage = (r.Downloads > 0) ? (r.Downloads / totalDownloads) * 100 : 0
						 };

			
			if (!string.IsNullOrEmpty(dtParams.sSearch))
			{
				aaData = aaData.Where(d => d.DatabaseName.Contains(dtParams.sSearch));
				filterdCount = aaData.Count();
			}

			var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
			var sortDirection = Request["sSortDir_0"]; // asc or desc

			Func<EResStatViewModel, string> orderingFunction = (c =>	sortColumnIndex == 0 ? c.StatID.ToString() :
																		sortColumnIndex == 1 ? c.DatabaseName :
																		sortColumnIndex == 2 ? c.Year.ToString() :
																		sortColumnIndex == 3 ? c.Month.ToString() :
																		sortColumnIndex == 4 ? c.Searches.ToString() :
																		sortColumnIndex == 5 ? c.Downloads.ToString() :
																		sortColumnIndex == 6 ? c.CostPerUse.ToString() :
																		sortColumnIndex == 7 ? c.LinkedTo.ToString() :
																		sortColumnIndex == 8 ? c.LinkedFrom.ToString() :
																		sortColumnIndex == 9 ? c.CostPerLinkFrom.ToString() :
																		sortColumnIndex == 10 ? c.Percentage.ToString() :
																		c.DatabaseName);

			if (sortDirection == "asc")
				aaData2 = aaData.OrderBy(orderingFunction).ToList();
			else
				aaData2 = aaData.OrderByDescending(orderingFunction).ToList();

			aaData2 = aaData2.Skip(dtParams.iDisplayStart).Take(dtParams.iDisplayLength);

			// The only reason we are doing this is so that we can format number values
			// values into something more readible
			var list = (from x in aaData2
						select new
						{
							StatID = x.StatID,
							DatabaseID = x.DatabaseID,
							DatabaseName = x.DatabaseName,
							Month = x.Month.ToString("D2"),
							Year = x.Year,
							Searches = x.Searches.ToString("N0"),
							Downloads = x.Downloads.ToString("N0"),
							LinkedTo = x.LinkedTo.ToString("N0"),
							LinkedFrom = x.LinkedFrom.ToString("N0"),
							CostPerUse = x.CostPerUse.ToString("C"),
							CostPerLinkFrom = x.CostPerLinkFrom.ToString("C"),
							Percentage = x.Percentage
						}).ToList();

			int count = _db.EResStats.Count();

			return Json(new
			{
				sEcho = dtParams.sEcho,
				iTotalRecords = totalRecords,
				iTotalDisplayRecords = filterdCount,
				aaData = list
			}, JsonRequestBehavior.AllowGet);			
		}

        [HttpPost, ValidateAntiForgeryToken,]
		public JsonResult CreateStat(EResStatViewModel viewModel)
		{
			bool bReturn = false;
			string message = "failed";

			// TODO: Add insert logic here
			EResStat newStat = new EResStat()
			{
				DatabaseID = viewModel.DatabaseID,
				Year = viewModel.Year,
				Month = viewModel.Month,
				Searches = viewModel.Searches,
				Downloads = viewModel.Downloads,
				LinkedTo = viewModel.LinkedTo,
				LinkedFrom = viewModel.LinkedFrom
			};

			if (_db.EResStats.Add(newStat) != null)
			{
				if (_db.SaveChanges() > 0)
				{
					bReturn = true;
					message = "success";
				}
			}

			return Json(new
			{
				Success = bReturn,
				Message = message
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public JsonResult DeleteStat(int id)
		{
			bool bReturn = false;
			string message = "failed";

			EResStat myStat = _db.EResStats.Find(id);
			if (myStat != null)
			{
				EResStat deletedStat = _db.EResStats.Remove(myStat);
				if (deletedStat != null)
				{
					try
					{
						if (_db.SaveChanges() > 0)
						{
							bReturn = true;
							message = "success";
						}
					}
					catch (Exception ex)
					{
						message = ex.Message;
					}
				}
			}

			return Json(new
			{
				Success = bReturn,
				Message = message
			}, JsonRequestBehavior.AllowGet);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public JsonResult UpdateStat(EResStatViewModel viewModel)
		{
			bool bReturn = false;
			string message = "failed";

			EResStat myStat = _db.EResStats.Find(viewModel.StatID);
			if (myStat != null)
			{
				myStat.DatabaseID = viewModel.DatabaseID;
				myStat.Year = viewModel.Year;
				myStat.Month = viewModel.Month;
				myStat.Searches = viewModel.Searches;
				myStat.Downloads = viewModel.Downloads;
				myStat.LinkedTo = viewModel.LinkedTo;
				myStat.LinkedFrom = viewModel.LinkedFrom;

				try
				{
					_db.SaveChanges();

					bReturn = true;
					message = "success";
				}
				catch(InvalidOperationException ex)
				{
					message = ex.Message;
				}
			}

			return Json(new
			{
				Success = bReturn,
				Message = message
			}, JsonRequestBehavior.AllowGet);
		}

		#endregion
	}
}