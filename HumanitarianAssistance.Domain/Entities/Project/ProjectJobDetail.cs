using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Project
{
    public class ProjectJobDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long ProjectJobId { get; set; }
        public string ProjectJobCode { get; set; }
        public string ProjectJobName { get; set; }

        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual ProjectDetail ProjectDetail { get; set; }

    }
}
