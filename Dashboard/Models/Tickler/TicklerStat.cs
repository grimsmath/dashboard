using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dashboard.Models.Tickler
{
    [Table("TicklerStats")]
    public class TicklerStat
    {
		[Key]
        public int id { get; set; }
        public int UserID { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int ActivityTypeID { get; set; }
        public int SubLibrary1 { get; set; }
        public int? SubLibrary2 { get; set; }
        public int? TitleCount { get; set; }
        public int? VolumeCount { get; set; }
        public DateTime? Created { get; set; }
        public int? TitleYear { get; set; }
    }
}