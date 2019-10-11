using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class LedgerReportPdfModel
    {
        public string Logo { get; set; }
        public List<LedgerReportMainPdfList> reportList { get; set; }
        public double? TotalDebit { get; set; }
        public double? TotalCredit { get; set; }
    }
    public class LedgerReportMainPdfList
    {
        public string VoucherDate { get; set; }
        public string ReferenceNo { get; set; }
        public string LineDescription { get; set; }
        public double? DebitAmount { get; set; }
        public double? CreditAmount { get; set; }
        public string Currency { get; set; }
        public string StatusText { get; set; }
    }
}