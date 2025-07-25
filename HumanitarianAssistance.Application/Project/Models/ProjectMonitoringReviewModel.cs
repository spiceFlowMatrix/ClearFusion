﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class ProjectMonitoringReviewModel
    {
        public ProjectMonitoringReviewModel()
        {
            IndicatorQuestions = new List<ProjectMonitoringQuestionModel>();
        }
        public long ProjectIndicatorId { get; set; }
        public long? MonitoringIndicatorId { get; set; }
        public string IndicatorName { get; set; }
        public int? TotalScore { get; set; }
        public int? QuestionTypeId{get; set;}
        public List<ProjectMonitoringQuestionModel> IndicatorQuestions { get; set; }
    }
}
