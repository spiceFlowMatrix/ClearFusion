using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddAdvanceRecoveryCommand: BaseModel, IRequest<object>
    {
        public int EmployeeId { get; set; }
        public int Month { get; set; }
        public double Amount { get; set; }
         public int AdvanceId { get; set; }
    }
}