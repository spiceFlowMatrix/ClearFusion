using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddProjectMonitoringReviewCommand : BaseModel, IRequest<ApiResponse>
    {
        public AddProjectMonitoringReviewCommand()
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
