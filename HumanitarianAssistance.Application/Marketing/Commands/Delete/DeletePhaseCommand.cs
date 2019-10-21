using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Commands.Delete
{
    public class DeletePhaseCommand : BaseModel, IRequest<ApiResponse>
    {
        public int JobPhaseId { get; set; } 
    }
}
