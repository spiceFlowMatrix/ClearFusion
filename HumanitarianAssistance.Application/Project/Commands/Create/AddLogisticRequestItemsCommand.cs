using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddLogisticRequestItemsCommand : BaseModel, IRequest<ApiResponse>
    {
        public long RequestId { get; set; }
        public long ItemId { get; set; }
        public int Quantity { get; set; }
        public double EstimatedCost { get; set; }
    }
}
