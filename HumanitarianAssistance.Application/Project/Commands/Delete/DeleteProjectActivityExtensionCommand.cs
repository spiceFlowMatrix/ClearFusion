using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteProjectActivityExtensionCommand : BaseModel, IRequest<ApiResponse>
    {
        public long extensionId { get; set; }  
    }
}
