using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class LeaveReasonDetailModel : BaseModel
    {
        public int LeaveReasonId { get; set; }
        public string ReasonName { get; set; }
        public int Unit { get; set; }
    }
}
