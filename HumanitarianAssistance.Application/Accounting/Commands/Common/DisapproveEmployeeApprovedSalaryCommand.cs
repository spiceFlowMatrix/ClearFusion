using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Commands.Common
{
    public class DisapproveEmployeeApprovedSalaryCommand : IRequest<ApiResponse>
    {
        public List<Employees> EmployeeList { get; set; }
        public int Month { get; set; }
        public int OfficeId { get; set; }
        public int Year { get; set; }
    }
    public class Employees
    {
        public int EmployeeId { get; set; }
    }
}