using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
    public class ProjectMonitoringViewModel
    {
        public ProjectMonitoringViewModel()
        {
            MonitoringReviewModel = new List<ProjectMonitoringReviewModel>();
        }
        public List<ProjectMonitoringReviewModel> MonitoringReviewModel { get; set; }
        public string NegativePoints { get; set; }
        public string PositivePoints { get; set; }
        public string Recommendations { get; set; }
        public string Remarks { get; set; }
        public long ProjectId { get; set; }
        public long ActivityId { get; set; }
        public long? ProjectMonitoringReviewId { get; set; }
        public DateTime? MonitoringDate { get; set; }
        public double? Rating { get; set; }
    }

    public class ProjectMonitoringReviewModel
    {
        public ProjectMonitoringReviewModel()
        {
            IndicatorQuestions = new List<ProjectMonitoringQuestionModel>();
        }
        public long ProjectIndicatorId { get; set; }
        public string IndicatorName { get; set; }
        public long? MonitoringIndicatorId { get; set; }
        public int? TotalScore { get; set; }
        public List<ProjectMonitoringQuestionModel> IndicatorQuestions { get; set; }
    }

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
