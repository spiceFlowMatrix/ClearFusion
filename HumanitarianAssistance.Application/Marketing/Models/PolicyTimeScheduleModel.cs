using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Models
{
    public class PolicyTimeScheduleModel
    {
        public long Id { get; set; }
        public long? PolicyId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public bool IsActive { get; set; }
        //public long? PolicyId { get; set; }
        public string[] RepeatDays { get; set; }
        public List<string> repeatDays { get; set; }
        //public string StartTime { get; set; }
        //public string EndTime { get; set; }
    }
}
