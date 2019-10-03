using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.Project.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Store.Models
{
    public class PurchaseFilterDataSourceModel
    {
        public List<InventoryTypeModel> InventoryTypes { get; set; }
        public List<ReceiptTypeModel> ReceiptTypes { get; set; }
        public IList<OfficeDetailModel> Offices { get; set; }
        public IList<CurrencyModel> CurrencyModel { get; set; }
        public List<ProjectModel> ProjectModel { get; set; }
        public List<ProjectJobDetailModel> ProjectJobModel { get; set; }
        public List<StoreInventoryModel> StoreInventoryModel { get; set; }
        public List<StoreItemGroupModel> StoreItemGroupModel { get; set; }
        public List<StoreInventoryItemModel> StoreInventoryItemModel { get; set; }
    }
}
