using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class SalaryTaxModel
    {
		public int OfficeId { get; set; }
		public int EmployeeId { get; set; }
		public int FinancialYearId { get; set; }
        public int? CurrencyId { get; set; }
    }
}
