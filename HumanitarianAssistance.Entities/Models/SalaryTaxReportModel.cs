using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class SalaryTaxReportModel
    {
		public DateTime Date { get; set; }
		public string Office { get; set; }
		public string Currency { get; set; }
        public int? CurrencyId { get; set; }
        public double? TotalTax { get; set; }
	}

    public class PensionPaymentModel
    {
        public int? EmployeeId { get; set; }
        public decimal TotalPensionAmount { get; set; }
        public decimal PensionAmountPaid { get; set; }
        public decimal BalancePensionAmount { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public int? CurrencyId { get; set; }
    }

    public class EmployeePensionPaymentModel
    {
        public int? EmployeeId { get; set; }
        public decimal PensionAmount { get; set; }
        public int? CurrencyId { get; set; }
        public int? DebitAccount { get; set; }
        public int? CreditAccount { get; set; }
        public int? OfficeId { get; set; }
        public string CreatedById { get; set; }
        public int? JournalId { get; set; }
        public int VoucherTypeId { get; set; }
        public int? JournalCode { get; set; }
    }

    public class PensionPaymentHistoryModel
    {
        public string Employee { get; set; }
        public decimal PensionPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public long VoucherNo { get; set; }
        public string VoucherReferenceNo { get; set; }
    }
}
