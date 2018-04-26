using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class AssignActivityApproveByModel
    {
        public long AssignActivityApprovedById { get; set; }
        public long AssignActivityId { get; set; }
        public string ApprovedById { get; set; }
        public string Status { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
    }
}
