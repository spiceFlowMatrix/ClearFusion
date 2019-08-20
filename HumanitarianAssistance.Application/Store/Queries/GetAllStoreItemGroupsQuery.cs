using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetAllStoreItemGroupsQuery : IRequest<ApiResponse>
    {
        public string inventoryId { get; set; } 
    }
}
