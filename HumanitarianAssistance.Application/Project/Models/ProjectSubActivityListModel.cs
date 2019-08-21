using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class ProjectSubActivityListModel
    {
        public long ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDescription { get; set; }
        public string ChallengesAndSolutions { get; set; }
        public DateTime? PlannedStartDate { get; set; }
        public DateTime? PlannedEndDate { get; set; }
        public long? BudgetLineId { get; set; }
        public int? EmployeeID { get; set; }
        public int? StatusId { get; set; }
        public bool? Recurring { get; set; }
        public int? RecurringCount { get; set; }
        public int? RecurrinTypeId { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public bool IsCompleted { get; set; }
        public float? Achieved { get; set; }
        public float? Target { get; set; }
        public string SubActivityTitle { get; set; }
    }
}
