using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteProjectJobCommand : BaseModel, IRequest<ApiResponse>
    {
        public long JobId { get; set; }
    }
}
