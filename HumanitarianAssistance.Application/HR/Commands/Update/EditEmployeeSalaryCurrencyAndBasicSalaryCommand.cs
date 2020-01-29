using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditEmployeeSalaryCurrencyAndBasicSalaryCommand: BaseModel, IRequest<object>
    {
        public long PayrollId { get; set; }
        public int CurrencyId { get; set; }
        public double ActiveSalary { get; set; }
        public double CapacityBuilding { get; set; }
        public double Security { get; set; }
    }
}