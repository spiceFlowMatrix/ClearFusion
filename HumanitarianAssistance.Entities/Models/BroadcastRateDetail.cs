using System;
using System.Collections.Generic;

namespace HumanitarianAssistance.Entities.Models
{
    public partial class BroadcastRateDetail
    {
        public long RateId { get; set; }
        public string TimeCategory { get; set; }
        public string Medium { get; set; }
        public float? Price { get; set; }
    }
}
