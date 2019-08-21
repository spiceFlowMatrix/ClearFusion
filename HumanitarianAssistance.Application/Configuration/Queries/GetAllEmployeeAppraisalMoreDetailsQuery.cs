using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetAllEmployeeAppraisalMoreDetailsQuery : IRequest<ApiResponse>
    {
        public int OfficeId { get; set; } 
    }
}
