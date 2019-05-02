using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
    public class ProjectActivityModel
    {
        public ProjectActivityModel()
        {
            Recurring = false;
            RecurringCount = 0;
            //ImplementationProgress = 0;
            //ImplementationStatus = false;

            //MonitoringProgress = 0;
            //MonitoringStatus = false;
            //MonitoringScore = 0;
            //MonitoringFrequency = 0;
        }
        //Planning
        public long ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDescription { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? BudgetLineId { get; set; }
        public string BudgetName { get; set; }

        public int? EmployeeID { get; set; }
        public string EmployeeName { get; set; }

        public int? StatusId { get; set; }
        public string StatusName { get; set; }

        public bool? Recurring { get; set; }
        public int? RecurringCount { get; set; }
        public int RecurrinTypeId { get; set; }

        ////Implementation
        //[Range(0, 100)]
        //public float? ImplementationProgress { get; set; }
        //public bool? ImplementationStatus { get; set; }
        //public DateTime? ActualStartDate { get; set; }
        //public DateTime? ActualEndDate { get; set; }
        //public string ImplementationMethod { get; set; }
        //public string ImplementationChalanges { get; set; }
        //public string OvercomingChallanges { get; set; }
        //public DateTime? ExtensionStartDate { get; set; }
        //public DateTime? ExtensionEndDate { get; set; }

        ////Monitoring
        //[Range(0, 100)]
        //public float? MonitoringProgress { get; set; }
        //public bool? MonitoringStatus { get; set; }

        //public int? MonitoringScore { get; set; }

        //public int? MonitoringFrequency { get; set; }
        //public string VerificationSource { get; set; }

        //public string Strengths { get; set; }

        //public string Weeknesses { get; set; }
        //public string MonitoringChallenges { get; set; }
        //public string Recommendation { get; set; }
        //public int? OfficeId { get; set; }

        //public string OfficeName { get; set; }
        //public string Comments { get; set; }
    }
}
