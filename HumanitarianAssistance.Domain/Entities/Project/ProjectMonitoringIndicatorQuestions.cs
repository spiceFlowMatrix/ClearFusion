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
        public long QuestionId { get; set; }
        public int? VerificationId { get; set; }
        public string Verification { get; set; }
        public int? Score { get; set; }
        public long MonitoringIndicatorId { get; set; }
        [ForeignKey("QuestionId")]
        public ProjectIndicatorQuestions ProjectIndicatorQuestions { get; set; }
    }
}
