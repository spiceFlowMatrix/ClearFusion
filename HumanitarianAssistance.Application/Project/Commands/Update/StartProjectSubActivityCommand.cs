using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class StartProjectSubActivityCommand : BaseModel, IRequest<ApiResponse>
    {
        public long activityId { get; set; }
    }
}
