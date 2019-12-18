using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetCompletedPurchaseOrderDetailQuery  : BaseModel, IRequest<ApiResponse>
    {       
        public long requestId { get; set; }
    }

    public class CompletedPurchaseOrderDetailModel 
    {
        public string VoucherReferenceNo { get; set; }
        public string ApprovedBy { get; set; }
        public List<PurchasedItemModel> purchasedItems { get; set; }
    }
    public class PurchasedItemModel 
    {
        public long PurchaseId { get; set; }
        public long ItemId { get; set; }
        public string ItemName { get; set; }
    }
}