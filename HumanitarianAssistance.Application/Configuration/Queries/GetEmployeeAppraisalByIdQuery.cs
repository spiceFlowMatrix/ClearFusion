using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetEmployeeAppraisalByIdQuery: IRequest<ApiResponse>
    {
        public long EmployeeId { get; set; }
        public DateTime CurrentAppraisalDate { get; set; }
    }
}