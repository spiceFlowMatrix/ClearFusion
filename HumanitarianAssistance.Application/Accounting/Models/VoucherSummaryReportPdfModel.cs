using System.Collections.Generic;
using System;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class VoucherSummaryMainReportPdfModel
    {
        public VoucherSummaryMainReportPdfModel()
        {
            VoucherDetails = new List<VoucherSummaryReportPdfModel>();
        }
        public string Logo { get; set; }
        public string RecordTypeText { get; set; }
        public IList<VoucherSummaryReportPdfModel> VoucherDetails { get; set; }
    }
    public class VoucherSummaryReportNewPdfModel
    {
        public long VoucherNo { get; set; }  
        public string ReferenceNo { get; set; } 
        public string ChequeNo { get; set; }   
        public DateTime VoucherDate { get; set; }   
        public string JournalName { get; set; }      
        public string OfficeName { get; set; }  
        public string CurrencyCode { get; set; }
        public string VoucherDescription { get; set; } 
        public string TransactionDescription { get; set; }
        public double Credit { get; set; }
        public double Debit { get; set; }
        public string ChartOfAccountNewCode { get; set; }
        public string ProjectCode { get; set; }  
        public string BudgetCode { get; set; }  
        public string ProjectJobCode { get; set; } 
        public string SectorCode { get; set; }
    }
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