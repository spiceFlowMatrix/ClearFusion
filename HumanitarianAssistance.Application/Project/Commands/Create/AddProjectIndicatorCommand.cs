using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddProjectIndicatorCommand : BaseModel, IRequest<ApiResponse>
    {
    }
}
