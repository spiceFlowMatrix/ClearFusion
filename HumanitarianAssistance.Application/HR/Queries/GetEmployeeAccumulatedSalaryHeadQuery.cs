using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeAccumulatedSalaryHeadQuery: IRequest<object>
    {
        public int EmployeeId { get; set; }
        public int Month {get; set;}
    }
}