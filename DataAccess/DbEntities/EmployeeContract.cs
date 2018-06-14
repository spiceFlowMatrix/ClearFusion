using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public class EmployeeContract: BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int EmployeeContractId { get; set; }

        public int EmployeeId { get; set; }
        public string FatherName { get; set; }
        public string EmployeeCode { get; set; }
        public int Designation { get; set; } //id
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public int DurationOfContract { get; set; }

        public double? Salary { get; set; }
        public int? Grade { get; set; }
        public int DutyStation { get; set; } //if
        public int Country { get; set; } //id
        public int Province { get; set; } //id
        public int Project { get; set; }
        public long BudgetLine { get; set; }
        public string Job { get; set; }
        public int WorkTime { get; set; }
        public int? WorkDayHours { get; set; }

        //[ForeignKey("Country")]
        //public CountryDetails CountryDetails { get; set; }

        //[ForeignKey("Province")]
        //public ProvinceDetails ProvinceDetails { get; set; }

        [ForeignKey("EmployeeId")]
        public EmployeeDetail EmployeeDetail { get; set; }

        [ForeignKey("DutyStation")]
        public OfficeDetail OfficeDetail { get; set; }

        [ForeignKey("Designation")]
        public DesignationDetail DesignationDetail { get; set; }

        [ForeignKey("BudgetLine")]
        public ProjectBudgetLine ProjectBudgetLine { get; set; }

        

    }
}
