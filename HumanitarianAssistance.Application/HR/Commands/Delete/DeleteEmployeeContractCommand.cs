using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
    public class DeleteEmployeeContractCommand: BaseModel, IRequest<ApiResponse>
    {
        public long EmployeeContractId { get; set; }
    }
}