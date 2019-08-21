using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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

        //public string CurrencyCode { get; set; }
        //public float? FoodAllowance { get; set; }
        //public float? MedicalAllowance { get; set; }
        //public float? TrAllowance { get; set; }
        //public float? OtherAllowance { get; set; }
        //public float? CapacityBuildingDeductibles { get; set; }
        //public float? SecurityDeductibles { get; set; }
        //public float? FinesDeductibles { get; set; }
        //public float? OtherDeductibles { get; set; }
        //public float? PensionDeductibles { get; set; }
        //public float? CasualLeaveAllowance { get; set; }
        //public float? MedicalLeaveAllowance { get; set; }
        //public float? EmergencyLeaveAllowance { get; set; }
        //public float? MaternityLeaveAllowance { get; set; }
        //public float? HourlyRate { get; set; }
        //public float? SalaryTaxDeductibles{get; set;}
        //public float? AdvancesDeductibles { get; set; }

    }
}
