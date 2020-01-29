using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddEmployeeSalaryCurrencyAndBasicSalaryCommand: BaseModel, IRequest<object>
    {
        public int EmployeeId { get; set; }
        public int CurrencyId { get; set; }
        public double ActiveSalary { get; set; }
        public double CapacityBuilding {get; set;}
        public double Security { get; set; }
    }
}