using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteLogisticRequestItemCommand : BaseModel, IRequest<ApiResponse>
    {
        public long ItemId { get; set; } 
        public long RequestId { get; set; }    
    }
}
