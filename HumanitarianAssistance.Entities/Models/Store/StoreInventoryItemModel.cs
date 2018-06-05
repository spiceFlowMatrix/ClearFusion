using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.ViewModels.Models.Store
{
    public class StoreInventoryItemModel : BaseModel
    {
		public string ItemId { get; set; }
		public string ItemInventory { get; set; }
		public string ItemName { get; set; }
		public string ItemCode { get; set; }
		public string Description { get; set; }

		public long Voucher { get; set; }
		public int ItemType { get; set; }
	}
}
