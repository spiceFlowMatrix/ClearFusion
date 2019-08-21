using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class EmployeePensionRate : BaseEntity
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1)]
		public int EmployeePensionRateId { get; set; }
		public int FinancialYearId { get; set; }
		public FinancialYearDetail FinancialYearDetail { get; set; }
		public double? PensionRate { get; set; }
		public bool IsDefault { get; set; }
	}
}
