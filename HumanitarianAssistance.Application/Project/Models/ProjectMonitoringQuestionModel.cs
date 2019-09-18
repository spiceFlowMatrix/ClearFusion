using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class ProjectMonitoringQuestionModel
    {
        public long? IndicatorQuestionId { get; set; }
        public int? Score { get; set; }
        public long? VerificationSourceId { get; set; }
        public string VerificationSourceName { get; set; }
        public string Question { get; set; }
        public long? MonitoringIndicatorQuestionId { get; set; }
    }
}
