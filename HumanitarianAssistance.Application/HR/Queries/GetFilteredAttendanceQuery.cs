using System;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetFilteredAttendanceQuery:IRequest<object>
    {
        public int EmployeeId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }

        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public int? TotalCount { get; set; }
    }
}