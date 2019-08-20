using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetStoreGroupItemCodeQuery : IRequest<ApiResponse>
    {
        public string inventoryId { get; set; }  
    }
}
