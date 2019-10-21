using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HumanitarianAssistance.Domain.Entities.Accounting;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public  class EmployeePayroll : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long PayrollId { get; set; }
        public int EmployeeID { get; set; }
        public EmployeeDetail EmployeeDetails { get; set; }
        public int? SalaryHeadId { get; set; }
        public SalaryHeadDetails SalaryHeadDetails { get; set; }
        public double? MonthlyAmount { get; set; }
        public int? CurrencyId { get; set; }
        public CurrencyDetails CurrencyDetails { get; set; }
        public int? PaymentType { get; set; }
        public int? HeadTypeId { get; set; }
        public double? PensionRate { get; set; }
        public string CurrencyCode { get; set; }
        public double? BasicPay { get; set; }
        public int? AllowDeductionFlag { get; set; }
        public long? AccountNo { get; set; }
        public int? TransactionTypeId { get; set; }
        [ForeignKey("AccountNo")]
        public ChartOfAccountNew ChartOfAccountNew { get; set; }

    }
}
