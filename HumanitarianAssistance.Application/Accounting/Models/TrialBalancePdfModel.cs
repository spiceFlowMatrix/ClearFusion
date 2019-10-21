using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class TrialBalancePdfModel
    {
        public string Logo { get; set; }
        public List<TrialBalanceMainPdfList> reportList { get; set; }
        public double? TotalDebit { get; set; }
        public double? TotalCredit { get; set; }
    }
    public class TrialBalanceMainPdfList
    {
        public string LineDescription { get; set; }
        public double? DebitAmount { get; set; }
        public double? CreditAmount { get; set; }
    }

    public class JournalTrialBalanceMainPdfList
    {
        public string Group { get; set; }
        public string AccountDescription { get; set; }
        public double? DebitAmount { get; set; }
        public double? CreditAmount { get; set; }
        public double? Balance { get; set; }
    }

    public class JournalTrialBalancePdfModel
    {
        public string Logo { get; set; }
        public List<JournalTrialBalanceMainPdfList> reportList { get; set; }
        public double? TotalDebit { get; set; }
        public double? TotalCredit { get; set; }
        public double? TotalBalance { get; set; }
    }
}