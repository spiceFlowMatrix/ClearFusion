using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Models
{
    public class PolicyScheduleModel
    {
        public long PolicyScheduleId { get; set; }
        public long? PolicyId { get; set; }
        public string Title { get; set; }
        public string RepeatDays { get; set; }
        public string Description { get; set; }
        public int? Frequency { get; set; }
        public int? ByMonth { get; set; }
        public int? ByWeek { get; set; }
        public int? ByDay { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime EndDate { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public string ScheduleCode { get; set; }
    }
}
