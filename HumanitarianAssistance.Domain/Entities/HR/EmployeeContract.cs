using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class EmployeeContract: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int EmployeeContractId { get; set; }
        public int EmployeeId { get; set; }
        public string FatherName { get; set; }
        public string EmployeeCode { get; set; }
        public int? Designation { get; set; } //id
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public int? DurationOfContract { get; set; }
        public double? Salary { get; set; }
        public int? Grade { get; set; }
        public int? DutyStation { get; set; } //if
        public int? Country { get; set; } //id
        public int? Province { get; set; } //id
        public int? Project { get; set; }
        public long? BudgetLine { get; set; }
        public string Job { get; set; }
        public int? WorkTime { get; set; }
        public int? WorkDayHours { get; set; }
        public string ContractStatus { get; set; }
        public string PeriodType { get; set; }
        public float? ContractNumber { get; set; }
        public float? ContractPeriod { get; set; }
        [ForeignKey("EmployeeId")]
        public EmployeeDetail Employee { get; set; }
        public string CountryDari { get; set; }
        public string DesignationDari { get; set; }
        public string DutyStationDari { get; set; }
        public string FatherNameDari { get; set; }
        public string GradeDari { get; set; }
        public string JobDari { get; set; }
        public string ProvinceDari { get; set; }
        public string EmployeeNameDari { get; set; }
        public string ProjectNameDari { get; set; }
        [ForeignKey("Grade")]
        public JobGrade JobGrade { get; set; }
        public string BudgetLineDari { get; set; }

    }
}
