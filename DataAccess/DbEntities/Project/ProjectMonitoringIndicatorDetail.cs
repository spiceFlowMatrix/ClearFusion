using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DbEntities.Project
{
    public class ProjectMonitoringIndicatorDetail : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long MonitoringIndicatorId { get; set; }
        public long ProjectIndicatorId { get; set; }
        public long ProjectMonitoringReviewId { get; set; }
        public virtual ICollection<ProjectMonitoringIndicatorQuestions> ProjectMonitoringIndicatorQuestions { get; set; }
        [ForeignKey("ProjectIndicatorId")]
        public ProjectIndicators ProjectIndicators { get; set; }
    }
}
