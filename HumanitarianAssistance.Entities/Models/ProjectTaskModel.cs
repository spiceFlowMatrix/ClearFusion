using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class ProjectTaskModel : BaseModel
    {
        public long ProjectTaskId { get; set; }
        public long ProjectId { get; set; }
        public int TaskId { get; set; }
        public DateTime? PlannedStartDate { get; set; }
        public DateTime? PlannedEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public string ApprovedById { get; set; }
        public string Status { get; set; }
    }
}
