using System;

namespace HumanitarianAssistance.Application.HR.Models
{
    public class AdvanceModel
    {
        public int AdvancesId { get; set; }
		public DateTime AdvanceDate { get; set; }
		public int EmployeeId { get; set; }
		public string EmployeeCode { get; set; }
		public int CurrencyId { get; set; }
		public long VoucherReferenceNo { get; set; }
		public string Description { get; set; }
		public string ModeOfReturn { get; set; }
		public int ApprovedBy { get; set; }
		public double RequestAmount { get; set; }
		public double AdvanceAmount { get; set; }
		public int OfficeId { get; set; }
		public bool IsApproved { get; set; }        // Is advance approved by manager or not (false means not approved)
		public bool IsDeducted { get; set; }        // Is advance amount deducted from next month salary when defining monthly salary payroll (false means not distributed)
		public string EmployeeName { get; set; }
        public int? NumberOfInstallments { get; set; }
    }
}