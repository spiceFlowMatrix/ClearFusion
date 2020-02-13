using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class ApproveEmployeePayrollByIdsCommand : BaseModel, IRequest<object>
    {
        public List<EmployeePayrollAdministrationModel> EmployeePayrollList { get; set; }
    }

    public class EmployeePayrollAdministrationModel
    {
        public double GrossSalary { get; set; }
        public double NetSalary { get; set; }
        public int Month { get; set; }
        public int EmployeeId { get; set; }
        public List<EmployeeAccumulatedSalaryHead> SalaryHeadList { get; set; }
    }

    public class EmployeeAccumulatedSalaryHead
    {
        public int Id { get; set; }
        public double SalaryAllowance { get; set; }
        public double SalaryDeduction { get; set; }
    }
}