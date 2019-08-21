using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetQualityByIdQuery : IRequest<ApiResponse>
    {
        public int QualityId { get; set; } 
    }
}
