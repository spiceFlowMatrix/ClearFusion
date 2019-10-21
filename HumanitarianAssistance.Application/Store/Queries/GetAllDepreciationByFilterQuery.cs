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
        public long InventoryId { get; set; }
        public long ItemId { get; set; }
        public long PurchaseId { get; set; }
        public long ItemGroupId { get; set; }
        public DateTime CurrentDate { get; set; }
    }
}
