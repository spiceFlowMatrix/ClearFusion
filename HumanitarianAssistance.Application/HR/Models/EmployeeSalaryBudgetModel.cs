using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Models
{
 public  class EmployeeSalaryBudgetModel
    {
        public int EmployeeSalaryBudgetId { get; set; }
        public string Year { get; set; }
        public int CurrencyId { get; set; }
        public double SalaryBudget { get; set; }
        public double BudgetDisbursed { get; set; }
        public int EmployeeID { get; set; }
    }
}
