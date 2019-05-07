using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
    public class ActivityAdvanceFilterModel
    {

        public ActivityAdvanceFilterModel()
        {

            // status
            Planning = false;
            Implementation = false;
            Completed = false;


            LateStart = false;
            LateEnd = false;
            OnSchedule = false;
        }

        public long ProjectId { get; set; }
        public DateTime? PlannedStartDate { get; set; }
        public DateTime? PlannedEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }

        public List<long?> BudgetLineId { get; set; }
        public List<long?> AssigneeId { get; set; }

        // status
        public bool Planning { get; set; }
        public bool Implementation { get; set; }
        public bool Completed { get; set; }

        // range
        public List<int?> ProgressRange { get; set; }
        public List<int?> SleepageRange { get; set; }
        public List<int?> DurationRange { get; set; }

        public bool LateStart { get; set; }
        public bool LateEnd { get; set; }
        public bool OnSchedule { get; set; }

    }
}
