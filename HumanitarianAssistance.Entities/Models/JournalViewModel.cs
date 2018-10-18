using HumanitarianAssistance.ViewModels.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class JournalViewModel: PaginationModel
    {
		public int CurrencyId { get; set; }
		public DateTime fromdate { get; set; }
        public DateTime todate { get; set; }
        public List<int?> OfficesList { get; set; }
		public int RecordType { get; set; }
		public List<int?> JournalCode { get; set; }
        public long? Project { get; set; }
        public string BudgetLine { get; set; }
    }
}
