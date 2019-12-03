using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class SubmitPurchaseOrderCommand : BaseModel, IRequest<ApiResponse>
    {
        public List<SubmitPurchaseOrderModel> ItemModel { get; set; }
        public long RequestId { get; set; }
        public DateTime PurchaseDate { get; set; }
    }

    public class SubmitPurchaseOrderModel
    {
        public long Id { get; set; }
        public double FinalCost { get; set; }
    }
}
 