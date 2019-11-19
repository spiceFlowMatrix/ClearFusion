using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteLogisticSupplierCommand : BaseModel, IRequest<ApiResponse>
    {
        public long SupplierId { get; set; }    
    }
}
