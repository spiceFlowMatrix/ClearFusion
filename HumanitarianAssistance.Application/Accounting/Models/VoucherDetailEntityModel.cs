using System;
using HumanitarianAssistance.Application.Infrastructure;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class VoucherDetailEntityModel : BaseModel
    {
        public long VoucherNo { get; set; }
        public int? CurrencyId { get; set; }
        public DateTime VoucherDate { get; set; }
        public string ChequeNo { get; set; }
        public string ReferenceNo { get; set; }
        public string Description { get; set; }
        public int? JournalCode { get; set; }
        public int? VoucherTypeId { get; set; }
        public int? OfficeId { get; set; }
        public long? ProjectId { get; set; }
        public long? BudgetLineId { get; set; }
        public int? FinancialYearId { get; set; }
        public string CurrencyCode { get; set; }
        public string VoucherType { get; set; }
        public string VoucherMode { get; set; }
        public string OfficeCode { get; set; }
        public bool IsExchangeGainLossVoucher { get; set; } = false;
        public bool IsVoucherVerified { get; set; } = false;
    }
}