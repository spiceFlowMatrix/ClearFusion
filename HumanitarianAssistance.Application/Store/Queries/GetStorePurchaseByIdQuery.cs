using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetStorePurchaseByIdQuery: IRequest<ApiResponse>
    {
        public long PurchaseId { get; set; }
    }
}