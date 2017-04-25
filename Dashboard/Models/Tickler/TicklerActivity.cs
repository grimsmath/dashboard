using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dashboard.Models.Tickler
{
    [Table("TicklerActivities")]
    public class TicklerActivity
    {
		[Key]
        public int id { get; set; }
        public string Title { get; set; }
        public string LegacyCode { get; set; }
        public bool SubLibrary1 { get; set; }
        public bool SubLibrary2 { get; set; }
        public int TitleCountMinimum { get; set; }
        public int VolumeCountMinimum { get; set; }
        public int StatusID { get; set; }
        public int Order { get; set; }
        public int MinRoleID { get; set; }
    }
}