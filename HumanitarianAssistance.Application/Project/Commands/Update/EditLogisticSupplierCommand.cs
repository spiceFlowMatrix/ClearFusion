using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class EditLogisticSupplierCommand : BaseModel, IRequest<ApiResponse>
    {
        public long SupplierId { get; set; }
        public long SourceId { get; set; }
        public long ItemId { get; set; }
        public long Quantity { get; set; }
        public double FinalUnitPrice { get; set; }
        public bool isInvoiceUpdated { get; set; }
        public bool isWarrantyUpdated { get; set; }
    }
}
