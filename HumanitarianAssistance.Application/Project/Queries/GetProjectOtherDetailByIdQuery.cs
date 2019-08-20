using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectOtherDetailByIdQuery: IRequest<ApiResponse>
    {
        public long Id { get; set; }
    }
}