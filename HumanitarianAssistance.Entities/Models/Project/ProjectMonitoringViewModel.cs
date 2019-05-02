using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
    public class ProjectMonitoringViewModel
    {
        public List<ProjectMonitoringReviewModel> MonitoringReviewModel { get; set; }
        public string NegativePoints { get; set; }
        public string PositivePoints { get; set; }
        public string Recommendations { get; set; }
        public string Remarks { get; set; }

    }

    public class ProjectMonitoringReviewModel
    {
        public long ProjectIndicatorId { get; set; }
        public List<ProjectMonitoringQuestionModel> ProjectMonitoringReview { get; set; }
    }

    public class ProjectMonitoringQuestionModel
    {
        public long QuestionId { get; set; }
        public int Score { get; set; }
        public string Verification { get; set; }
    }
}
