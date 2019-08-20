using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Project
{
    public class ProjectMonitoringReviewDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long ProjectMonitoringReviewId { get; set; }
        public string PostivePoints { get; set; }
        public string NegativePoints { get; set; }
        public string Recommendations { get; set; }
        public string Remarks { get; set; }
        public long ProjectId { get; set; }
        public long ActivityId { get; set; }
        public DateTime? MonitoringDate { get; set; }
        public virtual ICollection<ProjectMonitoringIndicatorDetail> ProjectMonitoringIndicatorDetail { get; set; }
    }
}
