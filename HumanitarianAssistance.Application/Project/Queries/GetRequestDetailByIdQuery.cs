using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetRequestDetailByIdQuery: IRequest<ApiResponse>
    {
        public long RequestId { get; set; }
    }
}