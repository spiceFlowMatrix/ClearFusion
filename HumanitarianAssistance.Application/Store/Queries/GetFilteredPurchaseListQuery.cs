using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetFilteredPurchaseListQuery: IRequest<List<PurchaseListModel>>
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

        public int? pageIndex { get; set; }
        public int? pageSize { get; set; }
        public int? totalCount { get; set; } 
    }
}