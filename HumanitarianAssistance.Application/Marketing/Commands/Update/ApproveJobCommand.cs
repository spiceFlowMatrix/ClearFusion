using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Commands.Update
{
    public class ApproveJobCommand : BaseModel, IRequest<ApiResponse>
    {
        public int JobId { get; set; }
    }
}
