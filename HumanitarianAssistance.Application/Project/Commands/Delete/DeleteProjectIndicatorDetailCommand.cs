using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteProjectIndicatorDetailCommand :BaseModel, IRequest<ApiResponse>
    {
        public long IndicatorId { get; set; } 
        
    }
}