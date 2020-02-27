using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class ApproveEmployeeMonthlyPayrollCommand: BaseModel, IRequest<object>
    {
        public double GrossSalary { get; set; }
        public double NetSalary { get; set; }
        public int Month { get; set; }
        public int EmployeeId { get; set; }
        public double HourlyRate { get; set; }
        public List<AccumulatedSalaryHead> SalaryHeadList { get; set; }
    }

    public class AccumulatedSalaryHead
    {
        public int Id { get; set; }
        public double SalaryAllowance { get; set; }
        public double SalaryDeduction { get; set; }
    }
}