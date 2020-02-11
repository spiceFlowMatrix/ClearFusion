using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeAppraisalByEmployeeIdQuery:IRequest<object>
    {
         public int EmployeeId { get; set; }
    }
}