using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class InterviewFeedbackDetailsModel : BaseModel
    {
        public long FeedbackId { get; set; }
        public long ScheduleId { get; set; }
        public int InterviewerId { get; set; }
        public string InterviewerName { get; set; }
        public int RoundId { get; set; }
        public string RoundName { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }

}
