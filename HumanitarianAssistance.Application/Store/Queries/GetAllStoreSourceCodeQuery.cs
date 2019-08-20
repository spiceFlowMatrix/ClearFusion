using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetAllStoreSourceCodeQuery : IRequest<ApiResponse>
    {
        public int? typeId { get; set; }  
    }
}
