using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteActivityDocumentCommand : BaseModel, IRequest<ApiResponse>
    {
        public long activityDocumentId { get; set; } 
    }
}
