using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Store.Models
{
    public class ItemSpecificationDetailModel
    {
        public int ItemSpecificationMasterId { get; set; }
        public string ItemId { get; set; }
        public string ItemSpecificationValue { get; set; }
        public string ItemSpecificationField { get; set; }
    }
}
