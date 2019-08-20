using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteProjectDetailCommand: BaseModel, IRequest<ApiResponse>
    {
        public long ProjectId { get; set; }
    }
}