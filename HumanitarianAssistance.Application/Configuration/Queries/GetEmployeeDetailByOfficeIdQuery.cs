using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetEmployeeDetailByOfficeIdQuery : IRequest<ApiResponse>
    {
        public int OfficeId { get; set; } 
    }
}
