using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class EmpPayrollDetailTestTable : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int PId { get; set; }
        public int? EmployeeId { get; set; }
        public int? payrollmonth { get; set; }
        public int? payrollyear { get; set; }
        public string currencycode { get; set; }
        public string regcode { get; set; }
        public double? basicpay { get; set; }
        public double? TotalAllowance { get; set; }
        public double? TotalDeduction { get; set; }
        public bool? Sent { get; set; }
        public bool? Status { get; set; }
        public int? Attendance { get; set; }
        public int? Absent { get; set; }
        public int? TotalDuration { get; set; }
        public float? FoodAllowance { get; set; }
        public float? TRAllowance { get; set; }
        public float? OtherAllowance { get; set; }
        public float? Medical { get; set; }
        public float? Other1 { get; set; }
        public float? Other2 { get; set; }
        public float? AdvanceDeduction { get; set; }
        public float? SalaryTaxDeduction { get; set; }
        public float? FineDeduction { get; set; }

        public float? OtherDeduction { get; set; }
        public float? CapacityBuildingDeductibles { get; set; }

        public float? SecurityDeduction { get; set; }
        public float? PensionDeduction { get; set; }
    }
}