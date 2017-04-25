using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dashboard.Models.Tickler
{
    [Table("VIEW_TICKLER_STATS_BY_USERID")]
    public class TicklerStatByUser
    {
        /*
         *  id (int)
         *  TableName (string)
         *  Year (int)
         *  Month (int)
         *  Cataloger (string)
         *  Activity (string)
         *  ActivityId (int)
         *  SubLibrary1 (string)
         *  SubLibrary1Id (int)
         *  SubLibrary2 (string)
         *  SubLibrary2Id (int)
         *  TitleCount (int)
         *  VolumeCount (int)
         */

        public int id { get; set; }
        public string TableName { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string Cataloger { get; set; }
        public int CatalogerId { get; set; }
        public string Activity { get; set; }
        public int ActivityId { get; set; }
        public string SubLibrary1 { get; set; }
        public int SubLibrary1Id { get; set; }
        public string SubLibrary2 { get; set; }
        public int? SubLibrary2Id { get; set; }
        public int? TitleCount { get; set; }
        public int? VolumeCount { get; set; }
    }
}