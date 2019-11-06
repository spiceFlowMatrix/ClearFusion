using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.AccountingNew
{
    public class ExchangeGainLossFilterModel
    {
        public int ToCurrencyId { get; set; }
        public DateTime ComparisionDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime FromDate { get; set; }
        public List<int?> OfficeIdList { get; set; }
        public List<int?> JournalIdList { get; set; }
        public List<long?> ProjectIdList { get; set; }
        public List<long?> AccountIdList { get; set; }
    }
}
