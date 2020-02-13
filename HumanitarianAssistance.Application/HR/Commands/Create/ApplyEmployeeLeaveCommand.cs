using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class ApplyEmployeeLeaveCommand: BaseModel, IRequest<object>
    {
        public int? EmployeeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; } 
        public int LeaveReasonId { get; set; }
        public string Remarks { get; set; }
        public int LeaveApplied { get; set; }
    }
}