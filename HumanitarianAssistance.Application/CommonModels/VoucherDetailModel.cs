using System;
using HumanitarianAssistance.Application.Infrastructure;

namespace HumanitarianAssistance.Application.CommonModels
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
}