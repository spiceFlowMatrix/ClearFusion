using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteLogisticsControlCommand : BaseModel, IRequest<ApiResponse>
    {
        public long id { get; set; }    
    }
}
