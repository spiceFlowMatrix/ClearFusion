using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class IssueComparativeStatementCommand : BaseModel, IRequest<ApiResponse>
    {
        public long RequestId { get; set; }
    }
}
 