using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectListByIdQuery: IRequest<ApiResponse>
    {
        public long Id { get; set; }
    }
}