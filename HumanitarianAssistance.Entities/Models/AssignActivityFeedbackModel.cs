using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class AssignActivityFeedbackModel : BaseModel
    {
        public long FeedbackId { get; set; }
        public long AssignActivityId { get; set; }
        public string UserId { get; set; }
        public string Feedback { get; set; }
        public DateTime Date { get; set; }
    }
}
