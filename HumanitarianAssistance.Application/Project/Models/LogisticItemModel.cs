using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class LogisticItemModel
    {
        public long Id { get; set; }
        public string Item { get; set; }
        public long Quantity { get; set; }
        public double? EstimatedCost { get; set; }
        public long Availability { get; set; }
        public long ItemId { get; set; }
    }
}
