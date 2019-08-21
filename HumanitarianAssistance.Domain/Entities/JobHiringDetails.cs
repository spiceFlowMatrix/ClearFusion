using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HumanitarianAssistance.Domain.Entities.Project;

namespace HumanitarianAssistance.Domain.Entities
{
    public class JobHiringDetails : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long JobId { get; set; }
        [StringLength(50)]
        public string JobCode { get; set; }
        public string JobDescription { get; set; }
        public int? ProfessionId { get; set; }
        public int Unit { get; set; }
        public int OfficeId { get; set; }
        public OfficeDetail OfficeDetails { get; set; }
        public bool IsActive { get; set; }
        public int? GradeId { get; set; }
        [ForeignKey("GradeId")]
        public JobGrade JobGrade { get; set; }

        [ForeignKey("HiringRequestId")]
        public long? HiringRequestId { get; set; }
        public ProjectHiringRequestDetail ProjectHiringRequestDetail { get; set; }
    }
}
