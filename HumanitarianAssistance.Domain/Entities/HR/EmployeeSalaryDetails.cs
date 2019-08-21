using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public  class EmployeeSalaryDetails : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long SalaryId { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeDetail EmployeeDetails { get; set; }
        public int CurrencyId { get; set; }
        public CurrencyDetails CurrencyDetails { get; set; }
        //public double? MonthlySalary { get; set; }
        //public double? FoodAllowance { get; set; }
        //public double? TRAllowance { get; set; }
        //public double? MedicalAllowance { get; set; }
        //public double? OtherAllowance { get; set; }  
        //public double? ProvidentFund { get; set; }
        //public double? Otherdeduction { get; set; }
        public double? TotalGeneralAmount { get; set; }
        public double? TotalAllowance { get; set; }
        public double? Totalduduction { get; set; }
        //public double? NetAmount { get; set; }
        public string Description { get; set; }
        public int PaymentType { get; set; }
		public double? PensionRate { get; set; }

	}
}
