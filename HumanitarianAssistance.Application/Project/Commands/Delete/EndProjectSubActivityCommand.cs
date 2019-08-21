using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class EndProjectSubActivityCommand : BaseModel, IRequest<ApiResponse>
    {
        public long activityId { get; set; }
    }
}
