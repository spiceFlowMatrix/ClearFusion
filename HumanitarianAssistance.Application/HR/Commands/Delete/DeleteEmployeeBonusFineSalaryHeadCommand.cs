using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
    public class DeleteEmployeeBonusFineSalaryHeadCommand: BaseModel, IRequest<object>
    {
        public long Id { get; set; }
    }
}