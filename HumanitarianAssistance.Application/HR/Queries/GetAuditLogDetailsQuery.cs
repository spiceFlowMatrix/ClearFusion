using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAuditLogDetailsQuery : IRequest<object>
    {
     public int EmployeeId { get; set; }   
    }
}