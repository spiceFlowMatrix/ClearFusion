using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class JournalLedgerReportModel
    {
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public double TotalDebit { get; set; }  
        public double TotalCredit { get; set; }
        public double Balance { get; set; }
        public List<AccountTransactionList> TransactionList { get; set; }
    }

    public class AccountTransactionList
    {
        public string Group { get; set; }
        public string TransactionDate { get; set; }
        public string VoucherNo { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public double Rate { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public bool IsVoucherVerified { get; set; }
    }
    public class JournalLedgerMainReportModel
    {
        public string Logo { get; set; }
        public List<JournalLedgerReportModel> mainList { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string RecordType { get; set; }
        public string Currency { get; set; }
    }
}