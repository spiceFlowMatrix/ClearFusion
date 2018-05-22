using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class CategoryPopulatorModel
    {
		public int CategoryPopulatorId { get; set; }
		public string SubCategoryLabel { get; set; }
		public long ChartOfAccountCode { get; set; }
		//public string AccountName { get; set; }
		public int AccountTypeId { get; set; }
		public int ValueSource { get; set; }			// 1 - Balance, 2 - Credit, 3 - Debit
	}
}
