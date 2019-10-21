using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanitarianAssistance.Domain.Entities.Project
{
    public class ProjectMonitoringIndicatorQuestions : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long Id { get; set; }
        public int? Score { get; set; }

        public long? VerificationSourceId { get; set; }
        public string VerificationSourceName { get; set; }
        [ForeignKey("MonitoringIndicatorId")]
        public long? MonitoringIndicatorId { get; set; }
        public ProjectMonitoringIndicatorDetail ProjectMonitoringIndicatorDetail { get; set; }
        [ForeignKey("IndicatorQuestionId")]
        public long? IndicatorQuestionId { get; set; }
        public ProjectIndicatorQuestions ProjectIndicatorQuestions { get; set; }




    }
}
