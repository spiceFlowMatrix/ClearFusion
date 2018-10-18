using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class AdvancesModel
    {
        public int? AdvancesId { get; set; }
        public DateTime AdvanceDate { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public int CurrencyId { get; set; }
        public long? VoucherReferenceNo { get; set; }
        public string Description { get; set; }
        public string ModeOfReturn { get; set; }
        public int ApprovedBy { get; set; }
        public double? RequestAmount { get; set; }
        public double AdvanceAmount { get; set; }
        public int OfficeId { get; set; }
        public int? NumberOfInstallments { get; set; }
    }

    public class AdvancesHistoryModel
    {
        public double InstallmentPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public double BalanceAmount { get; set; }
        public long? AdvanceId { get; set; }

    }
}
