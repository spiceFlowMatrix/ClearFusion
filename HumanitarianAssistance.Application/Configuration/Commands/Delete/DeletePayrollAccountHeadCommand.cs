using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Commands.Delete
{
    public class DeletePayrollAccountHeadCommand : BaseModel, IRequest<ApiResponse>
    {
        public int PayrollHeadId { get; set; }
    }
}
