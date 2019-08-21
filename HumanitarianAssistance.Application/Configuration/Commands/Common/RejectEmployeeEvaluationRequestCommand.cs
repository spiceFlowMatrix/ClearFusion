using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Commands.Common
{
    public class RejectEmployeeEvaluationRequestCommand : BaseModel, IRequest<ApiResponse>
    {
        public int EmployeeEvaluationId { get; set; }
    }
}
