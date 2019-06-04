﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class VoucherDetailModel : BaseModel
    {
        public long VoucherNo { get; set; }
        public int CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime VoucherDate { get; set; }
        public string ChequeNo { get; set; }
        public string ReferenceNo { get; set; }
        public string Description { get; set; }
        public int? JournalCode { get; set; }
        public string JournalName { get; set; }
        public int? VoucherTypeId { get; set; }
        public int? OfficeId { get; set; }
        public string OfficeName { get; set; }
        public long? ProjectId { get; set; }
        public long? BudgetLineId { get; set; }
        public int? FinancialYearId { get; set; }
        public string FinancialYearName { get; set; }
        public bool? IsVoucherVerified { get; set; }
        public int? TimezoneOffset { get; set; }
        public bool IsExchangeGainLossVoucher { get; set; } = false;
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
        public long AccountCode { get; set; }
        public string AccountName { get; set; }
		public string ChartOfAccountNewCode { get; set; }
        public int AccountLevelId { get; set; }
    }

    public class VoucherTransactionModel : BaseModel
    {        
        public long TransactionId { get; set; }
        public long? DebitAccount { get; set; }
        public long? CreditAccount { get; set; }
        public double? Amount { get; set; }
        public string Description { get; set; }
        public DateTime? TransactionDate { get; set; }
        public long? VoucherNo { get; set; }
        public int? CurrencyId { get; set; }
        public int? OfficeId { get; set; }
        public int? FinancialYearId { get; set; }
        public int? JournalId { get; set; }

        public string AccountName { get; set; }
        public string AccountCode { get; set; }
        public double? DebitAmount { get; set; }
        public double? CreditAmount { get; set; }

        //In Use
        public long? AccountNo { get; set; } //new COA ID ChartOfAccountNewId
        public double? Debit { get; set; }
        public double? Credit { get; set; }

		public double? AFGAmount { get; set; }
		public double? EURAmount { get; set; }
		public double? USDAmount { get; set; }
		public double? PKRAmount { get; set; }
        public long? ProjectId { get; set; }
        public long? BudgetLineId { get; set; }

    }


    public class LedgerModel
    {
        public long ChartOfAccountNewId { get; set; }
        public string ChartAccountName { get; set; }
        public string CurrencyName { get; set; }
        public string MainLevel { get; set; }
        public string ControlLevel { get; set; }
        public string SubLevel { get; set; }
        public double Amount { get; set; }

        public string TransactionType { get; set; }
        public string VoucherReferenceNo { get; set; }
        public int TransactionNo { get; set; }
        public string AccountName { get; set; }
        public DateTime? TransactionDate { get; set; }
        public double? DebitAmount { get; set; }
        public double? CreditAmount { get; set; }
        public string VoucherNo { get; set; }
        public string Description { get; set; }
        public List<Transaction> Transactionlist { get; set; }
		public double TotalCredit { get; set; }
		public double TotalDebit { get; set; }
        public string ChartOfAccountNewCode { get; set; }

    }

	public class AccountTransactionLogger {
		public long AccountCode { get; set; }
		public long ChartAccountCode { get; set; }
		public int? CurrencyId { get; set; }
		public double? TotalCredits { get; set; }
		public double? TotalDebits { get; set; }
		public double? ClosingBalance { get; set; }
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
        public double? DebitAmount { get; set; }
        public double? CreditAmount { get; set; }
    }
    public class AccountOpendingAndClosingBL
    {
        public double? ClosingBalance { get; set; }
        public double? OpeningBalance { get; set; }
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
    public class ExchangeGainLossVoucher : BaseModel
    {
        public long? BudgetLineId { get; set; }
        public string ChequeNo { get; set; }
        public string CurrencyCode { get; set; }
        public int CurrencyId { get; set; }
        public string Description { get; set; }
        public int? FinancialYearId { get; set; }
        public string FinancialYearName { get; set; }
        public int? JournalCode { get; set; }
        public string JournalName { get; set; }
        public int OfficeId { get; set; }
        public string OfficeName { get; set; }
        public int? ProjectId { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime VoucherDate { get; set; }
        public long? VoucherNo { get; set; }
        public string VoucherMode { get; set; }
        public int? VoucherTypeId { get; set; }
        public string VoucherType { get; set; }
        public long? AccountCode { get; set; }
        public double ExchangeGainLossAmount { get; set; }
        public string AccountName { get; set; }
        public int AccountCodeCredit { get; set; }
        public long ChartOfAccountNewIdDebit { get; set; }
        public string OfficeCode { get; set; }
    }

    public class ExchangeGainLossVoucherDetails : BaseModel
    {

        public long VoucherNo { get; set; }
        public int CurrencyId { get; set; }
        public string Description { get; set; }
        public int? JournalId { get; set; }
        public int? VoucherType { get; set; }
        public int? OfficeId { get; set; }
        public long? ProjectId { get; set; }
        public long? BudgetLineId { get; set; }
        public long CreditAccount { get; set; }
        public long DebitAccount { get; set; }
        public double Amount { get; set; }
    }

    public class GainLossVoucherList
    {
        public long VoucherId { get; set; }
        public string VoucherName { get; set; }
        public string JournalName { get; set; }
        public DateTime? VoucherDate { get; set; }
    }

}
