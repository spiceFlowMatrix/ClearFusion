using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddLogisticSupplierCommand : BaseModel, IRequest<ApiResponse>
    {
        public long RequestId { get; set; }
        public string SupplierName { get; set; }
        public long Quantity { get; set; }
        public double FinalCost { get; set; }
    }
}
