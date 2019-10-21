using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAllEmployeeLeavePdfQuery: BaseModel, IRequest<byte[]>
    {
        public long EmployeeId { get; set; }
    }
}