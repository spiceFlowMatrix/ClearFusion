using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeDetailByIdQuery: IRequest<object>
    {
        public int EmployeeId { get; set; }
    }
}