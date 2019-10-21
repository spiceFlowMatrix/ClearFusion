using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetContractTypeContentQuery: IRequest<ApiResponse>
    {
        public int OfficeId { get; set; }
        public int EmployeeContractTypeId { get; set; }
    }
}