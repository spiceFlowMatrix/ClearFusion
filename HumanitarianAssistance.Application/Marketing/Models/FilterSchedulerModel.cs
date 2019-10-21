using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Models
{
    public class FilterSchedulerModel
    {
        public long? MediumId { get; set; }
        public long? ChannelId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
