using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Configuration.Models
{
   public class EmployeePensionRateModel 
    {
        public int FinancialYearId { get; set; }
        public string FinancialYearName { get; set; }
        public double? PensionRate { get; set; }
        public bool IsDefault { get; set; }
    }
}
