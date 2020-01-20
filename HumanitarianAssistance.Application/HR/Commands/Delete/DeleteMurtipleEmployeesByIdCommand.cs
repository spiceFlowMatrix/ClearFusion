using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
    public class DeleteMurtipleEmployeesByIdCommand: BaseModel, IRequest<object>
    {
        public long[] EmpIds { get; set; }
    }
}