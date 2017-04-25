using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Models
{
    [Table("vea_SensorDataView")]
    public class PatronCounter
    {
		[Key]
        public int RecordId { get; set; }
        public int ServerId { get; set; }
        public int SensorId { get; set; }
        public DateTime ServerDate { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal ValueA { get; set; }
        public decimal ValueB { get; set; }
        public string State { get; set; }
        public decimal Humidity { get; set; }
        public decimal Temperature { get; set; }
        public decimal Analog { get; set; }
        public int SensorType { get; set; }
        public bool InHrOfOp { get; set; }
        public decimal RawA { get; set; }
        public decimal RawB { get; set; }
        public DateTime UTCDate { get; set; }
    }
}