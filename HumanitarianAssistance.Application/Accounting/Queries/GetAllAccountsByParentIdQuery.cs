using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetAllAccountsByParentIdQuery : IRequest<ApiResponse>
    {
        public long ParentId { get; set; }
    }
}