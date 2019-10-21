using System.Collections.Generic;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class EmployeeApprovePayrollCommand: BaseModel, IRequest<ApiResponse>
    {
        public List<EmployeeMonthlyPayrollModel> EmployeeMonthlyPayroll { get; set; }
    }
}