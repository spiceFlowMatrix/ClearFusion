using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetStorePurchasePdfQuery: BaseModel, IRequest<byte[]>
    {

        public int InventoryTypeId { get; set; }
        public int ReceiptTypeId { get; set; }
        public int OfficeId { get; set; }
        public int CurrencyId { get; set; }
        public long ProjectId { get; set; }
        public DateTime? PurchaseStartDate { get; set; }
        public DateTime? PurchaseEndDate { get; set; }
        public DateTime? IssueStartDate { get; set; }
        public DateTime? IssueEndDate { get; set; }
        public long InventoryId { get; set; }
        public long ItemGroupId { get; set; }
        public long ItemId { get; set; }
        public long JobId { get; set; }
        public List<string> SelectedColumns {get; set;}
        public int? DisplayCurrency { get; set;}
        public DateTime? DepreciationComparisionDate { get; set; }
        
    }
}