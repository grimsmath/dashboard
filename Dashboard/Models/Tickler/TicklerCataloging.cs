using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dashboard.Models
{
    [Table("VIEW_TICKLER_STATS_CATALOGING_BY_TITLE_1")]
    public class TicklerCatalogingByTitle1
    {
        [Key, Column(Order = 0)]
        public int Year { get; set; }

        [Key, Column(Order = 1)]
        public int Month { get; set; }

        [Key, Column(Order = 2)]
        public string Cataloger { get; set; }

        public int TitleCount { get; set; }
    }

    [Table("VIEW_TICKLER_STATS_CATALOGING_BY_VOLUME_1")]
    public class TicklerCatalogingByVolume1
    {
        [Key, Column(Order = 0)]
        public int Year { get; set; }

        [Key, Column(Order = 1)]
        public int Month { get; set; }

        [Key, Column(Order = 2)]
        public string Cataloger { get; set; }

        public int VolumeCount { get; set; }
    }

    [Table("VIEW_TICKLER_STATS_CATALOGING_BY_COLLECTION_1")]
    public class TicklerCatalogingByCollection
    {
        [Key, Column(Order = 0)]
        public int Year { get; set; }

        [Key, Column(Order = 1)]
        public int Month { get; set; }

        [Key, Column(Order = 2)]
        public string SubLibrary1 { get; set; }

        public int TitleCount { get; set; }
        public int VolumeCount { get; set; }
    }
}