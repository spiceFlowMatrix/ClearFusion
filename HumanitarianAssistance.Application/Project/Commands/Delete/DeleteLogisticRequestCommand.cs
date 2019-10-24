using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteLogisticRequestCommand : BaseModel, IRequest<ApiResponse>
    {
        public long RequestId { get; set; }    
    }
}
