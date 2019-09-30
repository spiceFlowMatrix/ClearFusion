using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Store.Models
{
    public class StorePurchaseListModel
    {
        public StorePurchaseListModel()
        {
            PurchaseList = new List<PurchaseListModel>();
        }
        public List<PurchaseListModel> PurchaseList { get; set; }
        public int RecordCount { get; set; }
    }
}