using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class EditProjectActivityDetailCommand : BaseModel, IRequest<ApiResponse>
    {
        public EditProjectActivityDetailCommand()
        {
            Recurring = false;
            RecurringCount = 0;
        }
        //Planning
        public long? ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDescription { get; set; }
        public DateTime? PlannedStartDate { get; set; }
        public DateTime? PlannedEndDate { get; set; }
        public long? BudgetLineId { get; set; }
        public string BudgetName { get; set; }

        public int? EmployeeID { get; set; }
        public string EmployeeName { get; set; }

        public int? StatusId { get; set; }
        public string StatusName { get; set; }

        public bool? Recurring { get; set; }
        public int? RecurringCount { get; set; }
        public int? RecurrinTypeId { get; set; }


        public IEnumerable<int> ProvinceId { get; set; }
        public IEnumerable<long?> DistrictID { get; set; }

        public long? ParentId { get; set; }
        public float? Target { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public double? Progress { get; set; }
        public double? Slippage { get; set; }
        public string SubActivityTitle { get; set; }
        public new bool? IsDeleted { get; set; }
    }
}
