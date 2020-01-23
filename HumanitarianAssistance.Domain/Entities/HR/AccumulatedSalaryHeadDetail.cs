using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class AccumulatedSalaryHeadDetail: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long Id { get; set; }
        public int SalaryComponentId { get; set; }
        public double SalaryAllowance { get; set; }
        public double SalaryDeduction { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}