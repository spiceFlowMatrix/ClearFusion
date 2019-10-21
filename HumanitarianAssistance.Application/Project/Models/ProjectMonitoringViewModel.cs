using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
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
}
