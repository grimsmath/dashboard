using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dashboard.Models.Tickler
{
    public class TicklerStatByUserId
    {
        public int id { get; set; }
        public string TableName { get; set; }
        public System.Nullable<int> Year { get; set; }
        public System.Nullable<int> Month { get; set; }
        public string Cataloger { get; set; }
        public int CatalogerId { get; set; }
        public string Activity { get; set; }
        public int ActivityId { get; set; }
        public string ActivityIdLegacyCode { get; set; }
        public string SubLibrary1 { get; set; }
        public System.Nullable<int> SubLibrary1Id { get; set; }
        public string SubLibrary1LegacyCode { get; set; }
        public string SubLibrary2 { get; set; }
        public System.Nullable<int> SubLibrary2Id { get; set; }
        public System.Nullable<int> TitleCount { get; set; }
        public System.Nullable<int> VolumeCount { get; set; }
    }
}