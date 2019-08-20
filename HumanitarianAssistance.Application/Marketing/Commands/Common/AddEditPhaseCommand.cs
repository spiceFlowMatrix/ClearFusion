using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
    public class AddEditPhaseCommand : BaseModel, IRequest<ApiResponse>
    {
        public long? JobPhaseId { get; set; }
        public string Phase { get; set; }
    }
}
