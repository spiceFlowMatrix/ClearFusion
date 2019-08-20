using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectActivityAdvanceFilterListQuery : IRequest<ApiResponse>
    {
        public GetProjectActivityAdvanceFilterListQuery()
        {
            ActivityDescription = string.Empty;

            // status
            Planning = false;
            Implementation = false;
            Completed = false;

            BudgetLineId = new List<long>();
            AssigneeId = new List<int>();

            ProgressRangeMin = 0;
            ProgressRangeMax = 0;

            SleepageMin = 0;
            SleepageMax = 0;

            DurationMin = 0;
            DurationMax = 0;

            LateStart = false;
            LateEnd = false;
            OnSchedule = false;
        }

        public long ProjectId { get; set; }
        public string ActivityDescription { get; set; }

        public DateTime? PlannedStartDate { get; set; }
        public DateTime? PlannedEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }

        public List<long> BudgetLineId { get; set; }
        public List<int> AssigneeId { get; set; }

        // status
        public bool Planning { get; set; }
        public bool Implementation { get; set; }
        public bool Completed { get; set; }

        // range
        public int ProgressRangeMin { get; set; }
        public int ProgressRangeMax { get; set; }

        public int SleepageMin { get; set; }
        public int SleepageMax { get; set; }

        public int DurationMin { get; set; }
        public int DurationMax { get; set; }


        public bool LateStart { get; set; }
        public bool LateEnd { get; set; }
        public bool OnSchedule { get; set; }

    }
}
