using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dashboard.Models
{
    [Table("LibAnswer")]
    public class LibAnswer
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Question { get; set; }
        public string Details { get; set; }
        public string Answer { get; set; }
        public string Notes { get; set; }
        public string EnteredBy { get; set; }
        public string WhereReceived { get; set; }
        public string QuestionType { get; set; }
        public string AnswerMethod { get; set; }
        public string TransactionDuration { get; set; }
    }
}