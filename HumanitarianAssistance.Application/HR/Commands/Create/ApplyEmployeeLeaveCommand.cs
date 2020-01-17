using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class ApplyEmployeeLeaveCommand: BaseModel, IRequest<object>
    {
        public long ApplyLeaveId { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int LeaveReasonId { get; set; }
        public string LeaveReasonName { get; set; }
        public int? ApplyLeaveStatusId { get; set; } //Approved, Not Approved, Rejected
        public string ApplyLeaveStatus { get; set; }
        public string Remarks { get; set; }
        public int? DepartmentId { get; set; }
        public int TotalLeaveCount { get; set; }
    }
}