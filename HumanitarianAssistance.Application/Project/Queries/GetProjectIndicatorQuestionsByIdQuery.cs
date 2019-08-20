using HumanitarianAssistance.Application.Infrastructure;
using MediatR;


namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectIndicatorQuestionsByIdQuery : IRequest<ApiResponse>
    {
        public long indicatorId { get; set; } 
    }
} 
