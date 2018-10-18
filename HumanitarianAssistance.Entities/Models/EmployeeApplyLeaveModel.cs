using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class EmployeeApplyLeaveModel : BaseModel
    {
        public long ApplyLeaveId { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        //public int LeaveTypeId { get; set; } //H
        //public string LeaveTypeName { get; set; }
        public int LeaveReasonId { get; set; }
        public string LeaveReasonName { get; set; }
        public int? ApplyLeaveStatusId { get; set; } //Approved, Not Approved, Rejected
        public string ApplyLeaveStatus { get; set; }
        public string Remarks { get; set; }
    }
}
