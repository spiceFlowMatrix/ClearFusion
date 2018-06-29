using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class ItemSpecificationDetailModel
    {		
		public int ItemSpecificationMasterId { get; set; }
		public string ItemId { get; set; }
		public string ItemSpecificationValue { get; set; }
		public string CreatedById { get; set; }
		public DateTime? CreatedDate { get; set; }
		public string ModifiedById { get; set; }
		public DateTime? ModifiedDate { get; set; }
	}
}
