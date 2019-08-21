using System.Collections.Generic;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditEmployeeSalaryDetailCommand: BaseModel, IRequest<ApiResponse>
    {
        public List<EmployeePayrollModel> EmployeePayroll { get; set; }
    }
}