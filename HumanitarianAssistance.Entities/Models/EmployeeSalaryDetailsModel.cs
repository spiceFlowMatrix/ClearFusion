using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class EmployeeSalaryDetailsModel : BaseModel
    {
        public long SalaryId { get; set; }
        public int EmployeeId { get; set; }
        public int CurrencyId { get; set; }
        public double? MonthlySalary { get; set; }
        public double? FoodAllowance { get; set; }
        public double? TRAllowance { get; set; }
        public double? MedicalAllowance { get; set; }
        public double? OtherAllowance { get; set; }
        public string Description { get; set; }   
        public double? TotalAllowance { get; set; }
        public double? TotalEarning { get; set; }
        public double? ProvidentFund { get; set; }
        public double? Otherdeduction { get; set; }
        public double? Totalduduction { get; set; }
        public double? NetAmount { get; set; }
        public int PaymentType { get; set; }
    }
}
