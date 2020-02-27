using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class EmployeePayrollInfoDetail: BaseEntity
    {
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1)]
        public long Id { get; set; }
        //public int CurrencyId { get; set; }
        //public double BasicSalary { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public double NetSalary { get; set; }
        public double GrossSalary { get; set; }
         public double HourlyRate { get; set; }
        public int EmployeeId { get; set; }
        public bool IsSalaryApproved { get; set; }
        // [ForeignKey("CurrencyId")]
        // public CurrencyDetails CurrencyDetails { get; set; }
        [ForeignKey("EmployeeId")]
        public EmployeeDetail EmployeeDetail { get; set; }
    }
}