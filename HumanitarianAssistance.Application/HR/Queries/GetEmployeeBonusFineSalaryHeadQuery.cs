using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeBonusFineSalaryHeadQuery: IRequest<object>
    {
        public int EmployeeId { get; set; }
        public int Month { get; set; }
    }
}