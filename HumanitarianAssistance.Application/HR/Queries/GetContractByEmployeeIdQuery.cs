using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetContractByEmployeeIdQuery: IRequest<ApiResponse>
    {
        public int EmployeeId { get; set; }
    }
}