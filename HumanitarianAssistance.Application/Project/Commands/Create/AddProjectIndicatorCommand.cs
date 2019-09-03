using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddProjectIndicatorCommand : BaseModel, IRequest<ApiResponse>
    {
        public string IndicatorName { get; set; }
        public string Description { get; set; }
        public long ProjectId { get; set; }

    }
}
