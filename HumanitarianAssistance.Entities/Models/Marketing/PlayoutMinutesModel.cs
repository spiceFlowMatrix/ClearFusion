using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Marketing
{
    public class PlayoutMinutesModel
    {
        public long PlayoutMinuteId { get; set; }
        public long? PolicyId { get; set; }
        public long? TotalMinutes { get; set; }
        public long? DroppedMinutes { get; set; }
    }
}
