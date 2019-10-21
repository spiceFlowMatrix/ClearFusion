using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
    public class ApproveInvoiceCommand : BaseModel, IRequest<ApiResponse>
    {
        public int jobId { get; set; }  
    }
}
