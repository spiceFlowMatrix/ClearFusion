using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetIndicatorQuestionDetailByIdQuery : IRequest<ApiResponse>
    {
        public long indicatorId { get; set; }  
    }
}
