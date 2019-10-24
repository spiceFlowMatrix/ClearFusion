using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class EmployeeSummaryModel : BaseModel
    {
		public int EmployeeTypeId { get; set; }
		public int RecordType { get; set; }
		public int OfficeId { get; set; }
		public int Year { get; set; }
		public int? Month { get; set; }		
		public int? CurrencyId { get; set; }
		public int? AllowanceId { get; set; }
		public int? DeductionId { get; set; }
	}
}
