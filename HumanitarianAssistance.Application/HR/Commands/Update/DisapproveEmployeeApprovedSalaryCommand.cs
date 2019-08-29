using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class DisapproveEmployeeApprovedSalaryCommand:BaseModel,IRequest<ApiResponse>
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
