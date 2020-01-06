using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class EditLogisticRequestItemsCommand : BaseModel, IRequest<ApiResponse>
    {
        public long Id { get; set; }
        public long RequestId { get; set; }
        public long ItemId { get; set; }
        public int RequestedUnits { get; set; }
        public double EstimatedUnitCost { get; set; }
    }
}