using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetFilteredEmployeeeListQuery:IRequest<object>
    {
        public string EmployeeName { get; set; }
    }
}