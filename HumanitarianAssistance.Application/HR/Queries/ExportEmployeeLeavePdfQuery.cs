using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class ExportEmployeeLeavePdfQuery: IRequest<byte[]>
    {
        public int EmployeeId { get; set; }
    }
}