using HumanitarianAssistance.ViewModels.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.AccountingNew
{
    public class VoucherSummaryFilterModel : PagingModel
    {
        public List<long> Accounts { get; set; }
        public List<long> BudgetLines { get; set; }
        public int Currency { get; set; }
        public List<int> Journals { get; set; }
        public List<int> Offices { get; set; }
        public List<long> ProjectJobs { get; set; }
        public List<long> Projects { get; set; }
        public int RecordType { get; set; }
    }

    public class TransactionFilterModel
    {
        public int VoucherNo { get; set; }
        public int CurrencyId { get; set; }
        public int RecordType { get; set; }
    }
}
