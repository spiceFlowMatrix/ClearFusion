using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class EmployeePayrollDetail : BaseEntity
    {        
        public DateTime? PaymentDate { get; set; }
        [Key, Column(Order = 1)]
        public int PayrollMonth { get; set; }
        [Key, Column(Order = 2)]
        public int PayrollYear { get; set; }

        [Key, Column(Order = 3)]
        [StringLength(10)]
        public string CurrencyCode { get; set; }

        [Key, Column(Order = 4)]
        [StringLength(10)]
        public string RegCode { get; set; }

        [Key, Column(Order = 5)]
        public int EmployeeID { get; set; }

        public float? BasicPay { get; set; }
        public float? FoodAllowance { get; set; }
        public float? TrAllowance { get; set; }
        public float? OtherAllowance { get; set; }
        public float? Medical { get; set; }
        public float? Other1 { get; set; }

        [StringLength(100)]
        public string Other1Description { get; set; }
        public float? Other2 { get; set; }
        [StringLength(100)]
        public string Other2Description { get; set; }
        public float? AdvanceDeduction { get; set; }
        public float? SalaryTaxDeduction { get; set; }
        public float? FineDeduction { get; set; }
        public float? OtherDeduction { get; set; }
        [StringLength(10)]
        public string Donor { get; set; }
        [StringLength(10)]
        public string Sector { get; set; }
        [StringLength(10)]
        public string Program { get; set; }
        [StringLength(10)]
        public string Project { get; set; }
        [StringLength(10)]
        public string Job { get; set; }
        [StringLength(10)]
        public string CostBook { get; set; }
        [StringLength(10)]
        public string Area { get; set; }
        [StringLength(10)]
        public string AccountCode { get; set; }
        public Boolean? Sent { get; set; }
        public Boolean? Status { get; set; }
        public float? BudgetBalance { get; set; }
        public float? CapacityBuildingDeductibles { get; set; }
        public float? SecurityDeduction { get; set; }
        public float? PensionDeduction { get; set; }
        public float? Attendance { get; set; }
        public float? Absent { get; set; }
        public int? TotalDuration { get; set; }
    }
}
