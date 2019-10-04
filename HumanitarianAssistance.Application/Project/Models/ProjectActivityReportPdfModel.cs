using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class ProjectActivityReportPdfModel
    {
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string ProjectGoal { get; set; }
        public string MainActivity { get; set; }
        public List<MonitoringReviewModel> MonitoringReviewModel { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string District { get; set; } 
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
    }
    public class MonitoringReviewModel
    {
        public string IndicatorName { get; set; }
        public List<string> ActivityQuestions { get; set; }
        public string Ratings { get; set; }
        public string StrongPoint { get; set; }
        public string WeakPoints { get; set; }
        public string Recommendations { get; set; }

    }
}
