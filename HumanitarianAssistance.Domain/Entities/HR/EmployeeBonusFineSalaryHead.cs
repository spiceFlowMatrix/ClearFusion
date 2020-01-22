using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class EmployeeBonusFineSalaryHead: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long Id { get; set; }
        public string SalaryHeadName { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public int TransactionTypeId { get; set; }
        public int Month {get; set;}
        public int Year {get; set;}
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public EmployeeDetail EmployeeDetail { get; set; }
    }
}