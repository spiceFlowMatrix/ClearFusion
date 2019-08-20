using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HumanitarianAssistance.Domain.Entities.HR;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class Advances : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int AdvancesId { get; set; }
        public DateTime AdvanceDate { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public EmployeeDetail EmployeeDetail { get; set; }
        public string EmployeeCode { get; set; }
        public int CurrencyId { get; set; }
        public CurrencyDetails CurrencyDetails { get; set; }
        public long VoucherReferenceNo { get; set; }
        public string Description { get; set; }
        public string ModeOfReturn { get; set; }
        public int ApprovedBy { get; set; }
        public double RequestAmount { get; set; }
        public double AdvanceAmount { get; set; }
        public int OfficeId { get; set; }
        public bool IsApproved { get; set; }        // Is advance approved by manager or not (false means not approved)
        public bool IsDeducted { get; set; }        // Is advance amount deducted from next month salary when defining monthly salary payroll (false means not distributed)
        public DateTime AppraisalApprovedDate { get; set; }
        public DateTime DeductedDate { get; set; }
        public bool IsAdvanceRecovery { get; set; }
        public DateTime AdvanceRecoveryDate { get; set; }
        public int? NumberOfInstallments { get; set; }
        public double RecoveredAmount { get; set; }
    }
}
