using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetActivityByIdQuery : IRequest<ApiResponse>
    {
        public int ActivityTypeId { get; set; }
    }
} 
