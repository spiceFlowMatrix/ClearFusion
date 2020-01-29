using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class AdvanceHistoryDetail: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long Id { get; set; }
        public int AdvanceId { get; set; }
        public DateTime PaymentDate { get; set; }
        public double InstallmentPaid { get; set; }
        public double InstallmentBalance { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("AdvanceId")]
        public Advances Advances { get; set; }
        [ForeignKey("EmployeeId")]
        public EmployeeDetail EmployeeDetail { get; set; }
    }
}