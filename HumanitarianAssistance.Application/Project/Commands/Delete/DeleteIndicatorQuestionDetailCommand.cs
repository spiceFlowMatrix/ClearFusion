using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteIndicatorQuestionDetailCommand: BaseModel, IRequest<ApiResponse>
    {
        public long IndicatorQuestionId { get; set; } 
        
    }
}