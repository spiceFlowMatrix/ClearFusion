using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Models
{
    public class NewSchedulerModel
    {
        public long ScheduleId { get; set; }
        public string ScheduleType { get; set; }
        public string ScheduleCode { get; set; }
        public string ScheduleName { get; set; }
        public long? PolicyId { get; set; }
        public long? ProjectId { get; set; }
        public long? JobId { get; set; }
        public string Name { get; set; }
        public long? MediumId { get; set; }
        public long? ChannelId { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool IsActive { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public List<string> repeatDays { get; set; }
        public List<RepeatDaysModel> RepeatDays { get; set; }
    }
}
