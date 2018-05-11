using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class LedgerModels
    {
		public int CurrencyId { get; set; }
		public DateTime fromdate { get; set; }
		public DateTime todate { get; set; }
		public int RecordType { get; set; }
		public List<int> accountLists { get; set; }
	}
}
