using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetStoreTypeCodeQuery : IRequest<ApiResponse>
    {
        public int CodeTypeId { get; set; } 
    }
}
