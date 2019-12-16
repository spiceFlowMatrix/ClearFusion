using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddLogisticSupplierCommand : BaseModel, IRequest<ApiResponse>
    {
        public long RequestId { get; set; }
        public long StoreSourceCode { get; set; }
        public long Quantity { get; set; }
        public double FinalUnitPrice { get; set; }
        public long ItemId { get; set; }
    }
}
