using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetAllInventoryItemsQuery : IRequest<ApiResponse>
    {
        public long ItemGroupId { get; set; } 
    }
}
