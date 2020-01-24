using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeDetailForContractByIdQuery: IRequest<object>
    {
        public int EmployeeId { get; set; }
    }
}