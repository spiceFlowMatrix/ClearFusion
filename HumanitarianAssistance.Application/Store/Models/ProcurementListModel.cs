using System;

namespace HumanitarianAssistance.Application.Store.Models
{
    public class ProcurementListModel
    {
        public long OrderId { get; set; }
        public DateTime IssueDate { get; set; }
        public string EmployeeName { get; set; }
        public int ProcuredAmount { get; set; }
        public bool MustReturn { get; set; }
        public bool Returned { get; set; }
        public DateTime? ReturnedOn { get; set; }
        public int EmployeeId { get; set; }
        public long ProjectId { get; set; }
        public string LocationId { get; set; }
        public long StatusId { get; set; }
    }
}