using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetItemsByRequestIdQuery : IRequest<ApiResponse>
    {
        public long RequestId { get; set; }
    }
}
