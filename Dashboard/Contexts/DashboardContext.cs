using Dashboard.Models;
using Dashboard.Models.EResources;
using Dashboard.Models.Tickler;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Dashboard
{
	public class DashboardContext : DbContext
	{
		// Tickler
        public DbSet<MiscData> MiscData { get; set; }
		public DbSet<TicklerStat> TicklerStats { get; set; }
        public DbSet<TicklerStatByUser> TicklerStatsByUser { get; set; }
		public DbSet<TicklerActivity> TicklerActivities { get; set; }
		public DbSet<TicklerSubLibrary> TicklerSubLibraries { get; set; }
		public DbSet<TicklerUserActivity> TicklerUserActivities { get; set; }
		public DbSet<TicklerUser> TicklerUsers { get; set; }
        public DbSet<TicklerCatalogingByTitle1> TicklerCatalogingByTitle1 { get; set; }
        public DbSet<TicklerCatalogingByVolume1> TicklerCatalogingByVolume1 { get; set; }
        public DbSet<TicklerCatalogingByCollection> TicklerCatalogingByCollection { get; set; }

		// E-Resources
		public DbSet<EResStat> EResStats { get; set; }
        public DbSet<EResDatabaseCost> EResDatabaseCosts { get; set; }
		public DbSet<EResDatabase> EResDatabases { get; set; }
		public DbSet<EResVendor> EResVendors { get; set; }

		// LibAnswers
		public DbSet<LibAnswer> LibAnswers { get; set; }

        public string Version { get; set; }

		public DashboardContext()
			: base("name=Frost")
		{	
		}

		public IEnumerable<SelectListItem> TicklerActivitiesDropdown(int statusID = 1)
		{
			IEnumerable<SelectListItem> selectList = from r in this.TicklerActivities
													 .Where(a => a.StatusID <= statusID)
													 .ToDictionary(a => a.id, a => a.Title)
													 .OrderBy(a => a.Key)
													 select new SelectListItem
													 {
														 Value = r.Key.ToString(),
														 Text = r.Value
													 };

			selectList = Collections.Pop(selectList,
				new SelectListItem { Text = "Please select an entry...", Value = "" });

			return selectList;
		}

		public IEnumerable<SelectListItem> TicklerSubLibrariesDropdown()
		{
			IEnumerable<SelectListItem> selectList = from r in this.TicklerSubLibraries
                                                     .Where(a => a.StatusID != 0)
													 .ToDictionary(a => a.id, a => a.Title)                                                     
													 .OrderBy(a => a.Key)
													 select new SelectListItem
													 {
														 Value = r.Key.ToString(),
														 Text = r.Value
													 };

			selectList = Collections.Pop(selectList,
				new SelectListItem { Text = "Please select an entry...", Value = "0" });

			return selectList;
		}

		public IEnumerable<SelectListItem> EResVendorsDropdown(int statusID = 1)
		{
			IEnumerable<SelectListItem> selectList = from r in this.EResVendors
													 .ToDictionary(a => a.VendorID, a => a.VendorName)
													 .OrderBy(a => a.Key)
													 select new SelectListItem
													 {
														 Value = r.Key.ToString(),
														 Text = r.Value
													 };

			selectList = Collections.Pop(selectList,
				new SelectListItem { Text = "Please select ...", Value = "" });

			return selectList;
		}

		public IEnumerable<SelectListItem> EResDatabaseTypeDropdown()
		{
			IEnumerable<SelectListItem> selectList = new List<SelectListItem> {
				new SelectListItem 
				{
					Value = "0",
					Text = "Please select..."
				},
				new SelectListItem 
				{
					Value = "1",
					Text = "A&I"
				},
				new SelectListItem 
				{
					Value = "2",
					Text = "Full Text"
				}
			};

			return selectList;
		}

		public IEnumerable<SelectListItem> EResFundingSourceTypeDropdown()
		{
			IEnumerable<SelectListItem> selectList = new List<SelectListItem> {
				new SelectListItem 
				{
					Value = "0",
					Text = "Please select..."
				},
				new SelectListItem 
				{
					Value = "1",
					Text = "UNF"
				},
				new SelectListItem 
				{
					Value = "2",
					Text = "FLVC"
				},
				new SelectListItem 
				{
					Value = "3",
					Text = "Other"
				}
			};

			return selectList;			
		}

		public IEnumerable<SelectListItem> EResDatabasesDropdown(int statusID = 1)
		{
			IEnumerable<SelectListItem> selectList = from r in this.EResDatabases
													 .ToDictionary(a => a.DatabaseID, a => a.DatabaseName)
													 .OrderBy(a => a.Key)
													 select new SelectListItem
													 {
														 Value = r.Key.ToString(),
														 Text = r.Value
													 };

			selectList = Collections.Pop(selectList,
				new SelectListItem { Text = "Please select ...", Value = "" });

			return selectList;
		}
	}
}