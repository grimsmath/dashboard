using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Dashboard.ViewModels
{
    public class PatronCounterQueryViewModel
    {
        [DisplayName("Start Date / Time")]
        public DateTime StartDateTime { get; set; }

        [DisplayName("End Date / Time")]
        public DateTime EndDateTime { get; set; }

        [DisplayName("Query Name")]
        public string QueryName { get; set; }

        [DisplayName("Desired Time Interval")]
        public TimeSpan Interval { get; set; }
    }
}