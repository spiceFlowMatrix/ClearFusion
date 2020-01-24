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
        public int CurrencyId { get; set; }
        public double BasicSalary { get; set; }
        public double NetSalary { get; set; }
        public double GrossSalary { get; set; }
        public bool IsSalaryApproved { get; set; }
    }
}