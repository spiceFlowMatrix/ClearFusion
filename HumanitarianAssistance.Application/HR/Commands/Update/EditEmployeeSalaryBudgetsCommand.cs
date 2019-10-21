using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
  public class EditEmployeeSalaryBudgetsCommand : BaseModel, IRequest<ApiResponse>
    {
        public int EmployeeSalaryBudgetId { get; set; }
        public string Year { get; set; }
        public int CurrencyId { get; set; }
        public double SalaryBudget { get; set; }
        public double BudgetDisbursed { get; set; }
        public int EmployeeID { get; set; }
    }
}
