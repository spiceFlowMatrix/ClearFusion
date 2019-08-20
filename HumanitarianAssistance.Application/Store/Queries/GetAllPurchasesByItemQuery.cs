using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetAllPurchasesByItemQuery : IRequest<ApiResponse>
    {
        public string itemId { get; set; }
    }
}
