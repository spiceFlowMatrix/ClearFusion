using HumanitarianAssistance.Application.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Configuration.Models
{
   public class LeaveReasonDetailModel:BaseModel
    {
        public int LeaveReasonId { get; set; }
        public string ReasonName { get; set; }
        public int Unit { get; set; }
    }
}
