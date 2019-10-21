using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Commands.Create
{
    public class GenerateInvoiceCommand : BaseModel, IRequest<ApiResponse>
    {
        public int jobId { get; set; } 
    }
}
