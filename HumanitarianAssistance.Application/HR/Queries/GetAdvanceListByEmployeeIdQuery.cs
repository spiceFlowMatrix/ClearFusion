using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAdvanceListByEmployeeIdQuery: IRequest<object>
    {
        public int EmployeeId { get; set; }
    }
}