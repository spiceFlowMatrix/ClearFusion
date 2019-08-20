using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Models
{
   public class EmployeeSalaryAnalyticalInfoModel
    {
        public int EmployeeSalaryAnalyticalInfoId { get; set; }
        public int? AccountCode { get; set; }
        public long ProjectId { get; set; }
        public long BudgetLineId { get; set; }
        public double SalaryPercentage { get; set; }
        public int EmployeeID { get; set; }

        public string BudgetLineName { get; set; }
    }
}
