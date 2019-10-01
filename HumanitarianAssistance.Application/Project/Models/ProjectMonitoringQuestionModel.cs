using System;
using System.Collections.Generic;
using System.Text;
using HumanitarianAssistance.Domain.Entities.Project;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class ProjectMonitoringQuestionModel
    {
        public long? IndicatorQuestionId { get; set; }
        public string IndicatorQuestion { get; set; }
        public int? Score { get; set; }
        public long? VerificationSourceId { get; set; }
        public string VerificationSourceName { get; set; }
        public long? MonitoringIndicatorQuestionId { get; set; }
        public string QuestionTypeName { get; set; }
        public int? QuestionType{ get; set;}
        public List<VerificationSources> VerificationSources { get; set; }
    }
}
