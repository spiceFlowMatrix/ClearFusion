using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetAllDepreciationByFilterQuery:IRequest<ApiResponse>
    {
        public int? StoreId { get; set; }
        public string InventoryId { get; set; }
        public string ItemId { get; set; }
        public string PurchaseId { get; set; }
        public long ItemGroupId { get; set; }
        public DateTime CurrentDate { get; set; }
    }
}
