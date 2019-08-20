using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanitarianAssistance.Domain.Entities.Project
{
    public class ProjectMonitoringIndicatorDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long MonitoringIndicatorId { get; set; }
        public long ProjectIndicatorId { get; set; }
        public long ProjectMonitoringReviewId { get; set; }
        public virtual ICollection<ProjectMonitoringIndicatorQuestions> ProjectMonitoringIndicatorQuestions { get; set; }
        [ForeignKey("ProjectIndicatorId")]
        public virtual ProjectIndicators ProjectIndicators { get; set; }
    }
}
