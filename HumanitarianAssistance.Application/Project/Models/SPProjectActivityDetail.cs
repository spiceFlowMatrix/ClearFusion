using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class SPProjectActivityDetail
    {
        public SPProjectActivityDetail()
        {
            Recurring = false;
            RecurringCount = 0;
        }
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDescription { get; set; }
        public DateTime PlannedStartDate { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public long BudgetLineId { get; set; }
        public string BudgetName { get; set; }

        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }

        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public bool Recurring { get; set; }
        public int RecurringCount { get; set; }
        public int RecurrinTypeId { get; set; }
        public double Progress { get; set; }
        public double Sleepage { get; set; }
    }
}
