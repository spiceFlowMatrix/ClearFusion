using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanitarianAssistance.Domain.Entities.Project {
    public class ProjectJobHiringDetail : BaseEntity {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        [Column (Order = 1)]
        public long JobId { get; set; }

        [StringLength (50)]
        public string JobCode { get; set; }
        public string JobDescription { get; set; }
        public int? ProfessionId { get; set; }

        [ForeignKey ("ProfessionId")]
        public ProfessionDetails ProfessionDetails { get; set; }
        public int? TotalVacancies { get; set; }
        public double? PayRate { get; set; }
        public int OfficeId { get; set; }

        [ForeignKey ("OfficeId")]
        public OfficeDetail OfficeDetails { get; set; }
        public int? GradeId { get; set; }

        [ForeignKey ("GradeId")]
        public JobGrade JobGrade { get; set; }

        public int? CurrencyId { get; set; }

        [ForeignKey ("CurrencyId")]

        public CurrencyDetails CurrencyDetail { get; set; }
        public long ProjectId { get; set; }

        [ForeignKey ("ProjectId")]
        public virtual ProjectDetail ProjectDetail { get; set; }
    }
}