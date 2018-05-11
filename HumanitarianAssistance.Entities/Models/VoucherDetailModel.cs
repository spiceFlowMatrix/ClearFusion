using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class VoucherDetailModel : BaseModel
    {
        public long VoucherNo { get; set; }
        public int CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime? VoucherDate { get; set; }
        public string ChequeNo { get; set; }
        public string ReferenceNo { get; set; }
        public string Description { get; set; }
        public int? JournalCode { get; set; }
        public string JournalName { get; set; }
        public int? VoucherTypeId { get; set; }
        public int? OfficeId { get; set; }
        public string OfficeName { get; set; }
        public long ? ProjectId { get; set; }
        public long? BudgetLineId { get; set; }
        public int? FinancialYearId { get; set; }
        public string FinancialYearName { get; set; }

    }

    public class VoucherTypeModel
    {
        public int VoucherTypeId { get; set; }
        public string VoucherTypeName { get; set; }
    }

    public class VoucherDocumentDetailModel : BaseModel
    {
        public int DocumentID { get; set; }
        public string DocumentName { get; set; }
        public string FilePath { get; set; }
		public DateTime? DocumentDate { get; set; }
        public long VoucherNo { get; set; }
		public string DocumentGUID { get; set; }
    }

    public class AccountDetailModel
    {
        public int AccountCode { get; set; }
        public string AccountName { get; set; }
		public long ChartOfAccountCode { get; set; }
    }

    public class VoucherTransactionModel : BaseModel
    {        
        public int TransactionId { get; set; }
        public int? DebitAccount { get; set; }
        public int? CreditAccount { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public long VoucherNo { get; set; }
        public int? CurrencyId { get; set; }
        public int? OfficeId { get; set; }
        public int? FinancialYearId { get; set; }

        public string AccountName { get; set; }
        public double? DebitAmount { get; set; }
        public double? CreditAmount { get; set; }
    }

    //public class LedgerModel
    //{
    //    public int AccountCode { get; set; }
    //    public string ChartAccountName { get; set; }
    //    public string CurrencyName { get; set; }
    //    public string MainLevel { get; set; }
    //    public string ControlLevel { get; set; }
    //    public string SubLevel { get; set; }

    //    public string TransactionType { get; set; }

    //    //public int TransactionNo { get; set; }
    //    //public string AccountName { get; set; }
    //    //public DateTime TransactionDate { get; set; }
    //    //public double DebitAccount { get; set; }
    //    //public double CreditAccount { get; set; }
    //    //public long VoucherNo { get; set; }
    //    //public string Description { get; set; }
    //    public List<Transaction> Transactionlist { get; set; }
    //}

    //public class Transaction
    //{
    //    public int TransactionNo { get; set; }
    //    public string AccountName { get; set; }
    //    public DateTime TransactionDate { get; set; }
    //    public double DebitAmount { get; set; }
    //    public double CreditAmount { get; set; }
    //    public long VoucherNo { get; set; }
    //    public string Description { get; set; }

    //}

    public class LedgerModel
    {
        public long AccountCode { get; set; }
        public string ChartAccountName { get; set; }
        public string CurrencyName { get; set; }
        public string MainLevel { get; set; }
        public string ControlLevel { get; set; }
        public string SubLevel { get; set; }
        public double Amount { get; set; }

        public string TransactionType { get; set; }

        public int TransactionNo { get; set; }
        public string AccountName { get; set; }
        public DateTime TransactionDate { get; set; }
        public double? DebitAmount { get; set; }
        public double? CreditAmount { get; set; }
        public string VoucherNo { get; set; }
        public string Description { get; set; }
        public List<Transaction> Transactionlist { get; set; }
		public double TotalCredit { get; set; }
		public double TotalDebit { get; set; }

	}

	public class AccountTransactionLogger {
		public long AccountCode { get; set; }
		public long ChartAccountCode { get; set; }
		public int? CurrencyId { get; set; }
		public double TotalCredits { get; set; }
		public double TotalDebits { get; set; }
		public double ClosingBalance { get; set; }
	}

	public class Transaction
    {
        public int TransactionNo { get; set; }
        public string AccountName { get; set; }
        public DateTime TransactionDate { get; set; }
        public double DebitAmount { get; set; }
        public double CreditAmount { get; set; }
        public long VoucherNo { get; set; }
        public string Description { get; set; }
        public string AccountType { get; set; }

    }

    public class TrailBlance
    {
        public string CurrencyName { get; set; }
        public string AccountName { get; set; }
        public double DebitAmount { get; set; }
        public double CreditAmount { get; set; }
    }
    public class AccountOpendingAndClosingBL
    {
        public double? ClosingBalance { get; set; }
        public double OpeningBalance { get; set; }
        public string OpenningBalanceType { get; set; }
        public string ClosingBalanceType { get; set; }
    }

    public class LedgerValueModel
    {
        int? _currencyId=null;
        int? _recordType=null;
        
        public int? CurrencyId
        {
            get { return this._currencyId ?? 1; }
            set
            {
                this._currencyId = value;
            }
        }
        public int ? RecordType
        {
            get { return this._recordType ?? 1; }
            set
            {
                this._recordType = value;
            }
        }
       
          
        

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        //public int? OfficeCode { get; set; }
        //public int? AccountId { get; set; }
		public List<int> accountLists { get; set; }


	}
}
