using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteProjectActivityCommand : BaseModel, IRequest<ApiResponse>
    {
        public long ActivityId { get; set; }
    }
}
