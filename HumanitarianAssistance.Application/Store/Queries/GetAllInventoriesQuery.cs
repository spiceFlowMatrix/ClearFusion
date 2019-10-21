using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetAllInventoriesQuery : IRequest<ApiResponse>
    {
        public int? AssetType { get; set; } 
    }
}
