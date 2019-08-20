using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class ProjectMonitoringQuestionModel
    {
        public long QuestionId { get; set; }
        public int? Score { get; set; }
        public int? VerificationId { get; set; }
        public string Verification { get; set; }
        public string Question { get; set; }
        public long? MonitoringIndicatorQuestionId { get; set; }
    }
}
