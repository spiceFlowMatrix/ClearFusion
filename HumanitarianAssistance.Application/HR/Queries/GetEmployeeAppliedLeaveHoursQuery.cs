using System;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeAppliedLeaveHoursQuery: IRequest<object>
    {
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}