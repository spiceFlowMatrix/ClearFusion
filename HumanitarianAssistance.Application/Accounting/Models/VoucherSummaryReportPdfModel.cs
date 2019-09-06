using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class VoucherSummaryReportPdfModel
    {
        public VoucherSummaryReportPdfModel()
        {
            TransactionDetails = new List<TransactionSummaryReportPdfModel>();
        }
        public string Currency { get; set; }
        public string Cheque { get; set; }
        public string VoucherNo { get; set; }
        public string Journal { get; set; }
        public string Date { get; set; }
        public string Region { get; set; }
        public string Description { get; set; }
        public IList<TransactionSummaryReportPdfModel> TransactionDetails { get; set; }
        public string TotalCredit { get; set; }
        public string TotalDebit { get; set; }
    }

    public class TransactionSummaryReportPdfModel
    {
        public string AccountNo { get; set; }
        public string Description { get; set; }
        public string Debit { get; set; }
        public string Credit { get; set; }
        public string BudgetLine { get; set; }
        public string Area { get; set; }
        public string Sector { get; set; }
        public string Program { get; set; }
        public string Project { get; set; }
        public string Job { get; set; }
    }
}