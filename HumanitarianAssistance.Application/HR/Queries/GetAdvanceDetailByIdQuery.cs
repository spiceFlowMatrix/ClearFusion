using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAdvanceDetailByIdQuery: IRequest<object>
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
    }
}