using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class SPVoucherSummaryReportPdfModel
    {
        // public SPVoucherSummaryReportPdfModel()
        // {
        //     TransactionDetails = new List<SPTransactionSummaryReportPdfModel>();
        // }
        public string Currency { get; set; }
        public string Cheque { get; set; }
        public string VoucherNo { get; set; }
        public string Journal { get; set; }
        public string VoucherDate { get; set; }
        public string Region { get; set; }
        public string Description { get; set; }
        public string TotalCredit { get; set; }
        public string TotalDebit { get; set; }

        // public IList<SPTransactionSummaryReportPdfModel> TransactionDetails { get; set; }

        
        public string AccountNo { get; set; }
        public string TransactionDescription { get; set; }
        public string Debit { get; set; }
        public string Credit { get; set; }
        public string BudgetLine { get; set; }
        public string Area { get; set; }
        public string Sector { get; set; }
        public string Program { get; set; }
        public string Project { get; set; }
        public string Job { get; set; }
    }

    // public class SPTransactionSummaryReportPdfModel
    // {
    //     public string AccountNo { get; set; }
    //     public string Description { get; set; }
    //     public string Debit { get; set; }
    //     public string Credit { get; set; }
    //     public string BudgetLine { get; set; }
    //     public string Area { get; set; }
    //     public string Sector { get; set; }
    //     public string Program { get; set; }
    //     public string Project { get; set; }
    //     public string Job { get; set; }
    // }
}