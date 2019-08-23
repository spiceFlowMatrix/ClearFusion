using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.ViewModels.Models.Store
{
    public class StoreInventoryItemModel : BaseModel
    {
		public long ItemId { get; set; }
		public long ItemInventory { get; set; }
		public string ItemName { get; set; }
		public string ItemCode { get; set; }
		public string Description { get; set; }
        public long ItemGroupId { get; set; }
        public int ItemType { get; set; }
	}
}
