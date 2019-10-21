using System.Collections.Generic;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditEmployeeAccountSalaryCommand: BaseModel, IRequest<ApiResponse>
    {
        public List<EmployeePayrollAccountModel> EmployeePayrollAccount { get; set; }
    }
}