using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class AssignActivityModel : BaseModel
    {
        public long AssignActivityId { get; set; }
        public long ProjectId { get; set; }
        public int TaskId { get; set; }
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime? PlannedStartDate { get; set; }
        public DateTime? PlannedEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public string Status { get; set; }
        public string ApprovedStatus { get; set; }
        public List<AssignActivityUserForApprove> UserForApprovelist { get; set; }
    }

    public class AssignActivityUserForApprove
    {
        public string ApprovedById { get; set; }
    }
}
