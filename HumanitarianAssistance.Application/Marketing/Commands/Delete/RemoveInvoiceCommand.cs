using HumanitarianAssistance.Application.Infrastructure;
using MediatR;


namespace HumanitarianAssistance.Application.Marketing.Commands.Delete
{
    public class RemoveInvoiceCommand : BaseModel, IRequest<ApiResponse>
    {
        public int jobId { get; set; }  
    }
}
