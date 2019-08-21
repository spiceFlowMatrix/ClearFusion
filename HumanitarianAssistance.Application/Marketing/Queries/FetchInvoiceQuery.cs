using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class FetchInvoiceQuery : IRequest<ApiResponse>
    {
        public int jobId { get; set; }
    }
}
