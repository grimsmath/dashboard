using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.ViewModels
{
    public class TicklerViewModel
    {
        public int id { get; set; }
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int ActivityTypeID { get; set; }
        public string ActivityType { get; set; }
        public int SubLibrary1 { get; set; }
        public string SubLibrary1Name { get; set; }
        public int SubLibrary2 { get; set; }
        public string SubLibrary2Name { get; set; }
        public int TitleCount { get; set; }
        public int VolumeCount { get; set; }
        public DateTime Created { get; set; }
    }
}