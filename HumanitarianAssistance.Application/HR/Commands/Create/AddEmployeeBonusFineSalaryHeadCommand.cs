using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddEmployeeBonusFineSalaryHeadCommand: BaseModel, IRequest<object>
    {
        public string SalaryHead { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public int EmployeeId { get; set; }
        public bool IsBonus { get; set; }
        public int Month { get; set; }
    }
}