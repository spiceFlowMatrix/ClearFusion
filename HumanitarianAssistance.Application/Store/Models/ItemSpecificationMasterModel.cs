using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Store.Models
{
    public class ItemSpecificationMasterModel
    {
        public int ItemSpecificationMasterId { get; set; }
        public string ItemSpecificationField { get; set; }
        public int OfficeId { get; set; }
        public int ItemTypeId { get; set; }
    }
}
