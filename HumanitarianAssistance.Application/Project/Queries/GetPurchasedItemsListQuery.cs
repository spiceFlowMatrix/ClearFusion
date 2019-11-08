using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetPurchasedItemsListQuery: IRequest<ApiResponse>
    {
        public long RequestId { get; set; }
    }
}