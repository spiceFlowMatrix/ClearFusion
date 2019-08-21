using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteActivitiesControlCommand : BaseModel, IRequest<ApiResponse>
    {
        public long id { get; set; } 
    }  
}
