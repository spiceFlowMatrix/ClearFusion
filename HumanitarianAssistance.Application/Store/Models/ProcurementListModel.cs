using System;

namespace HumanitarianAssistance.Application.Store.Models
{
    public class ProcurementListModel
    {
        public long IssueId { get; set; }
        public DateTime IssueDate { get; set; }
        public string EmployeeName { get; set; }
        public int ProcuredAmount { get; set; }
        public bool MustReturn { get; set; }
        public bool Returned { get; set; }
        public DateTime? ReturnedOn { get; set; }
    }
}