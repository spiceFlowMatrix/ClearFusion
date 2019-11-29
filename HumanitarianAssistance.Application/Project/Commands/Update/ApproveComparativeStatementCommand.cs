using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class ApproveComparativeStatementCommand : BaseModel, IRequest<ApiResponse>
    {
        public long requestId { get; set; }
    }
}
 