using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class ProjectActivityReportPdfModel
    {
        public ProjectActivityReportPdfModel()
        {
            ActiivtyListModel = new List<ActiivtyListModel>();
        }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string ProjectGoal { get; set; }
        public double ProjectDuration { get; set; }
        public string ProjectStartDate { get; set; }
        public string ProjectEndDate { get; set; }
        public string Country { get; set; }
        public List<string> Province { get; set; }
        public List<string> District { get; set; }
        public List<ActiivtyListModel> ActiivtyListModel { get; set; }
        public string LogoPath { get; set; }
    }

    public class ActiivtyListModel
    {
        public ActiivtyListModel()
        {
            ProjectMonitoringViewModel = new List<ProjectMonitoringViewModel>();
        }
        public long ActivityCode { get; set; }
        public string MainActivity { get; set; }
        public double ActivityDuration { get; set; }
        public string ActualStartDate { get; set; }
        public string ActualEndDate { get; set; }
        public List<ProjectMonitoringViewModel> ProjectMonitoringViewModel { get; set; }

    }
}
