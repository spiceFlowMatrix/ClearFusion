using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class JournalReportPdfModel
    {
        public string TransactionDate { get; set; }
        public string AccountCode { get; set; }
        public string TransactionDescription { get; set; }
        public string Currency { get; set; }
        public string Project { get; set; }
        public string BudgetLine { get; set; }
        public double? CreditAmount { get; set; }
        public double? DebitAmount { get; set; }
        public string ReferenceNo { get; set; }
        public string Program { get; set; }
        public double? Balance { get; set; }
    }

    public class MainJournalReportPdfModel
    {
        public string Logo { get; set; }
        public List<JournalReportPdfModel> reportList { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public double? TotalDebit { get; set; }
        public double? TotalCredit { get; set; }
    }
}
