using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Models
{
    public class ScheduleDetailModel
    {
        public long? Id { get; set; }
        public long? PolicyId { get; set; }
        public long? PolicyOrderId { get; set; }
        public long? PolicyTimeId { get; set; }
        public long? PolicyDayId { get; set; }
        public string PolicyName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }
        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
    }
}
