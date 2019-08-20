using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectProgramByIdQuery: IRequest<ApiResponse>
    {
        public long ProjectId { get; set; }
    }
}