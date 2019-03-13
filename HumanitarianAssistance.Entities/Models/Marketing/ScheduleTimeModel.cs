using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Marketing
{
    public class ScheduleTimeModel
    {
        public long? Id { get; set; }
        public long? PolicyId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Name { get; set; }
    }
}
