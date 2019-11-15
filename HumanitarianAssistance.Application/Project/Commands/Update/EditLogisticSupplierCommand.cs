using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class EditLogisticSupplierCommand : BaseModel, IRequest<ApiResponse>
    {
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public long Quantity { get; set; }
        public double FinalPrice { get; set; }
    }
}
