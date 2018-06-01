using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.ViewModels.Models.Store
{
    public class StoreInventoryItemModel : BaseModel
    {
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public int ItemType { get; set; }
    }
}
